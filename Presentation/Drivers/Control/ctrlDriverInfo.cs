using BusinessLayer;
using DVLD.Global_Classes;
using DVLD.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace DVLD.Licenses.Local_Licenses.Control
{
    public partial class ctrlDriverInfo : UserControl
    {
        public ctrlDriverInfo()
        {
            InitializeComponent();
        }
        int _LicensesID; 
        private clsLicense _License;

        public clsLicense SelectLicenseInfo 
        {
            get { return _License; }
        }

        public int LicenseID
        {
            get { return _LicensesID; }
        }
        public void _LoadImage()
        {
            if (_License.DriverInfo.PersonInfo.Gender == 0)
            {
                pbPersonImage.Image = Resources.Male;
    
            }
            else { pbPersonImage.Image = Resources.Female; }

            string ImagePath = _License.DriverInfo.PersonInfo.ImagePath;
            if (ImagePath != "")
            { 
            pbPersonImage.ImageLocation= ImagePath;
            if (File.Exists(ImagePath))
                pbPersonImage.Load(ImagePath);
            }
            //else
            //    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void LoadInfo(int LicenseCID)
        { 
        _LicensesID = LicenseCID;
            _License = clsLicense.FindLicnese(_LicensesID);
            if (_License == null)
            {
                MessageBox.Show($"No License With {_LicensesID}","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                _LicensesID = -1;
               return;
            }

            lblLicenseID.Text = _License.LicenseID.ToString();
            lblIsActive.Text = _License.IsActive ? "Yes" : "No";
            lblIsDetained.Text = _License.IsDetained ? "Yes" : "No";
            lblClass.Text = _License.LicenseClassIfo.LicenseClassName;
            lblFullName.Text = _License.DriverInfo.PersonInfo.FullName;
            lblNationalNo.Text = _License.DriverInfo.PersonInfo.NationalNumber;
            lblGendor.Text = _License.DriverInfo.PersonInfo.Gender == 0 ? "Male" : "Female";
            lblDateOfBirth.Text = clsFormat.DateToShort(_License.DriverInfo.PersonInfo.DateOfBirth);

            lblDriverID.Text = _License.DriverID.ToString();
            lblIssueDate.Text = clsFormat.DateToShort(_License.IssueDate);
            lblExpirationDate.Text = clsFormat.DateToShort(_License.ExpirationDate);
            lblIssueReason.Text = _License.IssueReasonText;
            lblNotes.Text = _License.Notes == "" ? "No Notes" : _License.Notes;
            _LoadImage();

        }
    }
}
