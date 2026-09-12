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
    public class clsApplicationTypes
    {
        public int ApplicationTypeID { get; set; }
        public string ApplicationTypeName { get; set; }
        public float ApplicationTypeFess { get; set; }

        enum enMode { Add = 1, Update = 2 }
        enMode Mode = enMode.Add;

        // =========================
        // Constructor (Add Mode)
        // =========================
        public clsApplicationTypes()
        {
            this.ApplicationTypeID = -1;
            this.ApplicationTypeName = "";
            this.ApplicationTypeFess = 0;

            Mode = enMode.Add;
        }

       
        private clsApplicationTypes(ApplicationTypeDTO dto)
        {
            this.ApplicationTypeID = dto.ApplicationTypeIdDTO;
            this.ApplicationTypeName = dto.ApplicationTypeNameDTO;
            this.ApplicationTypeFess = dto.ApplicationTypeFessDTO;

            Mode = enMode.Update;
        }

        private ApplicationTypeDTO _ToDTO()
        {
            return new ApplicationTypeDTO
            {
                ApplicationTypeIdDTO = this.ApplicationTypeID,
                ApplicationTypeNameDTO = this.ApplicationTypeName,
                ApplicationTypeFessDTO = this.ApplicationTypeFess
            };
        }

        
        public static clsApplicationTypes Find(int id)
        {
            var dto = clsApplicationTypesData.GetApplicationByID(id);

            if (dto != null)
                return new clsApplicationTypes(dto);

            return null;
        }

       
        public static DataTable GetAllApplicationTypes()
        {
            return clsApplicationTypesData.GetAllAppliction();
        }

    
        private bool _AddNew()
        {
            this.ApplicationTypeID = clsApplicationTypesData.AddNew(_ToDTO());
            return this.ApplicationTypeID != -1;
        }

      
        private bool _Update()
        {
            return clsApplicationTypesData.Update(_ToDTO());
        }

      
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.Add:
                    if (_AddNew())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _Update();
            }

            return false;
        }
    }
}