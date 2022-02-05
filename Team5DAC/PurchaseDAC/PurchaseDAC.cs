using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;
using Team5VO;
using System.Data;

namespace Team5DAC
{
    public class PurchaseDAC : IDisposable
    {
        SqlConnection conn;

        public PurchaseDAC()
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["local"].ConnectionString);
            conn.Open();
        }
        public void Dispose()
        {
            conn.Close();
        }
        public List<PurchaseVO> GetPurchaseInfo(string dtFrom, string dtTo)
        {
            string sql = @"select PurchaseID, b.BusinessName, p.UserID, userName, convert(varchar(10),PurchaseDate, 23) PurchaseDate,convert(varchar(10),PeriodDate, 23) PeriodDate, Price, Name State, Memo
from Purchase p inner join Business b on p.BusinessID = b.BusinessID
    inner join CommonCode C on P.State = C.Code
	inner join Member M on P.UserID = M.UserID
where PurchaseDate between @dtFrom and @dtTo ORDER BY PurchaseID DESC";

            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@dtFrom", dtFrom);
                    cmd.Parameters.AddWithValue("@dtTo", dtTo);
                    return Helper.DataReaderMapToList<PurchaseVO>(cmd.ExecuteReader());
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.Message);
                return null;
            }
        }
        public List<PurchaseDetailVO> GetPurchaseDetailInfo(int purchaseID)
        {
            string sql = @"select PurchaseProductID, c.PurchaseID, c.ProductID, p.ProductName, Amount
    , SupplyPrice, isnull(Vat, 0) VAT, CancelYN
from PurchaseProduct c inner join Product p on c.ProductID = p.ProductID
where PurchaseID = @PurchaseID";

            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@PurchaseID", purchaseID);

                    return Helper.DataReaderMapToList<PurchaseDetailVO>(cmd.ExecuteReader());
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.Message);
                return null;
            }
        }
        public List<ProductVO> GetProductInfo()
        {
            string sql = @"select ProductID, ProductName, BusinessName, BusinessNumber, isnull(ProductPrice,0) ProductPrice,
ProductUnit, b.BusinessID MainBusinessID
from Product p inner join Business b
			   on p.MainBusinessID = b.BusinessID
			   inner join CommonCode c
			   on p.ProductType = c.Code WHERE p.ProductType = 'PT01'";
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
        public List<PurchaseVO> GetPurchaseAddInfo(int purchaseID)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = @"select c.PurchaseProductID, p.PurchaseID, d.ProductID, d.ProductName, b.BusinessID, b.BusinessName, c.Amount,
d.ProductUnit, d.ProductPrice, SupplyPrice, convert(varchar(10),p.PurchaseDate, 23) PurchaseDate, convert(varchar(10),p.PeriodDate, 23) PeriodDate, Name State
from Purchase p inner join PurchaseProduct c on p.PurchaseID = c.PurchaseID
					   inner join Business b on p.BusinessID = b.BusinessID
					   inner join Product d on d.ProductID = c.ProductID
					   inner join CommonCode o on p.State = o.Code WHERE p.PurchaseID = @PurchaseID";
                cmd.Parameters.AddWithValue("@PurchaseID", purchaseID);

                List<PurchaseVO> list = Helper.DataReaderMapToList<PurchaseVO>(cmd.ExecuteReader());

                if (list != null && list.Count > 0)
                    return list;
                else
                    return null;
            }
        }

        public bool DeletePurchaseProduct(PurchaseDetailVO purc)
        {
            try
            {
                string sql = "DELETE PurchaseProduct WHERE PurchaseProductID = @PurchaseProductID";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@PurchaseProductID", purc.PurchaseProductID);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

        public List<int> GetProductName(string pname)
        {
            string sql = @"SELECT P.PurchaseID
FROM PurchaseProduct P JOIN Product D ON P.ProductID = D.ProductID
WHERE D.ProductName LIKE @ProductName";

            pname = $"%{pname}%";
            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@ProductName", pname);
            List<int> list = new List<int>();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(reader.GetInt32(0));
            }

            return list;


        }
        public bool PurchaseInput(List<PurchaseDetailVO> list, string uid)
        {
            SqlTransaction trans = conn.BeginTransaction();
            try
            {
                string wereHousingin = "SP_Warehousing_IN"; //PurchaseProductID / UserID

                SqlCommand cmd = new SqlCommand(wereHousingin, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter paramName = new SqlParameter("@PurchaseProductID", SqlDbType.Int);
                cmd.Parameters.Add(paramName);
                cmd.Parameters.AddWithValue("@UserID", uid);
                cmd.Parameters.AddWithValue("@PeriodDate", SqlDbType.DateTime);
                cmd.Transaction = trans;


                foreach (var purc in list)
                {
                    cmd.Parameters["@PurchaseProductID"].Value = purc.PurchaseProductID;
                    cmd.ExecuteNonQuery();
                }

                trans.Commit();

                cmd.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                return false;
                throw ex;
            }
        }
        public bool PurchaseCancel(int purchaseID, string uid)
        {
            SqlTransaction trans = conn.BeginTransaction();
            try
            {
                string cancelPurchase = "SP_CancelPurchase"; //PurchaseProductID / UserID

                SqlCommand cmd = new SqlCommand(cancelPurchase, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter paramName = new SqlParameter("@PurchaseID", purchaseID);
                cmd.Parameters.Add(paramName);
                cmd.Parameters.AddWithValue("@LastUserID", uid);
                cmd.Transaction = trans;


                //foreach (var purc in list)
                //{
                    cmd.Parameters["@PurchaseID"].Value = purchaseID;
                    cmd.ExecuteNonQuery();
                //}

                trans.Commit();

                cmd.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                return false;
                throw ex;
            }
        }
        public bool AddPurchase(List<PurchaseVO> list, List<PurchaseDetailVO> list2, string uid)
        {
            SqlTransaction trans = conn.BeginTransaction();
            try
            {
                string insertPurchase = "SP_InsertPurchase"; //BusinessID, UserID, Price, PeriodDate, Memo
                string insertPurchaseProduct = "SP_InsertPurchaseProduct";//PurchaseID, ProductID, Amount, UserID, SupplyPrice

                SqlCommand cmd = new SqlCommand(insertPurchase, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PurchaseID", list[0].PurchaseID);
                cmd.Parameters.AddWithValue("@BusinessID",list[0].BusinessID);
                cmd.Parameters.AddWithValue("@UserID", uid);
                cmd.Parameters.AddWithValue("@Price", list.Sum(w => w.Price));
                cmd.Parameters.AddWithValue("@PeriodDate", list[0].PeriodDate);
                cmd.Parameters.AddWithValue("@Memo", list[0].Memo);
                cmd.Transaction = trans;

                int purchaseID = Convert.ToInt32(cmd.ExecuteScalar());

                SqlCommand cmd2 = new SqlCommand(insertPurchaseProduct, conn);
                cmd2.CommandType = System.Data.CommandType.StoredProcedure;
                cmd2.Parameters.Add(new SqlParameter("@PurchaseID", SqlDbType.Int));
                cmd2.Parameters.Add(new SqlParameter("@ProductID", System.Data.SqlDbType.Int));
                cmd2.Parameters.Add(new SqlParameter("@Amount", System.Data.SqlDbType.Int));
                cmd2.Parameters.AddWithValue("@UserID", uid);
                cmd2.Parameters.Add(new SqlParameter("@SupplyPrice", System.Data.SqlDbType.Int));
                cmd2.Transaction = trans;

                foreach (var proc in list2)
                {
                    cmd2.Parameters["@PurchaseID"].Value = purchaseID;
                    cmd2.Parameters["@ProductID"].Value = proc.ProductID;
                    cmd2.Parameters["@Amount"].Value = proc.Amount;
                    cmd2.Parameters["@SupplyPrice"].Value = proc.SupplyPrice;
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
        public bool UpdatePurchase(PurchaseVO purc)
        {
            try
            {
                string sql = @"UPDATE Purchase SET PurchaseDate = GETDATE(), Price = @Price, Memo = @Memo, 
                                LastUserID = @UserID WHERE PurchaseID = @PurchaseID";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@PurchaseID", purc.PurchaseID);
                cmd.Parameters.AddWithValue("@UserID", purc.LastUserID);
                if (purc.Memo == null)
                    purc.Memo = ".";
                cmd.Parameters.AddWithValue("@Memo", purc.Memo);

                cmd.Parameters.AddWithValue("@Price", purc.Price);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }
        public bool UpdatePurchaseProduct(PurchaseDetailVO purc)
        {
            try
            {
                string sql = @"UPDATE PurchaseProduct SET Amount = @Amount, SupplyPrice = @SupplyPrice, LastRegDate = GETDATE(), 
LastUserID = @UserID WHERE PurchaseProductID = @PurchaseProductID";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@PurchaseProductID", purc.PurchaseProductID);
                cmd.Parameters.AddWithValue("@ProductID", purc.ProductID);
                cmd.Parameters.AddWithValue("@Amount", purc.Amount);
                cmd.Parameters.AddWithValue("@SupplyPrice", purc.SupplyPrice);
                cmd.Parameters.AddWithValue("@UserID", purc.LastUserID);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }
        public bool AddPurchaseProduct(PurchaseDetailVO purc)
        {
            try
            {
                string sql = @"INSERT INTO PurchaseProduct (PurchaseID, ProductID, Amount, SupplyPrice, CancelYN, RegDate, UserID)
	VALUES (@PurchaseID,@ProductID,@Amount,@SupplyPrice,'N',GETDATE(), @UserID)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@PurchaseID", purc.PurchaseID);
                cmd.Parameters.AddWithValue("@ProductID", purc.ProductID);
                cmd.Parameters.AddWithValue("@Amount", purc.Amount);
                cmd.Parameters.AddWithValue("@SupplyPrice", purc.SupplyPrice);
                cmd.Parameters.AddWithValue("@UserID", purc.UserID);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

        public List<ForExcelInfo> ForExcel(int purID)
        {
            string sql = @"select p.PurchaseID, p.BusinessID, BusinessNumber, BusinessName, b.Phone
, pp.ProductID,  
convert(varchar(10),PeriodDate, 23) PeriodDate, pd.ProductName, pd.ProductUnit
, pp.Amount, pp.SupplyPrice, pp.Vat
from Purchase p
inner join PurchaseProduct pp on p.PurchaseID = pp.PurchaseID
inner join Business b on p.BusinessID = b.BusinessID
inner join Product pd on pp.ProductID = pd.ProductID
where p.PurchaseID = @PurchaseID";

            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@PurchaseID", purID);
                    return Helper.DataReaderMapToList<ForExcelInfo>(cmd.ExecuteReader());
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.Message);
                return null;
            }
        }

    }
}
