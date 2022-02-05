using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team5VO;
using Team5DAC;

namespace Team5
{
    class CommonService
    {
        public List<ComboItemVO> GetCodeList(string[] categories)
        {
            CommonDAC db = new CommonDAC();
            List<ComboItemVO> list = db.GetCodeList(categories);
            db.Dispose();

            return list;
        }

        public List<ComboBusinessVO> GetBusinessList()
        {
            CommonDAC db = new CommonDAC();
            List<ComboBusinessVO> list = db.GetBusinessList();
            db.Dispose();

            return list;
        }
    }
}
