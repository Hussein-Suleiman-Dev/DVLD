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
    public partial class frmLoginScreen : Form
    {
        public frmLoginScreen()
        {
            InitializeComponent();
        }
        public static clsUser CurrentUser;
        private void btnLogin_Click(object sender, EventArgs e)
        {

    }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {

            clsUser User = clsUser.FindByUserNameAndPassword(txtUserName.Text.Trim(), txtPassword.Text.Trim());

            if (User != null)
            {
                if (chkRemmberMe.Checked)
                {
                    clsGlobal.RemmberUserNameAndPassword(txtUserName.Text.Trim(), txtPassword.Text.Trim());
                    //كتابة على ملف 
                }
                else { clsGlobal.RemmberUserNameAndPassword("", ""); }

                if (!User.isActive)
                { 
                txtUserName.Focus();
           MessageBox.Show("Your account is inactive. Please contact the administrator.", "Account Inactive", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }

                clsGlobal.CurrentUser = User;
                this.Hide();


                frmMainScreen frm = new frmMainScreen(this);
                frm.ShowDialog();

            }

        
            else
            {
                txtUserName.Focus();
                MessageBox.Show("Invalid Username or Password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
}

        private void frmLoginScreen_Load(object sender, EventArgs e)
        {
            string UserName = "";string Password = "";

            if (clsGlobal.GetStoreCredential(ref UserName, ref Password))
            { 
            txtUserName.Text= UserName;
                txtPassword.Text= Password;
                chkRemmberMe.Checked = true;
            
            }
          else{
                chkRemmberMe.Checked= false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    }
