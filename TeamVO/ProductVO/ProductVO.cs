using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team5VO
{
    public class ProductVO
    {
        //ProductID, ProductType, ProductName, ProductPrice,
        //ProductUnit, ProductImg, Description, Production, Stock,
        //SafetyStock, MainBusinessID, ExpirationDate, RegDate, 
        //UserID, LastRegDate, LastUserID

        public int ProductID { get; set; }
        public string ProductType { get; set; }
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
        public string ProductUnit { get; set; }
        //public string ProductImage { get; set; }
        public byte[] ProductImage { get; set; }
        public string Description { get; set; }
        public int Production { get; set; }
        public int Stock { get; set; }
        public int SafetyStock { get; set; }
        public int MainBusinessID { get; set; }
        public string BusinessNumber { get; set; }
        public string BusinessName { get; set; }
        public int ExpirationDate { get; set; }
        public string RegDate { get; set; }
        public string UserID { get; set; }
        public string LastRegDate { get; set; }
        public string LastUserID { get; set; }

        public int Bom { get; set; }


    }
}
