using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team5VO.GwonheeVO
{
    public class OrderProducts
    {
        //주문번호,상품목록,총가격 배송지
        public int OrderID { get; set; }
        public string ProductNames { get; set; }
        public int TotalPrice { get; set; }
        public string State { get; set; }
        public string Address { get; set; }
    }
    public class OrderT
    {
        public int OrderID { get; set; }
        public string UserID { get; set; }
        public DateTime OrderDate { get; set; }
        public string State { get; set; }
        public string Address { get; set; }
        public int TotalPrice { get; set; }
        public DateTime ReciveCreatedDate { get; set; }
        public DateTime ReciveLastUpdate { get; set; }
        public string Memo { get; set; }
    }

    public class OrderProduct
    {
        public int OrderID { get; set; }
        public string ProductName { get; set; }
        public int Amount { get; set; }
        public int Price { get; set; }
        public int TotalPrice { get; set; }
        //public DateTime RegDate { get; set; }
        //public DateTime LastRegDate { get; set; }
    }
    
    public class Product
    {
        public int ProductID { get; set; }
    }
    public class ProductForOrder : Product
    {
        public int ProductPrice { get; set; }
        public int Amount { get; set; }
    }
    public class ProductForAddToCart : Product
    {
        public string UserID { get; set; }
        public int ProductQty { get; set; }
    }
    public class ProductForShop : Product
    {
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
        public byte[] ProductImage { get; set; }
        public string ProductUnit { get; set; }
    }
    public class ProductForCart : Product
    {
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
        public byte[] ProductImage { get; set; }
        public int ProductQty { get; set; }

        ////영수증 떄문에 추가
        public DateTime OrderDate { get; set; }
        
        //
        public string ProductUnit { get; set; }

    }
    public class ProductForCustomer : ProductForShop
    {
        public string ProductType { get; set; }
        public string Description { get; set; }
        public int ExpirationDate { get; set; }

    }
    public class ProductForRandom
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
    }

    public class OrderInfoReceipt 
    {
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
        public int ProductQty { get; set; }
        public byte[] ProductImage { get; set; }
        public DateTime OrderDate { get; set; }

    }
}
