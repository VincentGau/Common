using System;
using System.Collections.Generic;
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
            PerformanceCounter currentConnectionsCounter = new PerformanceCounter("Web Service", "Current Connections", "_Total");
            List<string> ccList = new List<string>();
            int recordCount = 0;
            int estimateCount = 0;
            int fileCount = 1;

            string fileName = "currentConnections.txt";

            while (true)
            {
                recordCount++;
                string cc = currentConnectionsCounter.NextValue().ToString();
                Console.WriteLine(cc);
                ccList.Add(cc);                

                //假定一条记录7字节，60条即420字节，60000条即420KB，600000条即4.2MB
                if (estimateCount >= 10000)
                {
                    fileName = string.Format("currentConnections-{0}.txt", fileCount);
                    estimateCount = 0;
                    fileCount++;
                }

                //一分钟写一次文件
                if(recordCount >= 60)
                {
                    using (StreamWriter sw = new StreamWriter(fileName, true))
                    {
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
