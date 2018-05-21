using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Transactions;
using StockAdminContext.Models;

namespace StockAdminContext.Helpers
{
    public static class PurchaseHelper
    {
        public static ObservableCollection<OPCH_Purchase> GetPurchase(NNM1_Series serie, DateTime? startDate, DateTime? endDate, string keywords, LocalStatus? status)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();
                IQueryable<OPCH_Purchase> purchases = db.OPCH_Purchase.Include("PCH1_PurchaseDetail").Include("PCH1_PurchaseDetail.OITM_Articles");

                var p = new OPCH_Purchase();
                
               // if (!string.IsNullOrEmpty(type.ToString())) purchases = purchases.Where(t => t.Series == type);

                if (!string.IsNullOrEmpty(keywords)) purchases = purchases.Where(t => t.Comments.Contains(keywords));

                if (startDate.HasValue) purchases = purchases.Where(d => d.DocDueDate >= startDate.Value);

                if (endDate.HasValue) purchases = purchases.Where(t => t.DocDueDate <= endDate.Value);

                if (serie != null) purchases = purchases.Where(d => d.Series == serie.Series);

                if (status.HasValue) purchases = purchases.Where(t => t.StateL == status.Value);

                var queryNames = (from purchase in purchases
                                  join srie in db.NNM1_Series on purchase.Series equals srie.Series 
                    select new
                           {
                               purchaseId = purchase.IdPurchase,
                               SerieTitle = srie.SeriesName+" "+srie.Remark
                           }).ToList();
                var purchaseList = purchases.ToList();
                purchaseList.ForEach(
                        trl =>
                        {
                            var filler = queryNames.FirstOrDefault(f => f.purchaseId == trl.IdPurchase);

                            //var filler = db.NNM1_Series.FirstOrDefault(b => b.Series == trl.Series);
                            if (filler != null) trl.SeriesTitle = filler.SerieTitle;
                        });
                return new ObservableCollection<OPCH_Purchase>(purchaseList.OrderByDescending(t => t.ModifiedDateL).ThenBy(t => t.StateL));
            }
        }


        public static OPCH_Purchase GetPurchase(int id)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();
                return  db.OPCH_Purchase.Include("PCH1_PurchaseDetail")
                    .FirstOrDefault(p=> p.IdPurchase == id);
            }
        }

        public static void Add(OPCH_Purchase compra)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();
            
                db.OPCH_Purchase.Add(compra);
                //db.SaveChanges();
            }
        }

      

        //public static void Update()
        //{
        //    lock (Extensions.Locker)
        //    {

        //        var db = ContextFactory.GetDBContext();
        //        using (var scope = new TransactionScope())
        //        {
        //            db.SaveChanges();
        //            scope.Complete();
        //        }
        //    }
        //}

        public static void Delete(OPCH_Purchase purchase)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();
                using (var scope = new TransactionScope())
                {
                    purchase.PCH1_PurchaseDetail.ToList().ForEach(t => db.PCH1_PurchaseDetail.Remove(t));
                    db.OPCH_Purchase.Remove(purchase);
                    db.SaveChanges();
                    scope.Complete();
                }
            }
        }


        

        public static void DeleteDetail(PCH1_PurchaseDetail selectedtPurchaseDetail)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();
               
                db.PCH1_PurchaseDetail.Remove(selectedtPurchaseDetail);
            }
        }

        public static List<OPCH_Purchase> GetPendingToSyncPurchases()
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();
               return db.OPCH_Purchase.Include("PCH1_PurchaseDetail").Where(p => p.StateL == LocalStatus.Pendiente).ToList();
            }
        }

        public static void SaveTransaction(OPCH_Purchase obj)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                var transfers = obj.PCH1_PurchaseDetail.Select(t => new OINM_Transaction(obj, t)).ToList();

                transfers.ForEach(t => db.OINM_Transaction.Add(t));

                ContextFactory.SaveChanges();

                //var articles = (from detail in obj.PCH1_PurchaseDetail
                //                join article in db.OITW_BranchArticles on detail.ItemCode equals article.ItemCode
                //                where article.WhsCode == detail.WhsCode
                //                select article).ToList();

                //articles.ForEach(a =>
                //{
                //    var detail = obj.PCH1_PurchaseDetail.FirstOrDefault(d => d.ItemCode == a.ItemCode);
                //    a.OnHand = a.OnHand + detail.Quantity;
                //    a.OnHand1 = a.OnHand1 + detail.Quantity;
                //});

                //ContextFactory.SaveChanges();
                StoredCallbackProcessor.UpdateStock();
            }
        }

        internal static OPCH_Purchase Get(int id)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();
                return db.OPCH_Purchase.Include("PCH1_PurchaseDetail")
                       .FirstOrDefault(p => p.IdPurchase == id);
            }
        }

        public static bool VerifyNumAtCard(OPCH_Purchase purchase)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                return db.OPCH_Purchase.Any(p => p.NumAtCard == purchase.NumAtCard 
                    && p.IdPurchase!= purchase.IdPurchase);
            }
        }
    }
}
