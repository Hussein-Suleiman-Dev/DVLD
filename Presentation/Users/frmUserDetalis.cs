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

namespace DVLD.Users
{
    public partial class frmUserDetalis : Form
    {
       
        int _UserID;
        public frmUserDetalis()
        {
            InitializeComponent();
        }

        public frmUserDetalis(int UserID)
        {
            InitializeComponent();
            _UserID=UserID;
            Text = "User Dealis";
        }

        private void frmUserDetalis_Load(object sender, EventArgs e)
        {
            ctrlChangePasswordUser1.LoadUserInfo(_UserID);
        }
    }
}
