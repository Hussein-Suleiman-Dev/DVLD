using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer.User;
using DVLD.Applications.International_License;
using DVLD.Applications.RelaseDetainLicense;
using DVLD.Drivers;
using DVLD.Licenses.Detain_Licenses;
using DVLD.Licenses.International_Licenses;
using DVLD.Licenses.Renew_Local_License;
using DVLD.Licenses.ReplaceLostOrDamageLicense;
using DVLD.Users;
namespace DVLD
{
    public partial class frmMainScreen : Form
    {
        public frmMainScreen()
        {
            InitializeComponent();
        }
        frmLoginScreen _frmLogin;
        public frmMainScreen(frmLoginScreen Login)
        {
            InitializeComponent();
            _frmLogin = Login;
        }


        clsUser CurrentUser = frmLoginScreen.CurrentUser;

        

     
       
        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManagePeople frmManagePeople = new frmManagePeople();
            frmManagePeople.ShowDialog();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsGlobal.CurrentUser = null;
            _frmLogin.Show();
            this.Close();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageUser ManageUsers = new frmManageUser();
          ManageUsers.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword ChangePassword = new frmChangePassword(clsGlobal.CurrentUser.UserID);
            ChangePassword.ShowDialog();


        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserDetalis UserDeatlis = new frmUserDetalis(clsGlobal.CurrentUser.UserID);
            UserDeatlis.ShowDialog();
        }

        private void accountSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void drivingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void applicationToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageApplicationTypes ManageApp=new frmManageApplicationTypes();
            ManageApp.ShowDialog();
        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicense LocalDrivingLicense=new frmAddUpdateLocalDrivingLicense();
            LocalDrivingLicense.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void localDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListLocalDrivingLicense List=new frmListLocalDrivingLicense();
            List.ShowDialog();

        }

        private void renewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRenewLocalLicense frm=new frmRenewLocalLicense();
            frm.ShowDialog();
        }

        private void replaceOrDamageDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReplaceLostOrDamage frm=new frmReplaceLostOrDamage();
            frm.ShowDialog();
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListDrivers frm=new frmListDrivers();
            frm.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDetainLicenseApplication frm=new frmDetainLicenseApplication();
            frm.ShowDialog();
        }

        private void relaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRelaseDetainLicnese frm = new frmRelaseDetainLicnese();
            frm.ShowDialog();
        }

        private void manageDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListDetainLicense DetainedLicense=new frmListDetainLicense();
            DetainedLicense.ShowDialog();
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicense frm=new frmNewInternationalLicense();
            frm .ShowDialog();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListTest List=new frmListTest();
            List.ShowDialog();
        }

        private void internationalLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListInternationalLicense frm= new frmListInternationalLicense();
            frm.ShowDialog();
        }
    }
}
