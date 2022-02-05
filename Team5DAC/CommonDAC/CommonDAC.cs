using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team5VO;

namespace Team5DAC
{
    public class CommonDAC : IDisposable
    {
        SqlConnection conn;

        public CommonDAC()
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["local"].ConnectionString);
            conn.Open();
        }

        public void Dispose()
        {
            conn.Close();
        }

        public List<ComboItemVO> GetCodeList(string[] categories)
        {
            string category = string.Join("','", categories);

            string sql = $@"select Code, Name, Category 
from CommonCode
where category in ('{category}')";

            SqlCommand cmd = new SqlCommand(sql, conn);
            List<ComboItemVO> list;

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                list = Helper.DataReaderMapToList<ComboItemVO>(reader);

            }
            return list;
        }

        public List<ComboBusinessVO> GetBusinessList()
        {
            string sql = "select BusinessID, BusinessName from Business;";

            SqlCommand cmd = new SqlCommand(sql, conn);
            List<ComboBusinessVO> list;

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                list = Helper.DataReaderMapToList<ComboBusinessVO>(reader);

            }
            return list;
        }

    }
}
