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
    public  class clsInternationalLicenseData
    {

        public static InternationalLicenseDTO GetInternationalLicenseInfoByID(int InternationalLicenseID)
        {
           

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = "SELECT * FROM InternationalLicenses WHERE InternationalLicenseID = @InternationalLicenseID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {


                     InternationalLicenseDTO dto= new InternationalLicenseDTO()
                    {
                        ApplicationID = (int)reader["ApplicationID"],
                        DriverID = (int)reader["DriverID"],
                        IssuedUsingLocalLicenseID = (int)reader["IssuedUsingLocalLicenseID"],
                        IssueDate = (DateTime)reader["IssueDate"],
                        ExpirationDate = (DateTime)reader["ExpirationDate"],


                        IsActive = (bool)reader["IsActive"],
                        CreatedByUserID = (int)reader["DriverID"],
                    };
                    reader.Close();
                    return dto;
                }
              
              

            }
            catch (Exception ex)
            {
    
               
            }
            finally
            {
                connection.Close();
            }

            return null;
        }
        public static DataTable GetAllInternationalLicenses()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = @"
            SELECT    InternationalLicenseID, ApplicationID,DriverID,
		                IssuedUsingLocalLicenseID , IssueDate, 
                        ExpirationDate, IsActive
		    from InternationalLicenses 
                order by IsActive, ExpirationDate desc";

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
        public static DataTable GetDriverInternationalLicenses(int DriverID)
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = @"
            SELECT    InternationalLicenseID, ApplicationID,
		                IssuedUsingLocalLicenseID , IssueDate, 
                        ExpirationDate, IsActive
		    from InternationalLicenses where DriverID=@DriverID
                order by ExpirationDate desc";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

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
        public static int AddNewInternationalLicense(InternationalLicenseDTO internationalLicense)
        {
            int InternationalLicenseID = -1; 

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = @"
                               Update InternationalLicenses 
                               set IsActive=0
                               where DriverID=@DriverID;

                             INSERT INTO InternationalLicenses
                               (
                                ApplicationID,
                                DriverID,
                                IssuedUsingLocalLicenseID,
                                IssueDate,
                                ExpirationDate,
                                IsActive,
                                CreatedByUserID)
                         VALUES
                               (@ApplicationID,
                                @DriverID,
                                @IssuedUsingLocalLicenseID,
                                @IssueDate,
                                @ExpirationDate,
                                @IsActive,
                                @CreatedByUserID);
                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", internationalLicense.ApplicationID);
            command.Parameters.AddWithValue("@DriverID", internationalLicense.DriverID);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", internationalLicense.IssuedUsingLocalLicenseID);
            command.Parameters.AddWithValue("@IssueDate", internationalLicense.IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", internationalLicense.ExpirationDate);

            command.Parameters.AddWithValue("@IsActive", internationalLicense.IsActive);
            command.Parameters.AddWithValue("@CreatedByUserID", internationalLicense.CreatedByUserID);



            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    InternationalLicenseID = insertedID;
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


            return InternationalLicenseID;

        }

        public static bool UpdateInternationalLicense(InternationalLicenseDTO internationalLicense)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = @"UPDATE InternationalLicenses
                           SET 
                              ApplicationID=@ApplicationID,
                              DriverID = @DriverID,
                              IssuedUsingLocalLicenseID = @IssuedUsingLocalLicenseID,
                              IssueDate = @IssueDate,
                              ExpirationDate = @ExpirationDate,
                              IsActive = @IsActive,
                              CreatedByUserID = @CreatedByUserID
                         WHERE InternationalLicenseID=@InternationalLicenseID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@InternationalLicenseID", internationalLicense.InternationalLicenseID);
            command.Parameters.AddWithValue("@ApplicationID", internationalLicense.ApplicationID);
            command.Parameters.AddWithValue("@DriverID", internationalLicense.DriverID);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", internationalLicense.IssuedUsingLocalLicenseID);
            command.Parameters.AddWithValue("@IssueDate", internationalLicense.IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", internationalLicense.ExpirationDate);

            command.Parameters.AddWithValue("@IsActive", internationalLicense.IsActive);
            command.Parameters.AddWithValue("@CreatedByUserID", internationalLicense.CreatedByUserID);

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
        public static int GetActiveInternationalLicenseIDByDriverID(int DriverID)
        {
            int InternationalLicenseID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = @"  
                            SELECT Top 1 InternationalLicenseID
                            FROM InternationalLicenses 
                            where DriverID=@DriverID and GetDate() between IssueDate and ExpirationDate 
                            order by ExpirationDate Desc;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    InternationalLicenseID = insertedID;
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


            return InternationalLicenseID;
        }

    }
}
