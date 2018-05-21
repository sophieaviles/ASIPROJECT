using System;
using System.Activities.Validation;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
 
namespace SigiApi.Helpers
{
    public class ConfigHelper
    {
        static ConfigHelper()
        {

        }

        //public static void InitializeMembership()
        //{
        //    if (!WebSecurity.Initialized)
        //    {
        //        WebSecurity.InitializeDatabaseConnection("ModelContainer", "UserProfile", "Id", "UserName",
        //            autoCreateTables: true);

        //        AddUser(AdminUserName, AdminPassword);
        //    }
        //}

        //public static bool AddUser(string username, string password)
        //{
        //    if (!WebSecurity.UserExists(username))
        //    {
        //        var result = WebSecurity.CreateAccount(username, password);

        //        return !string.IsNullOrEmpty(result);

        //    }
        //    return false;
        //}

        //public static bool Login(string username, string password)
        //{
        //    return WebSecurity.Login(username, password, persistCookie: false);
        //}

       

        #region Local Config Settings

       

        public static string AdminUserName
        {
            get
            {
                return "admin";
            }

        }

        public static string AdminPassword
        {
            get { return "1234567"; }
        }

        #endregion

        #region SAP Login Config Section

        public string ServerName
        {
            get
            {
                // return @"WIN-5R7NDOVCUCR\SAP";
                return ConfigurationManager.AppSettings["ServerName"];

            }
            set
            {
                ConfigurationManager.AppSettings["ServerName"] = value;
            }
        }

        public string DatabaseName
        {
            get
            {
                //return "SBODemoUS";
                return ConfigurationManager.AppSettings["DatabaseName"];
            }
            set
            {
                ConfigurationManager.AppSettings["DatabaseName"] = value;
            }
        }



        public string CompanyUserName
        {
            get
            {
                return ConfigurationManager.AppSettings["CompanyUserName"];
            }
            set
            {
                ConfigurationManager.AppSettings["CompanyUserName"] = value;
            }
        }



        public string CompanyPassword
        {
            get
            {
                return ConfigurationManager.AppSettings["CompanyPassword"];
            }
            set
            {
                ConfigurationManager.AppSettings["CompanyPassword"] = value;
            }
        }


        public string LicenseServer
        {
            get
            {
                //return "WIN-5R7NDOVCUCR";
                return ConfigurationManager.AppSettings["LicenseServer"];
            }
            set
            {
                ConfigurationManager.AppSettings["LicenseServer"]=value;
            }
        }
        #endregion

        public static  string SpecialOrdersFileFolder
        {
            get { return @"C:\SpecialORdersFiles"; }
        }
    } 

   
    

}
