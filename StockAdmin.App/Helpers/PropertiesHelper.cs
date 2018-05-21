using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StockAdminContext;
using StockAdminContext.Models;

namespace SigiFluent.Helpers
{
    public static class PropertiesHelper
    {
        public static LocalStatus EntityStatus
        {
            get;
            set;
        }

        private static LocalStatus _entityStatus;
    }
}
