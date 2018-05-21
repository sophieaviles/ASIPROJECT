using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using StockAdminContext.Models;

namespace StockAdminContext.Helpers
{
    public static  class ReportMappingRepository
    {
        public static ObservableCollection<ReportMapping> GetReportCollection()
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                return new ObservableCollection<ReportMapping>(db.ReportMappings.ToList());
            }
        } 

    }
}
