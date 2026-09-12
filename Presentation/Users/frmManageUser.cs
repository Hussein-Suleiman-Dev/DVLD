using DVLD;
using DVLD.People.Controls;
using DVLD.Users;
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
    public partial class frmManageUser : Form
    {

        DataTable _dtAllUser;


        private void _RefreshUsers()
        {

            _dtAllUser = clsUser.GetAll();
            dvgAllUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // ربط البيانات بالـ DataGridView
            dvgAllUsers.DataSource = _dtAllUser;

            lblCount.Text = dvgAllUsers.Rows.Count.ToString();

            if (dvgAllUsers.Columns.Count > 0)
            {
                dvgAllUsers.Columns[0].HeaderText = "User ID";
                dvgAllUsers.Columns[0].Width = 110;

                dvgAllUsers.Columns[1].HeaderText = "Person ID";
                dvgAllUsers.Columns[1].Width = 110;

                dvgAllUsers.Columns[2].HeaderText = "Full Name";
                dvgAllUsers.Columns[2].Width = 170;

                dvgAllUsers.Columns[3].HeaderText = "User Name";
                dvgAllUsers.Columns[3].Width = 110;

                dvgAllUsers.Columns[4].HeaderText = "Is Active";
                dvgAllUsers.Columns[4].Width = 90;
            }
        }
        public frmManageUser()
        {
            InitializeComponent();
        }

        private void frmManageUser_Load(object sender, EventArgs e)
        {
            _RefreshUsers();
            cmbFilterBy.SelectedIndex = 0;

        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string ColumnName = "";
            switch (cmbFilterBy.Text)
            {

                case "User ID":
                    ColumnName = "UserID";
                    break;

                case "User Name":
                    ColumnName = "UserName";
                    break;
                case "Person ID":
                    ColumnName = "PersonID";
                    break;
                case "Full Name":
                    ColumnName = "FullName";
                    break;
                case "Is Active":
                    ColumnName = "IsActive";
                    break;



            }


            if (string.IsNullOrEmpty(txtFilterValue.Text) || ColumnName == "None")
            {
                _dtAllUser.DefaultView.RowFilter = "";
                lblCount.Text = dvgAllUsers.Rows.Count.ToString();
                return;
            }



            if (ColumnName == "PersonID" || ColumnName == "UserID")
            {

                _dtAllUser.DefaultView.RowFilter = string.Format(("[{0}]={1}"), ColumnName, txtFilterValue.Text);

            }

            else { _dtAllUser.DefaultView.RowFilter = string.Format("[{0}] Like'{1}%'", ColumnName, txtFilterValue.Text.Trim()); }
            lblCount.Text = dvgAllUsers.Rows.Count.ToString();
        }



        private void cmbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFilterBy.Text == "Is Active")
            {

                txtFilterValue.Visible = false;
                cmbFilterisActive.Visible = true;
                cmbFilterisActive.Focus();
                cmbFilterisActive.SelectedIndex = 0;
            }
            else 
            {
                txtFilterValue.Visible = (cmbFilterBy.Text != "None");
                cmbFilterisActive.Visible = false;
                if (cmbFilterBy.Text == "None")
                {
                    txtFilterValue.Enabled = false;
                }
                else { txtFilterValue.Enabled = true;
                    txtFilterValue.Text = "";
                    txtFilterValue.Focus();
                }
            }
        }

        private void cmbFilterisActive_SelectedIndexChanged(object sender, EventArgs e)
        {

            string FilterColumn = @"IsActive";
            string selectedValue = cmbFilterisActive.Text;

            switch (selectedValue)
            {
                case "All":
                    break;

                case "Yes":
                    selectedValue = "1";
                    break;

                case "No":
                   selectedValue= "0";
                    break;

             
            }

            if (selectedValue == "All")
            {
                _dtAllUser.DefaultView.RowFilter = "";
            }
            else _dtAllUser.DefaultView.RowFilter = string.Format("[{0}]={1}", FilterColumn, selectedValue);
            lblCount.Text = dvgAllUsers.Rows.Count.ToString();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cmbFilterBy.Text == "Person ID" || cmbFilterBy.Text == "User ID")
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAddUpdateUsers AddNewUser = new frmAddUpdateUsers();
            AddNewUser.ShowDialog();
            _RefreshUsers();
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dvgAllUsers.CurrentRow != null)
            {
                int id = Convert.ToInt16(dvgAllUsers.CurrentRow.Cells[0].Value);
                frmAddUpdateUsers UpdateUser = new frmAddUpdateUsers(id);
                UpdateUser.ShowDialog();
                _RefreshUsers();

            }
        }

        private void addUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dvgAllUsers.CurrentRow != null)
            {
                int id = Convert.ToInt16(dvgAllUsers.CurrentRow.Cells[0].Value);
                frmChangePassword ChangePasswordUser= new frmChangePassword(id);
                ChangePasswordUser.ShowDialog();
                _RefreshUsers();
            }
        }

        private void deleteUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dvgAllUsers.CurrentRow != null)
            {
                int id = Convert.ToInt16(dvgAllUsers.CurrentRow.Cells[0].Value);
                if (clsUser.Delete(id))
                {
                    MessageBox.Show("Delete User Succssfly", "Delete User", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    _RefreshUsers();
                }
                else { MessageBox.Show("The User Not Delete ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dvgAllUsers.CurrentRow != null)
            {
                int id = Convert.ToInt16(dvgAllUsers.CurrentRow.Cells[0].Value);
                frmUserDetalis UserDetalis= new frmUserDetalis(id);

                UserDetalis.ShowDialog();
            }
        }

        
    }
}
