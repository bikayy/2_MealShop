using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;
using Team5VO;

namespace Team5DAC
{
    public class InputDAC : IDisposable
    {
        SqlConnection conn;

        public InputDAC()
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["local"].ConnectionString);
            conn.Open();
        }
        public void Dispose()
        {
            conn.Close();
        }

        public List<InputVO> GetInputInfo(string dtFrom, string dtTo)
        {
            string sql = @"select InputID, i.PurchaseID, i.PurchaseProductID, d.ProductName,  b.BusinessName, convert(varchar(10),i.InputDate, 23) InputDate,
convert(varchar(10), i.ExpirationDate, 23) ExpirationDate, i.Amount, i.RestStock, d.ProductUnit, i.SupplyPrice, isNull(i.Vat, 0) Vat, Name State, m.userName, i.Memo
from Input i inner join Purchase p on p.PurchaseID = i.PurchaseID
inner join Business b on p.BusinessID = b.BusinessID
inner join Member m on i.UserID = m.UserID
inner join CommonCode C on P.State = C.Code
inner join Product d 
    on i.PurchaseProductID = d.ProductID 
where i.InputDate between @dtFrom and @dtTo ORDER BY InputID DESC";

            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@dtFrom", dtFrom);
                    cmd.Parameters.AddWithValue("@dtTo", dtTo);
                    return Helper.DataReaderMapToList<InputVO>(cmd.ExecuteReader());
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.Message);
                return null;
            }
        }
        public List<InputDetailVO> GetInputDetailInfo(int purchaseID)
        {
            string sql = @"select i.PurchaseID, InputID, i.PurchaseProductID, d.ProductName, Convert(varchar(10),i.InputDate, 23) InputDate,
Convert(varchar(10), i.ExpirationDate, 23) ExpirationDate, i.SupplyPrice, isNull(i.Vat, 0) Vat, i.Memo
from Input i inner join PurchaseProduct p on i.PurchaseProductID = p.PurchaseProductID
inner join Product d on d.ProductId = i.PurchaseProductID where i.PurchaseID = @PurchaseID";

            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@PurchaseID", purchaseID);

                    return Helper.DataReaderMapToList<InputDetailVO>(cmd.ExecuteReader());
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
