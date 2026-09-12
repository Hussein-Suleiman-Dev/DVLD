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
    public class clsDriver
    {
        enum enMode {Add=0,Update=1 }
        enMode _Mode=enMode.Add;
        public int DriverID { get; set; }
        public int PersonID { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime CreatedDate { get; set; }
        public clsPeople PersonInfo;

        public clsDriver()
        {
            this.DriverID = -1;
            this.PersonID = -1;
            this.CreatedByUserID = -1;
            this.CreatedDate = DateTime.Now;
            PersonInfo= new clsPeople();
            _Mode = enMode.Add;
        }
        private clsDriver(DriverDTO driver)
        { 
        this.DriverID= driver.DriverID;
            this.PersonID = driver.PersonID;
            this.CreatedByUserID= driver.CreatedByUserID;
            this.CreatedDate = driver.CreatedDate;
            this.PersonInfo = clsPeople.Find(driver.PersonID);
            _Mode = enMode.Update;
        }
        private DriverDTO _ToDTO()
        {
            return new DriverDTO
            {
                PersonID = this.PersonID,
                DriverID = this.DriverID,
                CreatedByUserID = this.CreatedByUserID,
                CreatedDate = this.CreatedDate,
            };
        }

        private bool _Add()
        {
            this.DriverID = clsDriverData.AddNewDriver(PersonID, CreatedByUserID);


            return (this.DriverID != -1);
        }
     
        private bool _Update()
        {
            return clsDriverData.UpdateDriver(_ToDTO());
        }

        public static clsDriver FindByDriverID(int DriverID)
        {

            return new clsDriver( clsDriverData.GetDriverByID(DriverID));   
        }
        public static clsDriver FindByPersonID(int PersonID)
        {
       
            DriverDTO driverDto = clsDriverData.GetDriverByPersonID(PersonID);
            return driverDto == null ? null : new clsDriver(driverDto);
        
        }
        public static DataTable GetAllDrivers()
        { 
        return clsDriverData.GetAllDrivers();
        }
        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.Add:
                    if (_Add())
                        return true;
                    else { return false; }
                    
                    case enMode.Update:
                    _Update();
                    return true;
            }
        return false;
        }

        public static DataTable GetLicenses(int DriverID)
        {
            return clsLicense.GetDriverLicenses(DriverID);
        }
        public static DataTable GetInternationalLicenses(int DriverID)
        {
             return clsInternationalLicense.GetDriverInternationalLicenses(DriverID);
         
        }
    }
}
