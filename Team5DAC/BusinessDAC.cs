using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using Team5VO;
using System.Windows.Forms;

namespace Team5DAC
{
    public class BusinessDAC : IDisposable
    {
        SqlConnection conn;

        public BusinessDAC()
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["local"].ConnectionString);
            conn.Open();
        }

        public void Dispose()
        {
            conn.Close();
        }


        //select 거래처 조회 dgv 리스트
        public List<BusinessVO> GetBusinessList()
        {
            string sql = @"SELECT b.BusinessID, BusinessName, RepName, 
    BusinessNumber, Phone, EMail, STRING_AGG(p.ProductName, ',') WITHIN GROUP(ORDER BY b.BusinessID) ProductName
FROM Business b 
    LEFT OUTER JOIN BusinessProduct bp on b.BusinessID = bp.BusinessID
    LEFT OUTER JOIN Product p on p.ProductID = bp.ProductID
GROUP BY b.BusinessID, BusinessName, RepName, BusinessNumber, Phone, EMail;";

            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    return Helper.DataReaderMapToList<BusinessVO>(cmd.ExecuteReader());
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.Message);
                return null;
            }
        }

        //select 거래처 상세 정보
        public BusinessVO GetBusinessInfo(int businessID)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = @"select BusinessID, BusinessName, RepName, 
BusinessNumber, Phone, EMail, Zipcode,
Address1, Address2, RegDate, UserID, LastRegDate, LastUserID
from Business where BusinessID = @BusinessID";
                cmd.Parameters.AddWithValue("@BusinessID", businessID);

                List<BusinessVO> list = Helper.DataReaderMapToList<BusinessVO>(cmd.ExecuteReader());

                if (list != null && list.Count > 0)
                    return list[0];
                else
                    return null;
            }
        }


        //select 거래처 상세 정보 - 취급품목 리스트(listbox)
        public List<BusinessProductVO> GetBusinessProduct(int businessID)
        {
            string sql = @"with temp as(
select BusinessID, ProductID, (CASE WHEN BusinessID = (select MainBusinessID from Product p where p.ProductID = bp.ProductID) THEN 1 else 0 END )MainBusinessID 
from BusinessProduct bp 
where BusinessID=@BusinessID
union
select MainBusinessID BusinessID, ProductID, 1 MainBusinessID
from product p
where MainBusinessID=@BusinessID and ProductType = 'PT01')
select BusinessID, t.ProductID, p.ProductName, t.MainBusinessID from temp t join product p on t.ProductID = p.ProductID
order by BusinessID, MainBusinessID desc, ProductID";

            //string sql = @"select BusinessProductID, BusinessID, bp.ProductID, p.ProductName, CASE WHEN BusinessID = MainBusinessID THEN 1 else 0 END MainBusinessID,bp.UserID, bp.RegDate, bp.LastUserID, bp.LastRegDate
            //from BusinessProduct bp join Product p on p.ProductID = bp.ProductID
            //where BusinessID = @BusinessID
            //order by MainBusinessID desc, ProductID";

            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@BusinessID", businessID);

                return Helper.DataReaderMapToList<BusinessProductVO>(cmd.ExecuteReader());
            }
        }

        //select 거래처 등록/수정 - [품목검색] 클릭 > 품목 리스트 (dgv)
        public List<BusinessProductSearchVO> GetProductList(int businessID)
        {
            string sql = @"with temp as 
(select ProductID from BusinessProduct where BusinessID=@BusinessID)

select distinct CASE WHEN t.ProductID is not null THEN 1 ELSE 0 END as Checked,
        MainBusinessID, p.ProductID, ProductName, 
        (select Name from CommonCode c where p.ProductType = c.Code )ProductType, ProductUnit
        from Product p left outer join temp t on p.ProductID = t.ProductID
		where ProductType='PT01'
		order by ProductID";

            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                    cmd.Parameters.AddWithValue("@BusinessID", businessID);

                return Helper.DataReaderMapToList<BusinessProductSearchVO>(cmd.ExecuteReader());
            }
        }
        

        //update 거래처 수정 + 취급품목 등록/삭제
        public bool UpdateBusinessInfo(BusinessVO busi, List<int> insertList, List<int> deleteList, string userID)
        {
            SqlTransaction trans = conn.BeginTransaction();
            try
            {

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;

                    //거래처 정보 수정
                    cmd.CommandText = @"Update Business SET 
	       BusinessName = @BusinessName, 
	       RepName = @RepName, 
	       BusinessNumber = @BusinessNumber, 
	       Phone = @Phone, 
	       EMail = @EMail, 
	       Zipcode = @Zipcode, 
	       Address1 = @Address1,
	       Address2 = @Address2,
           LastUserID = @LastUserID,
           LastRegDate = @LastRegDate
    where BusinessID = @BusinessID;";
                    cmd.Transaction = trans;

                    cmd.Parameters.AddWithValue("@BusinessName", busi.BusinessName);
                    cmd.Parameters.AddWithValue("@RepName", busi.RepName);
                    cmd.Parameters.AddWithValue("@BusinessNumber", busi.BusinessNumber);
                    cmd.Parameters.AddWithValue("@Phone", busi.Phone);
                    cmd.Parameters.AddWithValue("@EMail", busi.EMail);
                    cmd.Parameters.AddWithValue("@Zipcode", busi.Zipcode);
                    cmd.Parameters.AddWithValue("@Address1", busi.Address1);
                    cmd.Parameters.AddWithValue("@Address2", busi.Address2);
                    cmd.Parameters.AddWithValue("@BusinessID", busi.BusinessID);
                    cmd.Parameters.AddWithValue("@LastUserID", userID); 
                    cmd.Parameters.AddWithValue("@LastRegDate", DateTime.Now);

                    int iRowAffect = cmd.ExecuteNonQuery();

                    //insert 추가 - 거래처취급품목 
                    if (insertList.Count > 0)
                    {
                        cmd.CommandText = "INSERT INTO BusinessProduct(BusinessID, ProductID, UserID) VALUEs (@BusinessID, @InsProductID, @UserID);";
                        cmd.Parameters.Add("@InsProductID", SqlDbType.Int);
                        cmd.Parameters.AddWithValue("@UserID", userID);
                        foreach (int iProID in insertList)
                        {
                            cmd.Parameters["@InsProductID"].Value = iProID;
                            iRowAffect = cmd.ExecuteNonQuery();
                        }

                    }
                    //delete 삭제 - 거래처취급품목 
                    if (deleteList.Count > 0)
                    {
                        cmd.CommandText = "delete from BusinessProduct where BusinessID = @BusinessID and ProductID = @DelProductID";
                        cmd.Parameters.Add("@DelProductID", SqlDbType.Int);
                        foreach (int dProID in deleteList)
                        {
                            cmd.Parameters["@DelProductID"].Value = dProID;
                            int iRowAffect2 = cmd.ExecuteNonQuery();
                        }
                    }
                }
                trans.Commit();
                return true;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                trans.Rollback();
                return false;
            }
        }

        //select 신규 거래처 등록 - 사업자번호 중복체크
        public string RepCheck(string BusinessNumber)
        {
            try
            {
                string sql = @"select BusinessNumber from Business where BusinessNumber = @BusinessNumber;";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@BusinessNumber", BusinessNumber);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                        return ""; //사업자번호 중복
                    else
                        return BusinessNumber; //사업자번호 사용가능
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.Message);
                return null;
            }
        }


        //insert 신규 거래처 등록 + 취급품목 등록
        public bool AddBusiness(BusinessVO busi, List<BusinessProductVO> bpList)
        {

            SqlTransaction trans = conn.BeginTransaction();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.Transaction = trans;

                    //BusinessVO 1건 insert
                    cmd.CommandText = @"INSERT INTO Business(BusinessName, RepName, BusinessNumber, Phone, EMail, Zipcode, Address1, Address2, UserID)
VALUES (@BusinessName, @RepName, @BusinessNumber, @Phone, @EMail, @Zipcode, @Address1, @Address2, @UserID); select @@IDENTITY";

                    cmd.Parameters.AddWithValue("@BusinessName", busi.BusinessName);
                    cmd.Parameters.AddWithValue("@RepName", busi.RepName);
                    cmd.Parameters.AddWithValue("@BusinessNumber", busi.BusinessNumber);
                    cmd.Parameters.AddWithValue("@Phone", busi.Phone);
                    cmd.Parameters.AddWithValue("@EMail", busi.EMail);
                    cmd.Parameters.AddWithValue("@Zipcode", busi.Zipcode);
                    cmd.Parameters.AddWithValue("@Address1", busi.Address1);
                    cmd.Parameters.AddWithValue("@Address2", busi.Address2);
                    cmd.Parameters.AddWithValue("@UserID", busi.UserID);

                    int newBusinessID = Convert.ToInt32(cmd.ExecuteScalar());

                    //BusinessProductVO 여러건 insert
                    cmd.Parameters.Clear();

                    cmd.CommandText = @"INSERT INTO BusinessProduct(BusinessID, ProductID, UserID) VALUES (@BusinessID, @ProductID, @UserID);";
                    cmd.Parameters.AddWithValue("@BusinessID", newBusinessID);
                    cmd.Parameters.Add("@ProductID", SqlDbType.Int);
                    cmd.Parameters.Add("@UserID", SqlDbType.NVarChar);

                    foreach (BusinessProductVO item in bpList)
                    {
                        cmd.Parameters["@ProductID"].Value = item.ProductID;
                        cmd.Parameters["@UserID"].Value = item.UserID;

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


        //delete 거래처 취급품목 삭제 + 거래처 삭제
        public bool DeleteBusiness(int busiID)
        {
            //BusinessProduct 먼저 삭제 후 > Business 삭제 
            SqlTransaction trans = conn.BeginTransaction();
            try
            {
                int iRowAffect;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "delete from BusinessProduct where BusinessID = @BusinessID;" +
                            "update product set MainBusinessID = 0 where MainBusinessID = @BusinessID;";
                    cmd.Transaction = trans;
                    cmd.Parameters.AddWithValue("@BusinessID", busiID);
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "delete from Business where BusinessID = @BusinessID; " ;
                    iRowAffect = cmd.ExecuteNonQuery();
                    //cmd.CommandText = "update product set MainBusinessID = 0 where MainBusinessID = @BusinessID";
                    //cmd.ExecuteNonQuery();
                    
                }
                trans.Commit();
                return (iRowAffect > 0);
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.Message);
                trans.Rollback();
                return false;
            }
        }
    }
}
