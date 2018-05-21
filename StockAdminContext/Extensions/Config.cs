using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace StockAdminContext
{
    public static  class Config
    {
        static Config()
        {
            LoadConfigSections();
        }

        public static string CurrentCulture
        {
            get
            {
                return ConfigurationManager.AppSettings["DefaultCulture"];
            }
        }

        public static string CurrentConnection
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["SIGIContext"].ToString();
            }
        }

        public static string WhsCode
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["WHSCODE"];
                }
                catch (Exception ex)
                {
                    return string.Empty;
                }
            }
            set { ConfigurationManager.AppSettings["WHSCODE"] = value; }
        }

        public static string DefaultAcc
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["DefaultAcc"];
                }
                catch (Exception ex)
                {
                    return string.Empty;
                }
            }
            set { ConfigurationManager.AppSettings["DefaultAcc"] = value; }
        }
   
        public static string DownPaymentAcc
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["DownPaymentAcc"];
                }
                catch (Exception ex)
                {
                    return string.Empty;
                }
            }
            set { ConfigurationManager.AppSettings["DownPaymentAcc"] = value; }
        }

        public static string WebApiUri
        {
            get
            {
                #if !DEBUG 
                return ConfigurationManager.AppSettings["WebApiUrlRelease"];
                #else
                return ConfigurationManager.AppSettings["WebApiUrl"];
                #endif
            }
            set
            {
                #if !DEBUG
                ConfigurationManager.AppSettings["WebApiUrlRelease"] = value;
                #else
                ConfigurationManager.AppSettings["WebApiUrl"] = value;
                #endif
            }
        }

        public static string IVAEXE
        {
            get { return ConfigurationManager.AppSettings["IVAEXE"]; }
            set { ConfigurationManager.AppSettings["IVAEXE"] = value; }
        }

        public static string IVACOF
        {
            get { return ConfigurationManager.AppSettings["IVACOF"]; }
            set { ConfigurationManager.AppSettings["IVACOF"] = value; }
        }

        public static string  IVACOM 
        {
            get { return ConfigurationManager.AppSettings["IVACOM"]; }
            set { ConfigurationManager.AppSettings["IVACOM"] = value; }
        }

        public static double IVACOMValue
        {
            get { return Convert.ToDouble(ConfigurationManager.AppSettings["IVACOMValue"]); }
            set { ConfigurationManager.AppSettings["IVACOMValue"] = value.ToString(); }
        }

        public static string IVARET
        {
            get { return ConfigurationManager.AppSettings["IVARET"]; }
            set { ConfigurationManager.AppSettings["IVARET"] = value; }
        }

        public static double IVARETValue
        {
            get { return Convert.ToDouble(ConfigurationManager.AppSettings["IVARETValue"]); }
            set { ConfigurationManager.AppSettings["IVARETValue"] = value.ToString(); }
        }

        public static string BusinessPartner 
        {
            get { return ConfigurationManager.AppSettings["BusinessPartner"]; }
            set { ConfigurationManager.AppSettings["BusinessPartner"] = value; }
        }
        
        public static string SupplierBusinessPartner
        {
            get { return ConfigurationManager.AppSettings["SupplierBusinessPartner"]; }
            set { ConfigurationManager.AppSettings["SupplierBusinessPartner"] = value; }
        }
        
        public static double MaximoDepositos
        {
            get { return Convert.ToDouble(ConfigurationManager.AppSettings["MaxDepositos"]); }
            set { ConfigurationManager.AppSettings["MaxDepositos"] = value.ToString(); }
        }

        public static  double HourOrder
        {
            get { return Convert.ToDouble(ConfigurationManager.AppSettings["HourOrder"]); }
            set { ConfigurationManager.AppSettings["HourOrder"] = value.ToString(); }
        }

        public static string CurrentUser
        {
            get
            {
                //return "FapaSV";
                return CurrentUserName ?? "Developer";
            }
        }

        public static string ReportPath
        {
            get
            {
                var path = @"c:\Imagenes";
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                return path;
            }
        }

        public static void SetCurrentUserName(string username)
        {
            CurrentUserName = username;
        }

        private static string CurrentUserName { get; set; }

        public static void LoadConfigSections()
        {
            
        }
        
    }
}
