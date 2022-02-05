using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team5DAC;
using Team5VO;

namespace Team5
{
    class BomService
    {
        //정전개 모품목 리스트
        public List<BomVO> GetForwardHighBomList()
        {
            BomDAC db = new BomDAC();
            List<BomVO> list = db.GetForwardHighBomList();
            db.Dispose();

            return list;
        }

        //정전개 자품목 리스트
        public List<BomVO> GetForwardLowBomList()
        {
            BomDAC db = new BomDAC();
            List<BomVO> list = db.GetForwardLowBomList();
            db.Dispose();

            return list;
        }

        //역전개 모품목 리스트
        public List<BomVO> GetReverseHighBomList()
        {
            BomDAC db = new BomDAC();
            List<BomVO> list = db.GetReverseHighBomList();
            db.Dispose();

            return list;
        }

        //역전개 자품목 리스트
        public List<BomVO> GetReverseLowBomList()
        {
            BomDAC db = new BomDAC();
            List<BomVO> list = db.GetReverseLowBomList();
            db.Dispose();

            return list;
        }

        //BOM동륵 - 모품목 상세정보
        public ProductVO GetHighProductInfo(int productID)
        {
            BomDAC db = new BomDAC();
            ProductVO proVO = db.GetHighProductInfo(productID);
            db.Dispose();

            return proVO;
        }

        //BOM등록 - 자품목 리스트
        public List<ProductVO> GetLowProductList(List<string> productType)
        {
            BomDAC db = new BomDAC();
            List<ProductVO> list = db.GetLowProductList(productType);
            db.Dispose();

            return list;
        }

        //BOM등록 - 복사기능 dgv 리스트
        public List<ProductVO> GetCopyList()
        {
            BomDAC db = new BomDAC();
            List<ProductVO> list = db.GetCopyList();
            db.Dispose();

            return list;
        }


        //BOM등록 - 복사 dgv > 선택복사 후 자품목 리스트
        public List<BomVO> GetCopyLowList()
        {
            BomDAC db = new BomDAC();
            List<BomVO> list = db.GetCopyLowList();
            db.Dispose();

            return list;
        }

        //BOM 등록하기
        public bool AddBom(List<BomVO> AddBomList)
        {
            BomDAC db = new BomDAC();
            bool result = db.AddBom(AddBomList);
            db.Dispose();

            return result;
        }

        //BOM 삭제하기
        public int DeleteBom(int ProductID, int tabPage)
        {
            BomDAC db = new BomDAC();
            int result = db.DeleteBom(ProductID, tabPage);
            db.Dispose();

            return result;
        }

        public bool UpdateBom(List<BomVO> AddBomList, List<BomVO> UpdateBomList, List<BomVO> DeleteBomList)
        {
            BomDAC db = new BomDAC();
            bool result = db.UpdateBom(AddBomList, UpdateBomList, DeleteBomList);
            db.Dispose();

            return result;
        }

        //BOM동록 - 자품목 상세정보
        public bool GetLowProductInfo(int highProductID, int lowProductID, int lowProductAmount)
        {
            BomDAC db = new BomDAC();
            bool result = db.GetLowProductInfo(highProductID, lowProductID, lowProductAmount);
            db.Dispose();

            return result;
        }
    }
}
