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
    public class clsApplicationTypesData
    {

        public static DataTable GetAllAppliction()
        { 
        DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = @"select * from ApplicationTypes";
            SqlCommand cmd = new SqlCommand(query, conn);
            try
            {
                conn.Open();
                SqlDataReader Reader = cmd.ExecuteReader();
                if (Reader.HasRows)
                {
                    dt.Load(Reader);
                }

            }
            catch (Exception)
            {


            }
            finally { conn.Close(); }    
        return dt;
        }

        public static ApplicationTypeDTO GetApplicationByID(int ApplicationID)
        {
            ApplicationTypeDTO app = null;

            using (SqlConnection conn = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                string query = @"SELECT * FROM ApplicationTypes WHERE ApplicationTypeID = @ID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", ApplicationID);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            app = new ApplicationTypeDTO
                            {
                                ApplicationTypeIdDTO = (int)reader["ApplicationTypeID"],
                                ApplicationTypeNameDTO = reader["ApplicationTypeTitle"].ToString(),
                                ApplicationTypeFessDTO = Convert.ToSingle(reader["ApplicationFees"])
                            };
                        }
                    }
                }
            }

            return app;
        }
        public static int AddNew(ApplicationTypeDTO app)
        {
            int newID = -1;

            using (SqlConnection conn = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                string query = @"
        INSERT INTO ApplicationTypes (ApplicationTypeName, ApplicationTypeFees)
        VALUES (@Name, @Fees);
        SELECT SCOPE_IDENTITY();";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", app.ApplicationTypeNameDTO);
                    cmd.Parameters.AddWithValue("@Fees", app.ApplicationTypeFessDTO);

                    conn.Open();

                    object result = cmd.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int id))
                        newID = id;
                }
            }

            return newID;
        }

        public static bool Update(ApplicationTypeDTO app)
        {
            int rowsAffected = 0;

            using (SqlConnection conn = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                string query = @"UPDATE ApplicationTypes
                        SET ApplicationTypeTitle = @Name,
                            ApplicationFees = @Fees
                        WHERE ApplicationTypeID = @ID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", app.ApplicationTypeIdDTO);
                    cmd.Parameters.AddWithValue("@Name", app.ApplicationTypeNameDTO);
                    cmd.Parameters.AddWithValue("@Fees", app.ApplicationTypeFessDTO);

                    conn.Open();

                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }

            return (rowsAffected > 0);
        }
    }
}
