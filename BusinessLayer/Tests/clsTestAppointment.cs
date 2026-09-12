using DataAccessLayer;
using Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BusinessLayer.Tests
{
    public class clsTestAppointment
    {
        public int TestAppointmentID { get; set; }
        public clsTestType.enTestType TestTypeID { get; set; }
        public int LocalDrivingLicenseAppID { get; set; }
        public int TestID { get { return _GetTestID(); } }
        public DateTime AppointmentDate { get; set; }
        public float PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsLocked { get; set; }
        public int RetakeTestApplicationID { get; set; }
       public clsApplication ApplicationInfo;

        enum enMode { Add = 1, Update = 2 }
        enMode _Mode = enMode.Add;

        public clsTestAppointment()
        {
            TestAppointmentID = -1;
            TestTypeID = clsTestType.enTestType.VisionTest;
            LocalDrivingLicenseAppID = -1;
            AppointmentDate = DateTime.Now;
            PaidFees = 0;
            CreatedByUserID = -1;
            IsLocked = false;
            RetakeTestApplicationID = -1;
            _Mode = enMode.Add;
        }
        private clsTestAppointment(TestAppointmentDTO testAppointmentDTO)
        {
            TestAppointmentID = testAppointmentDTO.TestAppointmentID;
            TestTypeID = (clsTestType.enTestType)testAppointmentDTO.TestTypeID;
            LocalDrivingLicenseAppID = testAppointmentDTO.LocalDrivingLicenseAppID;
            AppointmentDate = testAppointmentDTO.AppointmentDate;
            PaidFees = testAppointmentDTO.PaidFees;
            CreatedByUserID = testAppointmentDTO.CreatedByUserID;
            IsLocked = testAppointmentDTO.IsLocked;
            RetakeTestApplicationID = testAppointmentDTO.RetakeTestApplicationID;
        //    ApplicationInfo = clsApplication.Find();
        _Mode = enMode.Update;
        }

        private TestAppointmentDTO _ToDTO()
        {
            return new TestAppointmentDTO()
            {
                TestAppointmentID = this.TestAppointmentID,
                TestTypeID = (int)this.TestTypeID,
                LocalDrivingLicenseAppID = this.LocalDrivingLicenseAppID,
                AppointmentDate = this.AppointmentDate,
                PaidFees = this.PaidFees,
                CreatedByUserID = this.CreatedByUserID,
                IsLocked = this.IsLocked,
                RetakeTestApplicationID = this.RetakeTestApplicationID
            };
        }

        public static clsTestAppointment Find(int TestAppointmentID)
        {
            TestAppointmentDTO testAppointmentDTO =
                clsTestAppointmentsData.GetTestAppointmentInfoByID(TestAppointmentID);

            if (testAppointmentDTO != null)
            {
                return new clsTestAppointment(testAppointmentDTO);
            }

            return null;
        }

        private bool _Add()
        {
            this.TestAppointmentID =
                clsTestAppointmentsData.AddTestAppointment(_ToDTO());

            return this.TestAppointmentID != -1;
        }
        private bool _Update()
        {
            return clsTestAppointmentsData.UpdateTestAppointment(_ToDTO());
        }
        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.Add:
                    if (_Add())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    break;
                case enMode.Update:
                    if (_Update())
                        return true;
                    break;
            }
            return false;

        }


        public static clsTestAppointment GetLastTestAppointment(int TestTypeID,int LocalDrivingLicenseApplication)
        {
            return  new clsTestAppointment( clsTestAppointmentsData.GetLastTestAppointment(TestTypeID, LocalDrivingLicenseApplication));

        }

        public static DataTable GetAllTestAppointment()
        { 
        return clsTestAppointmentsData.GetAllTestAppointment();
        }

        public DataTable GetApplicationTestAppointmentsPerTestType(clsTestType.enTestType TestTypeID)
        {
            return clsTestAppointmentsData.GetApplicationTestAppointmentsPerTestType(this.LocalDrivingLicenseAppID, (int)TestTypeID);

        }

        public static DataTable GetApplicationTestAppointmentsPerTestType(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            return clsTestAppointmentsData.GetApplicationTestAppointmentsPerTestType(LocalDrivingLicenseApplicationID, (int)TestTypeID);

        }
        private int _GetTestID()
        {
            return clsTestAppointmentsData.GetTestID(TestAppointmentID);
        }


    }
}