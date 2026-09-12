using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;
namespace DataAccessLayer
{
    public class clsApplicationData
    {
        public static DataTable GetAll()
        { 
        DataTable dt = new DataTable();
            SqlConnection Conn=new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = @"select * from Applications";
            SqlCommand cmd = new SqlCommand(query, Conn);
            try
            {
                Conn.Open();
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
            finally { Conn.Close(); }
            return dt;
        }

        public static int Add(ApplicatopnDTO Application)
        { 
        SqlConnection Conn=new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = @"insert into Applications(ApplicantPersonID,ApplicationDate,ApplicationTypeID,ApplicationStatus,LastStatusDate,PaidFees,CreatedByUserID) values(@ApplicationPersonID,@ApplicationDate,@ApplicationTypeID,@ApplicationStatusID,@LastStatusDate,@PaidFees,@CreatedUserID);select SCOPE_IDENTITY();";
            SqlCommand cmd = new SqlCommand(query, Conn);
            cmd.Parameters.AddWithValue("@ApplicationPersonID", Application.ApplicationPersonID);
            cmd.Parameters.AddWithValue("@ApplicationDate", Application.ApplicationDate);
            cmd.Parameters.AddWithValue("@ApplicationTypeID", Application.ApplicationTypeID);
            cmd.Parameters.AddWithValue("@ApplicationStatusID", Application.ApplicationStatus);
            cmd.Parameters.AddWithValue("@LastStatusDate", Application.LastStatusDate);
            cmd.Parameters.AddWithValue("@PaidFees", Application.PaidFees);
           
            cmd.Parameters.AddWithValue("@CreatedUserID", Application.CreatedUserID);
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
        public static bool Delete(int ApplicationID)
        {
            SqlConnection Conn = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = @"delete from Applications where ApplicationID=@ApplicationID";
            SqlCommand cmd = new SqlCommand(query, Conn);
            cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);
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
        public static bool Update(ApplicatopnDTO Application)
        {

            SqlConnection Conn = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = @"update Applications set ApplicantPersonID=@ApplicationPersonID,ApplicationDate=@ApplicationDate,ApplicationTypeID=@ApplicationTypeID,ApplicationStatus=@ApplicationStatusID,LastStatusDate=@LastStatusDate,PaidFees=@PaidFees,CreatedByUserID=@CreatedUserID where ApplicationID=@ApplicationID";
            SqlCommand cmd = new SqlCommand(query, Conn);
            cmd.Parameters.AddWithValue("@ApplicationPersonID", Application.ApplicationPersonID);
            cmd.Parameters.AddWithValue("@ApplicationDate", Application.ApplicationDate);
            cmd.Parameters.AddWithValue("@ApplicationTypeID", Application.ApplicationTypeID);
            cmd.Parameters.AddWithValue("@ApplicationStatusID", Application.ApplicationStatus);
            cmd.Parameters.AddWithValue("@LastStatusDate", Application.LastStatusDate);
            cmd.Parameters.AddWithValue("@PaidFees", Application.PaidFees);
            cmd.Parameters.AddWithValue("@CreatedUserID", Application.CreatedUserID);
            cmd.Parameters.AddWithValue("@ApplicationID", Application.ApplicationID);
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

        public static ApplicatopnDTO GetById(int ApplicationID)
        {
            SqlConnection Conn = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = @"select * from Applications where ApplicationID=@ApplicationID";
            SqlCommand cmd = new SqlCommand(query, Conn);
            cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            try
            {
                Conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    ApplicatopnDTO application = new ApplicatopnDTO
                    {
                        ApplicationID = Convert.ToInt32(reader["ApplicationID"]),
                        ApplicationPersonID = Convert.ToInt32(reader["ApplicantPersonID"]),
                        ApplicationDate = Convert.ToDateTime(reader["ApplicationDate"]),
                        ApplicationTypeID = Convert.ToInt32(reader["ApplicationTypeID"]),
                        ApplicationStatus = Convert.ToInt32(reader["ApplicationStatus"]),
                        LastStatusDate = Convert.ToDateTime(reader["LastStatusDate"]),
                        PaidFees = Convert.ToSingle(reader["PaidFees"]),
                        CreatedUserID = Convert.ToInt32(reader["CreatedByUserID"])
                    };
                    reader.Close();
                    return application;
                }
                reader.Close();
                return null;
            }
            catch (Exception)
            {
                throw;
            }
            finally { Conn.Close(); }
        }

        public static bool UpdateStatus(int ApplicationID, int StatusID)
        { 
        SqlConnection Conn=new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = @"update Applications set ApplicationStatus=@ApplicationStatus,LastStatusDate=@LastStatusDate where ApplicationID=@ApplicationID";
            SqlCommand cmd = new SqlCommand(query, Conn);
            cmd.Parameters.AddWithValue("@ApplicationStatus", StatusID);
            cmd.Parameters.AddWithValue("@LastStatusDate", DateTime.Now);
            cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);
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
        
        public static bool IsApplicationExists(int ApplicationID)
        {
            SqlConnection Conn = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = @"select 1 from Applications where ApplicationID=@ApplicationID";
            SqlCommand cmd = new SqlCommand(query, Conn);
            cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            try
            {
                Conn.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally { Conn.Close(); }
        }

        public static int GetActiveApplicationID(int ApplicationID, int ApplicationType)
        { 
        SqlConnection Conn= new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = @"select ApplicationID from Applications where ApplicationID=@ApplicationID and ApplicationTypeID=@ApplicationType and ApplicationStatus=1";
            SqlCommand cmd = new SqlCommand(query, Conn);
            cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            cmd.Parameters.AddWithValue("@ApplicationType", ApplicationType);
            try
            {
                Conn.Open();
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    return Convert.ToInt32(result);
                }
                return -1; // Return -1 if no active application found
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Conn.Close();

            }

    }
        public static bool DoesPersonHaveActiveApplication(int PersonID, int ApplicationType)
        {
            return GetActiveApplicationID(PersonID, ApplicationType) != -1;
        }
        public static int GetActiveApplicationIDForLicenseClass(int PersonID, int ApplicationTypeID, int LicenseClassID)
        {
            int ActiveApplicationID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = @"SELECT ActiveApplicationID=Applications.ApplicationID  
                            From
                            Applications INNER JOIN
                            LocalDrivingLicenseApplications ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
                            WHERE ApplicantPersonID = @ApplicantPersonID 
                            and ApplicationTypeID=@ApplicationTypeID 
							and LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID
                            and ApplicationStatus=1";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();


                if (result != null && int.TryParse(result.ToString(), out int AppID))
                {
                    ActiveApplicationID = AppID;
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return ActiveApplicationID;
            }
            finally
            {
                connection.Close();
            }

            return ActiveApplicationID;
        }


    }
}
