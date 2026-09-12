using BusinessLayer.User;
using DataAccessLayer;
using Shared;
using Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BusinessLayer
{
    public class clsDetainedLicense
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;


        public int DetainID { set; get; }
        public int LicenseID { set; get; }
        public DateTime DetainDate { set; get; }

        public float FineFees { set; get; }
        public int CreatedByUserID { set; get; }
        public clsUser CreatedByUserInfo { set; get; }
        public bool IsReleased { set; get; }
        public DateTime ReleaseDate { set; get; }
        public int ReleasedByUserID { set; get; }
        public clsUser ReleasedByUserInfo { set; get; }
        public int ReleaseApplicationID { set; get; }

        public clsDetainedLicense()

        {
            this.DetainID = -1;
            this.LicenseID = -1;
            this.DetainDate = DateTime.Now;
            this.FineFees = 0;
            this.CreatedByUserID = -1;
            this.IsReleased = false;
            this.ReleaseDate = DateTime.MaxValue;
            this.ReleasedByUserID = 0;
            this.ReleaseApplicationID = -1;



            Mode = enMode.AddNew;

        }

        public clsDetainedLicense(DetainedLicenseDTO detainedLicense)

        {
            this.DetainID = detainedLicense.DetainID;
            this.LicenseID = detainedLicense.LicenseID;
            this.DetainDate = detainedLicense.DetainDate;
            this.FineFees = detainedLicense.FineFees;
            this.CreatedByUserID = detainedLicense.CreatedByUserID;
            this.CreatedByUserInfo = clsUser.Find(this.CreatedByUserID);
            this.IsReleased = detainedLicense.IsReleased;
            this.ReleaseDate = detainedLicense.ReleaseDate;
            this.ReleasedByUserID = detainedLicense.ReleasedByUserID;
            this.ReleaseApplicationID = detainedLicense.ReleaseApplicationID;
            this.ReleasedByUserInfo = clsUser.Find(this.ReleasedByUserID);
            Mode = enMode.Update;
        }
        private DetainedLicenseDTO _ToDTO()
        {
            return new DetainedLicenseDTO 
            {
           
                LicenseID = this.LicenseID,
                DetainDate = this.DetainDate,
                DetainID = this.LicenseID,
                FineFees = this.FineFees,
                CreatedByUserID = this.CreatedByUserID,
                IsReleased = this.IsReleased,
                ReleaseDate = this.ReleaseDate,
                ReleasedByUserID= this.ReleasedByUserID,
                ReleaseApplicationID = this.ReleaseApplicationID,
            };
        }
        private bool _Add()
        { 
         this.DetainID = clsDetainedLicesnseData.AddNewDetainedLicense(_ToDTO());
            return this.DetainID != -1;
        }
        private bool _Update()
        {

            return clsDetainedLicesnseData.UpdateDetainedLicense(_ToDTO());
        }

        public static clsDetainedLicense FindByDetainID(int DetainID)
        {
            var item=  clsDetainedLicesnseData.GetDetainedLicenseInfoByID(DetainID);
            return new clsDetainedLicense(item);

        }
        public static clsDetainedLicense FindByLicenseID(int LicenseID)
        {
            if (clsDetainedLicesnseData.GetDetainedLicenseInfoByLicenseID(LicenseID)!=null)
            {
                return new clsDetainedLicense(clsDetainedLicesnseData.GetDetainedLicenseInfoByLicenseID(LicenseID));
            }
            return null;
        }

        public static bool IsLicenseDetained(int LicenseID)
        {
            return clsDetainedLicesnseData.IsLicenseDetained(LicenseID);
        }
        public bool ReleaseDetainedLicense(int ReleasedByUserID, int ReleaseApplicationID)
        {
            return clsDetainedLicesnseData.ReleaseDetainedLicense(this.DetainID,
                   ReleasedByUserID, ReleaseApplicationID);
        }
        public static DataTable GetAllDetainedLicenses()
        {
            return clsDetainedLicesnseData.GetAllDetainedLicenses();

        }
        public bool Save()
        {
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

    }
}
