using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using StockAdminContext.Models;

namespace StockAdminContext.Helpers
{
    public static class DepositHelper
    {

        public static ObservableCollection<Deposit> GetDeposits(NNM1_Series serie,DateTime? startDate, DateTime? endDate, string keyword,LocalStatus? status )
        {
           lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                IQueryable<Deposit> deposits = db.Deposits .Include("Cashier").Include("ShopManager");

               // if (serie!= null) deposits.Where(d => d.IdSerie== serie.Series);

                if (startDate.HasValue) deposits=  deposits.Where(d => d.Date >= startDate.Value);

                if (endDate.HasValue) deposits = deposits.Where(d => d.Date <= endDate.Value);

                if (!string.IsNullOrEmpty(keyword))  deposits= deposits.Where(d => d.Comments.ToLower().Contains(keyword));

                if (status.HasValue) deposits = deposits.Where(d => d.StateL == status.Value);

                return new ObservableCollection<Deposit>(deposits.ToList());
            }
        }

        public static void Delete(Deposit selecteDeposit)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                db.Deposits.Remove(selecteDeposit);
            }

        }

        public static void AddOrUpdate(Deposit deposit)
        {
            lock (Extensions.Locker)
            {

                var db = ContextFactory.GetDBContext();

                if(deposit.IdDeposits==0) db.Deposits.Add(deposit);
                else  deposit.ChangeEntityState(EntityState.Modified);
            }
        }
    }
}
