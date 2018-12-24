using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        }
    }
}
