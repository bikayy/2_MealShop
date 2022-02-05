using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team5VO
{
    public class PurchaseVO
    {
        //PurchaseID,BusinessID,BusinessName,UserID,UserName,PurchaseDate,PeriodDate,Price,State
        public int PurchaseID { get; set; }
        public int PurchaseProductID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int BusinessID { get; set; }
        public string BusinessName { get; set; }
        public int Amount { get; set; }
        public int SupplyPrice { get; set; }
        public string ProductUnit { get; set; }
        public int ProductPrice { get; set; }
        public string UserID { get; set; }
        public string LastUserID { get; set; }
        public string UserName { get; set; }
        public string PurchaseDate { get; set; }
        public string PeriodDate { get; set; }
        public int Price { get; set; }
        public string State { get; set; }
        public string Memo { get; set; }
        //public string InputDate { get; set; }
    }

    public class PurchaseDetailVO
    {
        //PurchaseProductID, c.PurchaseID, c.ProductID, p.ProductName, Amount, SupplyPrice, Vat, CancelYN

        public int PurchaseProductID { get; set; }
        public int PurchaseID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Amount { get; set; }
        public int SupplyPrice { get; set; }
        public int Vat { get; set; }
        public string UserID { get; set; }
        public string LastUserID { get; set; }
        public string RegDate { get; set; }
        public string LastRegDate { get; set; }
        public string CancelYN { get; set; }           
    }


    public class ForExcelInfo
    {
        public int PurchaseID { get; set; }
        public int BusinessID { get; set; }
        public string BusinessNumber { get; set; }
        public string BusinessName { get; set; }
        public string Phone { get; set; }
        public int ProductID { get; set; }
        public string PeriodDate { get; set; }
        public string ProductName { get; set; }
        public string ProductUnit { get; set; }
        public int Amount { get; set; }
        public int SupplyPrice { get; set; }
        public int Vat { get; set; }
    }


}
