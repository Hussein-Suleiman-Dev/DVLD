using Sahred.Dtos;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class clsPeopleDataAccess
    {
        public static PersonDTO GetPersonByID(int PersonIDFind)
        {
            Sahred.Dtos.PersonDTO Person = null;

            SqlConnection Connection =
                new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = "Select * from People where PersonID=@PersonID";

            SqlCommand cmd = new SqlCommand(query, Connection);

            cmd.Parameters.AddWithValue("@PersonID", PersonIDFind);

            try
            {
                Connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    Person = new Sahred.Dtos.PersonDTO();

                    Person.PersonID = Convert.ToInt32(reader["PersonID"]);
                    Person.NationalNumber = reader["NationalNo"].ToString();
                    Person.FirstName = reader["FirstName"].ToString();
                    Person.SecondName = reader["SecondName"].ToString();
                    Person.ThirdName = reader["ThirdName"].ToString();
                    Person.LastName = reader["LastName"].ToString();
                    Person.DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                    Person.Gender = Convert.ToInt16(reader["Gendor"]);
                    Person.Address = reader["Address"].ToString();
                    Person.Phone = reader["Phone"].ToString();
                    Person.Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : "";
                    Person.ImagePath = reader["ImagePath"] != DBNull.Value ? reader["ImagePath"].ToString() : "";
                    Person.CountryID = Convert.ToInt32(reader["NationalityCountryID"]);
                }

                reader.Close();
            }
            catch
            {
                return null;
            }
            finally
            {
                Connection.Close();
            }

            return Person;
        }
        public static PersonDTO GetPersonByNationalNumber(string NationalNumber)
        {
            Sahred.Dtos.PersonDTO Person = null;

            SqlConnection Connection =
                new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = "select * from People where NationalNo=@NationalNumber";

            SqlCommand cmd = new SqlCommand(query, Connection);

            cmd.Parameters.AddWithValue("@NationalNumber", NationalNumber);

            try
            {
                Connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    Person = new Sahred.Dtos.PersonDTO();

                    Person.PersonID = Convert.ToInt32(reader["PersonID"]);
                    Person.NationalNumber = reader["NationalNo"].ToString();
                    Person.FirstName = reader["FirstName"].ToString();
                    Person.SecondName = reader["SecondName"].ToString();
                    Person.ThirdName = reader["ThirdName"].ToString();
                    Person.LastName = reader["LastName"].ToString();
                    Person.DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                    Person.Gender = Convert.ToInt16(reader["Gendor"]);
                    Person.Address = reader["Address"].ToString();
                    Person.Phone = reader["Phone"].ToString();
                    Person.Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : "";
                    Person.ImagePath = reader["ImagePath"] != DBNull.Value ? reader["ImagePath"].ToString() : "";
                    Person.CountryID = Convert.ToInt32(reader["NationalityCountryID"]);
                }

                reader.Close();
            }
            catch
            {
                return null;
            }
            finally
            {
                Connection.Close();
            }

            return Person;
        }

        public static int AddNewPerson(PersonDTO Person)
        {
            int PersonID = -1;

            SqlConnection Connection =
                new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = @"INSERT INTO People
(
FirstName,SecondName,ThirdName,LastName,DateOfBirth,
NationalNo,Gendor,Address,Phone,Email,ImagePath,NationalityCountryID
)
VALUES
(
@FirstName,@SecondName,@ThirdName,@LastName,@DateOfBirth,
@NationalNumber,@Gender,@Address,@Phone,@Email,@ImagePath,@CountryID
)
SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, Connection);

            command.Parameters.AddWithValue("@FirstName", Person.FirstName);
            command.Parameters.AddWithValue("@SecondName", Person.SecondName);
            command.Parameters.AddWithValue("@ThirdName", Person.ThirdName);
            command.Parameters.AddWithValue("@LastName", Person.LastName);
            command.Parameters.AddWithValue("@DateOfBirth", Person.DateOfBirth);
            command.Parameters.AddWithValue("@NationalNumber", Person.NationalNumber);
            command.Parameters.AddWithValue("@Gender", Person.Gender);
            command.Parameters.AddWithValue("@Address", Person.Address);
            command.Parameters.AddWithValue("@Phone", Person.Phone);
            command.Parameters.AddWithValue("@Email", Person.Email);
            command.Parameters.AddWithValue("@CountryID", Person.CountryID);

            if (string.IsNullOrWhiteSpace(Person.ImagePath))
                command.Parameters.AddWithValue("@ImagePath", DBNull.Value);
            else
                command.Parameters.AddWithValue("@ImagePath", Person.ImagePath);

            try
            {
                Connection.Open();

                object Result = command.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int ID))
                {
                    PersonID = ID;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Connection.Close();
            }

            return PersonID;
        }

        public static bool Update(PersonDTO Person)
        {
            int RowAffected = 0;

            SqlConnection Connection =
                new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = @"UPDATE People
SET 
FirstName = @FirstName,
SecondName = @SecondName,
ThirdName = @ThirdName,
LastName = @LastName,
DateOfBirth = @DateOfBirth,
NationalNo = @NationalNumber,
Gendor = @Gender,
Address = @Address,
Phone = @Phone,
Email = @Email,
ImagePath = @ImagePath,
NationalityCountryID = @CountryID
WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, Connection);

            command.Parameters.AddWithValue("@PersonID", Person.PersonID);
            command.Parameters.AddWithValue("@FirstName", Person.FirstName);
            command.Parameters.AddWithValue("@SecondName", Person.SecondName);

            command.Parameters.AddWithValue("@ThirdName",
                string.IsNullOrWhiteSpace(Person.ThirdName)
                ? (object)DBNull.Value
                : Person.ThirdName);

            command.Parameters.AddWithValue("@LastName", Person.LastName);
            command.Parameters.AddWithValue("@DateOfBirth", Person.DateOfBirth);
            command.Parameters.AddWithValue("@NationalNumber", Person.NationalNumber);
            command.Parameters.AddWithValue("@Gender", Person.Gender);
            command.Parameters.AddWithValue("@Address", Person.Address);
            command.Parameters.AddWithValue("@CountryID", Person.CountryID);

            command.Parameters.AddWithValue("@Phone",
                string.IsNullOrWhiteSpace(Person.Phone)
                ? (object)DBNull.Value
                : Person.Phone);

            command.Parameters.AddWithValue("@Email",
                string.IsNullOrWhiteSpace(Person.Email)
                ? (object)DBNull.Value
                : Person.Email);

            command.Parameters.AddWithValue("@ImagePath",
                string.IsNullOrWhiteSpace(Person.ImagePath)
                ? (object)DBNull.Value
                : Person.ImagePath);

            try
            {
                Connection.Open();
                RowAffected = command.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                Connection.Close();
            }

            return (RowAffected > 0);
        }

        public static DataTable GetAllPeople()
        {
            DataTable dt = new DataTable();

            SqlConnection Connection =
                new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = @"SELECT 
PersonID,NationalNo,FirstName,SecondName,ThirdName,
LastName,
CASE WHEN Gendor=0 THEN 'Male' ELSE 'Female' END AS Gender,
DateOfBirth,Phone,Email
FROM People";

            SqlCommand command = new SqlCommand(query, Connection);

            try
            {
                Connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
               
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Connection.Close();
            }

            return dt;
        }

        public static bool DeletePerson(int PersonId)
        {
            SqlConnection Connection=new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = @"delete from People where PersonID=@PersonID";
            SqlCommand Command = new SqlCommand(query, Connection);
            Command.Parameters.AddWithValue("@PersonID", PersonId);
            int RowAffected = -1;
            bool IsDelete = false;
            try
            {
                Connection.Open();
                RowAffected = Command.ExecuteNonQuery();
              
            }
            catch (Exception)
            {

                throw;
            }
            return (RowAffected > 0);
        }







        public static bool IsNationalNumberExists(string NationalNumber, int PersonID)
        {
            bool IsFound = false;

            SqlConnection Connection =
                new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query;

            if (PersonID != -1)
            {
                query = @"SELECT 1 FROM People 
WHERE NationalNo = @NationalNo AND PersonID <> @PersonID";
            }
            else
            {
                query = @"SELECT 1 FROM People WHERE NationalNo = @NationalNo";
            }

            SqlCommand command = new SqlCommand(query, Connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNumber);

            if (PersonID != -1)
                command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                Connection.Open();
                IsFound = (command.ExecuteScalar() != null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Connection.Close();
            }

            return IsFound;
        }
        public static bool IsEmailExists(string Email, int PersonID) { bool IsFound = false; string query = (PersonID == -1) ? "SELECT 1 FROM People WHERE Email = @Email" : @"SELECT 1 FROM People WHERE Email = @Email AND PersonID <> @PersonID"; using (SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString)) { SqlCommand command = new SqlCommand(query, connection); command.Parameters.AddWithValue("@Email", Email); if (PersonID != -1) command.Parameters.AddWithValue("@PersonID", PersonID); connection.Open(); IsFound = (command.ExecuteScalar() != null); } return IsFound; }
        public static bool IsPhoneExists(string Phone, int PersonID) { bool IsFound = false; string query = (PersonID == -1) ? "SELECT 1 FROM People WHERE Phone = @Phone" : @"SELECT 1 FROM People WHERE Phone = @Phone AND PersonID <> @PersonID"; using (SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString)) { SqlCommand command = new SqlCommand(query, connection); command.Parameters.AddWithValue("@Phone", Phone); if (PersonID != -1) command.Parameters.AddWithValue("@PersonID", PersonID); connection.Open(); IsFound = (command.ExecuteScalar() != null); } return IsFound; }
    }
}
    