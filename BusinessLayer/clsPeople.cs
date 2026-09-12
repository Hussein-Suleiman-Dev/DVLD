using DataAccessLayer;
using Sahred.Dtos;
using System;
using System.Data;

namespace BusinessLayer
{
    public class clsPeople
    {
        public int PersonID { get; set; }

        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return $"{FirstName} {SecondName} {ThirdName} {LastName}";
            }
        }
        public DateTime DateOfBirth { get; set; }

        public string NationalNumber { get; set; }

        public short Gender { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string ImagePath { get; set; }

        public int CountryID { get; set; }

        public clsCountry CountryInfo
        {
            get
            {
                return clsCountry.FindCountry(CountryID);
            }
        }

        public enum enMode { Add = 1, Update = 2 }
        public enMode Mode = enMode.Add;

        public clsPeople()
        {
            PersonID = -1;
            FirstName = "";
            SecondName = "";
            ThirdName = "";
            LastName = "";
            DateOfBirth = DateTime.Now;
            NationalNumber = "";
            Gender = 0;
            Address = "";
            Phone = "";
            Email = "";
            ImagePath = "";
            CountryID = -1;

            Mode = enMode.Add;
        }

        private clsPeople(PersonDTO dto)
        {
            PersonID = dto.PersonID;
            FirstName = dto.FirstName;
            SecondName = dto.SecondName;
            ThirdName = dto.ThirdName;
            LastName = dto.LastName;
            DateOfBirth = dto.DateOfBirth;
            NationalNumber = dto.NationalNumber;
            Gender = dto.Gender;
            Address = dto.Address;
            Phone = dto.Phone;
            Email = dto.Email;
            ImagePath = dto.ImagePath;
            CountryID = dto.CountryID;

            Mode = enMode.Update;
        }

      
        private PersonDTO _ToDTO()
        {
            return new PersonDTO()
            {
                PersonID = PersonID,
                FirstName = FirstName,
                SecondName = SecondName,
                ThirdName = ThirdName,
                LastName = LastName,
                DateOfBirth = DateOfBirth,
                NationalNumber = NationalNumber,
                Gender = Gender,
                Address = Address,
                Phone = Phone,
                Email = Email,
                ImagePath = ImagePath,
                CountryID = CountryID
            };
        }

        public static clsPeople Find(int PersonID)
        {
            var dto = clsPeopleDataAccess.GetPersonByID(PersonID);

            if (dto == null)
                return null;

            return new clsPeople(dto);
        }
        public static clsPeople Find(string NationalNumber)
        {
            var dto = clsPeopleDataAccess.GetPersonByNationalNumber(NationalNumber);
            if (dto == null)
            {
                return null;
            }
            return new clsPeople(dto);
        }
        public static DataTable GetAllPeople()
        {
            return clsPeopleDataAccess.GetAllPeople();
        }

        private int _Add()
        {
            PersonID = clsPeopleDataAccess.AddNewPerson(_ToDTO());
           return PersonID;
        }

     
        private bool _Update()
        {
            return clsPeopleDataAccess.Update(_ToDTO());
        }

       
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.Add:

                    if (_Add()!=-1)
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

       
        public static bool IsNationalNumberExists(string nationalNumber, int personID)
        {
            return clsPeopleDataAccess.IsNationalNumberExists(nationalNumber, personID);
        }

        public static bool IsEmailExists(string email, int personID)
        {
            return clsPeopleDataAccess.IsEmailExists(email, personID);
        }

        public static bool IsPhoneExists(string phone, int personID)
        {
            return clsPeopleDataAccess.IsPhoneExists(phone, personID);
        }
        public static bool Delete(int PersonID)
        { 
        return clsPeopleDataAccess.DeletePerson(PersonID);
        }
    }
}