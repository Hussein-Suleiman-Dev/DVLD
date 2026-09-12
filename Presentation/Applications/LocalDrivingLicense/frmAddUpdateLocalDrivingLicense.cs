using BusinessLayer;
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
namespace DVLD
{
    public partial class frmAddUpdateLocalDrivingLicense : Form
    {
        clsLocalDrivingLicenseApplication _LocalDrivingApplication;
        int _LocalDrivingApplicationID = -1;
        int _PersonID;
        enum enMode { Add = 1, Update = 2 }
        enMode _Mode;



        public frmAddUpdateLocalDrivingLicense()
        {
            InitializeComponent();
            Text = "Add New Local Driving License";
            _Mode = enMode.Add;
        }
        public frmAddUpdateLocalDrivingLicense(int LocalDrivinLicenseApplicationID)
        {
            InitializeComponent();
            Text = "Add New Local Driving License";
            _Mode = enMode.Update;
            _LocalDrivingApplicationID= LocalDrivinLicenseApplicationID;
        }

        void _FillLicenseClass()
        {

            DataTable _dt = clsLicenseClass.GetAll();
            foreach (DataRow item in _dt.Rows)
            {
                cmbLicenseClass.Items.Add(item["ClassName"]);
            }

        }
        void _Reset()
        {
            _FillLicenseClass();

            if (_Mode == enMode.Add)
            {
                lblTitle.Text = "New Local Driving License Application";
                this.Text = "New Local Driving License Application";
                ctrlPersonInfoWithFilter2.Focus();
                lblAppFees.Text = (clsApplicationTypes.Find((int)clsApplication.enApplicationType.NewDrivingLicense).ApplicationTypeFess).ToString();
                lblAppDate.Text = DateTime.Now.ToShortTimeString();
                cmbLicenseClass.SelectedIndex = 2;
                _LocalDrivingApplication = new clsLocalDrivingLicenseApplication();
                lblCreatedUser.Text = clsGlobal.CurrentUser.UserName;
                btnSave.Enabled = false;
                tbApplicationInfon.Enabled = false;


            }
            else {

                lblTitle.Text = "Update Local Driving License Application";
                this.Text = "Update Local Driving License Application";
                ctrlPersonInfoWithFilter2.Enabled = false;
                tbApplicationInfon.Enabled=true ;
                btnSave.Enabled = true;
            }


         
        }

        private void _LoadInfo()
        {
            _LocalDrivingApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseID(_LocalDrivingApplicationID);
            if (_LocalDrivingApplication == null)
            {
                MessageBox.Show("The Application Is Not Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ctrlPersonInfoWithFilter2.LoadPersonInfo(_LocalDrivingApplication.PersonInfo.PersonID);
            lblAppID.Text=_LocalDrivingApplication.ApplicationID.ToString();
            lblAppDate.Text=_LocalDrivingApplication.ApplicationDate.ToString();
            cmbLicenseClass.SelectedItem=clsLicenseClass.Find(_LocalDrivingApplication.LicenseClassID).LicenseClassName;
            lblAppFees.Text = _LocalDrivingApplication.ApplicationInfo.ApplicationTypeFess.ToString();
            lblCreatedUser.Text = _LocalDrivingApplication.CreateByUserInfo.UserName;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            
        }

        private void frmAddUpdateLocalDrivingLicense_Load(object sender, EventArgs e)
        {
            _Reset();
            if (_Mode == enMode.Update)
            {
                _LoadInfo();
            }
        }

        private void btnNext_Click_1(object sender, EventArgs e)
        {
            if (ctrlPersonInfoWithFilter2.SelectPerosnInfo != null)
            {
                _PersonID = ctrlPersonInfoWithFilter2.PersonID;
                tbApplicationInfon.Enabled = true;
                btnNext.Enabled = true;
                btnSave.Enabled = true;
                tbLocalDivingLicense.SelectedTab = tbLocalDivingLicense.TabPages["tbApplicationInfon"];
            }
            else { MessageBox.Show("Please select a person first.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error); }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            int LicenseClassID = clsLicenseClass.Find(cmbLicenseClass.Text).LicenseClassID;


            int ActiveApplicationID = clsApplication.GetActiveApplicationIDForLicenseClass(_PersonID, clsApplication.enApplicationType.NewDrivingLicense, LicenseClassID);

            if (ActiveApplicationID != -1)
            {
                MessageBox.Show("Choose another License Class, the selected Person Already have an active application for the selected class with id=" + ActiveApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbLicenseClass.Focus();
                return;
            }

            //check if user already have issued license of the same driving  class.
            //if (clsLicense.IsLicenseExistByPersonID(ctrlPersonCardWithFilter1.PersonID, LicenseClassID))
            //{

            //    MessageBox.Show("Person already have a license with the same applied driving class, Choose diffrent driving class", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            _LocalDrivingApplication.ApplicationPersonID = ctrlPersonInfoWithFilter2.PersonID; ;
            _LocalDrivingApplication.ApplicationDate = DateTime.Now;
            _LocalDrivingApplication.ApplicationTypeID = 1;
            _LocalDrivingApplication.ApplicationStatus = clsApplication.enApplicationStatus.New;
            _LocalDrivingApplication.LastStatusDate = DateTime.Now;
            _LocalDrivingApplication.PaidFees = Convert.ToSingle(lblAppFees.Text);
            _LocalDrivingApplication.CreateByUserID = clsGlobal.CurrentUser.UserID;
            _LocalDrivingApplication.LicenseClassID = LicenseClassID;


            if (_LocalDrivingApplication.Save())
            {
                lblAppID.Text = _LocalDrivingApplication.LocalDrivingLicenseId.ToString();
                MessageBox.Show("Application Saved Successfully.");
                _Mode = enMode.Update;
            }
            else
            {
                MessageBox.Show("Failed to save application.");
            }


        }

        private void frmAddUpdateLocalDrivingLicense_Activated(object sender, EventArgs e)
        {
            ctrlPersonInfoWithFilter2.FilterFocus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
