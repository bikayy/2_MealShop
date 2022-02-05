using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team5VO;
using Team5DAC;

namespace Team5
{
    class PurchaseService
    {
        public List<PurchaseVO> GetPurchaseInfo(string dtFrom, string dtTo)
        {
            PurchaseDAC db = new PurchaseDAC();
            List<PurchaseVO> list = db.GetPurchaseInfo(dtFrom, dtTo);
            db.Dispose();

            return list;
        }
        public List<PurchaseDetailVO> GetPurchaseDetailInfo(int purchaseID)
        {
            PurchaseDAC db = new PurchaseDAC();
            List<PurchaseDetailVO> list = db.GetPurchaseDetailInfo(purchaseID);
            db.Dispose();

            return list;
        }
        public List<ProductVO> GetProductInfo()
        {
            PurchaseDAC db = new PurchaseDAC();
            List<ProductVO> list = db.GetProductInfo();
            db.Dispose();

            return list;
        }
        public List<PurchaseVO> GetPurchaseAddInfo(int purchaseID)
        {
            PurchaseDAC db = new PurchaseDAC();
            List<PurchaseVO> list = db.GetPurchaseAddInfo(purchaseID);
            db.Dispose();

            return list;
        }
        public List<int> GetProductName(string pname)
        {
            PurchaseDAC db = new PurchaseDAC();
            var result = db.GetProductName(pname);
            db.Dispose();

            return result;
        }
        public bool PurchaseInput(List<PurchaseDetailVO> purc)
        {
            PurchaseDAC db = new PurchaseDAC();
            bool list = db.PurchaseInput(purc, CurrentLoginID.LoginID);
            db.Dispose();

            return list;
        }
        public bool PurchaseCancel(int purchaseID)
        {
            PurchaseDAC db = new PurchaseDAC();
            bool list = db.PurchaseCancel(purchaseID, CurrentLoginID.LoginID);
            db.Dispose();

            return list;
        }
        public bool AddPurchase(List<PurchaseVO> purc, List<PurchaseDetailVO> deta)
        {
            PurchaseDAC db = new PurchaseDAC();
            bool list = db.AddPurchase(purc, deta, CurrentLoginID.LoginID);
            db.Dispose();

            return list;
        }
        public bool UpdatePurchase(PurchaseVO purc)
        {
            PurchaseDAC db = new PurchaseDAC();
            purc.LastUserID = CurrentLoginID.LoginID;
            bool list = db.UpdatePurchase(purc);
            db.Dispose();

            return list;
        }
        public bool UpdatePurchaseProduct(PurchaseDetailVO purc)
        {
            PurchaseDAC db = new PurchaseDAC();
            purc.LastUserID = CurrentLoginID.LoginID;
            bool list = db.UpdatePurchaseProduct(purc);
            db.Dispose();

            return list;
        }
        public bool AddPurchaseProduct(PurchaseDetailVO purc)
        {
            PurchaseDAC db = new PurchaseDAC();
            purc.UserID = CurrentLoginID.LoginID;
            bool list = db.AddPurchaseProduct(purc);
            db.Dispose();

            return list;
        }

        public bool DeletePurchaseProduct(PurchaseDetailVO purc)
        {
            PurchaseDAC db = new PurchaseDAC();
            purc.UserID = CurrentLoginID.LoginID;
            bool list = db.DeletePurchaseProduct(purc);
            db.Dispose();

            return list;
        }

        public List<ForExcelInfo> ForExcel(int purID)
        {
            PurchaseDAC db = new PurchaseDAC();
            List<ForExcelInfo> list = db.ForExcel(purID);
            db.Dispose();

            return list;
        }

    }
}
