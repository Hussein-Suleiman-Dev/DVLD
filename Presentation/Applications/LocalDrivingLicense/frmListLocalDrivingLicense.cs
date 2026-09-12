using BusinessLayer;
using DVLD.Applications;
using DVLD.Licenses;
using DVLD.Licenses.International_Licenses;
using DVLD.Licenses.Local_Licenses;
using DVLD.Tests;
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
    public partial class frmListLocalDrivingLicense : Form
    {
        public frmListLocalDrivingLicense()
        {
            InitializeComponent();
        }
        DataTable _dtLocalDrivingLicense;

        private void _LoadLocalDrivingLicenseList()
        {
            _dtLocalDrivingLicense = clsLocalDrivingLicenseApplication.GetAllLocalDrivngLicense();
            dgvLocalDrivingLicense.DataSource = _dtLocalDrivingLicense;
            if (_dtLocalDrivingLicense.Rows.Count > 0)
            {
                dgvLocalDrivingLicense.Columns[0].HeaderText = "L.D.L AppID";
                dgvLocalDrivingLicense.Columns[0].Width = 80;

                dgvLocalDrivingLicense.Columns[1].HeaderText = "Driving Class";
                dgvLocalDrivingLicense.Columns[1].Width = 120;

                dgvLocalDrivingLicense.Columns[2].HeaderText = "No Number";
                dgvLocalDrivingLicense.Columns[2].Width = 100;

                dgvLocalDrivingLicense.Columns[3].HeaderText = "Full Name";
                dgvLocalDrivingLicense.Columns[3].Width = 300;

                dgvLocalDrivingLicense.Columns[4].HeaderText = "Application Date";
                dgvLocalDrivingLicense.Columns[4].Width = 120;

                dgvLocalDrivingLicense.Columns[5].HeaderText = "Passed Test";
                dgvLocalDrivingLicense.Columns[5].Width = 120;

                dgvLocalDrivingLicense.Columns[6].HeaderText = "Status";
                dgvLocalDrivingLicense.Columns[6].Width = 120;
            }

        }


        private void frmListLocalDrivingLicense_Load(object sender, EventArgs e)
        {
            _LoadLocalDrivingLicenseList();
        }

        private void ApplyFilter()
        {
            string columnName = "";

            switch (cmbFilter.Text)
            {
                case "L.D.L AppID":
                    columnName = "LocalDrivingLicenseApplicationID";
                    break;

                case "National No":
                    columnName = "NationalNo";
                    break;

                case "Full Name":
                    columnName = "FullName";
                    break;

                case "Status":
                    columnName = "AppStatus";
                    break;
            }

            if (string.IsNullOrEmpty(cmbFilter.Text) || cmbFilter.Text == "None")
            {
                _dtLocalDrivingLicense.DefaultView.RowFilter = "";
                lblCount.Text = dgvLocalDrivingLicense.Rows.Count.ToString();
                return;
            }

            if (cmbFilter.Text == "Status")
            {
                if (string.IsNullOrEmpty(cmbFilterStatus.Text))
                {
                    _dtLocalDrivingLicense.DefaultView.RowFilter = "";
                }
                else
                {
                    string selectedStatus = cmbFilterStatus.Text.Trim().Replace("'", "''");
                    _dtLocalDrivingLicense.DefaultView.RowFilter = string.Format("[{0}] = '{1}'", columnName, selectedStatus);
                }
            }
            else if (string.IsNullOrEmpty(txtFilterValue.Text))
            {
                _dtLocalDrivingLicense.DefaultView.RowFilter = "";
            }
            else if (columnName == "LocalDrivingLicenseApplicationID")
            {
                _dtLocalDrivingLicense.DefaultView.RowFilter = string.Format("[{0}]={1}", columnName, txtFilterValue.Text);
            }
            else
            {
                string filterText = txtFilterValue.Text.Trim().Replace("'", "''");
                _dtLocalDrivingLicense.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", columnName, filterText);
            }

            lblCount.Text = dgvLocalDrivingLicense.Rows.Count.ToString();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFilter.Text == "Status")
            {
                txtFilterValue.Visible = false;
                cmbFilterStatus.Visible = true;
                cmbFilterStatus.Focus();
            }
            else
            {
                txtFilterValue.Visible = (cmbFilter.Text != "None");
                cmbFilterStatus.Visible = false;
                txtFilterValue.Enabled = (cmbFilter.Text != "None");

                if (cmbFilter.Text == "None")
                {
                    txtFilterValue.Text = "";
                }
                else
                {
                    txtFilterValue.Text = "";
                    txtFilterValue.Focus();
                }
            }

            ApplyFilter();
        }

        private void cmbFilterStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

      

    

     
        private void visionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = (int)dgvLocalDrivingLicense.CurrentRow.Cells[0].Value;
            frmListTestAppointment TestAppointment = new frmListTestAppointment(id, clsTestType.enTestType.VisionTest);
            TestAppointment.ShowDialog();
        }

       



        

        private void showApplicationDetailsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int LocalDrivingApplicationID = (int)dgvLocalDrivingLicense.CurrentRow.Cells[0].Value;
            frmLocalDrivingLicenseApplicationInfo frm = new frmLocalDrivingLicenseApplicationInfo(LocalDrivingApplicationID);
            frm.ShowDialog();
            _LoadLocalDrivingLicenseList();
        }

        private void editApplicationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicense AddUpdate = new frmAddUpdateLocalDrivingLicense
               (
              (int)dgvLocalDrivingLicense.CurrentRow.Cells[0].Value
               );
            AddUpdate.ShowDialog();
            _LoadLocalDrivingLicenseList();
        }

        private void deleteApplicationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int id = (int)dgvLocalDrivingLicense.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApplication app =
    clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseID(id);
            DialogResult result = MessageBox.Show("are Sure To Delete This Application", "Qustion", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                if (app.Delete())
                {
                    MessageBox.Show("Delete Succssfly", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
                else
                {
                    MessageBox.Show("Delete is not Succssfly", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void cancleApplicationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int id = (int)dgvLocalDrivingLicense.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApplication app =
    clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseID(id);
            DialogResult result = MessageBox.Show("are you Sure To Cancle This Application", "Alter", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                app.Cancle();
                _LoadLocalDrivingLicenseList();
            }
        }

        private void visionTestToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int id = (int)dgvLocalDrivingLicense.CurrentRow.Cells[0].Value;
            frmListTestAppointment TestAppointment = new frmListTestAppointment(id, clsTestType.enTestType.VisionTest);
            TestAppointment.ShowDialog();
            _LoadLocalDrivingLicenseList();
        }

        private void cmsApplication(object sender, CancelEventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicense.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseID(LocalDrivingLicenseApplicationID);

            bool PassedVision = _LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.VisionTest);
            bool PassedWritten = _LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.WrittenTest);
            bool PassedStreet = _LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.StreetTest);
            sechduleTestsToolStripMenuItem1.Enabled = (!PassedVision || !PassedWritten || !PassedStreet) && (_LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New);
            if (sechduleTestsToolStripMenuItem1.Enabled)
            {
                visionTestToolStripMenuItem1.Enabled = !PassedVision;

                writtenTestToolStripMenuItem1.Enabled =
                    PassedVision && !PassedWritten;

                streetTestToolStripMenuItem1.Enabled =
                    PassedWritten && !PassedStreet;
            }
            if (_LocalDrivingLicenseApplication.PassedAllTests())
            {
                sechduleTestsToolStripMenuItem1.Enabled = false;
                issueDrivingLicenseFirstTimeToolStripMenuItem1.Enabled = true;

            }
            if(_LocalDrivingLicenseApplication.GetActiveLicenseID() != -1)
            {
                issueDrivingLicenseFirstTimeToolStripMenuItem1.Enabled = false;
            deleteApplicationToolStripMenuItem1.Enabled = false;
                editApplicationToolStripMenuItem1.Enabled = false;
                cancleApplicationToolStripMenuItem1.Enabled = false;
                issueDrivingLicenseFirstTimeToolStripMenuItem1 .Enabled = false;

                showPersonLicenseHistoryToolStripMenuItem1 .Enabled = true;
                showLicenseToolStripMenuItem1.Enabled = true;


            }
        }

        private void writtenTestToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int id = (int)dgvLocalDrivingLicense.CurrentRow.Cells[0].Value;
            frmListTestAppointment TestAppointment = new frmListTestAppointment(id, clsTestType.enTestType.WrittenTest);
            TestAppointment.ShowDialog();
            _LoadLocalDrivingLicenseList();
        }

        private void streetTestToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int id = (int)dgvLocalDrivingLicense.CurrentRow.Cells[0].Value;
            frmListTestAppointment TestAppointment = new frmListTestAppointment(id, clsTestType.enTestType.StreetTest);
            TestAppointment.ShowDialog();
            _LoadLocalDrivingLicenseList();
        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int _LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicense.CurrentRow.Cells[0].Value;

            frmIssueDriverLicenseForFirstTIme frm = new frmIssueDriverLicenseForFirstTIme(_LocalDrivingLicenseApplicationID);
            frm.ShowDialog();
        }

        private void showLicenseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int _LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicense.CurrentRow.Cells[0].Value;

            clsLocalDrivingLicenseApplication _LocalDrivingApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseID(_LocalDrivingLicenseApplicationID);
            int LicenseID=_LocalDrivingApplication.GetActiveLicenseID();
            frmShowLicenseInfo LicenseInfo=new frmShowLicenseInfo(LicenseID);
            LicenseInfo.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int _LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicense.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApplication _LocalDrivingApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseID(_LocalDrivingLicenseApplicationID);

            frmShowPersonLicenseHistory LicenseHistory = new frmShowPersonLicenseHistory(_LocalDrivingApplication.ApplicationPersonID);
            LicenseHistory.ShowDialog();
        }
    }
}

