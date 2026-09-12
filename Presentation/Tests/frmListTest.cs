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
    public partial class frmListTest : Form
    {
        DataTable _dtListTest;
        public frmListTest()
        {
            InitializeComponent();
        }
        private void _Load()
        {
            Text = "List of Test Types";
            _dtListTest =clsTestType.GetAllTestTypes();

            dgvListTest.DataSource = _dtListTest;
            lblCount.Text =  _dtListTest.Rows.Count.ToString();
            if (dgvListTest.Columns.Count > 0)
            {
                dgvListTest.Columns[0].HeaderText = "Test Type ID";
                dgvListTest.Columns[0].Width = 80;
                dgvListTest.Columns[1].HeaderText = "Title";
                dgvListTest.Columns[1].Width = 200;
                dgvListTest.Columns[2].HeaderText = "Description";
                dgvListTest.Columns[2].Width = 300;
                dgvListTest.Columns[3].HeaderText = "Fees";
                dgvListTest.Columns[3].Width = 100;
            }
        }

        private void dgvListTest_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
         
        }

        private void frmListTest_Load(object sender, EventArgs e)
        {
            _Load();
        }

        private void editTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditTestType Edit = new frmEditTestType((clsTestType.enTestType)dgvListTest.CurrentRow.Cells[0].Value);
            Edit.ShowDialog();
            _Load();
        }
    }
}
