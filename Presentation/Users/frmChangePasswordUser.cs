
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
namespace DVLD.Users
{
    public partial class frmChangePassword : Form
    {

        private int _UserID;
        clsUser _User;

        public frmChangePassword()
        {
            InitializeComponent();
        }
        public frmChangePassword(int UserID)
        {
            InitializeComponent();
            _UserID= UserID;

        }

        private void _LoadUsersInfo()
        {
            Text = "Change Password";
            _User = clsUser.Find(_UserID);
            if (_User == null)
            {
                MessageBox.Show("No User Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ctrlChangePasswordUser1.LoadUserInfo(_UserID);

         }

        private void Valdate(object sender, CancelEventArgs e)
        {
            TextBox txt = (TextBox)sender;

            if (string.IsNullOrEmpty(txt.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txt, "Required Filed");
                return;
            }
            else { errorProvider1.SetError(txt, ""); }

            if (txt == txtCurrentPassword)
            {
                if (txtCurrentPassword.Text != _User.Password)
                {
                    e.Cancel= true;
                    errorProvider1.SetError(txtCurrentPassword, "Password is InCorrect");
                    return;
                }
                else { errorProvider1.SetError(txtCurrentPassword, ""); }
            }
            if (txt == txtNewPassword)
            {
                if (txtNewPassword.Text.Length < 4)
                {
                    errorProvider1.SetError(txtNewPassword, "The Password must 4 Charcter");
                }
            }

            if (txt == txtConfirmPassword)
            {
                if (txtConfirmPassword.Text.Trim() != txtNewPassword.Text.Trim())
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtConfirmPassword, "The Password is Not matching ");
                }
            }


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                return;
            }

            _User.Password = txtNewPassword.Text;
            if (_User.Save())
            {
                MessageBox.Show("Update Password Succssfly", "Update Done", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else { MessageBox.Show("The Password Not Updated", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void frmUserDetalis_Load(object sender, EventArgs e)
        {
            _LoadUsersInfo();
        }
    }
}
