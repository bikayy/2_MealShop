using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using Team5VO.GwonheeVO;

namespace Team5DAC.GwonheeDAC
{
    public class ProductDAC : IDisposable
    {
        SqlConnection conn;

        public ProductDAC()
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["local"].ConnectionString);
            conn.Open();
        }

        public List<ProductForCustomer> GetProductList() //반제품 완제품 상품 정보를 다 가져옴
        {
            string sql = @"SELECT ProductID, ProductType, ProductName, ProductPrice, ProductUnit, ProductImage, Description,ExpirationDate
                            FROM Product WHERE ProductType in ('PT02','PT03');";
            List<ProductForCustomer> list;
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                list = Helper.DataReaderMapToList<ProductForCustomer>(cmd.ExecuteReader());
            }

            return list;
        }

        public List<string> GetLowProductName(int productId) // BOM에 있는 하위품목 이름이 나옴
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            cmd.CommandText = "GetLowProductName";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProductID", productId);

            List<string> list = new List<string>();

            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(reader.GetString(0));
            }
            reader.Close();
            cmd.Dispose();

            return list;
        }

        public bool AddToCart(ProductForAddToCart info) // Cart에 추가
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "AddToCart";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserID", info.UserID);
            cmd.Parameters.AddWithValue("@ProductID", info.ProductID);
            cmd.Parameters.AddWithValue("@ProductQty", info.ProductQty);

            return cmd.ExecuteNonQuery() > 0;
        }

        public List<ProductForCart> GetProductInCart(string uID) //장바구니에 담긴 제품가져오기
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SP_GetProductInCart";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserID", uID);

            return Helper.DataReaderMapToList<ProductForCart>(cmd.ExecuteReader());
        }
        public void RandomPrice(List<ProductForRandom> list)
        {
            string sql = "UPDATE Product SET ProductPrice = @ProductPrice WHERE ProductID = @ProductID";

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = sql;
                Random random = new Random();

                cmd.Parameters.Add("@ProductPrice", System.Data.SqlDbType.Int);
                cmd.Parameters.Add("@ProductID", System.Data.SqlDbType.Int);
                foreach (var i in list)
                {
                    int price = random.Next(i.Start, i.End);

                    cmd.Parameters["@ProductID"].Value = i.ProductID;
                    cmd.Parameters["@ProductPrice"].Value = price;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteCartItemList(List<int> pidList,string uid)
        {
            string sql = @"DELETE FROM Cart WHERE UserID=@UserID AND ProductID = @ProductID";

            using(SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = sql;

                cmd.Parameters.AddWithValue("@UserID", uid);
                cmd.Parameters.Add("@ProductID", System.Data.SqlDbType.Int);

                foreach(var i in pidList)
                {
                    cmd.Parameters["@ProductID"].Value = i;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateCartQty(List<ProductForOrder> list,string uid)
        {
            string sql = "Update Cart Set ProductQty  = @ProductQty WHERE UserID = @UserID AND ProductID=@ProductID";

            using(SqlCommand cmd = new SqlCommand(sql,conn))
            {
                cmd.Parameters.AddWithValue("@UserID", uid);
                cmd.Parameters.Add("@ProductQty", System.Data.SqlDbType.Int);
                cmd.Parameters.Add("@ProductID", System.Data.SqlDbType.Int);

                foreach(var i in list)
                {
                    cmd.Parameters["@ProductQty"].Value = i.Amount;
                    cmd.Parameters["@ProductID"].Value = i.ProductID;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public bool OrderProducts(List<ProductForOrder>list,string uid,int totalPrice)
        {
            SqlTransaction trans = conn.BeginTransaction();
            try
            {
                string insertOrderT = "SP_InsertOrder_T";//UserID / TotalPrice
                string insertOrderProduct = "SP_InsertOrderProduct"; //OrderID / ProductID / Amount

                SqlCommand cmd = new SqlCommand(insertOrderT, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID",uid);
                cmd.Parameters.AddWithValue("@TotalPrice", totalPrice);
                cmd.Transaction = trans;

                int newOrderID = Convert.ToInt32(cmd.ExecuteScalar());

                SqlCommand cmd2 = new SqlCommand(insertOrderProduct, conn);
                cmd2.CommandType = System.Data.CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@OrderID", newOrderID);
                cmd2.Parameters.Add("@ProductID", System.Data.SqlDbType.Int);
                cmd2.Parameters.Add("@Amount", System.Data.SqlDbType.Int);
                cmd2.Transaction = trans;

                foreach(var i in list)
                {
                    cmd2.Parameters["@ProductID"].Value = i.ProductID;
                    cmd2.Parameters["@Amount"].Value = i.Amount;
                    cmd2.ExecuteNonQuery();
                }

                trans.Commit();

                cmd.Dispose();
                cmd2.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                return false;
                throw ex;
            }
        }
        public List<ProductForCart> Ohhahaha(int orderID)
        {
            string sql = @"SELECT  op.ProductID, ProductName, Price / Amount  AS ProductPrice , Amount AS ProductQty, ProductUnit, ProductImage, (select ReciveCreatedDate from Order_T o where o.OrderID=1)OrderDate
                            FROM OrderProduct op JOIN Product P ON op.ProductID = P.ProductID
                            WHERE OrderID = @OrderID";
            using(SqlCommand cmd = new SqlCommand(sql,conn))
            {
                cmd.Parameters.AddWithValue("@OrderID", orderID);

                return Helper.DataReaderMapToList<ProductForCart>(cmd.ExecuteReader());
            }
        }
        public void ResetCart(string uid)
        {
            string sql = "DELETE FROM Cart WHERE UserID=@UserID;";

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = sql;

                cmd.Parameters.AddWithValue("@UserID", uid);

                cmd.ExecuteNonQuery();
            }
        }

        public List<OrderProducts> GetOrderProductsList(string uid)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "SP_GetOrderedInfo";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", uid);

                var reader = cmd.ExecuteReader();

                List<OrderProducts> list =new List<OrderProducts>();

                while (reader.Read())
                {
                    OrderProducts info = new OrderProducts()
                    {
                        OrderID = Convert.ToInt32(reader["OrderID"]),
                        ProductNames = reader["ProductNames"].ToString(),
                        TotalPrice = Convert.ToInt32(reader["TotalPrice"]),
                        Address = reader["Address"].ToString(),
                        State = (reader["State"].ToString().Equals("ST01")) ? "준비중" :
                                (reader["State"].ToString().Equals("ST02")) ? "배송중" : "배송완료"
                    };
                    list.Add(info);
                }
                return list;
            }
        }
        public bool CartDeleteAt(int orderID)
        {
            string sql = @"DELETE FROM OrderProduct 
                            WHERE OrderID = @OrderID 

                            DELETE FROM Order_T 
                            WHERE OrderID = @OrderID";
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = sql;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@OrderID", orderID);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public void GoodBye()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SP_GoodBye";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.ExecuteNonQuery();
        }

        //-------------------------------------------------------------------------------------

        public List<OrderT> GetOrderT(DateTime from, DateTime to)
        {
            
            string sql = @"SELECT OrderID, UserID, OrderDate, C.Name AS State , CONCAT(Address1,' ',Address2) AS Address , 
                            TotalPrice, ReciveCreatedDate, ReciveLastUpdate, Memo
                        FROM Order_T O JOIN CommonCode C ON O.State = C.Code
                        WHERE OrderDate BETWEEN @From AND @To
                        ORDER BY OrderID DESC";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@From", from);
            cmd.Parameters.AddWithValue("@To", to);

            List<OrderT> list = Helper.DataReaderMapToList<OrderT>(cmd.ExecuteReader());
            cmd.Dispose();
            return list;
        }

        public List<int> GetOrderT_WherePname(string pname)
        {
            string sql = @"SELECT O.OrderID
                        FROM OrderProduct O JOIN Product P ON O.ProductID = P.ProductID
                        WHERE P.ProductName LIKE @ProductName";

            pname = $"%{pname}%";
            SqlCommand cmd = new SqlCommand(sql,conn);
            
            cmd.Parameters.AddWithValue("@ProductName", pname);
            List<int> list = new List<int>();
            var reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                list.Add(reader.GetInt32(0));
            }

            return list;
    

        }

        public List<OrderProduct> PiGonHa(int orderID)
        {
            string sql = @"SELECT P.ProductName , O.Amount , (O.Price / O.Amount) AS Price , O.Price AS TotalPrice
                        FROM OrderProduct O JOIN Product P ON O.ProductID = P.ProductID
                        WHERE OrderID=@OrderID";
            using(SqlCommand cmd =new SqlCommand(sql,conn))
            {
                cmd.Parameters.AddWithValue("@OrderID", orderID);

                return Helper.DataReaderMapToList<OrderProduct>(cmd.ExecuteReader());
            }
        }
        public int FINISH(int orderID)
        {
            using(SqlCommand cmd =new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "CheckAmount";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrderID", orderID);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public bool REALRINISH(int orderID)
        {
            SqlTransaction trans = conn.BeginTransaction();
            try
            {
                string sql = @" UPDATE Order_T
                              SET State ='ST02' ,ReciveLastUpdate = GETDATE()
                              WHERE OrderID = @OrderID";
                string sql2 = "SP_Out";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Transaction = trans;
                cmd.Parameters.AddWithValue("@OrderID", orderID);
                
                cmd.ExecuteNonQuery();

                cmd.CommandText = sql2;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.ExecuteNonQuery();

                trans.Commit();
                return true;

            }
            catch (Exception ex)
            {
                trans.Rollback();
                return false;
                throw ex;
            }
        }
        public void Dispose()
        {
            conn.Close();
        }
    }
}
