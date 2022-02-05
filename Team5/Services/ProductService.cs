using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team5VO;
using Team5DAC;

namespace Team5
{
    class ProductService
    {
        public List<ProductVO> GetProductInfo()
        {
            ProductDAC db = new ProductDAC();
            List<ProductVO> list = db.GetProductInfo();
            db.Dispose();

            return list;
        }

        public List<ProductVO> GetProductInfoFilter(string prodType)
        {
            ProductDAC db = new ProductDAC();
            List<ProductVO> list = db.GetProductInfoFilter(prodType);
            db.Dispose();

            return list;
        }

        //public List<ProductVO> GetProductSearch(string prodType)
        //{
        //    ProductDAC db = new ProductDAC();
        //    List<ProductVO> list = db.GetProductSearch();
        //    db.Dispose();

        //    return list;
        //}

        public bool RegisterProduct(ProductVO prod)
        {
            ProductDAC db = new ProductDAC();
            bool result = db.RegisterProduct(prod);
            db.Dispose();

            return result;
        }

        public bool UpdateProductInfo(ProductVO prod)
        {
            ProductDAC db = new ProductDAC();
            bool result = db.UpdateProductInfo(prod);
            db.Dispose();

            return result;
        }

        public bool DeleteProduct(int prodID, string type)
        {
            ProductDAC db = new ProductDAC();
            bool result = db.DeleteProduct(prodID, type);
            db.Dispose();

            return result;
        }
    }
}
