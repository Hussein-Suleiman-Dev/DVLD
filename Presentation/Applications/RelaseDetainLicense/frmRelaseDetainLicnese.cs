using BusinessLayer;
using DVLD.Global_Classes;
using DVLD.Licenses;
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

namespace DVLD.Applications.RelaseDetainLicense
{
    public partial class frmRelaseDetainLicnese : Form
    {
        public frmRelaseDetainLicnese()
        {
            InitializeComponent();
            this.Text = "Relase Detain Licenses";
        }
        int _SelectedLicenseId = -1;
        private void label10_Click(object sender, EventArgs e)
        {

        }
        public frmRelaseDetainLicnese(int LicenseID)
        {
            InitializeComponent();
            _SelectedLicenseId=LicenseID;
            ctrlDriverInfoWithFilter1.LoadLicenseInfo(_SelectedLicenseId);
            ctrlDriverInfoWithFilter1.FilterEnabled = false;
            this.Text = "Relase Detain Licenses";

        }

        private void ctrlDriverInfoWithFilter1_OnLicenseSelected(int obj)
        {

           _SelectedLicenseId= obj;
            lblLicenseID.Text = _SelectedLicenseId.ToString();

            llShowLicenseHisotry.Enabled= true;

            if (_SelectedLicenseId == -1)
            {
                return;
            }
            if (!ctrlDriverInfoWithFilter1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected License is Not Active, Please Insert The Active License.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!ctrlDriverInfoWithFilter1.SelectedLicenseInfo.IsDetain)
            { 
            MessageBox.Show("Select License Info is Not Detained","Chose another one ",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            lblApplicationFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationType.ReleaseDatainDrivingLicense).ApplicationTypeFess.ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
            lblDetainID.Text = ctrlDriverInfoWithFilter1.SelectedLicenseInfo.DetainedInfo.DetainID.ToString();
            lblLicenseID.Text=ctrlDriverInfoWithFilter1.SelectedLicenseInfo.LicenseID.ToString();
            lblDetainDate.Text =  clsFormat.DateToShort( ctrlDriverInfoWithFilter1.SelectedLicenseInfo.DetainedInfo.DetainDate);
            lblFineFees.Text=ctrlDriverInfoWithFilter1.SelectedLicenseInfo.DetainedInfo.FineFees.ToString();
            lblTotalFees.Text = ((Convert.ToSingle(lblApplicationFees.Text.Trim())) + (Convert.ToSingle(lblFineFees.Text.Trim()))).ToString();
            btnRelase.Enabled= true;

        }

        private void btnRelase_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You sure to Relase detain This License", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            int ApplicationID = -1;
            bool IsRelased = ctrlDriverInfoWithFilter1.SelectedLicenseInfo.ReleaseDetainedLicense(clsGlobal.CurrentUser.UserID,ref ApplicationID);
            lblApplicationID.Text= ApplicationID.ToString();
            btnRelase.Enabled = false;
            llShowLicenseInfo.Enabled= true;
            ctrlDriverInfoWithFilter1.FilterEnabled = false;
            MessageBox.Show($"Relase Detain License Succssfly with License {_SelectedLicenseId}", "Done", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_SelectedLicenseId);
            frm.ShowDialog();
        }

        private void llShowLicenseHisotry_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           frmShowPersonLicenseHistory frm=new frmShowPersonLicenseHistory(ctrlDriverInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void frmRelaseDetainLicnese_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
