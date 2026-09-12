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
    public  class clsDriverData
    {
        private static string ConnectionString = clsDataAccessSetting.ConnectionString;

        // Add New Driver
        public static int AddNewDriver(int PersonID, int CreatedByUserID)
        {
            int DriverID = -1;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"Insert Into Drivers (PersonID,CreatedByUserID,CreatedDate)
                            Values (@PersonID,@CreatedByUserID,@CreatedDate);
                          
                            SELECT SCOPE_IDENTITY();";


            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    DriverID = insertedID;
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


            return DriverID;

        }
        public static DriverDTO GetDriverByPersonID(int PersonID)
        {
            DriverDTO Driver = null;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = "SELECT * FROM Drivers WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Driver = new DriverDTO
                    {
                        DriverID = (int)reader["DriverID"],
                        PersonID = (int)reader["PersonID"],
                        CreatedByUserID = (int)reader["CreatedByUserID"],
                        CreatedDate = (DateTime)reader["CreatedDate"]
                    };
                }

                reader.Close();
            }
            catch (Exception)
            {
                Driver = null;
            }
            finally
            {
                connection.Close();
            }

            return Driver;
        }
        // Update Driver
        public static bool UpdateDriver(DriverDTO driver)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = @"
               Update  Drivers  
                            set PersonID = @PersonID,
                                CreatedByUserID = @CreatedByUserID
                                where DriverID = @DriverID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    
                    command.Parameters.AddWithValue("@PersonID", driver.PersonID);
                    command.Parameters.AddWithValue("@CreatedByUserID", driver.CreatedByUserID);
                    command.Parameters.AddWithValue("@DriverID", driver.DriverID);

                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
            }

            return rowsAffected > 0;
        }

        // Get Driver By ID
        public static DriverDTO GetDriverByID(int driverID)
        {
            DriverDTO driver = null;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "SELECT * FROM Drivers WHERE DriverID = @DriverID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DriverID", driverID);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            driver = new DriverDTO
                            {
                                DriverID = (int)reader["DriverID"],
                                PersonID = (int)reader["PersonID"],
                                CreatedByUserID = (int)reader["CreatedByUserID"],
                                CreatedDate = (DateTime)reader["CreatedDate"]
                            };
                        }
                    }
                }
            }

            return driver;
        }

        // Get All Drivers
        public static DataTable GetAllDrivers()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "select * from Drivers_View";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                            dt.Load(reader);
                    }
                }
            }

            return dt;
        }
    }
}
