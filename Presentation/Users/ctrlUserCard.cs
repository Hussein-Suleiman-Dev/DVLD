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

namespace DVLD.Users
{
    public partial class ctrlUserCard : UserControl
    {
        public ctrlUserCard()
        {
            InitializeComponent();
        }
   
        clsUser _User;
        int _UserID = -1;

        public int UserID
        {
            get{ return _UserID;  }
        }
        public void LoadUserInfo(int UserID)
        {
            _User = clsUser.Find(UserID);
            if (_User == null)
            {
                MessageBox.Show("No User Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
               
            }
            ctrlPersonInfo1.LoadPersonInfo(_User.PersonID);
            lblUserID.Text= UserID.ToString();
            lblUserName.Text=_User.UserName;
            lblIsActive.Text = (_User.isActive) ? "Yes" : "No";

        }
        private void ctrlChangePasswordUser_Load(object sender, EventArgs e)
        {
           
        }
    }
}
