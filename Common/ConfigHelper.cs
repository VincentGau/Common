using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class ConfigHelper
    {
        public static string GetString(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public static int GetInt(string key)
        {
            return Convert.ToInt32(ConfigurationManager.AppSettings[key]);
        }

        public static bool Contains(string key)
        {
            return ConfigurationManager.AppSettings.AllKeys.Contains(key);
        }
    }
}
