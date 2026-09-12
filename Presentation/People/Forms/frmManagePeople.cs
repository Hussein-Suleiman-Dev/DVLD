using BusinessLayer;
using DVLD.People.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmManagePeople : Form
    {
        private static DataTable _dtAllPeople = clsPeople.GetAllPeople();
        private DataTable _dtPeople = _dtAllPeople.DefaultView.ToTable(false,
            "PersonID", "NationalNo", "FirstName", "SecondName", "ThirdName", "LastName", "Gender", "DateOfBirth", "Phone", "Email");
        
        public frmManagePeople()
        {
            InitializeComponent();
        }
        void _Refresh()
        {
           dvgAllPeople.DataSource = _dtPeople;

           
            lblCountRecords.Text = dvgAllPeople.Rows.Count.ToString();
            if (dvgAllPeople.Rows.Count > 0)
            {
                //dgvAllPeople.Columns[""].DefaultCellStyle.NullValue = "No Email";
                dvgAllPeople.Columns[0].HeaderText = "Person Id";
                dvgAllPeople.Columns[0].Width = 110;

                dvgAllPeople.Columns[1].HeaderText = "National No.";
                dvgAllPeople.Columns[1].Width = 120;


                dvgAllPeople.Columns[2].HeaderText = "First Name";
                dvgAllPeople.Columns[2].Width = 120;

                dvgAllPeople.Columns[3].HeaderText = "Second Name";
                dvgAllPeople.Columns[3].Width = 140;


                dvgAllPeople.Columns[4].HeaderText = "Third Name";
                dvgAllPeople.Columns[4].Width = 120;

                dvgAllPeople.Columns[5].HeaderText = "Last Name";
                dvgAllPeople.Columns[5].Width = 120;

                dvgAllPeople.Columns[6].HeaderText = "Gender";
                dvgAllPeople.Columns[6].Width = 120;

               dvgAllPeople.Columns[7].HeaderText = "Date Of Birth";
                dvgAllPeople.Columns[7].Width = 140;

                dvgAllPeople.Columns[8].HeaderText = "Phone";
                dvgAllPeople.Columns[8].Width = 120;
                
                dvgAllPeople.Columns[9].HeaderText = "Email";
                dvgAllPeople.Columns[9].Width = 120;
                
              
               
            }
          
        }
        void _RefreshAll()
        {
            _Refresh();
            lblCountRecords.Text = dvgAllPeople.RowCount.ToString();
            
        }
        private void frmManagePeople_Load(object sender, EventArgs e)
        {
            _RefreshAll();
            cmbFilter.SelectedText = "None";
            txtFilter.Visible = false;
        }

        private void btnFrmAdd_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frmAddUpdatePerson = new frmAddUpdatePerson();
            frmAddUpdatePerson.ShowDialog();
            _RefreshAll();
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dvgAllPeople.CurrentRow != null)
            {
                int id = Convert.ToInt32(dvgAllPeople.CurrentRow.Cells[0].Value);
                frmAddUpdatePerson FrmAddUpadtePerson= new frmAddUpdatePerson(id);
                FrmAddUpadtePerson.ShowDialog();
                _RefreshAll();
            }



        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string filterColumn;

            switch (cmbFilter.Text)
            {
                case "Person Id":
                    filterColumn = "PersonID";
                    break;

                case "National No.":
                    filterColumn = "NationalNo";
                    break;

                case "First Name":
                    filterColumn = "FirstName";
                    break;

                case "Second Name":
                    filterColumn = "SecondName";
                    break;

                case "Last Name":
                    filterColumn = "LastName";
                    break;

                case "Nationality":
                    filterColumn = "Nationality";
                    break;

                case "Gender":
                    filterColumn = "Gender";
                    break;

                case "Phone":
                    filterColumn = "Phone";
                    break;

                case "Email":
                    filterColumn = "Email";
                    break;

                default:
                    filterColumn = "None";
                    break;
            }

            if (txtFilter.Text == "" || txtFilter.Text == "None")
            {
                _dtPeople.DefaultView.RowFilter = "";
                lblCountRecords.Text = _dtPeople.Rows.Count.ToString();
                return;
            }
            if (filterColumn == "PersonID")
            {
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}]={1}", filterColumn, txtFilter.Text.Trim());
             
            }
            else { _dtPeople.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", filterColumn, txtFilter.Text);
           
            }
            lblCountRecords.Text = dvgAllPeople.Rows.Count.ToString();
        }

        private void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Visible = (cmbFilter.Text != "None");
            if (txtFilter.Visible)
            {
                txtFilter.Text = "";
                txtFilter.Focus();
            }
            
        }

      

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson();
            frm.ShowDialog();
            _RefreshAll();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
            if(dvgAllPeople.CurrentRow!=null)
                {
                    int Id = Convert.ToInt32(dvgAllPeople.CurrentRow.Cells[0].Value);
                if (clsPeople.Delete(Id))
                {
                    MessageBox.Show("Delete Succssfly", "Delete");
                    _RefreshAll();
                }
                else { MessageBox.Show("Warring", "Delete Not Successfly", MessageBoxButtons.OKCancel, MessageBoxIcon.Error); }

                }
        }

        private void shToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dvgAllPeople.CurrentRow != null)
            {
                int Id = Convert.ToInt32(dvgAllPeople.CurrentRow.Cells[0].Value);
                if ( Id != -1)
                {
                    frmPersonDetails PersonDetails = new frmPersonDetails(Id);
                    PersonDetails.ShowDialog();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cmbFilter.Text == "Person Id") 
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                { 
                e.Handled = true;
                }
            }
          
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            frmFindPerson Find= new frmFindPerson();
            Find.ShowDialog();
        }

        private void dvgAllPeople_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
