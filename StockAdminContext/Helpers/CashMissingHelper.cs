using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using StockAdminContext.Models;

namespace StockAdminContext.Helpers
{
    public static  class CashMissingHelper
    {
        public static ObservableCollection<CashMissing> GetCashMissings(DateTime? startDate, DateTime? lastDate, string keyword, LocalStatus? localStatus)
        {

            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                IQueryable<CashMissing> missings = db.CashMissings.Include("Cashier");

                if (startDate.HasValue) missings = missings.Where(d => d.Date >= startDate.Value);
                if (lastDate.HasValue) missings = missings.Where(d => d.Date <= lastDate.Value);
                if (!string.IsNullOrEmpty(keyword)) missings.Where(m => m.Comments.ToLower().Contains(keyword));
                if (localStatus.HasValue) missings = missings.Where(m => m.StateL == localStatus.Value);

                return new ObservableCollection<CashMissing>(missings.ToList());
            }
        }


        public static void AddOrUpdate(CashMissing cash)
        {
            lock (Extensions.Locker)
            {
                if (cash.IdCashMissing == 0)
                {
                    ContextFactory.GetDBContext().CashMissings.Add(cash);
                }
                
                else cash.ChangeEntityState(EntityState.Modified);
            }
        }

        public static void Delete(CashMissing cash)
        {
            lock (Extensions.Locker)
            {
                ContextFactory.GetDBContext().CashMissings.Remove(cash);
            }
        }
    }
}
