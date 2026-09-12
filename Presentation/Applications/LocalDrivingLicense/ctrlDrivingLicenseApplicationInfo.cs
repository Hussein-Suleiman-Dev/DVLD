using BusinessLayer;
using BusinessLayer.Tests;
using DVLD.Licenses.Local_Licenses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.LocalDrivingLicense
{
    public partial class ctrlDrivingLicenseApplicationInfo : UserControl
    {
        public ctrlDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }
        int _LoacalDrivingLicenseID;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        int _ApplicationID = -1;

        private void _ResetLocalDrivingLicenseApplicationInfo()
        {
            _LoacalDrivingLicenseID = -1;
            ctrlApplicationBasicInfo1.ResetApplicationInfo();
            lblLocalDrivingLicenseApplicationID.Text = "[????]";
            lblAppliedFor.Text = "[????]";


        }

        private void _FillLocalDrivingLicenseAppInfo()
        {
            lblLocalDrivingLicenseApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseId.ToString();
            lblAppliedFor.Text = clsLicenseClass.Find(_LocalDrivingLicenseApplication.LicenseClassID).LicenseClassName;
            lblPassedTests.Text = _LocalDrivingLicenseApplication.GetPassedTestCount().ToString();
            ctrlApplicationBasicInfo1.LoadApplicationInfo(_LocalDrivingLicenseApplication.ApplicationID);
            _ApplicationID = ctrlApplicationBasicInfo1.ApplicationID;

            if (clsLicense.IsLicenseExistByPersonID(_LocalDrivingLicenseApplication.PersonInfo.PersonID, _LocalDrivingLicenseApplication.LicenseClassID))
            {
                llShowLicenceInfo.Enabled = true;
                _LoacalDrivingLicenseID = _LocalDrivingLicenseApplication.GetActiveLicenseID();
            }
            else { llShowLicenceInfo.Enabled = false; }
        }

        public void LoadApplicationInfoByLocalDrivingAppID(int LocalDrivingLicenseID)
        {
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseID(LocalDrivingLicenseID);
            if (_LocalDrivingLicenseApplication == null)
            {
                _ResetLocalDrivingLicenseApplicationInfo();


                MessageBox.Show("No Application with ApplicationID = " + _LoacalDrivingLicenseID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillLocalDrivingLicenseAppInfo();
        }

        private void ctrlApplicationBasicInfo1_Load(object sender, EventArgs e)
        {

        }
        
        private void ctrlDrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {

        }

        private void llShowLicenceInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo Licnes = new frmShowLicenseInfo(_LoacalDrivingLicenseID);
            Licnes.ShowDialog();
        }
    }
}
