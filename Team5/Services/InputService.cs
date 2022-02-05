using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team5DAC;
using Team5VO;

namespace Team5
{
    class InputService
    {
        public List<InputVO> GetInputInfo(string dtFrom, string dtTo)
        {
            InputDAC db = new InputDAC();
            List<InputVO> list = db.GetInputInfo(dtFrom, dtTo);
            db.Dispose();

            return list;
        }
        //public List<int> GetProductName(string pname)
        //{
        //    InputDAC db = new InputDAC();
        //    var result = db.GetProductName(pname);
        //    db.Dispose();

        //    return result;
        //}
        public List<InputDetailVO> GetInputDetailInfo(int purchaseID)
        {
            InputDAC db = new InputDAC();
            List<InputDetailVO> list = db.GetInputDetailInfo(purchaseID);
            db.Dispose();

            return list;
        }
    }
}
