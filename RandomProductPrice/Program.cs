using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team5DAC.GwonheeDAC;
using Team5VO.GwonheeVO;

namespace RandomProductPrice
{

    class Program
    {
        static void Main(string[] args)
        {

            string[] jsonStr = File.ReadAllLines(@"ProductToJson.txt");

            List<ProductForRandom> list = new List<ProductForRandom>();

            foreach (var i in jsonStr)
            {
                list.Add(JsonConvert.DeserializeObject<ProductForRandom>(i));
            }

            ProductDAC db = new ProductDAC();

            db.RandomPrice(list);

            db.Dispose();


        }
    }
}
