using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;
using DataAccessLayer;
using System.Data;
namespace BusinessLayer
{
    public class clsTestType
    {
        public enTestType TestTypeID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public float Fees { get; set; }

        enum enMode {Add=1, Update=2 }
        enMode Mode=enMode.Add;

      public  enum enTestType {  VisionTest =1,WrittenTest=2,StreetTest=3 }

        private clsTestType(TestTypeDTO dto)
        {
            this.TestTypeID = (enTestType)dto.TestTypeID;
            this.Title = dto.Title;
            this.Description = dto.Description;
            this.Fees = dto.Fees;
            Mode = enMode.Update;
        }

        private clsTestType()
        {
            this.TestTypeID = enTestType.VisionTest;
            this.Title = string.Empty;
            this.Description = string.Empty;
            this.Fees = 0.0f;
            Mode = enMode.Add;
        }

        public static DataTable GetAllTestTypes()
        {
            return clsTestTypeData.GetAllTestType();
        }

        private TestTypeDTO _ToDTO()
        {
            return new TestTypeDTO
            {
                TestTypeID = (int)this.TestTypeID,
                Title = this.Title,
                Description = this.Description,
                Fees = this.Fees
            };
        }
        private bool _Update()
        {

            return clsTestTypeData.UpdateTestType(_ToDTO());
        }

        public static clsTestType Find(clsTestType.enTestType TestTypeID)
        { 
            TestTypeDTO dto = clsTestTypeData.GetByID((int)TestTypeID);
            if (dto == null) return null;
            return new clsTestType(dto);
        }
        private bool _Add()
        {
            bool result = clsTestTypeData.AddTestType(_ToDTO());
            if (result)
                Mode = enMode.Update;

            return result;
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

        // Private constructor to create business object from DTO
}
}

