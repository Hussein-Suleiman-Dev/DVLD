using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Shared;
namespace BusinessLayer
{
    public class clsLicenseClass
    {
        public int LicenseClassID { get; set; }
        public string LicenseClassName { get; set; }
        public string Description { get; set; }
        public int MinimumAge { get; set; }
        public int DefaultValidityPeriod { get; set; }
        public float ClassFees { get; set; }

        enum enMode {Add,Update }
        enMode Mode= enMode.Add;
        public clsLicenseClass()
        {
            this.LicenseClassID = -1;
            this.LicenseClassName = string.Empty;
            this.ClassFees = 0;
            this.DefaultValidityPeriod = 0;
            this.Description = string.Empty;
            this.MinimumAge = 0;
        }
        private clsLicenseClass(LicenseClassDTO licenseClass)
        { 
        this.LicenseClassID = licenseClass.LicenseClassIDDTO;
            this.LicenseClassName = licenseClass.LicenseClassNameDTO;
            this.ClassFees = licenseClass.ClassFeesDTO;
            this.DefaultValidityPeriod = licenseClass.DefaultValidityPeriodDTO;
            this.Description = licenseClass.DescriptionDTO;
            this.MinimumAge = licenseClass.MinimumAgeDTO;
            Mode = enMode.Update;
        }

        private LicenseClassDTO _ToDTO()
        {
            LicenseClassDTO licenseClass = new LicenseClassDTO();
            licenseClass.LicenseClassIDDTO = this.LicenseClassID;
            licenseClass.LicenseClassNameDTO = this.LicenseClassName;
            licenseClass.ClassFeesDTO = this.ClassFees;
            licenseClass.DefaultValidityPeriodDTO = this.DefaultValidityPeriod;
            licenseClass.DescriptionDTO = this.Description;
            licenseClass.MinimumAgeDTO = this.MinimumAge;
            return licenseClass;
        }

        public static DataTable GetAll()
        {
            return clsLicenseClassData.GetAllLicenseClass();
        }

        public static clsLicenseClass Find(int licenseClassId)
        {
            LicenseClassDTO licenseClassDTO = clsLicenseClassData.GetByID(licenseClassId);
            if (licenseClassDTO != null)
            {
                return new clsLicenseClass(licenseClassDTO);
            }
            else
            {
                return null;
            }
        }
        public static clsLicenseClass Find(string licenseClassName)
        {
            LicenseClassDTO licenseClassDTO = clsLicenseClassData.GetByName(licenseClassName);
            if (licenseClassDTO != null)
            {
                return new clsLicenseClass(licenseClassDTO);
            }
            else
            {
                return null;
            }
        }
        private bool _Update()
        {
            if (Mode == enMode.Update)
            {
                return clsLicenseClassData.UpdateLicenseClass(this._ToDTO());
            }
            else
            {
                return false;
            }
        }
      private bool Add()
        {
            if (Mode == enMode.Add)
            {
                return clsLicenseClassData.InsertLicenseClass(this._ToDTO());
            }
            else
            {
                return false;
            }
        }
        public bool Save()
        {
           switch(Mode)
            {
                case enMode.Add:
                    return this.Add();
                case enMode.Update:
                    return this._Update();
                default:
                    return false;
            }
        }
}
}
