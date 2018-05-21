using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;

namespace SigiFluent.Helpers
{
    class Connection
    {

        static Connection()
        {
            Apiurl =  ConfigurationManager.AppSettings["WebApiUrl"];
        }

        public static bool CheckForInternetConnection()
        {
            try
            {
                
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
            finally { }
        }

        public static string Apiurl { get; set; }
    }


}
