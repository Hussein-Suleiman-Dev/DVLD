using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer;
using DVLD.Properties;
namespace DVLD
{
    public partial class ctrlPersonInfo : UserControl
    {
        private int _PersonID;
        private clsPeople _Person;

        public clsPeople SelectedPersonInfo{ get { return _Person; } }
        public ctrlPersonInfo ()
        {
            InitializeComponent();
        }
        private void _LoadImagePerson()
        {
            if (_Person.Gender == 0)
            {
                PicImage.Image = Resources.Male;

            }
            else { PicImage.Image = Resources.Female; }

            if (_Person.ImagePath != "")
            {
                if (File.Exists(_Person.ImagePath))
                {
                    PicImage.ImageLocation = _Person.ImagePath;
                   
                }
                else { MessageBox.Show($"Couldn't Find this Image{_Person.ImagePath}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }

        
         }
        private void _FillPersonInfo()
        {
            _PersonID = _Person.PersonID;
            lblPersonID.Text = _Person.PersonID.ToString();
            lblName.Text = _Person.FirstName + " " + _Person.SecondName + " " + _Person.ThirdName + " " + _Person.LastName;
            lblName.ForeColor= Color.Red;
            lblNationalNo.Text = _Person.NationalNumber.ToString();
            lblGender.Text = (_Person.Gender == 0) ? "Male" : "Female";
            lblEmail.Text = (_Person.Email != "") ? _Person.Email : lblEmail.Text;
            lblAddress.Text = _Person.Address;
            lblDateOfBirth.Text = _Person.DateOfBirth.ToString("dd/MM/yyyy");
            lblPhone.Text = _Person.Phone;
            lblCountry.Text = _Person.CountryInfo.CountryName;

            _LoadImagePerson();

            }

        public int PersonID { get { return _PersonID; } }

        public void LoadPersonInfo(string NationalNumber)
        {
            _Person = clsPeople.Find(NationalNumber);

            if (_Person == null)
            {
                MessageBox.Show($"No Person with National Number Person {NationalNumber} ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            _FillPersonInfo();
        }
        public void LoadPersonInfo(int PersonID)
        {
            _Person = clsPeople.Find(PersonID);

            if (_Person == null)
            {
                MessageBox.Show($"No Person with Person Id {_PersonID} ", "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);

                return;
            }
            _FillPersonInfo();

        }
      

        private void ctrlPersonInfo_Load(object sender, EventArgs e)
        {
            //_LoadPersonInfo();
        }

        private void lnkEditInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddUpdatePerson Update = new frmAddUpdatePerson(_PersonID);
            Update.ShowDialog();
            LoadPersonInfo(_PersonID);
        }
    }
}
