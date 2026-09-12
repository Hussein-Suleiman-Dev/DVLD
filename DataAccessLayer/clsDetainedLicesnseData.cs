using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataAccessLayer
{
    public class clsDetainedLicesnseData
    {
        public static DetainedLicenseDTO GetDetainedLicenseInfoByID(int DetainID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = "SELECT * FROM DetainedLicenses WHERE DetainID = @DetainID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DetainID", DetainID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    DetainedLicenseDTO dto = new DetainedLicenseDTO
                    {
                        DetainID = (int)reader["DetainID"],
                        LicenseID = (int)reader["LicenseID"],
                        DetainDate = (DateTime)reader["DetainDate"],
                        FineFees = Convert.ToSingle(reader["FineFees"]),
                        CreatedByUserID = (int)reader["CreatedByUserID"],
                        IsReleased = (bool)reader["IsReleased"],

                        ReleaseDate = reader["ReleaseDate"] == DBNull.Value
                                        ? DateTime.MaxValue
                                        : (DateTime)reader["ReleaseDate"],

                        ReleasedByUserID = reader["ReleasedByUserID"] == DBNull.Value
                                        ? -1
                                        : (int)reader["ReleasedByUserID"],

                        ReleaseApplicationID = reader["ReleaseApplicationID"] == DBNull.Value
                                        ? -1
                                        : (int)reader["ReleaseApplicationID"]
                    };

                    reader.Close();
                    return dto;
                }

                reader.Close();
            }
            catch (Exception)
            {
                // يمكنك تسجيل الخطأ هنا إذا أردت
            }
            finally
            {
                connection.Close();
            }

            return null;
        }

        public static DetainedLicenseDTO GetDetainedLicenseInfoByLicenseID(int LicenseID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = "SELECT top 1 * FROM DetainedLicenses WHERE LicenseID = @LicenseID order by DetainID desc";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);


            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    DetainedLicenseDTO dto = new DetainedLicenseDTO
                    {
                        DetainID = (int)reader["DetainID"],
                        LicenseID = (int)reader["LicenseID"],
                        DetainDate = (DateTime)reader["DetainDate"],
                        FineFees = Convert.ToSingle(reader["FineFees"]),
                        CreatedByUserID = (int)reader["CreatedByUserID"],
                        IsReleased = (bool)reader["IsReleased"],

                        ReleaseDate = reader["ReleaseDate"] == DBNull.Value
                                        ? DateTime.MaxValue
                                        : (DateTime)reader["ReleaseDate"],

                        ReleasedByUserID = reader["ReleasedByUserID"] == DBNull.Value
                                        ? -1
                                        : (int)reader["ReleasedByUserID"],

                        ReleaseApplicationID = reader["ReleaseApplicationID"] == DBNull.Value
                                        ? -1
                                        : (int)reader["ReleaseApplicationID"]
                    };

                    reader.Close();
                    return dto;
                }

                reader.Close();
            }
            catch (Exception)
            {
                // يمكنك تسجيل الخطأ هنا إذا أردت
            }
            finally
            {
                connection.Close();
            }

            return null;
        }

        public static DataTable GetAllDetainedLicenses()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = "select * from detainedLicenses_View order by IsReleased ,DetainID;";

            SqlCommand command = new SqlCommand(query, connection);

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
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dt;

        }

      


        public static int AddNewDetainedLicense(DetainedLicenseDTO detainedLicense)
        {
            int DetainID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = @"INSERT INTO dbo.DetainedLicenses
                               (LicenseID,
                               DetainDate,
                               FineFees,
                               CreatedByUserID,
                               IsReleased
                               )
                            VALUES
                               (@LicenseID,
                               @DetainDate, 
                               @FineFees, 
                               @CreatedByUserID,
                               0
                             );
                            
                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", detainedLicense.LicenseID);
            command.Parameters.AddWithValue("@DetainDate", detainedLicense. DetainDate);
            command.Parameters.AddWithValue("@FineFees", detainedLicense. FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", detainedLicense.CreatedByUserID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    DetainID = insertedID;
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


            return DetainID;

        }
        public static bool UpdateDetainedLicense(DetainedLicenseDTO detainedLicense)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = @"UPDATE dbo.DetainedLicenses
                              SET LicenseID = @LicenseID, 
                              DetainDate = @DetainDate, 
                              FineFees = @FineFees,
                              CreatedByUserID = @CreatedByUserID,   
                              WHERE DetainID=@DetainID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DetainedLicenseID", detainedLicense.DetainID);
            command.Parameters.AddWithValue("@LicenseID", detainedLicense. LicenseID);
            command.Parameters.AddWithValue("@DetainDate", detainedLicense. DetainDate);
            command.Parameters.AddWithValue("@FineFees", detainedLicense. FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", detainedLicense. CreatedByUserID);


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
        public static bool ReleaseDetainedLicense(int DetainID, int ReleasedByUserID, int ReleaseApplicationID)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = @"UPDATE dbo.DetainedLicenses
                              SET IsReleased = 1, 
                              ReleaseDate = @ReleaseDate, 
ReleasedByUserID=@ReleasedByUserID,
                              ReleaseApplicationID = @ReleaseApplicationID   
                              WHERE DetainID=@DetainID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DetainID", DetainID);
            command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
            command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
            command.Parameters.AddWithValue("@ReleaseDate", DateTime.Now);
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

        public static bool IsLicenseDetained(int LicenseID)
        {
            bool IsDetained = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = @"select IsDetained=1 
                            from detainedLicenses 
                            where 
                            LicenseID=@LicenseID 
                            and IsReleased=0;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                {
                    IsDetained = Convert.ToBoolean(result);
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


            return IsDetained;
            ;

        }
    }
}
