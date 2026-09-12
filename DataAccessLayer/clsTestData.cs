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
    public class clsTestData
    {
        public static TestDTO GetByID(int TestID)
        {
            SqlConnection Connection = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = @"
select * from Tests
where TestID=@TestID";
            SqlCommand cmd= new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@TestID", TestID);
            try
            {
                Connection.Open();
                SqlDataReader reader= cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new TestDTO
                    {
                        TestID = (int)reader["TestID"],
                        TestAppointmentID = (int)reader["TestAppointmentID"],
                        Notes = reader["Notes"] == DBNull.Value? "": reader["Notes"].ToString(),
                        TestResult = (bool)reader["TestResult"],
                        CreatedByUserID = (int)reader["CreatedByUserID"]
                    };
                }
                reader.Close();
                return null;

            }
            catch (Exception)
            {

                throw;
            }
            finally { Connection.Close(); }
        }

        public static TestDTO GetLastTestByPersonAndTestTypeAndLicenseClass(int PersonID, int LicenseClassID, int TestTypeID)
        {
            SqlConnection Connection = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = @"select top(1) t.TestID,t.TestAppointmentID,t.TestResult,t.Notes,t.CreatedByUserID from TestAppointments tp
inner join Tests t on t.TestAppointmentID=tp.TestAppointmentID
inner join LocalDrivingLicenseApplications Lc on Lc.LocalDrivingLicenseApplicationID=tp.LocalDrivingLicenseApplicationID
inner join Applications App on App.ApplicationID=Lc.ApplicationID
where Lc.LicenseClassID=@LicenseID and tp.TestTypeID=@TestTypeID and App.ApplicantPersonID=@PersonID
order by t.TestAppointmentID Desc
";
            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@LicenseID", LicenseClassID);
            cmd.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            cmd.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new TestDTO
                    {
                        TestID = (int)reader["TestID"],
                        TestAppointmentID = (int)reader["TestAppointmentID"],
                        Notes = reader["Notes"] == DBNull.Value ? "": reader["Notes"].ToString(),
                        TestResult = (bool)reader["TestResult"],
                        CreatedByUserID = (int)reader["CreatedByUserID"]
                    };
                }
                    reader.Close();
                return null;

            }
            catch (Exception) { throw; }
            finally { Connection.Close(); }
        }

        public static DataTable GetAllTests()
        { 
        SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = @"select * from Tests
order by TestID ";
            DataTable dt= new DataTable();
            SqlCommand cmd= new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader Reader= cmd.ExecuteReader();
                if (Reader.HasRows)
                {
                   dt.Load(Reader);
                }
                Reader.Close();
                return dt;
            }
            catch (Exception)
            {

                throw;
            }
            finally { connection.Close(); }
            

        }

        public static int AddNewTest(TestDTO Test)
        {
            int TestID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = @"Insert Into Tests (TestAppointmentID,TestResult,
                                                Notes,   CreatedByUserID)
                            Values (@TestAppointmentID,@TestResult,
                                                @Notes,   @CreatedByUserID);
                            
                                UPDATE TestAppointments 
                                SET IsLocked=1 where TestAppointmentID = @TestAppointmentID;

                                SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", Test.TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", Test.TestResult);

            if (Test.Notes != "" && Test.Notes != null)
                command.Parameters.AddWithValue("@Notes", Test.Notes);
            else
                command.Parameters.AddWithValue("@Notes", System.DBNull.Value);



            command.Parameters.AddWithValue("@CreatedByUserID", Test. CreatedByUserID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TestID = insertedID;
                }
            }

            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

            }

            finally
            {
                connection.Close();
            }


            return TestID;
        }

        public static bool UpdateTest(TestDTO Test)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = @"Update  Tests  
                            set TestAppointmentID = @TestAppointmentID,
                                TestResult=@TestResult,
                                Notes = @Notes,
                                CreatedByUserID=@CreatedByUserID
                                where TestID = @TestID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestID",Test. TestID);
            command.Parameters.AddWithValue("@TestAppointmentID",Test. TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult",Test. TestResult);
            command.Parameters.AddWithValue("@Notes", Test.Notes);
            command.Parameters.AddWithValue("@CreatedByUserID",Test. CreatedByUserID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
           byte CountTest = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = @"select PassedTestCount= Count(TestID) from Tests t
inner join TestAppointments tp on tp.TestAppointmentID=t.TestAppointmentID

where tp.LocalDrivingLicenseApplicationID=@LocalDrivingID and t.TestResult=1 ";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@LocalDrivingID", LocalDrivingLicenseApplicationID);
            try
            {
                connection.Open();
                Object result = cmd.ExecuteScalar();


                if (result != null && byte.TryParse(result.ToString(), out byte Tests))
                {
                 CountTest = Tests;
                }

            }
            catch (Exception)
            {

                throw;
            }
            return CountTest;
        }

    }


    }

