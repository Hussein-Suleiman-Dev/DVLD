using Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataAccessLayer
{
    public class clsTestAppointmentsData
    {
        public static TestAppointmentDTO GetTestAppointmentInfoByID(int TestAppointmentID)
        {
            TestAppointmentDTO testAppointment = null;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = @"SELECT *
                     FROM TestAppointments
                     WHERE TestAppointmentID = @TestAppointmentID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    testAppointment = new TestAppointmentDTO();

                    testAppointment.TestAppointmentID =
                        (int)reader["TestAppointmentID"];

                    testAppointment.TestTypeID =
                        (int)reader["TestTypeID"];

                    testAppointment.LocalDrivingLicenseAppID =
                        (int)reader["LocalDrivingLicenseApplicationID"];

                    testAppointment.AppointmentDate =
                        (DateTime)reader["AppointmentDate"];

                    testAppointment.PaidFees =
                        Convert.ToSingle(reader["PaidFees"]);

                    testAppointment.CreatedByUserID =
                        (int)reader["CreatedByUserID"];

                    testAppointment.IsLocked =
                        (bool)reader["IsLocked"];

                    if (reader["RetakeTestApplicationID"] == DBNull.Value)
                        testAppointment.RetakeTestApplicationID = -1;
                    else
                        testAppointment.RetakeTestApplicationID =
                            (int)reader["RetakeTestApplicationID"];
                }

                reader.Close();
            }
            finally
            {
                connection.Close();
            }

            return testAppointment;
        }
        public static DataTable GetAllTestAppointment()
        {
            SqlConnection Conn = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = @"";
            SqlCommand cmd = new SqlCommand(query, Conn);
            DataTable _dt = new DataTable();
            try
            {
                Conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    _dt.Load(reader);
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally { Conn.Close(); }
            return _dt;
        }

        public static DataTable GetApplicationTestAppointmentsPerTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = @"SELECT TestAppointmentID, AppointmentDate,PaidFees, IsLocked
                        FROM TestAppointments
                        WHERE  
                        (TestTypeID = @TestTypeID) 
                        AND (LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID)
                        order by TestAppointmentID desc;";


            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);


            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    dt.Load(reader);
                }

                reader.Close();


            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return dt;
            //اعطني  كل المواعيد حسب رقم طلب رخصة محلية ونوع فحص
        }
        public static TestAppointmentDTO GetLastTestAppointment(int TestTypeID, int LocalDrivingLicenseAppID)
        {
            TestAppointmentDTO testAppointment = null;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = @"SELECT TOP 1 
                        TestAppointmentID,
                        TestTypeID,
                        LocalDrivingLicenseApplicationID,
                        AppointmentDate,
                        PaidFees,
                        CreatedByUserID,
                        IsLocked,
                        RetakeTestApplicationID
                     FROM TestAppointments
                     WHERE TestTypeID = @TestTypeID
                     AND LocalDrivingLicenseApplicationID = @LocalDrivingLicenseAppID
                     ORDER BY TestAppointmentID DESC";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseAppID", LocalDrivingLicenseAppID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    testAppointment = new TestAppointmentDTO()
                    {
                        TestAppointmentID = (int)reader["TestAppointmentID"],
                        TestTypeID = (int)reader["TestTypeID"],
                        LocalDrivingLicenseAppID = (int)reader["LocalDrivingLicenseApplicationID"],
                        AppointmentDate = (DateTime)reader["AppointmentDate"],
                        PaidFees = Convert.ToSingle(reader["PaidFees"]),
                        CreatedByUserID = (int)reader["CreatedByUserID"],
                        IsLocked = (bool)reader["IsLocked"],
                        RetakeTestApplicationID = (int)reader["RetakeTestApplicationID"]
                    };
                }

                reader.Close();
            }
            finally
            {
                connection.Close();
            }

            return testAppointment;
        }

        public static int AddTestAppointment(TestAppointmentDTO testAppointmentDTO)
        {
            int TestAppointmentID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = @"INSERT INTO TestAppointments
                    (
                        TestTypeID,
                        LocalDrivingLicenseApplicationID,
                        AppointmentDate,
                        PaidFees,
                        CreatedByUserID,
                        IsLocked,
                        RetakeTestApplicationID
                    )
                    VALUES
                    (
                        @TestTypeID,
                        @LocalDrivingLicenseApplicationID,
                        @AppointmentDate,
                        @PaidFees,
                        @CreatedByUserID,
                        @IsLocked,
                        @RetakeTestApplicationID
                    );

                    SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestTypeID", testAppointmentDTO.TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID",
                                            testAppointmentDTO.LocalDrivingLicenseAppID);
            command.Parameters.AddWithValue("@AppointmentDate", testAppointmentDTO.AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", testAppointmentDTO.PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", testAppointmentDTO.CreatedByUserID);
            command.Parameters.AddWithValue("@IsLocked", testAppointmentDTO.IsLocked);
            if (testAppointmentDTO.RetakeTestApplicationID == -1)
            {
                command.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@RetakeTestApplicationID",
                    testAppointmentDTO.RetakeTestApplicationID);
            }


            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int id))
                {
                    TestAppointmentID = id;
                }
            }
            finally
            {
                connection.Close();
            }

            return TestAppointmentID;
        }

        public static bool UpdateTestAppointment(TestAppointmentDTO testAppointmentDTO)
        {
            bool isUpdated = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = @"UPDATE TestAppointments
                     SET
                        TestTypeID = @TestTypeID,
                        LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID,
                        AppointmentDate = @AppointmentDate,
                        PaidFees = @PaidFees,
                        CreatedByUserID = @CreatedByUserID,
                        IsLocked = @IsLocked,
                        RetakeTestApplicationID = @RetakeTestApplicationID
                     WHERE TestAppointmentID = @TestAppointmentID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", testAppointmentDTO.TestAppointmentID);
            command.Parameters.AddWithValue("@TestTypeID", testAppointmentDTO.TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID",
                                            testAppointmentDTO.LocalDrivingLicenseAppID);
            command.Parameters.AddWithValue("@AppointmentDate", testAppointmentDTO.AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", testAppointmentDTO.PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", testAppointmentDTO.CreatedByUserID);
            command.Parameters.AddWithValue("@IsLocked", testAppointmentDTO.IsLocked);
            if (testAppointmentDTO.RetakeTestApplicationID == -1)
            {
                command.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@RetakeTestApplicationID",
                    testAppointmentDTO.RetakeTestApplicationID);
            }

            try
            {
                connection.Open();

                int rowsAffected = command.ExecuteNonQuery();

                isUpdated = rowsAffected > 0;
            }
            finally
            {
                connection.Close();
            }

            return isUpdated;
        }

        public static int GetTestID(int TestAppointmentID)
        {
            int TestID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = @"select TestID from Tests where TestAppointmentID=@TestAppointmentID;";

            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);


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

    }
}

