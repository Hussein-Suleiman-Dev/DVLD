using BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Shared.clsImageHelper;
using static System.Net.Mime.MediaTypeNames;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DVLD
{
    public partial class frmAddUpdatePerson : Form
    {

        private int _PersonID = -1;
         private clsPeople _Person;
        enum enMode { Add = 1, Update = 2 }
        enMode _Mode;



        public delegate void DataBackEventHandler(object sender, int PersonId);
        public event DataBackEventHandler DataBack;


        //Constructer
        public frmAddUpdatePerson()
        {
            InitializeComponent();
            _Person = new clsPeople();
            _Mode = enMode.Add;
            _PersonID = -1;
        }
        public frmAddUpdatePerson(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
            _Mode = enMode.Update;
        
        }



        private void _formAdd()
        {
            Text = "Add New Perosn";
        lnkRemoveImage.Visible = false;
            lblPersonID.Visible=false;
            label18.Visible=false;
            dateTimePicker1.MaxDate = DateTime.Now.AddYears(-18);
            dateTimePicker1.MinDate = DateTime.Now.AddYears(-100);

         
            _ResetDefualt();
        }
        private void _formUpdate()
        {
            Text = "Edit Perosn";
            lblTitle.Text = "Edit Person";
            lnkSetImage.Text = "Update Image";
            lblPersonID.Text = _PersonID.ToString();
            
      lnkRemoveImage.Visible=true;
            label18.Visible=true;
            lblPersonID.Visible = true;
            
        }

        private void _LoadPersonData()
        {
            _Person = clsPeople.Find(_PersonID);

            if (_Person == null)
            {
                MessageBox.Show("Person Not Found");
                this.Close();
                return;
            }

            txtFirstName.Text = _Person.FirstName;
            txtSecoundName.Text = _Person.SecondName;
            txtThirdName.Text = _Person.ThirdName;
            txtLastName.Text = _Person.LastName;

            txtNationalNo.Text = _Person.NationalNumber;
            txtPhone.Text = _Person.Phone;

            txtEmail.Text = _Person.Email;
            txtAddress.Text = _Person.Address;

            rbMale.Checked = (_Person.Gender == 0);
            rbFemale.Checked = _Person.Gender == 1;
            dateTimePicker1.Value = _Person.DateOfBirth;
            cmbCounrits.SelectedValue = _Person.CountryID;
           
            if (!string.IsNullOrEmpty(_Person.ImagePath) && File.Exists(_Person.ImagePath))
            {

                lnkRemoveImage.Visible = true;
                PicImages.ImageLocation = _Person.ImagePath;
            }
            else { PicImages.Image=  _GetDefaultImage(); }
        }

        private System.Drawing.Image _GetDefaultImage()
        {
            PicImages.ImageLocation = null;
            if (rbMale.Checked)
            {
                return Properties.Resources.Male;
            }
     
           
             return Properties.Resources.Female;
           
          
        }

        private void _ResetDefualt()
        {
            rbMale.Checked = true;
            PicImages.Image = _GetDefaultImage();
            _FillCountries();

        }

        private void _FillCountries()
        {
            cmbCounrits.DataSource = clsCountry.GetAllCountries();
            cmbCounrits.DisplayMember = "CountryName";
            cmbCounrits.ValueMember = "CountryID";


            cmbCounrits.DropDownStyle = ComboBoxStyle.DropDown;


            cmbCounrits.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbCounrits.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        }
     
        private void frmAddPeople_Load(object sender, EventArgs e)
        {


            _FillCountries();

            if (_Mode == enMode.Add)
            {
                _formAdd();
            }
            else 
            {
                _formUpdate();
                _LoadPersonData(); 
            }
        }



        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
      
            PicImages.ImageLocation = null;
            PicImages.Image = _GetDefaultImage(); 
            lnkRemoveImage.Visible = false;
        }
        

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Title = "Select Image";
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png";
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string _SelectedImagePath = openFileDialog1.FileName;

                if (!string.IsNullOrEmpty(_SelectedImagePath))
                {
                    PicImages.Image = null; // تفريغ الصورة الافتراضية أولاً
                    PicImages.ImageLocation = _SelectedImagePath; // وضع المسار الجديد هنا
                    lnkRemoveImage.Visible = true;
                }
            }
        }




        private bool MappingPersonInfo()
        {
           
            if (!_HandlePersonImage())
            {
                return false; 
            }

            _Person.FirstName = txtFirstName.Text.Trim();
            _Person.SecondName = txtSecoundName.Text.Trim();
            _Person.ThirdName = string.IsNullOrEmpty(txtThirdName.Text) ? "" : txtThirdName.Text.Trim();
            _Person.LastName = txtLastName.Text.Trim();
            _Person.NationalNumber = txtNationalNo.Text.Trim();
            _Person.Phone = txtPhone.Text.Trim();
            _Person.Email = txtEmail.Text.Trim();
            _Person.Address = txtAddress.Text.Trim();
            _Person.Gender = rbMale.Checked ? (short)0 : (short)1;
            _Person.DateOfBirth = dateTimePicker1.Value.Date;
            _Person.CountryID = Convert.ToInt32(cmbCounrits.SelectedValue);

         
            _Person.ImagePath = PicImages.ImageLocation;

            return true;
        }

        private bool _HandlePersonImage()
        {
            // إذا كان المسار في الكائن يساوي المسار في الـ PictureBox، يعني لم يتم اختيار صورة جديدة
            if (_Person.ImagePath == PicImages.ImageLocation)
            {
                return true;
            }

            if (!string.IsNullOrEmpty(_Person.ImagePath))
            {
                try
                {
                    if (File.Exists(_Person.ImagePath))
                    {
                        File.Delete(_Person.ImagePath);
                    }
                }
                catch (IOException)
                {
                   
                }
            }

        
            if (!string.IsNullOrEmpty(PicImages.ImageLocation))
            {
                string SourceImagePath = PicImages.ImageLocation;

                if (CopyImageToProjectImagesFolder(ref SourceImagePath))
                {
                
                    PicImages.ImageLocation = SourceImagePath;
                    return true;
                }
                else
                {
                    MessageBox.Show("Error Copy", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            return true;
        }


        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                MessageBox.Show("Fix validation errors first!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

          
            if (!MappingPersonInfo())
            {
                return; 
            }

            bool Success = _Person.Save();

            if (Success)
            {
                MessageBox.Show("Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (_Mode == enMode.Add)
                {
                    _PersonID = _Person.PersonID;
                    _Mode = enMode.Update; // تحويل الوضع إلى تعديل
                    _formUpdate();
                    DataBack?.Invoke(this, _Person.PersonID);
                }
            }
            else
            {
                MessageBox.Show("Error: Data could not be saved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }






        }
    
        private void btnCancle_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        
        private void Valdating(object sender, CancelEventArgs e)
        {
            Guna.UI2.WinForms.Guna2TextBox txt = (Guna.UI2.WinForms.Guna2TextBox)sender;

         
            if (string.IsNullOrEmpty(txt.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txt, "This field is required");
                return;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txt, "");
            }

           
           
            if (txt == txtNationalNo)
            {
                if (clsPeople.IsNationalNumberExists(txtNationalNo.Text.Trim(), _PersonID))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtNationalNo, "Already exists");
                }
                else
                {
                    errorProvider1.SetError(txtNationalNo, "");
                }
            }

           
            if (txt == txtEmail)
            {
          
                if (!Regex.IsMatch(txtEmail.Text.Trim(), @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtEmail, "Invalid Email Format");
                    return;
                }

               
                if (clsPeople.IsEmailExists(txtEmail.Text.Trim(), _PersonID))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtEmail, "Already exists");
                    return;
                }

                errorProvider1.SetError(txtEmail, "");
            }

            if (txt == txtPhone)
            {
                if (clsPeople.IsPhoneExists(txtPhone.Text.Trim(), _PersonID))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtPhone, "The Phone Already Exists");
                }
                else
                {
                    errorProvider1.SetError(txtPhone, "");
                }
            }
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMale.Checked)
            {
              
                if (string.IsNullOrEmpty(PicImages.ImageLocation))
                {
                    PicImages.Image = _GetDefaultImage();
                }
            }
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFemale.Checked)
            {
               
                if (string.IsNullOrEmpty(PicImages.ImageLocation))
                {
                    PicImages.Image = _GetDefaultImage();
                }
            }
        }
    }


 }

