using BusinessLayer;
using DVLD.Global_Classes;
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

namespace DVLD.Licenses.Renew_Local_License
{
    public partial class frmRenewLocalLicense : Form
    {
        public frmRenewLocalLicense()
        {
            InitializeComponent();
        }
        int _NewLicenseID = -1;

        private void frmRenewLocalLicense_Load(object sender, EventArgs e)
        {
            ctrlDriverInfoWithFilter1.txtLicenseIDFouces();
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblIssueDate.Text = lblApplicationDate.Text;
            lblExpirationDate.Text="???";
            lblApplicationFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationType.RenewDrivingLicense).ApplicationTypeFess.ToString();

            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;

        
        }

        private void ctrlDriverInfoWithFilter1_OnLicenseSelected(int obj)
        {
            int SelectedLicesneID = obj;
            lblOldLicenseID.Text = SelectedLicesneID.ToString();


            llShowLicenseHisotry.Enabled = (SelectedLicesneID != -1);
            if (SelectedLicesneID == -1)
            {
                return;
            }
            int DefaultValidityLength = ctrlDriverInfoWithFilter1.SelectedLicenseInfo.LicenseClassIfo.DefaultValidityPeriod;
            lblExpirationDate.Text = clsFormat.DateToShort(DateTime.Now.AddYears(DefaultValidityLength));
            lblLicenseFees.Text=ctrlDriverInfoWithFilter1.SelectedLicenseInfo.LicenseClassIfo.ClassFees.ToString();
            lblTotalFees.Text=(Convert.ToSingle(lblApplicationFees.Text)+Convert.ToSingle(lblLicenseFees.Text)).ToString();
            txtNotes.Text = ctrlDriverInfoWithFilter1.SelectedLicenseInfo.Notes;
            if (!ctrlDriverInfoWithFilter1.SelectedLicenseInfo.IsLicenseExpired())
            {
                MessageBox.Show("Selected License is not yet expiared, it will expire on: " + clsFormat.DateToShort(ctrlDriverInfoWithFilter1.SelectedLicenseInfo.ExpirationDate)
                      , "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRenewLicense.Enabled = false;
                return;
            }

            if (!ctrlDriverInfoWithFilter1.SelectedLicenseInfo.IsActive)
            {

                MessageBox.Show("Selected License is not Not Active, choose an active license."
                    , "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRenewLicense.Enabled = false;
                return;
            }
            btnRenewLicense.Enabled = true;



        }

        private void btnRenewLicense_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Renew the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            clsLicense NewLicense = ctrlDriverInfoWithFilter1.SelectedLicenseInfo.RenewLicense(txtNotes.Text.Trim(), clsGlobal.CurrentUser.UserID);

            if (NewLicense == null)
            {
                MessageBox.Show("Faild to Renew the License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            lblApplicationID.Text = NewLicense.ApplicationID.ToString();
            _NewLicenseID = NewLicense.LicenseID;
            lblRenewedLicenseID.Text = _NewLicenseID.ToString();
            MessageBox.Show("Licensed Renewed Successfully with ID=" + _NewLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnRenewLicense.Enabled = false;
            ctrlDriverInfoWithFilter1.FilterEnabled = false;
            llShowNewLicense.Enabled = true;


        }

        private void llShowNewLicense_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo LicenseInfo = new frmShowLicenseInfo(_NewLicenseID);
            LicenseInfo.ShowDialog();
        }

        private void ctrlDriverInfoWithFilter1_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
