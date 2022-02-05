using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team5VO
{
    public class BomVO
    {
        public int BomID { get; set; }

        public int HighProductID { get; set; }
        public string HighProductName { get; set; }
        public string HighUnit { get; set; }

        public int LowProductID { get; set; }
        public string LowProductName { get; set; }
        public int LowProductAmount { get; set; }
        public string LowUnit { get; set; }


        public string Bom { get; set; }
        public string ProductType { get; set; }
        public byte[] ProductImage { get; set; }
        

        public DateTime RegDate { get; set; }

        public DateTime LastRegDate { get; set; }

        public string UserID { get; set; }

        public string LastUserID { get; set; }


    }
}
