using BusinessLayer.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace DVLD
{
    public partial class frmAddUpdateUsers : Form
    {

        enum enMode {Add=1,Update=2 }
        enMode _Mode = enMode.Add;
        int _userID = -1;
        clsUser _User;
        int _PersonID;

        public frmAddUpdateUsers()
        {
            InitializeComponent();
            _Mode = enMode.Add;
         
        }
        public frmAddUpdateUsers(int UserId)
        {
            InitializeComponent();
            _userID = UserId;
            _Mode = enMode.Update;
        }


        private void _ResetDefaultValues()
        {
            if (_Mode == enMode.Add)
            {
                lblTitle.Text = "Add New User";
                this.Text = "Add New User";
                _User = new clsUser();
                tbLoginInfo.Enabled = false;

                ctrlPersonInfoWithFilter1.FilterFocus();
            }

            else 
            {
                lblTitle.Text = "Update User";
                this.Text = "Update User";
                tbUserInfo.Enabled = true;
                btnSave.Enabled = true;
            
            }
            txtPassword.Text = "";
            txtUserName.Text = "";
            txtConfirmPassword.Text = "";
            chkIsActive.Checked = true;
        }


        private void _LoadData()
        {
          
            _User = clsUser.Find(_userID);
            ctrlPersonInfoWithFilter1.EnabledFilter = false;

            if (_User == null)
            {
                MessageBox.Show("No User Found with ID = " + _userID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

           
            ctrlPersonInfoWithFilter1.LoadPersonInfo(_User.PersonID);
            ctrlPersonInfoWithFilter1.ShowAddPerson = false;

           
            txtUserName.Text = _User.UserName;
            txtPassword.Text = _User.Password;
            txtConfirmPassword.Text = _User.Password;
            chkIsActive.Checked = _User.isActive;
            lblsUserID.Text = _User.UserID.ToString();

            lblTitle.Text = "Update User";
            this.Text = "Update User";
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {

            if (_Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tbLoginInfo.Enabled=true;
                tbUserInfo.SelectedTab = tbUserInfo.TabPages["tbLoginInfo"];
                return;
            
            }
            if (_Mode == enMode.Add) {
                if (ctrlPersonInfoWithFilter1.PersonID != -1)
                {

                    if (clsUser.IsUserExsist(ctrlPersonInfoWithFilter1.PersonID))
                    {
                        MessageBox.Show("Select Person already Has User,Chose another one ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        ctrlPersonInfoWithFilter1.FilterFocus();

                    }
                    else
                    {
                        btnSave.Enabled = true;
                        tbLoginInfo.Enabled = true;
                        tbUserInfo.SelectedTab = tbUserInfo.TabPages["tbLoginInfo"];
                    }


                }
            }
            else { MessageBox.Show("Please Select a Person", "Select a Person",MessageBoxButtons.OK,MessageBoxIcon.Warning); }

               

        }


        private bool _LoadUserToSave()
        {
            if (_User != null)
            {
                _User.UserName = txtUserName.Text.Trim();
                _User.Password = txtPassword.Text.Trim();
                _User.isActive = chkIsActive.Checked;
                _User.PersonID = ctrlPersonInfoWithFilter1.PersonID;
                _User.PersonInfo = ctrlPersonInfoWithFilter1.SelectPerosnInfo;
                return true;
            }
            else { MessageBox.Show("Error to Save");return false; }
        }
      
        private void btnSave_Click(object sender, EventArgs e)
        {
          

            
            if (!ValidateChildren())
            {
                MessageBox.Show("Fix validation errors first!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (_LoadUserToSave())
            {
                if (_User.Save())
                {
                    MessageBox.Show("Data Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _Mode = enMode.Update;
                    lblTitle.Text = txtUserName.Text;
                    lblsUserID.Text = _User.UserID.ToString();
                    this.Text = "Update User";
                }

            }


        }

        private void Validate(object sender, CancelEventArgs e)
        {
            TextBox txt = (TextBox)sender;

            // تحقق الحقول الفارغة
            if (string.IsNullOrEmpty(txt.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txt, "This field is required");
                return;
            }
            else
            {
                errorProvider1.SetError(txt, "");
            }

            if (txt == txtUserName)
            {
                if (_Mode == enMode.Add || txtUserName.Text.Trim() != _User.UserName)
                {
                    if (clsUser.IsUserExsist(txtUserName.Text.Trim()))
                    {
                        e.Cancel = true;
                        errorProvider1.SetError(txtUserName, "The User Name Is Already Used In System");
                        return;
                    }
                }
                errorProvider1.SetError(txtUserName, "");
            }

            // تحقق من تطابق الباسورد
            if (txt == txtConfirmPassword)
            {
                if (txtConfirmPassword.Text.Trim() != txtPassword.Text.Trim())
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtConfirmPassword, "Passwords do not match!");
                }
                else
                {
                    errorProvider1.SetError(txtConfirmPassword, "");
                }
            }

        }

       
        private void DataBack(object sender, int PersonID)
        {
            _PersonID = PersonID;
            ctrlPersonInfoWithFilter1.LoadPersonInfo(_PersonID); // شحن أداة الفلتر بالشخص المضاف فوراً
        }

        private void frmAddNewUser_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();
            if(_Mode==enMode.Update)
            {
                _LoadData();
            }
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
