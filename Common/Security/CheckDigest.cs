using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Security
{
    public static class CheckDigest
    {
        /// <summary>
        /// 计算文件摘要
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static string GetFileDigest(string filepath)
        {
            using (FileStream file = new FileStream(filepath, FileMode.Open))
            {
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// 检查文件的摘要是否与预期一致
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="expectedString"></param>
        /// <returns></returns>
        public static bool CheckFileDigest(string filepath, string expectedString)
        {
            return GetFileDigest(filepath).Equals(expectedString);
        }


        /// <summary>
        /// 检查两个文件的摘要是否一致
        /// </summary>
        /// <param name="filepath1"></param>
        /// <param name="filepath2"></param>
        /// <returns></returns>
        public static bool CheckFiles(string filepath1, string filepath2)
        {
            return GetFileDigest(filepath1).Equals(GetFileDigest(filepath2));
        }
    }
}
