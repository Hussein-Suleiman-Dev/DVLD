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

namespace DVLD.Licenses.Detain_Licenses
{
    public partial class frmDetainLicenseApplication : Form
    {
        public frmDetainLicenseApplication()
        {
            InitializeComponent();
        }
        private int _DetainID = -1;
        private int _SelectedLicenseID = -1;

        private void ctrlDriverInfoWithFilter1_Load(object sender, EventArgs e)
        {
           
        }

        private void ctrlDriverInfoWithFilter1_OnLicenseSelected(int obj)
        {
       
            _SelectedLicenseID =obj;
          
            lblLicenseID.Text= _SelectedLicenseID.ToString();
            if (_SelectedLicenseID == -1)
            {
             
                return;
            }
            
            if (!ctrlDriverInfoWithFilter1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected License is Not Active, Please Insert The Active License.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnDetain.Enabled = false;
                return;
            }
            if(ctrlDriverInfoWithFilter1.SelectedLicenseInfo.IsDetain)
            {
                MessageBox.Show("Selected License i already detained, choose another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;


            }
            llShowLicenseHisotry.Enabled = true;
            txtFineFees.Focus();
            btnDetain.Enabled= true;
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You sure to detain This License", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return; 
            }
            _DetainID = ctrlDriverInfoWithFilter1.SelectedLicenseInfo.DetainLicense(Convert.ToSingle(txtFineFees.Text.Trim()),clsGlobal.CurrentUser.UserID);
            if(_DetainID==-1)
            {

                
                    MessageBox.Show("Faild to Detain License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                
            }
            MessageBox.Show($"Detain License Succssfly with License {_SelectedLicenseID}", "Done", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            lblDetainID.Text= _DetainID.ToString();
            ctrlDriverInfoWithFilter1.FilterEnabled = false;
            txtFineFees.Enabled= false;
            llShowLicenseHisotry.Enabled= true;

        }

        private void frmDetainLicenseApplication_Load(object sender, EventArgs e)
        {
            this.Text = "Detain License";
                
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
            lblDetainDate.Text = clsFormat.DateToShort(DateTime.Now);
        }

        private void llShowLicenseHisotry_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(_SelectedLicenseID);
            frm.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm= new frmShowLicenseInfo(ctrlDriverInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID); frm.ShowDialog();
        }
    }
}
