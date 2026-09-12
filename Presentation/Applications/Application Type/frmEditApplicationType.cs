using BusinessLayer;
using FastUI.FastUILibrary.Components;
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
    public partial class frmEditApplicationType : Form
    {
        clsApplicationTypes _Application;
        int _ApplicationTypeID;

        public frmEditApplicationType(int ApplicationTypeID)
        {
            InitializeComponent();
            _ApplicationTypeID = ApplicationTypeID;
        }
        void _Load()
        {
            _Application = clsApplicationTypes.Find(_ApplicationTypeID);

            if (_Application == null)
            {
                MessageBox.Show(
                    $"No Application with ID {_ApplicationTypeID}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Stop);

                this.Close();
                return;
            }

            lblAppID.Text = _ApplicationTypeID.ToString();

            txtAppTitle.Text = _Application.ApplicationTypeName;

        
            txtAppFess.Text = (_Application.ApplicationTypeFess.ToString());
        }

        private void frmEditApplicationType_Load(object sender, EventArgs e)
        {
            _Load();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                MessageBox.Show("Fix The Error ", "Error",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                return;
            }

            _Application.ApplicationTypeName = txtAppTitle.Text.Trim();
            _Application.ApplicationTypeFess=Convert.ToSingle(txtAppFess.Text);
            if (_Application.Save())
            {
                MessageBox.Show("Saved Successfly", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            }
            else { MessageBox.Show("Saved Faild", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void Validate(object sender, CancelEventArgs e)
        {
            Control txt = sender as Control;

            if (txt == null) return;

            if (string.IsNullOrEmpty(txt.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txt, "Required field");
            }
            else
            {
                errorProvider1.SetError(txt, "");
            }
        }

        private void txtAppFess_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; 
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
