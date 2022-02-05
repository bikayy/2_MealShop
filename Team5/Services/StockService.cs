using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team5VO;
using Team5DAC;

namespace Team5
{
    class StockService
    {
        public List<StockVO> GetStockInfo()
        {
            StockDAC db = new StockDAC();
            List<StockVO> list = db.GetStockInfo();
            db.Dispose();

            return list;
        }

        public List<StockVO> GetStockChart(int prodID)
        {
            StockDAC db = new StockDAC();
            List<StockVO> list = db.GetStockChart(prodID);
            db.Dispose();

            return list;
        }

        public bool StockUse(StockVO stc, int amount)
        {
            StockDAC db = new StockDAC();
            bool result = db.StockUse(stc, amount);
            db.Dispose();

            return result;
        }


        public List<TreeViewVO> GetProduct(string name)
        {
            StockDAC db = new StockDAC();
            List<TreeViewVO> prodType = db.GetProduct(name);
            db.Dispose();

            return prodType;
        }

        public List<ProductionVO> GetProduction(int prodID)
        {
            StockDAC db = new StockDAC();
            List<ProductionVO> list = db.GetProduction(prodID);
            db.Dispose();

            return list;
        }


        public bool FIFO(int prodID, int amount)
        {
            StockDAC db = new StockDAC();
            bool result = db.FIFO(prodID, amount);
            db.Dispose();

            return result;
        }

        public bool ProductionProduct(ProductionProductVO pp)
        {
            StockDAC db = new StockDAC();
            bool result = db.ProductionProduct(pp);
            db.Dispose();

            return result;
        }


    }
}
