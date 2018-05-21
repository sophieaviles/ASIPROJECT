using StockAdminContext.Models;
using System.Collections.ObjectModel;
using System.Transactions;

namespace StockAdminContext.Helpers
{
    public static class CanceledCheckBookHelper
    {
        public static void Add(CanceledCheckBook canceledCheckBook)
        {
            var db = ContextFactory.GetDBContext();
            using (var scope = new TransactionScope())
            {
                db.CanceledCheckbooks.Add(canceledCheckBook);
                db.SaveChanges();

                scope.Complete();
            }

        }

        public static ObservableCollection<CanceledCheckBook> GetCollection()
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();
                return new ObservableCollection<CanceledCheckBook>(db.CanceledCheckbooks);
               
            }
        }
    }
}
