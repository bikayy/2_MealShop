using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team5.Services;
namespace GoodBye
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                LogWorker.WriteLog("폐기 프로그램 실행");
                ItemService srv = new ItemService();
                srv.GoodBye();
            }
            catch(Exception ex)
            {
                LogWorker.WriteErrorLog(ex.Message);
            }
        }
    }
}
