using BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Licenses
{
    public partial class frmIssueDriverLicenseForFirstTIme : Form
    {
        int _LocalDrivingLicnenseApplicationID = -1;
        clsLocalDrivingLicenseApplication _LocalDrivingLicneseApplication;


        public frmIssueDriverLicenseForFirstTIme(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _LocalDrivingLicnenseApplicationID = LocalDrivingLicenseApplicationID;
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            int LicenseID = _LocalDrivingLicneseApplication.IssueLicenseForFirstTime(txtNotes.Text, clsGlobal.CurrentUser.UserID);
            if (LicenseID != -1) 
            {
                MessageBox.Show("License Issued Successfully with License ID = " + LicenseID.ToString(),
                       "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            else
            {
                MessageBox.Show("License Was not Issued ! ",
                 "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmIssueDriverLicenseForFirstTIme_Load(object sender, EventArgs e)
        {
            this.Text = "Issue Driver License For The First Time";
            txtNotes.Focus();
            _LocalDrivingLicneseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseID(_LocalDrivingLicnenseApplicationID);

            if (_LocalDrivingLicneseApplication == null)
            {

                MessageBox.Show("No Applicaiton with ID=" + _LocalDrivingLicnenseApplicationID.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            if (!_LocalDrivingLicneseApplication.PassedAllTests())
            {

                MessageBox.Show("Person Should Pass All Tests First.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            int LicenseID = _LocalDrivingLicneseApplication.GetActiveLicenseID();
            if (LicenseID != -1)
            {

                MessageBox.Show("Person already has License before with License ID=" + LicenseID.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;

            }
            ctrlDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingAppID(_LocalDrivingLicnenseApplicationID);
        }
    }
}
