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

namespace DVLD
{
    public partial class frmEditTestType : Form
    {
        clsTestType.enTestType _TestTypeID=clsTestType.enTestType.VisionTest;
        clsTestType _TestType;
        public frmEditTestType(clsTestType.enTestType TestTypeID)
        {
            InitializeComponent();
            _TestTypeID = TestTypeID;
        }
        private void _Load()
        { 
        Text= "Update Test Type";
            _TestType = clsTestType.Find(_TestTypeID);
            if(_TestType==null)
            {
                MessageBox.Show("Test Type not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            lblTestTypeID.Text = _TestTypeID.ToString();
            txtTitle.Text = _TestType.Title;
            txtDescription.Text = _TestType.Description;
            txtFees.Text = _TestType.Fees.ToString();
        }

        private void Validate(object sender, CancelEventArgs e)
        {
            TextBox txt= sender as TextBox;
            if(string.IsNullOrEmpty(txt.Text))
            {
                errorProvider1.SetError(txt, "This field is required.");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txt, "");
            }   
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            { 
            MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _TestType.Title = txtTitle.Text;
            _TestType.Description = txtDescription.Text;
            _TestType.Fees = float.Parse(txtFees.Text);
            if (_TestType.Save())
            { 
            MessageBox.Show("Test Type updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to update Test Type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmEditTestType_Load(object sender, EventArgs e)
        {
            _Load();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
