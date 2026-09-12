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

namespace DVLD.Licenses.Local_Licenses.Control
{
    public partial class ctrlDriverInfoWithFilter : UserControl
    {
        public ctrlDriverInfoWithFilter()
        {
            InitializeComponent();
        }

        public event Action<int> OnLicenseSelected;
        protected virtual void LicenseSelected(int LicenseID)
        { 
        Action<int>handler= OnLicenseSelected;
            if (handler != null)
            { 
            handler(LicenseID);
            }
        
        }

        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get { return _FilterEnabled; }

            set { _FilterEnabled = value;
                gbFilter.Enabled = _FilterEnabled;
            }
        }


        int _LicenseID = -1;
        public int PeronID 
        {
            get { return ctrlDriverInfo1.LicenseID; }
        }
        public clsLicense SelectedLicenseInfo 
        {
            get { return ctrlDriverInfo1.SelectLicenseInfo; }
        }
        public void LoadLicenseInfo(int LicenseID)
        {
            txtLicenseID.Text = LicenseID.ToString();
            ctrlDriverInfo1.LoadInfo(LicenseID);
            _LicenseID = ctrlDriverInfo1.LicenseID;
            if (OnLicenseSelected != null && FilterEnabled)
                // Raise the event with a parameter
                OnLicenseSelected(_LicenseID);
        }

        private void txtLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);


            // Check if the pressed key is Enter (character code 13)
            if (e.KeyChar == (char)13)
            {

                btnFind.PerformClick();
            }

        }

        private void txtLicenseID_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtLicenseID.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtLicenseID, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(txtLicenseID, null);
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLicenseID.Focus();
                return;

            }
            _LicenseID = int.Parse(txtLicenseID.Text);
            LoadLicenseInfo(_LicenseID);

        }
        public void txtLicenseIDFouces()
        {
            txtLicenseID.Focus();
        }

        private void ctrlDriverInfoWithFilter_Load(object sender, EventArgs e)
        {

        }
    }
}
