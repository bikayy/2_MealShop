using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team5VO
{
    public class StockVO
    {
        //ProductName, InputID, InputDate,
        //i.ExpirationDate, RestStock

        //InputID, PurchaseProductID, p.ProductName,
        //p.ProductType, i.Unit, RestStock, b.BusinessName,
        //InputDate, i.ExpirationDate / StockUseAmount, StockUseReason

        public int PurchaseProductID { get; set; }
        public string ProductName { get; set; }
        public int InputID { get; set; }
        public string InputDate { get; set; }
        public string ExpirationDate { get; set; }
        public int RestStock { get; set; }
        public int SafetyStock { get; set; }
        public string ProductType { get; set; }
        public string Unit { get; set; }
        public string BusinessName { get; set; }
        public string Name { get; set; }

        public int Stock { get; set; }
        public string StockUseReason { get; set; }
    }


    public class ProductionVO
    {
        public int BomID { get; set; }
        public int LowProductID { get; set; }
        public string ProductName { get; set; }
        public int LowProductAmount { get; set; }
        public string Unit { get; set; }
        public string ProductType { get; set; }

        //BomID, LowProductID, ProductName, LowProductAmount, b.Unit
    }

    public class TreeViewVO
    {
        public string ProductName { get; set; }
    }

    public class ProductionProductVO
    {
        public int ProductID { get; set; }
        public int RestStock { get; set; }
        public string Unit { get; set; }
    }


}
