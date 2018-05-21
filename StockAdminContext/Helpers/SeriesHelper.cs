using System.Collections.ObjectModel;
using StockAdminContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockAdminContext.Helpers
{
    public static class SeriesHelper
    {
        public static NNM1_Series GetSerie(short? serie) {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();
                return db.NNM1_Series.FirstOrDefault(s => s.Series == serie);
            }
        }

        public static ObservableCollection<NNM1_Series> GetSeries(string serie, bool royalities=false)
        {

            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();
                IQueryable<NNM1_Series> series = db.NNM1_Series;
                series = series.Where(t => t.ObjectCode == serie && t.StateL=="A" && t.Locked=="N");
                return new ObservableCollection<NNM1_Series>(series);
                        
            }
        }


       
    }
}
