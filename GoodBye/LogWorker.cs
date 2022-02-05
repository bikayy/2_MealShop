using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GoodBye
{
    public class LogWorker
    {
        public static void WriteLog(string msg)
        {
            try
            {
                StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\GoodByeLogFile.txt", true);

                sw.WriteLine(DateTime.Now.ToString() + ":" + msg);
                sw.Flush();
                sw.Close();
            }
            catch (Exception err)
            {
                WriteErrorLog(err.Message);
            }
        }

        public static void WriteErrorLog(string msg)
        {
            try
            {
                StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\GoodByeLogFile.txt", true);

                sw.WriteLine("[ERROR] " + DateTime.Now.ToString() + ":" + msg);
                sw.Flush();
                sw.Close();
            }
            catch
            {

            }
        }
    }
}
