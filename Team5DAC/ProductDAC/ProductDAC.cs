using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team5VO;

namespace Team5DAC
{
    public class ProductDAC : IDisposable
    {
        SqlConnection conn;

        public ProductDAC()
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["local"].ConnectionString);
            conn.Open();
        }

        public void Dispose()
        {
            conn.Close();
        }

        public List<ProductVO> GetProductInfo()
        {
            string sql = @"select ProductID, ProductName, ProductPrice, 
ProductUnit, Stock, SafetyStock, MainBusinessID,
ProductImage, BusinessName, Name ProductType, Description
from Product p left outer join Business b
			   on p.MainBusinessID = b.BusinessID
			   inner join CommonCode c
			   on p.ProductType = c.Code";

            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    return Helper.DataReaderMapToList<ProductVO>(cmd.ExecuteReader());
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.Message);
                return null;
            }
        }

        public List<ProductVO> GetProductInfoFilter(string ProdType)
        {
            string sql = @"select ProductID, ProductName, ProductPrice, 
ProductUnit, Stock, SafetyStock, MainBusinessID,
ProductImage, BusinessName, Name ProductType, Description
from Product p inner join Business b
			   on p.MainBusinessID = b.BusinessID
			   inner join CommonCode c
			   on p.ProductType = c.Code
where ProductType = @ProductType";

            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ProductType", ProdType);

                    return Helper.DataReaderMapToList<ProductVO>(cmd.ExecuteReader());
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.Message);
                return null;
            }
        }

        //public List<ProductVO> GetProductSearch(string prodType)
        //{

        //}


        public bool RegisterProduct(ProductVO prod)
        {
//            int iRowAffect = 0;
//            //int newProductID = 0;
//            try
//            {
//                string sqlProd = @"insert into Product(ProductName, ProductType, ProductPrice, ProductUnit,
//                    ProductImage, Description,SafetyStock, MainBusinessID, ExpirationDate, RegDate, Stock, UserID)
//                    values(@ProductName, @ProductType, @ProductPrice, @ProductUnit,
//                    @ProductImage, @Description, @SafetyStock, @MainBusinessID, @ExpirationDate, getdate(), 0, 'emp1');
//                    select @@IDENTITY";

//                using (SqlCommand cmd = new SqlCommand(sqlProd, conn))
//                {
//                    cmd.Parameters.AddWithValue("@ProductName", prod.ProductName);
//                    cmd.Parameters.AddWithValue("@ProductType", prod.ProductType);
//                    cmd.Parameters.AddWithValue("@ProductPrice", prod.ProductPrice);
//                    cmd.Parameters.AddWithValue("@ProductUnit", prod.ProductUnit);
//                    cmd.Parameters.AddWithValue("@ProductImage", prod.ProductImage);
//                    cmd.Parameters.AddWithValue("@Description", prod.Description);
//                    cmd.Parameters.AddWithValue("@SafetyStock", prod.SafetyStock);
//                    cmd.Parameters.AddWithValue("@MainBusinessID", prod.MainBusinessID);
//                    cmd.Parameters.AddWithValue("@ExpirationDate", prod.ExpirationDate);
//                    //cmd.Parameters.AddWithValue("@RegDate", prod.RegDate);

//                    iRowAffect = cmd.ExecuteNonQuery();
//                    //newProductID = Convert.ToInt32(cmd.ExecuteScalar());
//                    //cmd.Parameters.Clear();

//                    //return true;
//                }

                

//                string sqlBusProd = @"insert into BusinessProduct(BusinessID, ProductID, RegDate)
//values(@BusinessID, @ProductID, getdate())";
//                using (SqlCommand cmd = new SqlCommand(sqlBusProd, conn))
//                {

//                    cmd.Parameters.AddWithValue("@BusinessID", prod.MainBusinessID);
//                    cmd.Parameters.AddWithValue("@ProductID", prod.ProductID);

//                    iRowAffect = cmd.ExecuteNonQuery();
//                }

//                if (iRowAffect > 0)
//                    return true;
//                else
//                    return false;
//            }
//            catch (Exception err)
//            {
//                Debug.WriteLine(err.Message);
//                return false;
//            }



            SqlTransaction trans = conn.BeginTransaction();

            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.Transaction = trans;

                    cmd.CommandText = @"insert into Product(ProductName, ProductType, ProductPrice, ProductUnit,
                    ProductImage, Description,SafetyStock, MainBusinessID, ExpirationDate, RegDate)
                    values(@ProductName, @ProductType, @ProductPrice, @ProductUnit,
                    @ProductImage, @Description, @SafetyStock, @MainBusinessID, @ExpirationDate, getdate());
                    select @@IDENTITY";

                    //                    cmd.CommandText = @"insert into Product(ProductName, ProductType, ProductPrice, ProductUnit,
                    // Description,SafetyStock, MainBusinessID, ExpirationDate, RegDate)
                    //values(@ProductName, @ProductType, @ProductPrice, @ProductUnit, @Description, @SafetyStock, @MainBusinessID, @ExpirationDate,@RegDate);
                    //select @@IDENTITY";

                    cmd.Parameters.AddWithValue("@ProductName", prod.ProductName);
                    cmd.Parameters.AddWithValue("@ProductType", prod.ProductType);
                    cmd.Parameters.AddWithValue("@ProductPrice", prod.ProductPrice);
                    cmd.Parameters.AddWithValue("@ProductUnit", prod.ProductUnit);
                    cmd.Parameters.AddWithValue("@ProductImage", prod.ProductImage);
                    cmd.Parameters.AddWithValue("@Description", prod.Description);
                    cmd.Parameters.AddWithValue("@SafetyStock", prod.SafetyStock);
                    cmd.Parameters.AddWithValue("@MainBusinessID", prod.MainBusinessID);
                    cmd.Parameters.AddWithValue("@ExpirationDate", prod.ExpirationDate);

                    //cmd.ExecuteScalar()
                    int newProductID = Convert.ToInt32(cmd.ExecuteScalar());

                    cmd.Parameters.Clear();

                    if (prod.MainBusinessID > 0)
                    {
                        cmd.CommandText = @"insert into BusinessProduct(BusinessID, ProductID, RegDate)
values(@BusinessID, @ProductID, getdate())";
                        cmd.Parameters.AddWithValue("@BusinessID", prod.MainBusinessID);
                        cmd.Parameters.AddWithValue("@ProductID", newProductID);
                        //cmd.Parameters.Add("@ProductID", SqlDbType.Int);
                        //cmd.Parameters["@ProductID"].Value = prod.ProductID;


                        cmd.ExecuteNonQuery();
                    }
                    trans.Commit();
                    return true;
                }
            }
            catch (Exception err)
            {
                trans.Rollback();
                Debug.WriteLine(err.Message);
                return false;
            }
            

        }


        public bool UpdateProductInfo(ProductVO prod)
        {
            try
            {
                string sqlProd = @"update Product
                    set ProductName = @ProductName, ProductType = @ProductType, ProductUnit = @ProductUnit,
                    SafetyStock = @SafetyStock, MainBusinessID = @MainBusinessID, ProductImage = @ProductImage,
                    Description = @Description
                    where ProductID = @ProductID";

                using (SqlCommand cmd = new SqlCommand(sqlProd, conn))
                {
                    cmd.Parameters.AddWithValue("@ProductID", prod.ProductID);
                    cmd.Parameters.AddWithValue("@ProductName", prod.ProductName);
                    cmd.Parameters.AddWithValue("@ProductType", prod.ProductType);
                    cmd.Parameters.AddWithValue("@ProductUnit", prod.ProductUnit);
                    cmd.Parameters.AddWithValue("@SafetyStock", prod.SafetyStock);
                    cmd.Parameters.AddWithValue("@MainBusinessID", prod.MainBusinessID);
                    cmd.Parameters.AddWithValue("@Description", prod.Description);
                    cmd.Parameters.AddWithValue("@ProductImage", prod.ProductImage);

                    int iRowAffect = cmd.ExecuteNonQuery();

                    //return true;
                }

                string sqlBusProd = @"update BusinessProduct
                                set BusinessID = @BusinessID,
                                RegDate = getdate()
                                where ProductID = @ProductID";
                using (SqlCommand cmd = new SqlCommand(sqlBusProd, conn))
                {

                    cmd.Parameters.AddWithValue("@BusinessID", prod.MainBusinessID);
                    cmd.Parameters.AddWithValue("@ProductID", prod.ProductID);

                    int iRowAffect = cmd.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.Message);
                return false;
            }
        }


        public bool DeleteProduct(int prodID, string type)
        {
            int iRow = 0;

            

            try
            {
                if (type.Equals("원자재"))
                {
                    string sql = "delete from BusinessProduct where ProductID = @ProductID";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductID", prodID);

                        iRow = cmd.ExecuteNonQuery();
                    }

                    if (iRow > 0)
                    {
                        string sqlPP = "delete from PurchaseProduct where ProductID = @ProductID";

                        using (SqlCommand cmd = new SqlCommand(sqlPP, conn))
                        {
                            cmd.Parameters.AddWithValue("@ProductID", prodID);

                            iRow = cmd.ExecuteNonQuery();
                        }


                        string sqlP = "delete from Product where ProductID = @ProductID";

                        using (SqlCommand cmd = new SqlCommand(sqlP, conn))
                        {
                            cmd.Parameters.AddWithValue("@ProductID", prodID);

                            iRow = cmd.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    string sqlP = "delete from Product where ProductID = @ProductID";

                    using (SqlCommand cmd = new SqlCommand(sqlP, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductID", prodID);

                        iRow = cmd.ExecuteNonQuery();
                    }
                }
                return iRow > 0;
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.Message);
                return false;
            }
        }

    }
}
