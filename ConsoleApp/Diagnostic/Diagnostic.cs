using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp.Diagnostic
{
    static class Diagnostic
    {
        public static void GetPerformance()
        {
            string categoryName = ConfigurationManager.AppSettings["categoryName"];
            string counterName = ConfigurationManager.AppSettings["counterName"];
            string instanceName = ConfigurationManager.AppSettings["instanceName"];
            PerformanceCounter currentConnectionsCounter = new PerformanceCounter(categoryName, counterName, instanceName);
            List<string> ccList = new List<string>();
            int recordCount = 0; //每秒记录一次性能数值，累积到60次写文件，recordCount清零
            int estimateCount = 0; //防止文件过大，一个文件写入多少次后则新建文件
            int fileCount = 1; //新文件编号

            string fileName = string.Format("{0}-{1}-{2}.txt", counterName, instanceName, fileCount);

            while (true)
            {
                recordCount++;
                string cc = currentConnectionsCounter.NextValue().ToString();
                Console.WriteLine(cc);
                ccList.Add(cc); 

                //假定一条记录7字节，60条即420字节，60000条即420KB，600000条即4.2MB
                if (estimateCount >= 10000)
                {
                    fileCount++;
                    fileName = string.Format("{0}-{1}-{2}.txt", counterName, instanceName, fileCount);
                    estimateCount = 0;                    
                }

                //一分钟写一次文件
                if(recordCount >= 60)
                {
                    using (StreamWriter sw = new StreamWriter(fileName, true))
                    {
                        sw.WriteLine(DateTime.Now.ToString());
                        foreach (string s in ccList)
                        {
                            sw.WriteLine(s);
                        }
                    }
                    ccList.Clear();
                    recordCount = 0;
                    estimateCount++;
                }
                Thread.Sleep(1000);
            }
        }
    }
}
