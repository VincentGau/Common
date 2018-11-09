using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class StringHelper
    {
        /// <summary>
        /// 将形如abc#efg的字符串转换成列表，自动过滤空串； 
        /// </summary>
        /// <param name="targetString"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static List<string> GetListFromString(string targetString, char separator = '#')
        {
            List<string> result = new List<string>();

            if (string.IsNullOrEmpty(targetString))
                return null;

            if (!targetString.Contains(separator))
                return new List<string>() { targetString };

            List<string> tmpList = targetString.Split(separator).ToList();
            tmpList.ForEach(i =>
            {
                if (i.Length != 0)
                    result.Add(i);
            });

            return result;
        }
    }
}
