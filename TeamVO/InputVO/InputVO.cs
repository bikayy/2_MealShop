using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team5VO
{
    public class InputVO
    {
        public int InputID { get; set; }
        public int PurchaseID { get; set; }
        public int PurchaseProductID { get; set; }
        public string ProductName { get; set; }
        public string InputDate { get; set; }
        public string ExpirationDate { get; set; }
        public string BusinessName { get; set; }
        public string State { get; set; }
        public int Vat { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int Amount { get; set; }
        public int RestStock { get; set; }
        public string ProductUnit { get; set; }

        public int SupplyPrice { get; set; }
        public string Memo { get; set; }
    }
    public class InputDetailVO
    {
        //PurchaseID, InputID, PurchaseProductID, ProductName, InputDate, ExpirationDate, SupplyPrice, Vat, Memo

        public int PurchaseID { get; set; }
        public int InputID { get; set; }
        public int PurchaseProductID { get; set; }
        public string ProductName { get; set; }
        public string InputDate { get; set; }
        public string ExpirationDate { get; set; }
        public int SupplyPrice { get; set; }
        public int Vat { get; set; }
        public string Memo { get; set; }
    }
}
