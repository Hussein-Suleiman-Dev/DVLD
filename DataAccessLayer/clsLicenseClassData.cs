using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Shared;
using System.IO;
namespace DataAccessLayer
{
    public class clsLicenseClassData
    {

        public static DataTable GetAllLicenseClass()
        {
        DataTable dtLicenceClass= new DataTable();
            SqlConnection Conn=new SqlConnection(clsDataAccessSetting.ConnectionString);
           
            string query = "SELECT * FROM LicenseClasses order by ClassName";
            SqlCommand cmd = new SqlCommand(query,Conn);
            try
            {
                Conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    dtLicenceClass.Load(reader);
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally { Conn.Close(); }
return dtLicenceClass;
        }

        public static LicenseClassDTO GetByID(int LicenseClassID)
        { 
        SqlConnection Conn=new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = "select * from LicenseClasses where LicenseClassID=@LicenseClassID";
            SqlCommand cmd = new SqlCommand(query, Conn);
            cmd.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            try
            {
                Conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {

                    return new LicenseClassDTO
                    {
                        LicenseClassIDDTO = Convert.ToInt32(reader["LicenseClassID"]),
                        LicenseClassNameDTO = Convert.ToString(reader["ClassName"]) ,
                        ClassFeesDTO = Convert.ToSingle(reader["ClassFees"]),
                        DescriptionDTO = Convert.ToString(reader["ClassDescription"]) ,
                        MinimumAgeDTO = Convert.ToInt32(reader["MinimumAllowedAge"]),
                        DefaultValidityPeriodDTO = Convert.ToInt32(reader["DefaultValidityLength"])
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

        public static LicenseClassDTO GetByName(string LicenseClassName)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                string query = "select * from LicenseClasses where ClassName=@ClassName";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClassName", LicenseClassName);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new LicenseClassDTO
                            {
                                LicenseClassIDDTO = Convert.ToInt32(reader["LicenseClassID"]),
                                LicenseClassNameDTO = reader["ClassName"].ToString(),
                                ClassFeesDTO = Convert.ToSingle(reader["ClassFees"]),
                                DescriptionDTO = reader["ClassDescription"].ToString(),
                                MinimumAgeDTO = Convert.ToInt32(reader["MinimumAllowedAge"]),
                                DefaultValidityPeriodDTO = Convert.ToInt32(reader["DefaultValidityLength"])
                            };
                        }
                    }
                }
            }

            return null;
        }

        public static bool UpdateLicenseClass(LicenseClassDTO licenseClass)
        {
            SqlConnection Conn = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = "update LicenseClasses set ClassName=@ClassName, ClassDescription=@ClassDescription, MinimumAllowedAge=@MinimumAllowedAge, DefaultValidityLength=@DefaultValidityLength, ClassFees=@ClassFees where LicenseClassID=@LicenseClassID";
            SqlCommand cmd = new SqlCommand(query, Conn);
            cmd.Parameters.AddWithValue("@LicenseClassID", licenseClass.LicenseClassIDDTO);
            cmd.Parameters.AddWithValue("@ClassName", licenseClass.LicenseClassNameDTO);
            cmd.Parameters.AddWithValue("@ClassDescription", licenseClass.DescriptionDTO);
            cmd.Parameters.AddWithValue("@MinimumAllowedAge", licenseClass.MinimumAgeDTO);
            cmd.Parameters.AddWithValue("@DefaultValidityLength", licenseClass.DefaultValidityPeriodDTO);
            cmd.Parameters.AddWithValue("@ClassFees", licenseClass.ClassFeesDTO);
            try
            {
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception)
            {
                throw;
            }



        }

        public static bool DeleteLicenseClass(int LicenseClassID)
        {
            SqlConnection Conn = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = "delete from LicenseClasses where LicenseClassID=@LicenseClassID";
            SqlCommand cmd = new SqlCommand(query, Conn);
            cmd.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            try
            {
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception)
            {
                throw;
            }


        }
        public static bool InsertLicenseClass(LicenseClassDTO licenseClass)
        {
            SqlConnection Conn = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = "insert into LicenseClasses (ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees) values (@ClassName, @ClassDescription, @MinimumAllowedAge, @DefaultValidityLength, @ClassFees)";
            SqlCommand cmd = new SqlCommand(query, Conn);
            cmd.Parameters.AddWithValue("@ClassName", licenseClass.LicenseClassNameDTO);
            cmd.Parameters.AddWithValue("@ClassDescription", licenseClass.DescriptionDTO);
            cmd.Parameters.AddWithValue("@MinimumAllowedAge", licenseClass.MinimumAgeDTO);
            cmd.Parameters.AddWithValue("@DefaultValidityLength", licenseClass.DefaultValidityPeriodDTO);
            cmd.Parameters.AddWithValue("@ClassFees", licenseClass.ClassFeesDTO);
            try
            {
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception)
            {
                throw;
            }

        }
}
}

