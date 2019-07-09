using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Common.Security;

namespace TestCommon
{
    class Program
    {
        static void Main(string[] args)
        {
            //Logger.Info("lalala");
            //Console.WriteLine(Validation.isPhoneNo("123") );
            //ConfigHelper.GetIntValue("kk");
            //var a = StringHelper.GetListFromString("123");
            //Console.WriteLine(CheckDigest.GetFileMD5(@"d:\test.txt")); 

            Console.WriteLine(Validation.IsDateTime("2018/01/01"));
            Console.WriteLine(Validation.IsDateTime("2018-01-01"));
            Console.WriteLine(Validation.IsDateTime("20180101"));
            
            
            //powershell.exe./ 123.ps1

            //123 > ./ 123.txt

            string batPath = @"C:\Users\Haku\Desktop\123.bat";
            Process pro = new Process();
            FileInfo file = new FileInfo(batPath);
            pro.StartInfo.WorkingDirectory = file.Directory.FullName;
            pro.StartInfo.FileName = batPath;
            pro.Start();
            pro.WaitForExit();
        }
    }
}
