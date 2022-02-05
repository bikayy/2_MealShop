using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team5DAC.GwonheeDAC;
using Team5VO.GwonheeVO;

namespace Team5.Services
{
    class CustomerService
    {
        public bool IDCheck(string id) // 아이디가 있으면 True 아이디가 없으면 False
        {
            CustomerDAC db = new CustomerDAC();
            bool result = db.IDCheck(id);
            db.Dispose();

            return result;
        }

        public bool AddID(CustomerVO customer)
        {
            CustomerDAC db = new CustomerDAC();
            bool result = db.AddID(customer);
            db.Dispose();

            return result;
        }

        public bool UpdateID(CustomerVO customer)
        {
            CustomerDAC db = new CustomerDAC();
            bool result = db.UpdateID(customer);
            db.Dispose();

            return result;
        }

        public CustomerVO GetCustomerInfo(string id)
        {
            CustomerDAC db = new CustomerDAC();
            var result = db.GetCustomerInfo(id);
            db.Dispose();

            return result;
        }

        public bool LoginCheck(CustomerVO customer, bool IsEmp)
        {
            CustomerDAC db = new CustomerDAC();
            var result = db.LoginCheck(customer,IsEmp);
            db.Dispose();

            return result;
        }

        public string GetMemberName(string id)
        {
            CustomerDAC db = new CustomerDAC();
            var result = db.GetMemberName(id);
            db.Dispose();

            return result;
        }
    }
}
