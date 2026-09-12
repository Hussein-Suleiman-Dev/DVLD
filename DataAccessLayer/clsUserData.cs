using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;
namespace DataAccessLayer
{
    public class clsUserData
    {

        public static DataTable GetAll()
        {
            DataTable dt= new DataTable();
            SqlConnection Connection = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = @"select u.UserID,u.PersonID,

    CONCAT_WS(' ', p.FirstName, p.SecondName, p.ThirdName, p.LastName) AS FullName,u.UserName,u.IsActive

from Users u
Inner join People p on p.PersonID=u.PersonID";
            SqlCommand cmd=new SqlCommand(query,Connection);
            try
            {
                Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);

                }



            }
            catch (Exception)
            {

                throw;
            }
            finally { Connection.Close(); }
            return dt;
        }

        public static int AddUsers(UserDTO User)
        {
            SqlConnection Connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            
            string query = @"INSERT INTO Users (PersonID, UserName, Password, IsActive) 
                     VALUES (@PersonID, @UserName, @Password, @isActive);
                     SELECT SCOPE_IDENTITY();";

            SqlCommand cmd = new SqlCommand(query, Connection);

           
            cmd.Parameters.AddWithValue("@PersonID", User.PersonID);
            cmd.Parameters.AddWithValue("@UserName", User.UserName);
            cmd.Parameters.AddWithValue("@Password", User.Password);
            cmd.Parameters.AddWithValue("@isActive", User.isActive);

            int NewUserID = -1;

            try
            {
                Connection.Open();
                object result = cmd.ExecuteScalar(); 

                if (result != null && int.TryParse(result.ToString(), out int insertedId))
                {
                    NewUserID = insertedId;
                }
            }
            catch (Exception)
            {
                return -1;
            }
            finally
            {
                Connection.Close();
            }

            return NewUserID; 
        }

        public static UserDTO GetUserByID(int UserID)
        {
            UserDTO User = null;
            SqlConnection Connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

          
            string query = @"SELECT UserID, PersonID, UserName, Password, IsActive 
                     FROM Users 
                     WHERE UserID = @UserID";

            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                 
                    User = new UserDTO
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        PersonID = Convert.ToInt32(reader["PersonID"]), 
                        UserName = reader["UserName"].ToString(),
                        Password = reader["Password"].ToString(),
                        isActive = Convert.ToBoolean(reader["IsActive"])
                    };
                }
                reader.Close();
            }
            catch (Exception)
            {
             
                return null;
            }
            finally
            {
                Connection.Close(); 
            }

            return User;
        }
        public static UserDTO GetUserByName(string UserName)
        {
            UserDTO User = null;
            SqlConnection Connection = new SqlConnection(clsDataAccessSetting.ConnectionString);


            string query = @"SELECT UserID, PersonID, UserName, Password, IsActive 
                     FROM Users 
                     WHERE UserName = @UserName";

            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@UserName", UserName);

            try
            {
                Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {

                    User = new UserDTO
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        PersonID = Convert.ToInt32(reader["PersonID"]),
                        UserName = reader["UserName"].ToString(),
                        Password = reader["Password"].ToString(),
                        isActive = Convert.ToBoolean(reader["IsActive"])
                    };
                }
                reader.Close();
            }
            catch (Exception)
            {

                return null;
            }
            finally
            {
                Connection.Close();
            }

            return User;
        }

        public static UserDTO GetUserByPerson(int PersonID)
        {
            UserDTO User = null;
            SqlConnection Connection = new SqlConnection(clsDataAccessSetting.ConnectionString);


            string query = @"SELECT UserID, PersonID, UserName, Password, IsActive 
                     FROM Users 
                     WHERE PersonID = @PersonID";

            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {

                    User = new UserDTO
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        PersonID = Convert.ToInt32(reader["PersonID"]),
                        UserName = reader["UserName"].ToString(),
                        Password = reader["Password"].ToString(),
                        isActive = Convert.ToBoolean(reader["IsActive"])
                    };
                }
                reader.Close();
            }
            catch (Exception)
            {

                return null;
            }
            finally
            {
                Connection.Close();
            }

            return User;
        }



        public static bool UpdateUser(UserDTO User)
        {
            SqlConnection Connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = @"UPDATE Users 
                     SET UserName = @UserName, 
                         Password = @Password, 
                         IsActive = @isActive 
                     WHERE UserID = @UserID"; // التعديل بناءً على الـ UserID

            SqlCommand cmd = new SqlCommand(query, Connection);

            cmd.Parameters.AddWithValue("@UserID", User.UserID);
            cmd.Parameters.AddWithValue("@UserName", User.UserName);
            cmd.Parameters.AddWithValue("@Password", User.Password);
            cmd.Parameters.AddWithValue("@isActive", User.isActive);

            int RowAffected = -1;

            try
            {
                Connection.Open();
                RowAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                Connection.Close();
            }

            return RowAffected > 0;
        }

        public static bool DeleteUser(int UserID)
        {
            SqlConnection Connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = @"DELETE FROM Users WHERE UserID = @UserID";

            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@UserID", UserID);

            int RowAffected = -1;

            try
            {
                Connection.Open();
                RowAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                Connection.Close();
            }

            return RowAffected > 0;
        }

        public static bool CheckUserNameAndPassword(string UserName,string Password)
        { 
        SqlConnection Connection=new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = @"select 1 from Users u
where u.UserName=@UserName and u.Password=@Password";
            SqlCommand cmd=new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@UserName",UserName);
            cmd.Parameters.AddWithValue("@Password", Password);
            int res = -1;
            try
            {
                Connection.Open();
            object Result= cmd.ExecuteScalar();
                if (Result != null && int.TryParse(Result.ToString(), out int resultquery))
                { 
                res=resultquery;
                }
            }
            catch (Exception)
            {
                return false;
                
            }
            finally { Connection.Close(); }
         return res > 0;   
        }
        public static bool IsActiveUserByID(int UserID)
        {
            SqlConnection Connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = @"SELECT IsActive FROM Users WHERE UserID = @UserID";

            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@UserID", UserID);

            bool isActive = false;

            try
            {
                Connection.Open();
                object Result = cmd.ExecuteScalar();

                if (Result != null && bool.TryParse(Result.ToString(), out bool resultquery))
                {
                    isActive = resultquery;
                }
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                Connection.Close();
            }

            return isActive;
        }





        public static bool IsUserExistForPersonID(int PersonID)
        {
            bool isFound = false;

            // 'using' statements automatically close and dispose of connections and commands
            using (SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                // Fixed the SQL syntax error (changed == to =)
                string query = "SELECT 1 FROM Users WHERE PersonID=@PersonID";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    // Safely bind the parameter to prevent SQL Injection
                    cmd.Parameters.AddWithValue("@PersonID",PersonID );

                    try
                    {
                        connection.Open();

                    
                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            isFound = true;
                        }
                    }
                    catch (Exception ex)
                    {
                      
                    }
                }
            }

            return isFound;
        }
        public static bool IsUserExist(string UserName)
        {
            bool isFound = false;

            // 'using' statements automatically close and dispose of connections and commands
            using (SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                // Fixed the SQL syntax error (changed == to =)
                string query = "SELECT 1 FROM Users WHERE UserName=@UserName";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    // Safely bind the parameter to prevent SQL Injection
                    cmd.Parameters.AddWithValue("@UserName", UserName);

                    try
                    {
                        connection.Open();

                        // ExecuteScalar returns the first column of the first row, or null if no rows match
                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            isFound = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Optional: Log the error here before rethrowing
                       
                    }
                }
            }

            return isFound;
        }
        public static bool IsUserExist(int UserID)
        {
            bool isFound = false;

            // 'using' statements automatically close and dispose of connections and commands
            using (SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                string query = "select 1 from Users where UserID=@UserID";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
     
                    cmd.Parameters.AddWithValue("@UserID", UserID);

                    try
                    {
                        connection.Open();

                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            isFound = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }

            return isFound;
        }
        public static bool ChangePassword(int UserId, string Password)
        {
            SqlConnection Conn = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = @"update Users
set Password=@Password
where UserID=@UserID ";

            SqlCommand cmd = new SqlCommand(query, Conn);
            cmd.Parameters.AddWithValue("@UserID",UserId);
            cmd.Parameters.AddWithValue("@Password",Password);
            int RowAffected = -1;
            try
            {
                RowAffected = cmd.ExecuteNonQuery();
                return (RowAffected > 0);
            }
            catch (Exception)
            {

                return false;
            }
           
        
        }

        public static UserDTO FindByUserNameAndPassword(string UserName,string Password)
        {
            SqlConnection conn = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = @"select * from Users where UserName=@UserName and Password=@Password";
            SqlCommand cmd= new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@UserName",UserName);
            cmd.Parameters.AddWithValue("@Password", Password);
            UserDTO User = null;
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    User = new UserDTO
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        PersonID = Convert.ToInt32(reader["PersonID"]),
                        UserName = reader["UserName"].ToString(),
                        Password = reader["Password"].ToString(),
                        isActive = Convert.ToBoolean(reader["IsActive"])
                    };

                }

                reader.Close();
            }
            catch (Exception)
            {
                return null;
                
            }


            return User;



        }



    }
}
