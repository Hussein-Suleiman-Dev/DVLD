using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using BusinessLayer.User;
namespace DVLD
{
    internal class clsGlobal
    {
        public static clsUser CurrentUser;
        public static bool RemmberUserNameAndPassword(string UserName, string Password)
        {
            

            using (StreamWriter writer = new StreamWriter("LoginData.txt"))
            {
                writer.Write(UserName + "#" + Password);
                return true;
            }



        
        }
        public static bool GetStoreCredential(ref string UserName, ref string Password)
        {
            if (File.Exists("LoginData.txt"))
            {
                using (StreamReader reader = new StreamReader("LoginData.txt"))
                {
                    string Line = reader.ReadLine();
                    string[] data = Line.Split('#');
                    UserName = data[0];
                    Password = data[1];
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

    }
}
