using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace RapidTux_Demo
{
    public class TestConfig
    {
        public const string APIIDKey = "APIID";
        public const string UsernameKey = "Username";
        public const string SvcUrlKey = "SvcUrl";
        
        public static string APIID
        {
            get
            {
                return ConfigurationManager.AppSettings[APIIDKey];
            }
        }

        public static string Username
        {
            get
            {
                return ConfigurationManager.AppSettings[UsernameKey];
            }
        }

        public static string SvcUrl
        {
            get
            {
                return ConfigurationManager.AppSettings[SvcUrlKey];
            }
        }
    }
}
