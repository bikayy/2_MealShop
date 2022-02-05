using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Team5VO;

namespace Team5DAC
{
    public class BomDAC : IDisposable
    {
        SqlConnection conn;

        public BomDAC()
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["local"].ConnectionString);
            conn.Open();
        }

        //정전개 모품목 리스트
        public List<BomVO> GetForwardHighBomList()
        {
            string sql = @"select distinct ProductID as HighProductID, 
(select Name from CommonCode c where p.ProductType = c.Code )ProductType,
ProductName as HighProductName, p.ProductUnit as HighUnit,
(case when CONVERT(NVARCHAR(5),BomID) IS NULL then 'N' else 'Y' end)Bom
from BOM b right outer join Product p on HighProductID = ProductID
where ProductType != 'PT01';";

            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    return Helper.DataReaderMapToList<BomVO>(cmd.ExecuteReader());
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.Message);
                return null;
            }
        }

        //정전개 자품목 리스트
        public List<BomVO> GetForwardLowBomList()
        {
            string sql = @"declare @row int=1;
with mybom as
(
	select  HighProductID, LowProductID, p.ProductName LowProductName,
		ROW_NUMBER() over (order by @row)as idx, 
		LowProductAmount, Unit LowUnit, (select Name from CommonCode c where p.ProductType = c.Code )ProductType,
		p.ProductImage
	from bom b join Product p on b.LowProductID = p.ProductID

	union all
	select A.HighProductID, B.LowProductID, convert(nvarchar(50), ' ㄴ'+p.ProductName) as LowProductName ,
		A.idx as idx, 
		B.LowProductAmount, B.UNIT LowUnit,
		(select Name from CommonCode c where p.ProductType = c.Code )ProductType, p.ProductImage
	from mybom A join BOM B on A.LowProductID = B.HighProductID join Product p on b.LowProductID = p.ProductID
	WHERE A.ProductType='반제품'
)
select * from mybom order by idx, ProductType, LowProductID;";
            //            string sql = @"select HighProductID, LowProductID, ProductName LowProductName, LowProductAmount, Unit LowUnit,
            //    (select Name from CommonCode c where p.ProductType = c.Code )ProductType
            //from BOM b join Product p on LowProductID = ProductID";


            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    //cmd.Parameters.AddWithValue("@HighProductID", highProductID);
                    return Helper.DataReaderMapToList<BomVO>(cmd.ExecuteReader());
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.Message);
                return null;
            }
        }


        //역전개 자품목 리스트
        public List<BomVO> GetReverseLowBomList()
        {
            string sql = @"select distinct ProductID as LowProductID, 
ProductName as LowProductName, p.ProductUnit as LowUnit,
(select Name from CommonCode c where p.ProductType = c.Code )ProductType,
(case when CONVERT(NVARCHAR(5),BomID) IS NULL then 'N' else 'Y' end)Bom
from BOM b right outer join Product p on LowProductID = ProductID
where ProductType = 'PT01';";

            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    return Helper.DataReaderMapToList<BomVO>(cmd.ExecuteReader());
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.Message);
                return null;
            }
        }

        //역전개 모품목 리스트
        public List<BomVO> GetReverseHighBomList()
        {
            string sql = @"select ProductID as HighProductID, ProductName as HighProductName
, LowProductID,  LowProductAmount, b.Unit as LowUnit
,(select Name from CommonCode c where p.ProductType = c.Code )ProductType
, p.ProductImage
from BOM b  join Product p on HighProductID = ProductID
where ProductType != 'PT01';";

            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    //cmd.Parameters.AddWithValue("@HighProductID", highProductID);
                    return Helper.DataReaderMapToList<BomVO>(cmd.ExecuteReader());
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.Message);
                return null;
            }
        }

        //BOM동륵 - 모품목 상세정보
        public ProductVO GetHighProductInfo(int productID)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = @"select ProductID, 
(select Name from CommonCode c where p.ProductType = c.Code )ProductType, ProductName, ProductPrice, ProductImage,
 isNULL((select count(bomID) from Bom b where p.ProductID = b.HighProductID),0)Bom
from Product p 
where ProductID=@ProductID;";
                cmd.Parameters.AddWithValue("@ProductID", productID);

                List<ProductVO> list = Helper.DataReaderMapToList<ProductVO>(cmd.ExecuteReader());

                if (list != null && list.Count > 0)
                    return list[0];
                else
                    return null;
            }
        }

        //BOM등록 - 자품목 리스트 <
        public List<ProductVO> GetLowProductList(List<string> productType)
        {
            int a = 1;
            StringBuilder sb = new StringBuilder();
            sb.Append($@"select ProductID, 
(select Name from CommonCode c where p.ProductType = c.Code )ProductType, ProductName, ProductPrice,
ProductImage, ProductUnit, ProductImage
from Product p 
where ProductType in (@ProductType{a})"); //('PT02', 'PT01')

            if (productType.Count > 1)
            {
                sb.Remove(sb.Length - 1, 1);
                sb.Append(", @ProductType2)");
            }

            try
            {
                using (SqlCommand cmd = new SqlCommand(sb.ToString(), conn))
                {
                    cmd.Parameters.AddWithValue("@productType1", productType[0]);
                    if (productType.Count > 1)
                    {
                        cmd.Parameters.AddWithValue("@productType2", productType[1]);
                    }

                    return Helper.DataReaderMapToList<ProductVO>(cmd.ExecuteReader());
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.Message);
                return null;
            }
        }

        //BOM등록 - 복사 dgv 리스트
        public List<ProductVO> GetCopyList()
        {
            string sql = @"select ProductID, ProductName,
    (select Name from CommonCode c where p.ProductType = c.Code )ProductType
from Product p
where ProductType != 'PT01' 
    and ProductID = (select distinct HighProductID from bom where HighProductID = ProductID)";

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

        //BOM등록 - 복사 dgv > 선택복사 후 자품목 리스트
        public List<BomVO> GetCopyLowList()
        {
            string sql = @"select HighProductID, LowProductID, ProductName LowProductName, LowProductAmount, Unit LowUnit,
    (select Name from CommonCode c where p.ProductType = c.Code )ProductType, ProductImage
from BOM b join Product p on LowProductID = ProductID";

            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    //cmd.Parameters.AddWithValue("@HighProductID", highProductID);
                    return Helper.DataReaderMapToList<BomVO>(cmd.ExecuteReader());
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.Message);
                return null;
            }
        }

        //신규 BOM 등록(추가)하기
        public bool AddBom(List<BomVO> AddBomList)
        {
            string sql = @"INSERT into BOM(HighProductID, LowProductID, LowProductAmount, Unit, UserID) 
VALUES(@HighProductID, @LowProductID, @LowProductAmount, @Unit, @UserID);";

            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@HighProductID", SqlDbType.Int);
                    cmd.Parameters.Add("@LowProductID", SqlDbType.Int);
                    cmd.Parameters.Add("@LowProductAmount", SqlDbType.Int);
                    cmd.Parameters.Add("@Unit", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@UserID", SqlDbType.NVarChar);

                    foreach (BomVO item in AddBomList)
                    {
                        cmd.Parameters["@HighProductID"].Value = item.HighProductID;
                        cmd.Parameters["@LowProductID"].Value = item.LowProductID;
                        cmd.Parameters["@LowProductAmount"].Value = item.LowProductAmount;
                        cmd.Parameters["@Unit"].Value = item.LowUnit;
                        cmd.Parameters["@UserID"].Value = item.UserID;

                        int a = cmd.ExecuteNonQuery();
                    }
                    return true;
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.Message);
                return false;
            }
        }

        public int DeleteBom(int productID, int tabPage)
        {
            SqlTransaction trans = conn.BeginTransaction();
            try
            {
                int iRowAffect;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.Transaction = trans;
                    if (tabPage < 1)
                    {
                        cmd.CommandText = "delete from BOM where HighProductID = @HighProductID";
                        cmd.Parameters.AddWithValue("@HighProductID", productID);
                    }
                    else
                    {
                        cmd.CommandText = "delete from BOM where LowProductID = @LowProductID";
                        cmd.Parameters.AddWithValue("@LowProductID", productID);
                    }

                    iRowAffect = cmd.ExecuteNonQuery();
                }
                trans.Commit();
                return iRowAffect;
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.Message);
                trans.Rollback();
                return 0;
            }
        }

        //BOM등록 - 이미 등록된 동일한 자품목이 있는지 중복확인 
        public bool GetLowProductInfo(int highProductID, int lowProductID, int lowProductAmount)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"select HighProductID, LowProductID, LowProductAmount from BOM 
where HighProductID=@HighProductID 
    and LowProductID=@LowProductID 
    and LowProductAmount=@LowProductAmount;";
                    cmd.Parameters.AddWithValue("@HighProductID", highProductID);
                    cmd.Parameters.AddWithValue("@LowProductID", lowProductID);
                    cmd.Parameters.AddWithValue("@LowProductAmount", lowProductAmount);

                    int result = Convert.ToInt32(cmd.ExecuteReader());

                    return (result > 0);
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.Message);
                return false;
            }
        }


        //        public bool CheckLowProduct(int highProductID)
        //        {
        //            try
        //            {
        //                using (SqlCommand cmd = new SqlCommand())
        //                {
        //                    cmd.Connection = conn;
        //                    cmd.CommandText = @"select count(*) from BOM 
        //where HighProductID=@HighProductID and LowProductID=@LowProductID and LowProductAmount=@LowProductAmount;";
        //                    cmd.Parameters.AddWithValue("@HighProductID", highProductID);
        //                    cmd.Parameters.AddWithValue("@LowProductID", lowProductID);
        //                    cmd.Parameters.AddWithValue("@LowProductAmount", lowProductAmount);

        //                    int result = Convert.ToInt32(cmd.ExecuteReader());

        //                    return (result > 0);
        //                }
        //            }
        //            catch (Exception err)
        //            {
        //                Debug.WriteLine(err.Message);
        //                return false;
        //            }
        //        }


        public bool UpdateBom(List<BomVO> AddBomList, List<BomVO> UpdateBomList, List<BomVO> DeleteBomList)
        {
            SqlTransaction trans = conn.BeginTransaction();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.Transaction = trans;

                    //BOM정보 수정
                    if (UpdateBomList.Count > 0)
                    {
                        cmd.CommandText = @"Update BOM SET 
	           LowProductAmount = @LowProductAmount, 
	           LastUserID = @LastUserID,
		       LastRegDate = @LastRegDate
        where HighProductID = @HighProductID and LowProductID = @LowProductID;";

                        cmd.Parameters.AddWithValue("@LastRegDate", DateTime.Now);
                        cmd.Parameters.Add("@HighProductID", SqlDbType.Int); ;
                        cmd.Parameters.Add("@LowProductID", SqlDbType.Int);
                        cmd.Parameters.Add("@LowProductAmount", SqlDbType.Int);
                        cmd.Parameters.Add("@LastUserID", SqlDbType.NVarChar);

                        foreach (BomVO item in UpdateBomList)
                        {
                            cmd.Parameters["@HighProductID"].Value = item.HighProductID;
                            cmd.Parameters["@LowProductID"].Value = item.LowProductID;
                            cmd.Parameters["@LowProductAmount"].Value = item.LowProductAmount;
                            cmd.Parameters["@LastUserID"].Value = item.UserID;

                            int iRowAffect = cmd.ExecuteNonQuery();
                        }
                    }

                    //BOM정보 추가
                    if (AddBomList.Count > 0)
                    {
                        cmd.Parameters.Clear();

                        cmd.CommandText = @"INSERT INTO BOM(HighProductID, LowProductID, LowProductAmount, Unit, UserID)
VALUES (@HighProductID, @LowProductID, @LowProductAmount, @Unit, @UserID);";

                        cmd.Parameters.Add("@HighProductID", SqlDbType.Int); ;
                        cmd.Parameters.Add("@LowProductID", SqlDbType.Int);
                        cmd.Parameters.Add("@LowProductAmount", SqlDbType.Int);
                        cmd.Parameters.Add("@Unit", SqlDbType.NVarChar);
                        cmd.Parameters.Add("@UserID", SqlDbType.NVarChar);

                        foreach (BomVO item in AddBomList)
                        {
                            cmd.Parameters["@HighProductID"].Value = item.HighProductID;
                            cmd.Parameters["@LowProductID"].Value = item.LowProductID;
                            cmd.Parameters["@LowProductAmount"].Value = item.LowProductAmount;
                            cmd.Parameters["@Unit"].Value = item.LowUnit;
                            cmd.Parameters["@UserID"].Value = item.UserID;

                            int iRowAffect = cmd.ExecuteNonQuery();
                        }
                    }

                    //BOM정보 삭제
                    if (DeleteBomList.Count > 0)
                    {

                        cmd.Parameters.Clear();
                        cmd.CommandText = @"DELETE FROM BOM 
WHERE HighProductID=@HighProductID and LowProductID=@LowProductID;";

                        cmd.Parameters.Add("@HighProductID", SqlDbType.Int); ;
                        cmd.Parameters.Add("@LowProductID", SqlDbType.Int);

                        foreach (BomVO item in DeleteBomList)
                        {
                            cmd.Parameters["@HighProductID"].Value = item.HighProductID;
                            cmd.Parameters["@LowProductID"].Value = item.LowProductID;

                            int iRowAffect = cmd.ExecuteNonQuery();
                        }
                    }
                }
                trans.Commit();
                return true;
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.Message);
                trans.Rollback();
                return false;
            }

        }

        public void Dispose()
        {
            conn.Close();
        }
    }
}
