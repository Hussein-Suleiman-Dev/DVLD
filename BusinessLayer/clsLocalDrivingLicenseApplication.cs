using BusinessLayer.Tests;
using DataAccessLayer;
using Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
namespace BusinessLayer
{
    public class clsLocalDrivingLicenseApplication:clsApplication
    {
    public int LocalDrivingLicenseId { get; set; }
       public clsLicenseClass ClassInfo;
      public  int LicenseClassID { get; set; }
      public  string PersonFullName
            { 
            get { return base.PersonInfo.FullName; } 
            }

        public enum enModeApp{Add=0,Update=1}
      public   enModeApp Mode= enModeApp.Add;

        public static DataTable GetAllLocalDrivngLicense()
        {
            return clsLocalDrivingApplicationData.GetAllLocalDrivingApplication();
        }


        public clsLocalDrivingLicenseApplication()
        {
            LocalDrivingLicenseId = -1;
       
            LicenseClassID = -1;
            Mode= enModeApp.Add;

        }
        private clsLocalDrivingLicenseApplication(LocalDrivingApplicationDTO localDrivingApp)
        {
            LocalDrivingLicenseId = localDrivingApp.LocalDrivingIdApp;
            LicenseClassID = localDrivingApp.LicenseClassID;
            ClassInfo = clsLicenseClass.Find(LicenseClassID);
            Mode = enModeApp.Update;
        }
       protected clsLocalDrivingLicenseApplication(ApplicatopnDTO app,LocalDrivingApplicationDTO localDrivingApp): base(app) 
        {
            LocalDrivingLicenseId = localDrivingApp.LocalDrivingIdApp;
            LicenseClassID = localDrivingApp.LicenseClassID;
            ClassInfo = clsLicenseClass.Find(LicenseClassID);
            Mode = enModeApp.Update;
        }

        private LocalDrivingApplicationDTO _ToDTO()
        {
            LocalDrivingApplicationDTO localDrivingApp = new LocalDrivingApplicationDTO();
            localDrivingApp.LocalDrivingIdApp = this.LocalDrivingLicenseId;
            localDrivingApp.ApplicationId = this.ApplicationID;
            localDrivingApp.LicenseClassID = this.LicenseClassID;
            return localDrivingApp;
        }

        private bool _Add()
        {
           this.LocalDrivingLicenseId= clsLocalDrivingApplicationData.AddLocalDrivingApplication(_ToDTO());
            return (this.LocalDrivingLicenseId != -1);
        }

        private bool _Update()
        {
            return clsLocalDrivingApplicationData.UpdateLocalDrivingApplication(_ToDTO());
        }

        public bool Save()
        {



            base.Mode = (clsApplication.enMode)Mode;
            if(!base.Save())return false;

            switch(Mode)
            {
                case enModeApp.Add:
                    return _Add();
                case enModeApp.Update:
                    return _Update();
            }

            return false;
        }

        public static DataTable GetAllLocalDrivingApplication()
        { 
        return clsLocalDrivingApplicationData.GetAllLocalDrivingApplication();
        }

        public static clsLocalDrivingLicenseApplication FindByLocalDrivingLicenseID(int LocalDrivingLicenseId)
        {
            var localDrivingApp = clsLocalDrivingApplicationData.GetByLocalDrivingAppID(LocalDrivingLicenseId);
         
            if (localDrivingApp != null)
            {
           ApplicatopnDTO app=     clsApplicationData.GetById(localDrivingApp.ApplicationId);
                return new clsLocalDrivingLicenseApplication(app, localDrivingApp);
            }
            else
            {
                return null;
            }
        }
        public static clsLocalDrivingLicenseApplication FindByApplicationID(int ApplicationID)
        { 
        var localDrivingApp = clsLocalDrivingApplicationData.GetByAppID(ApplicationID);
            if (localDrivingApp != null)
            {
                return new clsLocalDrivingLicenseApplication(localDrivingApp);
            }
            else
            {
                return null;
            }
        }
        public override bool Delete()
        {



            if (!clsLocalDrivingApplicationData.DeleteLocalDrivingApplication(this.LocalDrivingLicenseId))
            { 
            return false;
            }
            if (!base.Delete())
            {
                return false;
            }
            return true;
        }

        public bool DoesAttendTestType(clsTestType.enTestType TestType)
        {
            return clsLocalDrivingApplicationData.DoseAttendedTestType(this.LocalDrivingLicenseId, (int)TestType) != -1;

            //من اجل تحديد مود ال الاختبار اذا كان جديد او اذا كان عنده اي اختبار سواء ناجح او راسب يعيد اذا عنده اي اختبار من هذا نوع

        }

public byte TotalTrailPerTest(clsTestType.enTestType TestType)
        {
            return clsLocalDrivingApplicationData.TotalTrialsPerTest(this.LocalDrivingLicenseId, (int)TestType);
            //عرض عدد محاولات في اعادة الاختبار
        }

        public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)

        {
            //يتحقق اذا كان موجود موعد فعال
            return clsLocalDrivingApplicationData.IsThereAnActiveScheduledTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool IsThereAnActiveScheduledTest(clsTestType.enTestType TestTypeID)

        {

            return clsLocalDrivingApplicationData.IsThereAnActiveScheduledTest(this.LocalDrivingLicenseId, (int)TestTypeID);

            //يتحقق اذا كان موجود موعد فعال
        }

        public  bool DoesPassTestType(clsTestType.enTestType Type)
        {
         return(clsLocalDrivingApplicationData.DoesPassTestType(this.LocalDrivingLicenseId, (int)Type));
            
        }
        public static bool DoesPassTestType(int LDLID,clsTestType.enTestType Type)
        {
            return clsLocalDrivingApplicationData.DoesPassTestType(LDLID, (int)Type);
        }
        public clsTest GetLastTestPerTestType(clsTestType.enTestType TestTypeID)
        {
            return clsTest.FindLastTestPerPersonAndLicenseClass(this.ApplicationPersonID, this.LicenseClassID, (int)TestTypeID);
        }


        public  int IssueLicenseForFirstTime(string Notes, int CreatedByUserID)
        {

            int DriverID = -1;
            clsDriver _Driver = clsDriver.FindByPersonID(this.ApplicationPersonID);
            if (_Driver == null)
            {
                //Not Driver In System
                _Driver=new clsDriver();
                _Driver.PersonID = this.ApplicationPersonID;
                _Driver.CreatedByUserID = CreatedByUserID;
                if (_Driver.Save())
                {
                    DriverID = _Driver.DriverID;
                }
                else
                {
                    return -1;
                }

            }

            else 
            {
                //اذا كان موجود
                DriverID = _Driver.DriverID;
            }

            clsLicense License = new clsLicense();
           License.ApplicationID = this.ApplicationID;
            License.DriverID = DriverID;
            License.IssueDate = DateTime.Now;
            License.LicenseClass = this.LicenseClassID;
            License.Notes= Notes;
            License.CreatedByUserID = CreatedByUserID;
            License.ExpirationDate = DateTime.Now.AddYears(this.ClassInfo.DefaultValidityPeriod);
            License.IssueReason = clsLicense.enIssueReason.FirstTime;
            License.IsActive= true;
            License.PaidFees = this.ClassInfo.ClassFees;
            if (License.Save())
            {
                this.Complete();
                return License.LicenseID;
            }
            return -1;
        }

        public bool PassedAllTests()
        {
            return clsTest.PassedAllTests(this.LocalDrivingLicenseId);
        }

        public int GetActiveLicenseID()
        {
            return clsLicense.GetActiveLicenseIDByPersonID(this.ApplicationPersonID, this.LicenseClassID);
        }
        public byte GetPassedTestCount()
        {
            return clsTest.GetPassedTestCount(this.LocalDrivingLicenseId);
        }
    }
}
