using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace StockAdminContext.Helpers
{
    public static  class LoggerHelper
    {

      

        public static void WriteLog(Exception ex, string message = "")
        {
          // TODO log 

        }

        public static void DebugInfo(string Message)
        {
#if DEBUG
            WriteLog(Message);
#endif
        }

        public static void WriteLog(string Message)
        {
            // TODO log 
        }

        public static void OnExceptionRaised(object sender, UnhandledExceptionEventArgs e)
        {
           // throw new NotImplementedException();
            var ex = e.ExceptionObject as Exception;

            WriteLog(ex,"Unhandled Exception:");
        }
    }
}
