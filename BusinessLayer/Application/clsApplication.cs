using BusinessLayer.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;
using System.Xml.Serialization;
using DataAccessLayer;
using System.Runtime.CompilerServices;
namespace BusinessLayer
{
    public class clsApplication
    {


        public enum enMode { Add=0, Update=1 }
        public enum enApplicationType
        {
            NewDrivingLicense=1, RenewDrivingLicense=2, ReplaceLostDrivingLicense = 3,
            RepplaceDamagedDrivingLicense=4,ReleaseDatainDrivingLicense=5, NewInternationalDrivingLicense=6, RetakeTest=7
        }
        public enMode Mode = enMode.Add;
        public enum enApplicationStatus { New = 1, Cancelled = 2, Completed = 3 }
        public int ApplicationID { get; set; }
        public int ApplicationPersonID { get; set; }
        public clsPeople PersonInfo;
        private string _ApplicationFullName;

        public string ApplicationFullName
        {
            get { return clsPeople.Find(ApplicationPersonID).FullName; }
            set { _ApplicationFullName = value; }
        }
        public DateTime ApplicationDate { get; set; }
        public int ApplicationTypeID { get; set; }
        public clsApplicationTypes ApplicationInfo;
        public enApplicationStatus ApplicationStatus { get; set; }
        public string ApplicationStatusText { get 
            {
            switch(ApplicationStatus)
                {
                    case enApplicationStatus.New:
                        return "New";
                    case enApplicationStatus.Cancelled:
                        return "Cancelled";
                    case enApplicationStatus.Completed:
                        return "Completed";
                    default:
                        return "Unknown";
                }
            }
        }
        
        public DateTime LastStatusDate { get; set; }
        public float PaidFees { get; set; }
        public int CreateByUserID { get; set; }
        public clsUser CreateByUserInfo;


        protected clsApplication(ApplicatopnDTO App)
        {
            this.ApplicationID = App.ApplicationID;
            this.ApplicationPersonID = App.ApplicationPersonID;
            this.ApplicationDate = App.ApplicationDate;
            this.ApplicationTypeID = App.ApplicationTypeID;
            this.ApplicationStatus = (enApplicationStatus)App.ApplicationStatus;
            this.LastStatusDate = App.LastStatusDate;
            this.CreateByUserID = App.CreatedUserID;
            this.PaidFees = App.PaidFees;
            this.CreateByUserInfo = clsUser.Find(CreateByUserID);
            this.ApplicationInfo = clsApplicationTypes.Find(ApplicationTypeID);
            this.PersonInfo = clsPeople.Find(ApplicationPersonID);
            Mode = enMode.Update;
        }
        public clsApplication()
        {
            this.ApplicationID = 0;
            this.ApplicationPersonID = 0;
            this.ApplicationStatus = enApplicationStatus.New;
            this.ApplicationDate = DateTime.Now;
            this.ApplicationTypeID = 1;
            this.LastStatusDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreateByUserID = 0;
            Mode = enMode.Add;

        }

        private ApplicatopnDTO _ToDTO()
        {
            return new ApplicatopnDTO
            {
                ApplicationID = this.ApplicationID,
                ApplicationPersonID = this.ApplicationPersonID,
                ApplicationDate = this.ApplicationDate,
                ApplicationTypeID = this.ApplicationTypeID,
                ApplicationStatus = (int)this.ApplicationStatus,
                LastStatusDate = this.LastStatusDate,
                CreatedUserID = this.CreateByUserID,
                PaidFees = this.PaidFees
            };
        }

        public static clsApplication Find(int ApplicationID)
        {
            ApplicatopnDTO dto = clsApplicationData.GetById(ApplicationID);


                if (dto == null)
                return null;

            return new clsApplication(dto);
        }
        private bool _Add()
        {
            this.ApplicationID = clsApplicationData.Add(_ToDTO());

            if (ApplicationID != -1)
            {
                Mode = enMode.Update;
                return true;
            }

            return false;
        }
        private bool _Update()
        {
            return clsApplicationData.Update(this._ToDTO());
        }

        public static bool Delete(int ApplicationID)
        {
            return clsApplicationData.Delete(ApplicationID);
        }
        public virtual bool Delete()
        {
            return clsApplicationData.Delete(this.ApplicationID);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.Add:
                    return _Add();
                case enMode.Update:
                    return _Update();
                default:
                    return false;
            }
        }
        public bool Cancle()
        {
            return clsApplicationData.UpdateStatus(ApplicationID, (int)enApplicationStatus.Cancelled);


        }
        public bool Complete()
        {
            return clsApplicationData.UpdateStatus(ApplicationID, (int)enApplicationStatus.Completed);
        }
        public static bool IsApplicationExist(int PersonID)
        {
            return clsApplicationData.IsApplicationExists(PersonID);
            //check if Application exist for this person and return true or false
        }
        public static bool DoesPersonHaveActiveApplication(int PersonID, int ApplicationType)
        {
            return clsApplicationData.DoesPersonHaveActiveApplication(PersonID, ApplicationType);
            //check if person has active application of this type and return true or false
        }
        public  bool DoesPersonHaveActiveApplication(int ApplicationTypeID)
        {
            return clsApplicationData.DoesPersonHaveActiveApplication(this.ApplicationID,ApplicationTypeID);
            //check Person Have Active Application of this type and return true or false
        }
        public static int GetActiveApplicationIDForLicenseClass(int PersonID, clsApplication.enApplicationType ApplicationTypeID, int LicenseClassID)
        {
            return clsApplicationData.GetActiveApplicationIDForLicenseClass(PersonID, (int)ApplicationTypeID, LicenseClassID);
            //check if Person Have The Application of this type and License Class and return the Application ID or -1 if not found
        }
        public static int GetActiveApplicationID(int PersonID, clsApplication.enApplicationType ApplicationTypeID)
        {
            return clsApplicationData.GetActiveApplicationID(PersonID, (int)ApplicationTypeID);
            //return the App id when is Active
        }
    }
}
