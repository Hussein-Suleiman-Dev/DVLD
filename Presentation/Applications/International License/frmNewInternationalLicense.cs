using BusinessLayer;
using DVLD.Global_Classes;
using DVLD.Licenses;
using DVLD.Licenses.International_Licenses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.International_License
{
    public partial class frmNewInternationalLicense : Form
    {
        public frmNewInternationalLicense()
        {
            InitializeComponent();
        }
        int _SelectedLicenseID = -1;
        int _InternationalLicenseI = -1;
        private void ctrlDriverInfoWithFilter1_OnLicenseSelected(int obj)
        {
           _SelectedLicenseID = obj;
            lblLocalLicenseID.Text = _SelectedLicenseID.ToString();
            if (_SelectedLicenseID == -1)
            {
                return;
            }
            llShowLicenseHisotry.Enabled = true;
            if (ctrlDriverInfoWithFilter1.SelectedLicenseInfo.LicenseClass != 3)
            {
                MessageBox.Show("Selected License should be Class 3, select another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int ActiveInternaionalLicenseID = clsInternationalLicense.GetActiveInternationalLicenseIDByDriverID(ctrlDriverInfoWithFilter1.SelectedLicenseInfo.DriverID);
            //هل لديه رخصة دولية فعالة

            if (ActiveInternaionalLicenseID != -1)
            {
                MessageBox.Show("Person already have an active international license with ID = " + ActiveInternaionalLicenseID.ToString(), "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                llShowLicenseInfo.Enabled = true;
                _InternationalLicenseI = ActiveInternaionalLicenseID;
                btnIssueLicense.Enabled = false;
                return;
            }
            btnIssueLicense.Enabled = true;
        }

        private void frmNewInternationalLicense_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblIssueDate.Text = lblApplicationDate.Text;
            lblExpirationDate.Text = clsFormat.DateToShort(DateTime.Now.AddYears(1));//add one year.
            lblFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationType.NewInternationalDrivingLicense).ApplicationTypeFess.ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
        }

        private void btnIssueLicense_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to issue the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            clsInternationalLicense InternationalLicense = new clsInternationalLicense();
            //those are the information for the base application, because it inhirts from application, they are part of the sub class.

            InternationalLicense.ApplicationPersonID = ctrlDriverInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID;
            InternationalLicense.ApplicationDate = DateTime.Now;
            InternationalLicense.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            InternationalLicense.LastStatusDate = DateTime.Now;
            InternationalLicense.PaidFees = clsApplicationTypes.Find((int)clsApplication.enApplicationType.NewInternationalDrivingLicense).ApplicationTypeFess;
            InternationalLicense.CreateByUserID = clsGlobal.CurrentUser.UserID;


            InternationalLicense.DriverID = ctrlDriverInfoWithFilter1.SelectedLicenseInfo.DriverID;
            InternationalLicense.IssuedUsingLocalLicenseID = ctrlDriverInfoWithFilter1.SelectedLicenseInfo.LicenseID;
            InternationalLicense.IssueDate = DateTime.Now;
            InternationalLicense.ExpirationDate = DateTime.Now.AddYears(1);

            InternationalLicense.CreatedByUserIDInternational = clsGlobal.CurrentUser.UserID;

            if (!InternationalLicense.Save())
            {
                MessageBox.Show("Faild to Issue International License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            lblApplicationID.Text = InternationalLicense.ApplicationID.ToString();
            _InternationalLicenseI = InternationalLicense.InternationalLicenseID;
            lblLicenseID.Text = InternationalLicense.InternationalLicenseID.ToString();
            MessageBox.Show("International License Issued Successfully with ID=" + InternationalLicense.InternationalLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnIssueLicense.Enabled = false;
            ctrlDriverInfoWithFilter1.FilterEnabled = false;
            llShowLicenseInfo.Enabled = true;

        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowInternationalLicenseInfo frm =
              new frmShowInternationalLicenseInfo(_InternationalLicenseI);
            frm.ShowDialog();
        }

        private void llShowLicenseHisotry_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm =
                new frmShowPersonLicenseHistory(ctrlDriverInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();

        }
    }
}
