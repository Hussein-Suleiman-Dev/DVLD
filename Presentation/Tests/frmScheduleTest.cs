using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer;
namespace DVLD.Tests
{
    public partial class frmScheduleTest : Form
    {
     
        int _LocalDrivingLicsnseApplicationID = -1;
        int _TestAppointmentsID = -1;
        clsTestType.enTestType _TestType = clsTestType.enTestType.VisionTest;
        public frmScheduleTest(int LocalDrivngLicenseApplicationID,clsTestType.enTestType TestType,int TestAppointmentID=-1)
        {
            InitializeComponent();
            _LocalDrivingLicsnseApplicationID = LocalDrivngLicenseApplicationID;
            _TestType = TestType;
            _TestAppointmentsID = TestAppointmentID;
        }

        private void frmScheduleTest_Load(object sender, EventArgs e)
        {
            crlScheduleTest1.TestType= _TestType;
            crlScheduleTest1.LoadInfo(_LocalDrivingLicsnseApplicationID, _TestAppointmentsID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void crlScheduleTest1_Load(object sender, EventArgs e)
        {

        }
    }
}
