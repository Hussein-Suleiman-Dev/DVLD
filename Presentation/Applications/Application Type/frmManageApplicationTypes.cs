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
    public partial class frmManageApplicationTypes : Form
    {
        DataTable _dtApllicationTypes;

        private void _LoadApplication()
        {
            _dtApllicationTypes = clsApplicationTypes.GetAllApplicationTypes();
            dgvApplicationTypes.DataSource = _dtApllicationTypes;
            lblCountRecord.Text=dgvApplicationTypes.RowCount.ToString();
            if (_dtApllicationTypes.Columns.Count > 0)
            {
                dgvApplicationTypes.Columns[0].HeaderText = "ID";
                dgvApplicationTypes.Columns[0].Width = 110;

                dgvApplicationTypes.Columns[1].HeaderText = "Title";
                dgvApplicationTypes.Columns[1].Width = 350;


                dgvApplicationTypes.Columns[2].HeaderText = "Fess";
                dgvApplicationTypes.Columns[2].Width = 110;


            }

        
        }

        public frmManageApplicationTypes()
        {
            InitializeComponent();
        }

        private void frmManageApplicationTypes_Load(object sender, EventArgs e)
        {
            Text = "List Appliction Types";
            _LoadApplication();
        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int AppID=(int)dgvApplicationTypes.CurrentRow.Cells[0].Value;
            frmEditApplicationType EditApp=new frmEditApplicationType(AppID);
           EditApp.ShowDialog();
            frmManageApplicationTypes_Load(null, null);
        }
    }
}
