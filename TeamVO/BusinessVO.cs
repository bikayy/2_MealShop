using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team5VO
{
    public class BusinessVO
    {
        public int BusinessID { get; set; }
        public string BusinessName { get; set; }

        public string ProductName { get; set; }
        public string RepName { get; set; }
        public string BusinessNumber { get; set; }
        public string Phone { get; set; }
        public string EMail { get; set; }
        public string Zipcode { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public DateTime RegDate { get; set; }
        public DateTime LastRegDate { get; set; }
        public string UserID { get; set; }
        public string LastUserID { get; set; }
    }

    public class BusinessProductVO
    {
        public int BusinessProductID { get; set; }
        public int BusinessID { get; set; }
        public int MainBusinessID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public DateTime RegDate { get; set; }
        public DateTime LastRegDate { get; set; }
        public string UserID { get; set; }
        public string LastUserID { get; set; }
    }


    public class BusinessProductSearchVO
    {
        public int ProductID { get; set; }
        public string ProductType { get; set; }
        public string ProductName { get; set; }
        public string ProductUnit { get; set; }
        public int Checked { get; set; }

        public int MainBusinessID { get; set; }

    }
}
