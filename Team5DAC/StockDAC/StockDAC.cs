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
    public class StockDAC : IDisposable
    {
        SqlConnection conn;
        public StockDAC()
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["local"].ConnectionString);
            conn.Open();
        }
        public void Dispose()
        {
            conn.Close();
        }

        public List<StockVO> GetStockInfo()
        {
            string sql = @"select InputID, ProductID as PurchaseProductID, p.ProductName,
       p.ProductType, ProductUnit as Unit, RestStock, SafetyStock,
       b.BusinessName, Name, Stock
       , left(convert(varchar(20),InputDate, 120), 16) InputDate
       , left(convert(varchar(20),i.ExpirationDate, 120),16) ExpirationDate
       from input i right outer join Product p
       	 on i.PurchaseProductID = p.ProductID
       	 left outer join Business b
       	 on p.MainBusinessID = b.BusinessID
       	 inner join CommonCode c 
       	 on c.Code = p.ProductType";

//            string sql = @"select InputID, pur.PurchaseProductID, p.ProductName,
//p.ProductType, i.Unit, RestStock, SafetyStock,
//b.BusinessName, Name, Stock
//, left(convert(varchar(20),i.InputDate, 120), 16) InputDate
//, left(convert(varchar(20),i.ExpirationDate, 120),16) ExpirationDate
//from input i 
//inner join PurchaseProduct pur
//     on i.PurchaseProductID = pur.PurchaseProductID
//right outer join Product p
//     on pur.ProductID = p.ProductID
//     left outer join Business b
//     on p.MainBusinessID = b.BusinessID
//     inner join CommonCode c 
//     on c.Code = p.ProductType";

//            string sql = @"select ProductName, InputID, convert(varchar(10),InputDate, 23) InputDate
//,convert(varchar(10),i.ExpirationDate, 23) ExpirationDate, RestStock
//from Input i inner join Product p
//on i.PurchaseProductID = p.ProductID";

            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    return Helper.DataReaderMapToList<StockVO>(cmd.ExecuteReader());
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.Message);
                return null;
            }
        }


        public List<StockVO> GetStockChart(int prodID)
        {
            string sql = @"select PurchaseProductID, RestStock, convert(nvarchar(3), datepart(dd, InputDate)) InputDate
from Input
where CONVERT(CHAR(10), InputDate , 23)
between CONVERT(CHAR(10), DATEADD(day, -10, getdate()) , 23)
and CONVERT(CHAR(10), getdate(), 23)
and PurchaseProductID = @PurchaseProductID";


            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@PurchaseProductID", prodID);
                    return Helper.DataReaderMapToList<StockVO>(cmd.ExecuteReader());
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.Message);
                return null;
            }
        }

        public bool StockUse(StockVO stc, int amount)
        {
            SqlTransaction trans = conn.BeginTransaction();

            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.Transaction = trans;

                    cmd.CommandText = @"update Input set RestStock = (RestStock - @UseAmount) where InputID = @InputID";
                    

                    cmd.Parameters.AddWithValue("@UseAmount", amount);
                    cmd.Parameters.AddWithValue("@InputID", stc.InputID);

                    cmd.ExecuteNonQuery();

                    cmd.CommandText = @"update Product set Stock = (select sum(RestStock) 총재고량
                    from Input
                    where PurchaseProductID = @PurchaseProductID) where ProductID = @ProductID";
                    cmd.Parameters.AddWithValue("@PurchaseProductID", stc.PurchaseProductID);
                    cmd.Parameters.AddWithValue("@ProductID", stc.PurchaseProductID);

                    cmd.ExecuteNonQuery();

                    cmd.Parameters.Clear();

                    cmd.CommandText = @"insert into StockUse(InputID, ProductID, StockUseAmount, StockUseReason, RegDate)
values(@InputID, @ProductID, @StockUseAmount, (select Code from CommonCode where Name = '폐기'), getdate())";
                    cmd.Parameters.AddWithValue("@InputID", stc.InputID);
                    cmd.Parameters.AddWithValue("@ProductID", stc.PurchaseProductID);
                    cmd.Parameters.AddWithValue("@StockUseAmount", amount);

                    cmd.ExecuteNonQuery();

                }
                trans.Commit();
                return true;
            }
            catch (Exception err)
            {
                trans.Rollback();
                Debug.WriteLine(err.Message);
                return false;
            }
        }


        public List<TreeViewVO> GetProduct(string name)
        {
            string sql = @"select ProductName
from Product p inner join CommonCode c
on p.ProductType = c.Code
where Name = @Name";

            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    return Helper.DataReaderMapToList<TreeViewVO>(cmd.ExecuteReader());
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.Message);
                return null;
            }

        }


        public List<ProductionVO> GetProduction(int prodID)
        {
            string sql = @"select BomID, LowProductID, ProductName,
LowProductAmount, b.Unit,
case ProductType
when 'PT01' then '원자재'
when 'PT02' then '반제품'
else '완제품'
end ProductType
from BOM b inner join Product p
on b.LowProductID = p.ProductID
where HighProductID = @HighProductID";


            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@HighProductID", prodID);
                    return Helper.DataReaderMapToList<ProductionVO>(cmd.ExecuteReader());
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.Message);
                return null;
            }
        }

        public bool FIFO(int prodID, int amount)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SP_Fifo";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ProductID", prodID);
            cmd.Parameters.AddWithValue("@Amount", amount);

            return cmd.ExecuteNonQuery() > 0;

           
        }

        public bool ProductionProduct(ProductionProductVO pp)
        {
            SqlTransaction trans = conn.BeginTransaction();

            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.Transaction = trans;

                    cmd.CommandText = @"insert into Input(PurchaseProductID, InputDate,
ExpirationDate, SupplyPrice , RestStock, Unit, Memo)
values(@PurchaseProductID, getdate(), dateadd(day, (select ExpirationDate
from Product
where ProductID = @PurchaseProductID), getdate()), (select ProductPrice
from Product
where ProductID = @PurchaseProductID), @RestStock, @Unit, '생산');";


                    cmd.Parameters.AddWithValue("@PurchaseProductID", pp.ProductID);
                    cmd.Parameters.AddWithValue("@RestStock", pp.RestStock);
                    cmd.Parameters.AddWithValue("@Unit", pp.Unit);

                    cmd.ExecuteNonQuery();

                    cmd.CommandText = @"update Product set Stock = (select sum(RestStock) 총재고량
        from Input
        where PurchaseProductID = @ProductID) where ProductID = @ProductID";
                    cmd.Parameters.AddWithValue("@ProductID", pp.ProductID);

                    cmd.ExecuteNonQuery();

                }
                trans.Commit();
                return true;
            }
            catch (Exception err)
            {
                trans.Rollback();
                Debug.WriteLine(err.Message);
                return false;
            }
        }


    }
}
