using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team5DAC.GwonheeDAC;
using Team5VO.GwonheeVO;

namespace Team5.Services
{
    public class ItemService
    {
        public List<ProductForCustomer> GetProductList() //반제품 완제품 상품 정보를 다 가져옴
        {
            ProductDAC db = new ProductDAC();
            var result = db.GetProductList();
            db.Dispose();

            return result;
        }

        public List<string> GetLowProductName(int productId)
        {
            ProductDAC db = new ProductDAC();
            var result = db.GetLowProductName(productId);
            db.Dispose();

            return result;
        }
        public bool AddToCart(ProductForAddToCart info) // Cart에 추가
        {
            ProductDAC db = new ProductDAC();
            var result = db.AddToCart(info);
            db.Dispose();

            return result;
        }
        public List<ProductForCart> GetProductInCart(string uID) //장바구니에 담긴 제품가져오기
        {
            ProductDAC db = new ProductDAC();
            var result = db.GetProductInCart(uID);
            db.Dispose();

            return result;
        }
        public void RandomPrice(List<ProductForRandom> list)
        {
            ProductDAC db = new ProductDAC();
            db.RandomPrice(list);
            db.Dispose();
        }
        public void DeleteCartItemList(List<int> pidList, string uid)
        {
            ProductDAC db = new ProductDAC();
            db.DeleteCartItemList(pidList,uid);
            db.Dispose();
        }
        public void UpdateCartQty(List<ProductForOrder> list, string uid)
        {
            ProductDAC db = new ProductDAC();
            db.UpdateCartQty(list, uid);
            db.Dispose();
        }
        public bool OrderProducts(List<ProductForOrder> list, string uid, int totalPrice)
        {
            ProductDAC db = new ProductDAC();
            var result = db.OrderProducts(list, uid,totalPrice);
            db.Dispose();

            return result;
        }
        public List<ProductForCart> Ohhahaha(int orderID)
        {
            ProductDAC db = new ProductDAC();
            var result = db.Ohhahaha(orderID);
            db.Dispose();

            return result;
        }
        public void ResetCart(string uid)
        {
            ProductDAC db = new ProductDAC();
            db.ResetCart(uid);
            db.Dispose();
        }
        public List<OrderProducts> GetOrderProductsList(string uid)
        {
            ProductDAC db = new ProductDAC();
            var result = db.GetOrderProductsList(uid);
            db.Dispose();

            return result;
        }
        public bool CartDeleteAt(int orderID)
        {
            ProductDAC db = new ProductDAC();
            var result = db.CartDeleteAt(orderID);
            db.Dispose();

            return result;
        }
        public void GoodBye()
        {
            ProductDAC db = new ProductDAC();
            db.GoodBye();
            db.Dispose();
        }

        public List<OrderT> GetOrderT(DateTime from, DateTime to)
        {
            ProductDAC db = new ProductDAC();
            var result = db.GetOrderT(from,to);
            db.Dispose();

            return result;
        }

        public List<int> GetOrderT_WherePname(string pname)
        {
            ProductDAC db = new ProductDAC();
            var result = db.GetOrderT_WherePname(pname);
            db.Dispose();

            return result;
        }
        public List<OrderProduct> PiGonHa(int orderID)
        {
            ProductDAC db = new ProductDAC();
            var result = db.PiGonHa(orderID);
            db.Dispose();

            return result;
        }

        public int FINISH(int orderID)
        {
            ProductDAC db = new ProductDAC();
            var result = db.FINISH(orderID);
            db.Dispose();

            return result;
        }

        public bool REALRINISH(int orderID)
        {
            ProductDAC db = new ProductDAC();
            var result = db.REALRINISH(orderID);
            db.Dispose();

            return result;
        }
    }
}
