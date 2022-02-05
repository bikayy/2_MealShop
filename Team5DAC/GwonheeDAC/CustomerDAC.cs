using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team5VO;
using Team5VO.GwonheeVO;

namespace Team5DAC.GwonheeDAC
{
    public class CustomerDAC : IDisposable
    {
        SqlConnection conn;
        public CustomerDAC()
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["local"].ConnectionString);
            conn.Open();
        }
        public bool IDCheck(string id) // 아이디가 있으면 True 아이디가 없으면 False
        {
            string sql = "SELECT Count(*) FROM Member WHERE UserID=@UserID;";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@UserID", id);
            int result = Convert.ToInt32(cmd.ExecuteScalar());

            return (result > 0);
        }
        public bool AddID(CustomerVO customer) // 계정추가
        {
            string sql = @"insert into Member(UserID, UserPassword, UserName, Phone, Birth, EMail, UserZipcode, UserAddress1, UserAddress2,EmployeeYN)
                    values(@UserID, @UserPassword, @UserName, @Phone, @Birth, @EMail, @UserZipcode, @UserAddress1, @UserAddress2,@EmployeeYN)";
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@UserID", customer.UserID);
                cmd.Parameters.AddWithValue("@UserPassword", customer.UserPassword);
                cmd.Parameters.AddWithValue("@UserName", customer.UserName);
                cmd.Parameters.AddWithValue("@Phone", customer.Phone);
                cmd.Parameters.AddWithValue("@Birth", customer.Birth);
                cmd.Parameters.AddWithValue("@EMail", customer.EMail);
                cmd.Parameters.AddWithValue("@UserZipcode", customer.UserZipcode);
                cmd.Parameters.AddWithValue("@UserAddress1", customer.UserAddress1);
                cmd.Parameters.AddWithValue("@UserAddress2", customer.UserAddress2);
                cmd.Parameters.AddWithValue("@EmployeeYN", 'N');
                return 0 < cmd.ExecuteNonQuery();
            }
        }
        public bool UpdateID(CustomerVO customer) // 업데이트
        {
            string sql = @"Update Member SET UserPassword=@UserPassword , Phone=@Phone , EMail = @EMail ,Birth=@Birth , UserZipcode = @UserZipcode,
                            UserAddress1 = @UserAddress1 , UserAddress2=@UserAddress2 , LastRegDate = GETDATE()
                            WHERE UserID=@UserID";
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@UserPassword", customer.UserPassword);
                cmd.Parameters.AddWithValue("@Phone", customer.Phone);
                cmd.Parameters.AddWithValue("@EMail", customer.EMail);
                cmd.Parameters.AddWithValue("@Birth", customer.Birth);
                cmd.Parameters.AddWithValue("@UserZipcode", customer.UserZipcode);
                cmd.Parameters.AddWithValue("@UserAddress1", customer.UserAddress1);
                cmd.Parameters.AddWithValue("@UserAddress2", customer.UserAddress2);
                cmd.Parameters.AddWithValue("@UserID", customer.UserID);
                return 0 < cmd.ExecuteNonQuery();
            }
        }
        public CustomerVO GetCustomerInfo(string id)
        {
            string sql = @"SELECT UserID, UserPassword, UserName, Phone, Birth, EMail, UserZipcode, UserAddress1, UserAddress2 
                            FROM Member WHERE UserID = @UserID";
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@UserID", id);

                var temp = Helper.DataReaderMapToList<CustomerVO>(cmd.ExecuteReader());
                return temp[0];
            }

            
        }
        public bool LoginCheck(CustomerVO customer,bool IsEmp) // 로그인
        {
            string sql = @"SELECT count(*) FROM Member WHERE UserID=@UserID AND UserPassword=@UserPassword AND EmployeeYN=@EmployeeYN";

            using(SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = sql;

                cmd.Parameters.AddWithValue("@UserID", customer.UserID);
                cmd.Parameters.AddWithValue("@UserPassword", customer.UserPassword);
                cmd.Parameters.AddWithValue("@EmployeeYN", IsEmp == true ? 'Y' : 'N');

                return Convert.ToInt32(cmd.ExecuteScalar())>0;
            }
        }
        public string GetMemberName(string id)
        {
            string sql = @"SELECT UserName FROM Member WHERE UserID = @UserID";

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@UserID", id);

                return cmd.ExecuteScalar().ToString();
            }

        }
        public void Dispose()
        {
            conn.Close();
        }
    }
}
