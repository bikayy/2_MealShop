using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team5VO;
using Team5DAC;

namespace Team5
{
    class BusinessService
    {
        //거래처 리스트 조회
        public List<BusinessVO> GetBusinessList()
        {
            BusinessDAC db = new BusinessDAC();
            List<BusinessVO> list = db.GetBusinessList();
            db.Dispose();

            return list;
        }

        //거래처 취급품목 조회
        public List<BusinessProductVO> GetBusinessProduct(int businessID)
        {
            BusinessDAC db = new BusinessDAC();
            List<BusinessProductVO> list = db.GetBusinessProduct(businessID);
            db.Dispose();

            return list;
        }

        //거래처 상세정보 조회
        public BusinessVO GetBusinessInfo(int businessID)
        {
            BusinessDAC db = new BusinessDAC();
            BusinessVO busiVO = db.GetBusinessInfo(businessID);
            db.Dispose();

            return busiVO;
        }

        //BusinessProductSearch의 품목리스트 조회
        public List<BusinessProductSearchVO> GetProductList(int businessID)
        {
            BusinessDAC db = new BusinessDAC();
            List<BusinessProductSearchVO> list = db.GetProductList(businessID);
            db.Dispose();

            return list;
        }


        //거래처 정보 수정
        public bool UpdateBusinessInfo(BusinessVO busi, List<int> insertList, List<int> deleteList, string userID)
        {
            BusinessDAC db = new BusinessDAC();
            bool result = db.UpdateBusinessInfo(busi, insertList, deleteList, userID);
            db.Dispose();

            return result;
        }

        public bool DeleteBusiness(int busiID)
        {
            BusinessDAC db = new BusinessDAC();
            bool result = db.DeleteBusiness(busiID);
            db.Dispose();

            return result;
        }


        /*************** ▼ 거래처 등록 폼 ▼ ****************/

        //거래처 상세정보 조회

        public string RepCheck(string BusinessNumber)
        {
            BusinessDAC db = new BusinessDAC();
            string result = db.RepCheck(BusinessNumber);
            db.Dispose();

            return result;
        }

        //신규 거래처 등록(추가)하기
        public bool AddBusiness(BusinessVO busi, List<BusinessProductVO> bpList)
        {
            BusinessDAC db = new BusinessDAC();
            bool result = db.AddBusiness(busi, bpList);
            db.Dispose();

            return result;
        }

    }
}
