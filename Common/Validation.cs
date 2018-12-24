using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class Validation
    {

        #region Constants and Fields
        /// <summary>
        /// 默认手机号匹配规则
        /// </summary>
        private static string phonePattern = @"^1((3[0-9]|4[57]|5[0-35-9]|7[0678]|8[0-9])\d{8}$)";

        /// <summary>
        /// 默认邮箱匹配规则
        /// </summary>
        private static string emailPattern = @"([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,5})+";

        #endregion

        #region Public Methods
        /// <summary>
        /// 验证是否是合法手机号，默认正则规则为@"^1((3[0-9]|4[57]|5[0-35-9]|7[0678]|8[0-9])\d{8}$)"，如果配置文件中有配置phonePattern字段，则以配置文件为准。
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsPhoneNo(string input)
        {
            phonePattern = ConfigurationManager.AppSettings["phonePattern"] == null
                ? phonePattern
                : ConfigurationManager.AppSettings["phonePattern"];

            return IsValid(input, phonePattern);
        }

        /// <summary>
        /// 验证是否是合法邮箱地址，默认正则规则为@"([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,5})+"，如果配置文件中有配置phonePattern字段，则以配置文件为准。
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsEmail(string input)
        {
            emailPattern = ConfigurationManager.AppSettings["emailPattern"] == null
                ? emailPattern
                : ConfigurationManager.AppSettings["emailPattern"];

            return IsValid(input, emailPattern);
        }


        /// <summary>
        /// 验证输入字符串input是否符合pattern
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static bool IsValid(string input, string pattern)
        {
            return System.Text.RegularExpressions.Regex.Match(input, pattern).Success;
        }

        /// <summary>
        /// 判断字符串是否是日期格式
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsDateTime(string s)
        {
            return DateTime.TryParse(s, out DateTime dt);
        }

        #endregion
    }
}
