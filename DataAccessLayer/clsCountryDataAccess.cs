using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsCountryDataAccess
    {

        public static DataTable GetAllCountries()
        {
            DataTable dt = new DataTable();

            string query = @"SELECT CountryID, CountryName
                             FROM Countries
                             ORDER BY CountryName";

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

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
                // ممكن تعمل Log لاحقًا
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static int GetCountryID(string NameCountry)
        {
            SqlConnection conn = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = @"select CountryID from Countries
where CountryName=@NameCountry";
            SqlCommand cmd=new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@NameCountry", NameCountry);
            int CountryID = -1;
            try
            {
                conn.Open();
                object result= cmd.ExecuteScalar();
                if (result != null)
                {
                CountryID= (int)result;
                }
              
            }
            catch (Exception)
            {

                throw;
            }


            return CountryID;
        }


        public static string GetCountryName(int CountryID)
        {
            SqlConnection Connection=new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = @"select CountryName from Countries
where CountryID=@CountryID";
            string CountryName = "";
            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@CountryID", CountryID);
            try
            {
                Connection.Open();
                object Result = cmd.ExecuteScalar();
                if (Result != null)
                {
                    CountryName = Result.ToString();
                }
                else { CountryName = ""; }

            }
            catch (Exception)
            {

                throw;
            }
            finally { Connection.Close(); }
            return CountryName;
        }



    }
}
