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

namespace DVLD.Tests.Control
{
    public partial class ctrlScheduleTest : UserControl
    {
        public ctrlScheduleTest()
        {
            InitializeComponent();
        }

        public enum enMode { Add = 0, Update = 1 }
        private enMode _Mode = enMode.Add;

        public enum enCreationMode { FirstTimeSchedual = 0, RetakeTestSchedual = 1 }
        private enCreationMode _CreationMode = enCreationMode.FirstTimeSchedual;

        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        private int _LocalDrivingLicenseApplicationID = -1;

        clsTestAppointment _TestAppointment;
        private int _TestAppointmentID = -1;

        private clsTestType.enTestType _TestType = clsTestType.enTestType.VisionTest;

        public clsTestType.enTestType TestType
        {
            get
            {
                return _TestType;
            }
            set
            {
                _TestType = value;
                switch (_TestType)
                {
                    case clsTestType.enTestType.VisionTest:
                        gbTestType.Text = "Vision Test";
                        pbTestTypeImage.Image = Resources.Vision_512;

                        break;
                    case clsTestType.enTestType.WrittenTest:
                        gbTestType.Text = "Written Test";
                        pbTestTypeImage.Image = Resources.Written_Test_512;
                        break;
                    case clsTestType.enTestType.StreetTest:
                        gbTestType.Text = "Street Test";
                        pbTestTypeImage.Image = Resources.Written_Test_512; break;

                }
            }
        }

        public void LoadInfo(int LocalDrivingLicenseApplicationID, int AppointmentID = -1)
        {
            if (AppointmentID == -1)
            {
                _Mode = enMode.Add;
            }
            else { _Mode = enMode.Update; }

            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestAppointmentID = AppointmentID;
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseID(_LocalDrivingLicenseApplicationID);
            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("Error: No Local Driving License Application with ID = " + _LocalDrivingLicenseApplicationID.ToString(),
                       "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return;
            }
            if (_LocalDrivingLicenseApplication.DoesAttendTestType(_TestType))
                _CreationMode = enCreationMode.RetakeTestSchedual;
            else
                _CreationMode = enCreationMode.FirstTimeSchedual;


            if (_CreationMode == enCreationMode.RetakeTestSchedual)
            {
                lblRetakeAppFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationType.RetakeTest).ApplicationTypeFess.ToString();
                gbRetakeTestInfo.Enabled = true;
                lblTitle.Text = "Sechdule Retake Test";
                lblRetakeTestAppID.Text = "0";


            }
            else
            {
                gbRetakeTestInfo.Enabled = false;
                lblRetakeAppFees.Text = "0";
                lblRetakeTestAppID.Text = "N/A";
                //Creation Mode =First Time
            }



            lblLocalDrivingLicenseAppID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseId.ToString();
            lblDrivingClass.Text = _LocalDrivingLicenseApplication.ClassInfo.LicenseClassName;
            lblFullName.Text = _LocalDrivingLicenseApplication.PersonFullName;
            lblTrial.Text = _LocalDrivingLicenseApplication.TotalTrailPerTest(_TestType).ToString();

            if (_Mode == enMode.Add)
            {
                lblFees.Text = clsTestType.Find(_TestType).Fees.ToString();//سعر كل امتحان
                dtpTestDate.MinDate = DateTime.Now;
                lblRetakeTestAppID.Text = "N/A";
                _TestAppointment = new clsTestAppointment();

              
            }
            else
            {

                if (!_LoadAppointmentData())
                    return;
            }

            //كمل في حالة كان موجود وبدو اعادة فحص 
            lblTotalFees.Text = ((Convert.ToSingle(lblFees.Text)) + Convert.ToSingle(lblRetakeAppFees.Text)).ToString();//في حالة امتحان مع اعادة
            if (!_HandleActiveTestAppointmentConstraint())
                return;

            if (!_HandleAppointmentLockedConstraint())
                return;

            if (!_HandlePrviousTestConstraint())
                return;



        }
        private bool _LoadAppointmentData()
        {//في حال كان موجود الموعد ومود ليس انشاء
            _TestAppointment = clsTestAppointment.Find(_TestAppointmentID);
            if (_TestAppointment == null)
            {
                MessageBox.Show("Error: No Appointment with ID = " + _TestAppointmentID.ToString(),
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return false;

            }

            lblFees.Text = _TestAppointment.PaidFees.ToString();

            if (DateTime.Compare(DateTime.Now, _TestAppointment.AppointmentDate) < 0)
            {

                dtpTestDate.MinDate = DateTime.Now;
            }
            else { dtpTestDate.MinDate = _TestAppointment.AppointmentDate; }

            if (_TestAppointment.RetakeTestApplicationID == -1)
            {
                lblRetakeAppFees.Text = "0";
                lblRetakeTestAppID.Text = "N/A";
                //New Appointment
            }

            else
            {
                lblFees.Text = _TestAppointment.ApplicationInfo.PaidFees.ToString();
                gbRetakeTestInfo.Enabled = true;
                lblTitle.Text = "Schedule Retake Test";
                lblRetakeTestAppID.Text = _TestAppointment.RetakeTestApplicationID.ToString();
                //Retake Appointment
            }
            return true;
        }
        private bool _HandleActiveTestAppointmentConstraint()
        {
            if (_Mode == enMode.Add && clsLocalDrivingLicenseApplication.IsThereAnActiveScheduledTest(_LocalDrivingLicenseApplicationID, _TestType))
            {

                lblUserMessage.Text = "Person Already have an active appointment for this test";
                btnSave.Enabled = false;
                dtpTestDate.Enabled = false;
                return false;
            }
            return true;
        }
        private bool _HandleAppointmentLockedConstraint()
        {
            //if appointment is locked that means the person already sat for this test
            //we cannot update locked appointment
            if (_TestAppointment.IsLocked)
            {
                lblUserMessage.Visible = true;
                lblUserMessage.Text = "Person already sat for the test, appointment loacked.";
                dtpTestDate.Enabled = false;
                btnSave.Enabled = false;
                return false;

            }
            else
                lblUserMessage.Visible = false;

            return true;
        }

        private bool _HandlePrviousTestConstraint()
        {
            switch (_TestType)
            {
                case clsTestType.enTestType.VisionTest:
                    lblUserMessage.Visible = false;

                    break;
                case clsTestType.enTestType.WrittenTest:
                    if (!_LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.VisionTest))
                    {
                        lblUserMessage.Text = "Cannot Sechule, Vision Test should be passed first";
                        lblUserMessage.Visible = true;
                        btnSave.Enabled = false;
                        dtpTestDate.Enabled = false;
                        return false;
                     }
                    else
                    {
                        lblUserMessage.Visible = false;
                        btnSave.Enabled = true;
                        dtpTestDate.Enabled = true;
                    }


                    return true;
                case clsTestType.enTestType.StreetTest:

                    //Street Test, you cannot sechdule it before person passes the written test.
                    //we check if pass Written 2.
                    if (!_LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.WrittenTest))
                    {
                        lblUserMessage.Text = "Cannot Sechule, Written Test should be passed first";
                        lblUserMessage.Visible = true;
                        btnSave.Enabled = false;
                        dtpTestDate.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblUserMessage.Visible = false;
                        btnSave.Enabled = true;
                        dtpTestDate.Enabled = true;
                    }


                    return true;

            }
            return true;

        }

        private bool _HandleRetakeApplication()
        {
            if (_Mode == enMode.Add && _CreationMode == enCreationMode.RetakeTestSchedual)
            {

                clsApplication Application = new clsApplication();

                Application.ApplicationPersonID = _LocalDrivingLicenseApplication.ApplicationPersonID;

                Application.ApplicationDate = DateTime.Now;
                Application.ApplicationTypeID = clsApplicationTypes.Find((int)clsApplication.enApplicationType.RetakeTest).ApplicationTypeID;
                Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
                Application.LastStatusDate= DateTime.Now;
                Application.PaidFees = clsApplicationTypes.Find((int)clsApplication.enApplicationType.RetakeTest).ApplicationTypeFess;
                Application.CreateByUserID = clsGlobal.CurrentUser.UserID;
                if (!Application.Save())
                {
                    _TestAppointment.RetakeTestApplicationID = -1;
                    MessageBox.Show("Faild to Create application", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;

                }
                _TestAppointment.RetakeTestApplicationID = Application.ApplicationID;


            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!_HandleRetakeApplication())
                return;

            _TestAppointment.TestTypeID = _TestType;
            _TestAppointment.LocalDrivingLicenseAppID = _LocalDrivingLicenseApplication.LocalDrivingLicenseId;
            _TestAppointment.AppointmentDate = dtpTestDate.Value;
            _TestAppointment.PaidFees = Convert.ToSingle(lblFees.Text);
            _TestAppointment.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (_TestAppointment.Save())
            {
                _Mode = enMode.Update;
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }

  }
    