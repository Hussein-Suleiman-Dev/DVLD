using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Shared;

namespace BusinessLayer.User
{
    public class clsUser
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool isActive { get; set; }
        public clsPeople PersonInfo { get; set; }
        public int PersonID
        {
            get;set;
        }
        enum enMode { Add = 1, Update = 2 }
        enMode Mode = enMode.Add;
        public clsUser()
        {
            this.UserID = -1;
            this.UserName = "";
            this.Password = "";
            this.isActive = true;
            this.PersonInfo = null; // سيتم ربطه لاحقاً قبل الحفظ
            this.Mode = enMode.Add;
        }
        private clsUser(UserDTO userDTO)
        {
            this.UserID = userDTO.UserID;
            this.UserName = userDTO.UserName;
            this.Password = userDTO.Password;
            this.isActive = userDTO.isActive;
            this.PersonID = userDTO.PersonID;
            this.PersonInfo = clsPeople.Find(PersonID);
        
            Mode = enMode.Update;
        }
        private UserDTO _toUserDTO()
        {
            return new UserDTO()
            {
                UserID = this.UserID,
                UserName = this.UserName,
                Password = this.Password,
                isActive = this.isActive,
                PersonID = this.PersonID

            };
        }

        public static DataTable GetAll()
        { 
        return clsUserData.GetAll();    
        }
        private bool _AddNewUser()
        {
            
            UserID = clsUserData.AddUsers(_toUserDTO());
            return UserID != -1;
        }

        private bool _UpdateUser()
        {
            Mode=enMode.Update;
            return clsUserData.UpdateUser(_toUserDTO());
        }

        public static clsUser FindByPersonID(int PersonID)
        {
            var userFind = clsUserData.GetUserByPerson(PersonID);
            if (userFind != null)
            {
                return new clsUser(userFind);
            }
            return null;
        }
        public static clsUser Find(int UserID)
        {
            var UserFind = clsUserData.GetUserByID(UserID);
            if (UserFind != null)
            {
                return new clsUser(UserFind);
            }
            return null;
        }

        public static clsUser Find(string UserName)
        {
            var User = clsUserData.GetUserByName(UserName);
            if (User != null)
            { 
            return new clsUser(User);
            }
            return null;
        }

        public static bool Delete(int UserID)
        {
            return clsUserData.DeleteUser(UserID);
        }

        public bool Save()
        {
            //if (this.Person == null)
            //{
            //    return false;
            //}
            switch (Mode)
            {
                case enMode.Add:
                    if (_AddNewUser())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    
                    else { return false; }

                    
                case enMode.Update:
                    return _UpdateUser();
;
            }
            return false;
        }

     

       public static clsUser FindByUserNameAndPassword(string UserName,string Password)
        { 
        
            var FindUser=clsUserData.FindByUserNameAndPassword(UserName, Password);
            if (FindUser != null)
            { 
            return new clsUser(FindUser);
            }
            return null;
        }


        public static bool IsUserExsist(int PersonID)
        { 
return clsUserData.IsUserExistForPersonID(PersonID);        
        }
        public static bool IsUserExsist(string UserName)
        {
            return clsUserData.IsUserExist(UserName);

        }
    
    }
}
