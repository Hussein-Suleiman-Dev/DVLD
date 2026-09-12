using BusinessLayer.User;
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
    public class clsInternationalLicense : clsApplication
    {//تعامل معها على انها طلب مستقل له application خاص فيها
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public clsDriver DriverInfo;
        public int InternationalLicenseID { set; get; }
        public int DriverID { set; get; }
        public int IssuedUsingLocalLicenseID { set; get; }
        public DateTime IssueDate { set; get; }
        public DateTime ExpirationDate { set; get; }
        public bool IsActive { set; get; }
        public int CreatedByUserIDInternational { set; get; }

        public clsInternationalLicense()

        {

            this.ApplicationTypeID = (int)clsApplication.enApplicationType.NewInternationalDrivingLicense;

            this.InternationalLicenseID = -1;
            this.DriverID = -1;
            this.IssuedUsingLocalLicenseID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;

            this.IsActive = true;


            Mode = enMode.AddNew;

        }
        public clsInternationalLicense(InternationalLicenseDTO internationalLicense)

        {


            this.InternationalLicenseID = internationalLicense.InternationalLicenseID;
            this.ApplicationID = internationalLicense.ApplicationID;
            this.DriverID = internationalLicense.DriverID;
            this.IssuedUsingLocalLicenseID = internationalLicense.IssuedUsingLocalLicenseID;
            this.IssueDate = internationalLicense.IssueDate;
            this.ExpirationDate = internationalLicense.ExpirationDate;
            this.IsActive = internationalLicense.IsActive;
            this.CreatedByUserIDInternational = internationalLicense.CreatedByUserID;

            this.DriverInfo = clsDriver.FindByDriverID(this.DriverID);

            Mode = enMode.Update;
        }

        public clsInternationalLicense(ApplicatopnDTO app, InternationalLicenseDTO internationalLicense) : base(app)

        {


            this.InternationalLicenseID = internationalLicense.InternationalLicenseID;
            this.ApplicationID = internationalLicense.ApplicationID;
            this.DriverID = internationalLicense.DriverID;
            this.IssuedUsingLocalLicenseID = internationalLicense.IssuedUsingLocalLicenseID;
            this.IssueDate = internationalLicense.IssueDate;
            this.ExpirationDate = internationalLicense.ExpirationDate;
            this.IsActive = internationalLicense.IsActive;
            this.CreatedByUserIDInternational = internationalLicense.CreatedByUserID;

            this.DriverInfo = clsDriver.FindByDriverID(this.DriverID);

            Mode = enMode.Update;
        }

        public InternationalLicenseDTO _TODTO()
        {
            return new InternationalLicenseDTO
            {
                ApplicationID = this.ApplicationID,
                DriverID = this.DriverID,
                InternationalLicenseID = this.InternationalLicenseID,
                IssuedUsingLocalLicenseID = this.IssuedUsingLocalLicenseID,
                IssueDate = this.IssueDate,
                ExpirationDate = this.ExpirationDate,
                IsActive = this.IsActive,
                CreatedByUserID = this.CreatedByUserIDInternational,
            };
        }
        private bool _Add()
        {
            this.InternationalLicenseID = clsInternationalLicenseData.AddNewInternationalLicense(_TODTO());
            return this.InternationalLicenseID != -1;
        }
        private bool _Update()
        {
            return clsInternationalLicenseData.UpdateInternationalLicense(_TODTO());

        }

        public static clsInternationalLicense FindInternationalLicenseByID(int InternationalId)
        {

            clsInternationalLicense InternationalLicnese = new clsInternationalLicense();
            InternationalLicenseDTO dto = clsInternationalLicenseData.GetInternationalLicenseInfoByID(InternationalId);
            if (dto != null)
            {
              
                ApplicatopnDTO applicatopnDTO = clsApplicationData.GetById(dto.ApplicationID);
                return new clsInternationalLicense(applicatopnDTO, dto);

            }
            return null;
        }
        public static DataTable GetAllInternationalLicenses()
        {
            return clsInternationalLicenseData.GetAllInternationalLicenses();

        }
        public bool Save()
        {

            //Because of inheritance first we call the save method in the base class,
            //it will take care of adding all information to the application table.
            base.Mode = (clsApplication.enMode)Mode;
            if (!base.Save())
                return false;

            switch (Mode)
            {
                case enMode.AddNew:
                    if (_Add())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _Update();

            }

            return false;
        }
        public static int GetActiveInternationalLicenseIDByDriverID(int DriverID)
        {

            return clsInternationalLicenseData.GetActiveInternationalLicenseIDByDriverID(DriverID);

        }
        public static DataTable GetDriverInternationalLicenses(int DriverID)
        {
            return clsInternationalLicenseData.GetDriverInternationalLicenses(DriverID);
        }
    }
}