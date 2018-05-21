using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockAdminContext.Models
{
    public  class ApiException
    {
        public ApiException()
        {
            
        }
            public string Message { get; set; }
            public string ExceptionMessage { get; set; }
            public string ExceptionType { get; set; }
            public string StackTrace { get; set; }
     
    }
}
