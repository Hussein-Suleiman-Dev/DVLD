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
    public class clsLocalDrivingApplicationData
    {
        public static LocalDrivingApplicationDTO GetByLocalDrivingAppID(int LoaclDrivingApplicationID)
        {
            SqlConnection Conn = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = "SELECT * FROM LocalDrivingLicenseApplications WHERE LocalDrivingLicenseApplicationID=@ID";
            SqlCommand cmd = new SqlCommand(query, Conn);
            cmd.Parameters.AddWithValue("@ID", LoaclDrivingApplicationID);
            try
            {
                Conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new LocalDrivingApplicationDTO
                    {
                        LocalDrivingIdApp = Convert.ToInt32(reader["LocalDrivingLicenseApplicationID"]),
                        ApplicationId = Convert.ToInt32(reader["ApplicationID"]),
                        LicenseClassID = Convert.ToInt32(reader["LicenseClassID"])
                    };
                }
                return null;

            }
            catch (Exception)
            {

                throw;
            }

            finally { Conn.Close(); }

        }
        public static LocalDrivingApplicationDTO GetByAppID(int ApplicationID)
        {
            SqlConnection Conn = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = "SELECT * FROM LocalDrivingLicenseApplications WHERE ApplicationID=@ID";
            SqlCommand cmd = new SqlCommand(query, Conn);
            cmd.Parameters.AddWithValue("@ID", ApplicationID);
            try
            {
                Conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new LocalDrivingApplicationDTO
                    {
                        LocalDrivingIdApp = Convert.ToInt32(reader["LocalDrivingLicenseApplicationID"]),
                        ApplicationId = Convert.ToInt32(reader["ApplicationID"]),
                        LicenseClassID = Convert.ToInt32(reader["LicenseClassID"])
                    };
                }
                return null;

            }
            catch (Exception)
            {

                throw;
            }

            finally { Conn.Close(); }

        }

        public static bool UpdateLocalDrivingApplication(LocalDrivingApplicationDTO dto)
        {
            SqlConnection Conn = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = "UPDATE LocalDrivingLicenseApplications SET ApplicationID=@AppID, LicenseClassID=@ClassID WHERE LocalDrivingLicenseApplicationID=@LocalAppID";
            SqlCommand cmd = new SqlCommand(query, Conn);
            cmd.Parameters.AddWithValue("@AppID", dto.ApplicationId);
            cmd.Parameters.AddWithValue("@ClassID", dto.LicenseClassID);
            cmd.Parameters.AddWithValue("@LocalAppID", dto.LocalDrivingIdApp);
            try
            {
                Conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally { Conn.Close(); }



        }

        public static bool DeleteLocalDrivingApplication(int LocalDrivingAppID)
        {
            SqlConnection Conn = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = "DELETE FROM LocalDrivingLicenseApplications WHERE LocalDrivingLicenseApplicationID=@LocalAppID";
            SqlCommand cmd = new SqlCommand(query, Conn);
            cmd.Parameters.AddWithValue("@LocalAppID", LocalDrivingAppID);
            try
            {
                Conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally { Conn.Close(); }
        }

        public static DataTable GetAllLocalDrivingApplication() {

            SqlConnection Conn = new SqlConnection(clsDataAccessSetting.ConnectionString);
            DataTable dt = new DataTable();
            string query = @"select LD.LocalDrivingLicenseApplicationID,Lc.ClassName,p.NationalNo,
(p.FirstName +' '+p.SecondName+''+p.ThirdName+''+p.LastName)As FullName,App.ApplicationDate,

(SELECT COUNT(dbo.TestAppointments.TestTypeID) AS PassedTestCount
                       FROM      dbo.Tests INNER JOIN
                                         dbo.TestAppointments ON dbo.Tests.TestAppointmentID = dbo.TestAppointments.TestAppointmentID
                       WHERE   (dbo.TestAppointments.LocalDrivingLicenseApplicationID =LD.LocalDrivingLicenseApplicationID) AND (dbo.Tests.TestResult = 1)) AS PassedTestCount
,case when App.ApplicationStatus=1 then 'New' when App.ApplicationStatus=2 then 'Cancled' when App.ApplicationStatus=3 then 'Completed'End As AppStatus

from LocalDrivingLicenseApplications LD
inner join LicenseClasses Lc on Lc.LicenseClassID=LD.LicenseClassID
inner join dbo.Applications App on App.ApplicationID=LD.ApplicationID
inner join dbo.People p on p.PersonID=App.ApplicantPersonID
";
            SqlCommand cmd= new SqlCommand(query, Conn);
            try
            {
                Conn.Open();
                SqlDataReader Reader = cmd.ExecuteReader();
                if(Reader.HasRows)
                {
                    dt.Load(Reader);
                }
                Reader.Close();
            }
            catch (Exception)
            {

                throw;
            }
            finally { Conn.Close(); }
            return dt;
        }

        public static int AddLocalDrivingApplication(LocalDrivingApplicationDTO dto)
        {
            SqlConnection Conn = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = "INSERT INTO LocalDrivingLicenseApplications (ApplicationID, LicenseClassID) VALUES (@AppID, @ClassID); SELECT SCOPE_IDENTITY();";
            SqlCommand cmd = new SqlCommand(query, Conn);
            cmd.Parameters.AddWithValue("@AppID", dto.ApplicationId);
            cmd.Parameters.AddWithValue("@ClassID", dto.LicenseClassID);
            try
            {
                Conn.Open();
                object result = cmd.ExecuteScalar();
                return Convert.ToInt32(result);
            }
            catch (Exception)
            {
                throw;
            }
            finally { Conn.Close(); }
        }

        public static bool DosePassedTestType(int LocalDrivingApplicationID, int TestTypeID)
        {

            bool Result = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = @" SELECT top 1 TestResult
                            FROM LocalDrivingLicenseApplications INNER JOIN
                                 TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                                 Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                            WHERE
                            (LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID) 
                            AND(TestAppointments.TestTypeID = @TestTypeID)
                            ORDER BY TestAppointments.TestAppointmentID desc";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && bool.TryParse(result.ToString(), out bool returnedResult))
                {
                    Result = returnedResult;
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

            return Result;


        }

        public static int DoseAttendedTestType(int LocalDrivingLicenseApplicationID,int TestTypeID)
        { 
        SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = @"select top 1 found=1
from 
Tests 
Inner Join TestAppointments Tp on Tests.TestAppointmentID=Tp.TestAppointmentID
Inner join LocalDrivingLicenseApplications LDL on Tp.LocalDrivingLicenseApplicationID=LDL.LocalDrivingLicenseApplicationID

WHERE LDL.LocalDrivingLicenseApplicationID=@LDLID and Tp.TestTypeID=@TestType
order by Tp.TestAppointmentID Desc";

            SqlCommand cmd= new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@LDLID",LocalDrivingLicenseApplicationID);
            cmd.Parameters.AddWithValue("@TestType", TestTypeID);

            try
            {
                connection.Open();
               object Result= cmd.ExecuteScalar();
                if (Result != null && int.TryParse(Result.ToString(), out int result))
                { 
                return result;
                }
            }
            catch (Exception)
            {

                throw;
            }


            return -1;
        }

        public static byte TotalTrialsPerTest(int LocalDrivingLicenseApplicationID, int TestTypeID)

        {


            byte TotalTrialsPerTest = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = @" SELECT TotalTrialsPerTest = count(TestID)
                            FROM LocalDrivingLicenseApplications INNER JOIN
                                 TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                                 Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                            WHERE
                            (LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID) 
                            AND(TestAppointments.TestTypeID = @TestTypeID)
                       ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && byte.TryParse(result.ToString(), out byte Trials))
                {
                    TotalTrialsPerTest = Trials;
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

            return TotalTrialsPerTest;

        }
        public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, int TestTypeID)

        {

            bool Result = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = @" SELECT top 1 Found=1
                            FROM LocalDrivingLicenseApplications INNER JOIN
                                 TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID 
                            WHERE
                            (LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID)  
                            AND(TestAppointments.TestTypeID = @TestTypeID) and isLocked=0
                            ORDER BY TestAppointments.TestAppointmentID desc";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();


                if (result != null)
                {
                    Result = true;
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

            return Result;

        }
        public static bool DoesPassTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            bool Result = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = @"SELECT TOP 1 TestResult
                     FROM TestAppointments
                     INNER JOIN Tests 
                     ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                     WHERE TestAppointments.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                     AND TestAppointments.TestTypeID = @TestTypeID
                     ORDER BY Tests.TestID DESC";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                    Result = Convert.ToBoolean(result);

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return Result;
        }

    }
}