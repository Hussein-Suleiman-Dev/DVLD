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
    public class clsTestTypeData
    {

        public static DataTable GetAllTestType()
        {
            SqlConnection Conn = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = @"select * from TestTypes";
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(query, Conn);
            try
            {
                Conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch (Exception)
            {


            }
            finally { Conn.Close(); }
            return dt;

        }

        public static bool UpdateTestType(TestTypeDTO Test)
        {
            SqlConnection Connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = @"update TestTypes 
set TestTypeTitle=@Title,TestTypeDescription=@Description,TestTypeFees=@Fees
where TestTypeID=@ID
";
            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@Title",Test.Title);
            cmd.Parameters.AddWithValue("@Description",Test.Description);
            cmd.Parameters.AddWithValue("@Fees",Test.Fees);
            cmd.Parameters.AddWithValue("@ID",Test.TestTypeID);
            int RowAffected = -1;
            try
            {
                Connection.Open();

                RowAffected = cmd.ExecuteNonQuery();
                return RowAffected > 0;

            }
            catch (Exception)
            {

                return false;
            }


        }

        public static TestTypeDTO GetByID(int TestTypeID)
        {
            SqlConnection Conn = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = @"select * from TestTypes
where TestTypeID=@TestTypeID";
            SqlCommand cmd= new SqlCommand(query,Conn);
            cmd.Parameters.AddWithValue("@TestTypeID",TestTypeID);
            TestTypeDTO TestDto= new TestTypeDTO();
            try
            {
                Conn.Open();
                SqlDataReader reader= cmd.ExecuteReader();
                if(reader.Read()) 
                {

                    TestDto.TestTypeID = (int)reader["TestTypeID"];
                    TestDto.Title = (string)reader["TestTypeTitle"];
                    TestDto.Description = (string)reader["TestTypeDescription"];
                    TestDto.Fees =  Convert.ToSingle(reader["TestTypeFees"]);
                }
            }
            catch (Exception)
            {

          
            }
                return TestDto;
        }

        public static bool AddTestType(TestTypeDTO Test)
        {
            SqlConnection Connection = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = @"insert into TestTypes (TestTypeTitle, TestTypeDescription, TestTypeFees) 
values (@Title, @Description, @Fees)";
            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@Title", Test.Title);
            cmd.Parameters.AddWithValue("@Description", Test.Description);
            cmd.Parameters.AddWithValue("@Fees", Test.Fees);
            int RowAffected = -1;
            try
            {
                Connection.Open();
                RowAffected = cmd.ExecuteNonQuery();
                return RowAffected > 0;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                Connection.Close();
            }
        }



    }
}
