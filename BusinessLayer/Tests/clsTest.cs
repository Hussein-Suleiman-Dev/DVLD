using BusinessLayer.User;
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
    public class clsTest
    {
        public enum enMode {Add=1,Update=2 }
        enMode _Mode= enMode.Add;
        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }
       public bool TestResult { get; set; }
        public string Notes { get; set; }
        public int CreatedByUserID { get; set; }
      
        public clsTestAppointment AppointmentInfo { get; set; }


        public clsTest()
        {
            TestID = 1;
            TestAppointmentID = -1;
            TestResult = false;
            Notes = "";
            CreatedByUserID= -1;
            AppointmentInfo = new clsTestAppointment();
            _Mode= enMode.Add;
        }

        private clsTest(TestDTO Test)
        { 
        this.TestID = Test.TestID;
            this.TestResult = Test.TestResult;
            this.Notes = Test.Notes;
            this.TestAppointmentID = Test.TestAppointmentID;
            this.CreatedByUserID=Test.CreatedByUserID;
            this.AppointmentInfo = clsTestAppointment.Find(TestAppointmentID);
            _Mode= enMode.Update;


        }

        public TestDTO _ToDto()
        {
            return new TestDTO 
            {
            TestID=this.TestID,
            TestAppointmentID=this.TestAppointmentID,
            TestResult=this.TestResult,
            Notes=this.Notes,
            CreatedByUserID=this.CreatedByUserID,
            };
        }

        private bool _AddNewTest()
        {
           this.TestID= clsTestData.AddNewTest(_ToDto());
            return this.TestID != -1;
        }

        private bool _UpdateTest()
        {
            return clsTestData.UpdateTest(_ToDto());
        }
        public static byte GetPassedTestCount(int LocalDrvingLicenseApplication)
        {
            return clsTestData.GetPassedTestCount(LocalDrvingLicenseApplication);
        }
       
        public static clsTest FindLastTestPerPersonAndLicenseClass(int PersonID, int TestTypeID, int LicenseClass)
        {
     var item= clsTestData.GetLastTestByPersonAndTestTypeAndLicenseClass(PersonID, TestTypeID, LicenseClass);
            if (item != null)
            { 
            return new clsTest(item);
            }
            return null;
        }

        public static clsTest FindByTestID(int TestID)
        {
            return new clsTest(clsTestData.GetByID(TestID));
        }

        public static DataTable GetAllTests()
        { 
        return clsTestData.GetAllTests();
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.Add:
                    if (_AddNewTest())
                        return true;
                    else { return false; }

                case enMode.Update:
                    return _UpdateTest();
            }
            return false;

        }

        public static bool PassedAllTests(int LocalDrivingLicenseApplication)
        {
            return GetPassedTestCount(LocalDrivingLicenseApplication) == 3;
        }


    }
}
