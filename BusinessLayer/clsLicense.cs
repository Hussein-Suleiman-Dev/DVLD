using DataAccessLayer;
using Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsLicense
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public enum enIssueReason { FirstTime = 1, Renew = 2, DamagedReplacement = 3, LostReplacement = 4 };

        public clsDriver DriverInfo;
        public int LicenseID { set; get; }
        public int ApplicationID { set; get; }
        public int DriverID { set; get; }
        public int LicenseClass { set; get; }
        public clsLicenseClass LicenseClassIfo;
        public DateTime IssueDate { set; get; }
        public DateTime ExpirationDate { set; get; }
        public string Notes { set; get; }
        public float PaidFees { set; get; }
        public bool IsActive { set; get; }
        public enIssueReason IssueReason { set; get; }
        public bool IsDetain { get { return clsDetainedLicense.IsLicenseDetained(LicenseID); } }
        public string IssueReasonText
        {
            get
            {
                return GetIssueReasonText(this.IssueReason);
            }
        }
        public clsDetainedLicense DetainedInfo { set; get; }
        public int CreatedByUserID { set; get; }
        public bool IsDetained
        {
            get { return clsDetainedLicense.IsLicenseDetained(this.LicenseID); }
        }

        public clsLicense()

        {
            this.LicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.LicenseClass = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.Notes = "";
            this.PaidFees = 0;
            this.IsActive = true;
            this.IssueReason = enIssueReason.FirstTime;
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;

        }
        private clsLicense(LicenseDTO license)
        {
            this.LicenseID = license.LiceeneseID;
            this.ApplicationID = license.ApplicationID;
            this.DriverID = license.DriverID;
            this.LicenseClass = license.LicensesClassID;
            this.IssueDate = license.IssueDate;
            this.ExpirationDate = license.ExpirationDate;
            this.Notes = license.Notes;
            this.PaidFees = license.PaidFess;
            this.IsActive = license.IsActive;
            this.IssueReason = (enIssueReason)license.IssueReason;
            this.CreatedByUserID = license.CreatedByUserID;

            this.DriverInfo = clsDriver.FindByDriverID(this.DriverID);
            this.LicenseClassIfo = clsLicenseClass.Find(this.LicenseClass);
            this.DetainedInfo = clsDetainedLicense.FindByLicenseID(this.LicenseID);
            Mode = enMode.Update;
        }
        public LicenseDTO _toDTO()
        {
            return new LicenseDTO
            {
                LiceeneseID = this.LicenseID,
                ApplicationID = this.ApplicationID,
                DriverID = this.DriverID,
                LicensesClassID = this.LicenseClass,
                IssueDate = this.IssueDate,
                ExpirationDate = this.ExpirationDate,
                Notes = this.Notes,
                PaidFess = this.PaidFees,
                IsActive = this.IsActive,
                IssueReason = (byte)this.IssueReason,
                CreatedByUserID = this.CreatedByUserID
            };
        }

        private bool _AddLicense()
        {

            this.LicenseID = clsLicenseData.AddNewLicense(_toDTO());
            return this.LicenseID != -1;
        }

        private bool _Update()
        {
            return clsLicenseData.UpdateLicense(_toDTO());
        }

        public static clsLicense FindLicnese(int LicsenseID)
        {
            if (clsLicenseData.GetLicenseByID(LicsenseID) != null)
            {
             return   new clsLicense(clsLicenseData.GetLicenseByID(LicsenseID));
            }
            return null;
        }

        public static DataTable GetAllLicenses()
        {
            return clsLicenseData.GetAllLicenses();

        }

        public static bool IsLicenseExistByPersonID(int PersonID, int LicenseClassID)
        {
            return (GetActiveLicenseIDByPersonID(PersonID, LicenseClassID) != -1);




        }
        public static int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {

            return clsLicenseData.GetActiveLicenseIDByPersonID(PersonID, LicenseClassID);

        }

        public static DataTable GetDriverLicenses(int DriverID)
        {
            return clsLicenseData.GetDriverLicenses(DriverID);
        }

        public Boolean IsLicenseExpired()
        {
            return (this.ExpirationDate < DateTime.Now);
        }


        public bool DeactivateCurrentLicense()
        {
            return (clsLicenseData.DeactivateLicense(this.LicenseID));
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddLicense())
                        return true;
                    else { return false; }
                case enMode.Update:
                    return _Update();
            }
            return false;
        }


        public static string GetIssueReasonText(enIssueReason IssueReason)
        {

            switch (IssueReason)
            {
                case enIssueReason.FirstTime:
                    return "First Time";
                case enIssueReason.Renew:
                    return "Renew";
                case enIssueReason.DamagedReplacement:
                    return "Replacement for Damaged";
                case enIssueReason.LostReplacement:
                    return "Replacement for Lost";
                default:
                    return "First Time";
            }
        }
        //public int Detain(float FineFees, int CreatedByUserID)
        //{
        //    clsDetainedLicense detainedLicense = new clsDetainedLicense();
        //    detainedLicense.LicenseID = this.LicenseID;
        //    detainedLicense.DetainDate = DateTime.Now;
        //    detainedLicense.FineFees = Convert.ToSingle(FineFees);
        //    detainedLicense.CreatedByUserID = CreatedByUserID;

        //    if (!detainedLicense.Save())
        //    {

        //        return -1;
        //    }

        //    return detainedLicense.DetainID;

        //}




        //public bool ReleaseDetainedLicense(int ReleasedByUserID, ref int ApplicationID)
        //{

        //    //First Create Applicaiton 
        //    clsApplication Application = new clsApplication();

        //    Application.ApplicationPersonID = this.DriverInfo.PersonID;
        //    Application.ApplicationDate = DateTime.Now;
        //    Application.ApplicationTypeID = (int)clsApplication.enApplicationType.ReleaseDatainDrivingLicense;
        //    Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
        //    Application.LastStatusDate = DateTime.Now;
        //    Application.PaidFees = clsApplicationTypes.Find((int)clsApplication.enApplicationType.ReleaseDatainDrivingLicense).ApplicationTypeFess;
        //    Application.CreateByUserID = ReleasedByUserID;

        //    if (!Application.Save())
        //    {
        //        ApplicationID = -1;
        //        return false;
        //    }

        //    ApplicationID = Application.ApplicationID;


        //    return this.DetainedInfo.ReleaseDetainedLicense(ReleasedByUserID, Application.ApplicationID);

        //}



        public clsLicense RenewLicense(string Notes, int CreatedByUserID)
        {

            //First Create Applicaiton 
            clsApplication Application = new clsApplication();

            Application.ApplicationPersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID = (int)clsApplication.enApplicationType.RenewDrivingLicense;
            Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = clsApplicationTypes.Find((int)clsApplication.enApplicationType.RenewDrivingLicense).ApplicationTypeFess;
            Application.CreateByUserID = CreatedByUserID;

            if (!Application.Save())
            {
                return null;
            }

            clsLicense NewLicense = new clsLicense();

            NewLicense.ApplicationID = Application.ApplicationID;
            NewLicense.DriverID = this.DriverID;
            NewLicense.LicenseClass = this.LicenseClass;
            NewLicense.IssueDate = DateTime.Now;

            int DefaultValidityLength = this.LicenseClassIfo.DefaultValidityPeriod;

            NewLicense.ExpirationDate = DateTime.Now.AddYears(DefaultValidityLength);
            NewLicense.Notes = Notes;
            NewLicense.PaidFees = this.LicenseClassIfo.ClassFees;
            NewLicense.IsActive = true;
            NewLicense.IssueReason = clsLicense.enIssueReason.Renew;
            NewLicense.CreatedByUserID = CreatedByUserID;


            if (!NewLicense.Save())
            {
                return null;
            }

            //we need to deactivate the old License.
            DeactivateCurrentLicense();

            return NewLicense;
        }

        public clsLicense Replace(enIssueReason IssueReason, int CreatedByUserID)
        {


            //First Create Applicaiton 
            clsApplication Application = new clsApplication();

            Application.ApplicationPersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;

            Application.ApplicationTypeID = (IssueReason == enIssueReason.DamagedReplacement) ?
                (int)clsApplication.enApplicationType.RepplaceDamagedDrivingLicense :
                (int)clsApplication.enApplicationType.ReplaceLostDrivingLicense;

            Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = clsApplicationTypes.Find(Application.ApplicationTypeID).ApplicationTypeFess;
            Application.CreateByUserID = CreatedByUserID;

            if (!Application.Save())
            {
                return null;
            }

            clsLicense NewLicense = new clsLicense();

            NewLicense.ApplicationID = Application.ApplicationID;
            NewLicense.DriverID = this.DriverID;
            NewLicense.LicenseClass = this.LicenseClass;
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.ExpirationDate = this.ExpirationDate;
            NewLicense.Notes = this.Notes;
            NewLicense.PaidFees = 0;// no fees for the license because it's a replacement.
            NewLicense.IsActive = true;
            NewLicense.IssueReason = IssueReason;
            NewLicense.CreatedByUserID = CreatedByUserID;



            if (!NewLicense.Save())
            {
                return null;
            }

            //we need to deactivate the old License.
            DeactivateCurrentLicense();

            return NewLicense;
        }


        public int DetainLicense(float FineFees, int CreatByUserID)
        {
            clsDetainedLicense _detainedLicense = new clsDetainedLicense();
            _detainedLicense.LicenseID = this.LicenseID;
            _detainedLicense.DetainDate = DateTime.Now;
            _detainedLicense.CreatedByUserID = CreatedByUserID;
            _detainedLicense.FineFees = FineFees;
            if (_detainedLicense.Save())
            {
                return _detainedLicense.DetainID;
            }
            return -1;
        }
        public bool ReleaseDetainedLicense(int RelasedUserID, ref int ApplicationID)
        {//create new application type Release
            clsApplication application = new clsApplication();
            application.ApplicationPersonID = this.DriverInfo.PersonID;
            application.ApplicationDate = DateTime.Now;
            application.ApplicationTypeID = (int)clsApplication.enApplicationType.ReleaseDatainDrivingLicense;
            application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            application.LastStatusDate = DateTime.Now;
            application.CreateByUserID = RelasedUserID;
            application.PaidFees = clsApplicationTypes.Find((int)clsApplication.enApplicationType.ReleaseDatainDrivingLicense).ApplicationTypeFess;
            if (!application.Save())
            {
                ApplicationID = -1;
                return false;
            }
            ApplicationID = application.ApplicationID;
            return DetainedInfo.ReleaseDetainedLicense(RelasedUserID, ApplicationID);

        }

    }
}
