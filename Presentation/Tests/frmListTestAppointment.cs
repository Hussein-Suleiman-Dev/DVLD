using BusinessLayer;
using BusinessLayer.Tests;
using DVLD.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Tests
{
    public partial class frmListTestAppointment : Form
    {

        int _LocalDrivingLicenseApplicationID = -1;
        clsTestType.enTestType _TestType = clsTestType.enTestType.VisionTest;
        //  int TestAppointments = -1;
        DataTable _ListAppointment;
        public frmListTestAppointment(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestType)
        {
            InitializeComponent();

            _TestType = TestType;
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;





        }
        private void _LoadListTestAppointment()
        {

            _ListAppointment = clsTestAppointment.GetApplicationTestAppointmentsPerTestType(_LocalDrivingLicenseApplicationID, _TestType);
            dgvListTestAppointment.DataSource = _ListAppointment;
        }
        private void frmListTestAppointment_Load(object sender, EventArgs e)
        {
            _LoadTestTypeImageAndTitle();
            ctrlDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingAppID(_LocalDrivingLicenseApplicationID);
            _LoadListTestAppointment();
        }
        private void _LoadTestTypeImageAndTitle()
        {
            switch (_TestType)
            {

                case clsTestType.enTestType.VisionTest:
                    {
                        lblTitle.Text = "Vision Test Appointments";
                        this.Text = lblTitle.Text;
                        pbTestTypeImage.Image = Resources.Vision_512;
                        break;
                    }

                case clsTestType.enTestType.WrittenTest:
                    {
                        lblTitle.Text = "Written Test Appointments";
                        this.Text = lblTitle.Text;
                        pbTestTypeImage.Image = Resources.Written_Test_512;
                        break;
                    }
                case clsTestType.enTestType.StreetTest:
                    {
                        lblTitle.Text = "Street Test Appointments";
                        this.Text = lblTitle.Text;
                        pbTestTypeImage.Image = Resources.driving_test_512;
                        break;
                    }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplication LocalDrivingLicense = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseID(_LocalDrivingLicenseApplicationID);
            if (LocalDrivingLicense.IsThereAnActiveScheduledTest(_TestType))
            {
                MessageBox.Show("Person Already have an active appointment for this test, You cannot add new appointment", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clsTest LastTest = LocalDrivingLicense.GetLastTestPerTestType(_TestType);

            if (LastTest == null)
            {
                frmScheduleTest frm1 = new frmScheduleTest(_LocalDrivingLicenseApplicationID, _TestType);
                frm1.ShowDialog();
                frmListTestAppointment_Load(null, null);
                return;
            }

            //if person already passed the test s/he cannot retak it.
            if (LastTest.TestResult == true)
            {
                MessageBox.Show("This person already passed this test before, you can only retake faild test", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmScheduleTest frm2 = new frmScheduleTest
                (LastTest.AppointmentInfo.LocalDrivingLicenseAppID, _TestType);
            frm2.ShowDialog();
            frmListTestAppointment_Load(null, null);
          

        }



    

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestAppointmentsID = (int)dgvListTestAppointment.CurrentRow.Cells[0].Value;
            frmScheduleTest Test = new frmScheduleTest(_LocalDrivingLicenseApplicationID, _TestType, TestAppointmentsID);
            Test.ShowDialog();
            frmListTestAppointment_Load(null, null);
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestAppointmentsID = (int)dgvListTestAppointment.CurrentRow.Cells[0].Value;
            frmTakeTest TakeTest = new frmTakeTest(TestAppointmentsID, _TestType);
            TakeTest.ShowDialog();
            frmListTestAppointment_Load(null, null);
        }
    }
}
