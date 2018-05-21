using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Windows.Input;
using StockAdminContext.Models;

namespace StockAdminContext.Helpers
{
    public static class TransferHelper
    {
        public static ObservableCollection<OWTR_Transfers> GetTransfers(string warehouse,
            DateTime? startDate, DateTime? endDate, string keywords, LocalStatus? status)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                IQueryable<OWTR_Transfers> transfers =
                    db.OWTR_Transfers.Include("WTR1_TransferDetails");

                if (!string.IsNullOrEmpty(warehouse)) transfers = transfers.Where(t => t.Filler == warehouse);

                if (!string.IsNullOrEmpty(keywords)) transfers = transfers.Where(t => t.Comments.Contains(keywords));

                if (startDate.HasValue) transfers = transfers.Where(d => d.DocDueDate >= startDate.Value);

                if (endDate.HasValue) transfers = transfers.Where(t => t.DocDueDate <= endDate.Value);

                if (status.HasValue) transfers = transfers.Where(t => t.StateL == status.Value);

                var transferCollection = transfers.OrderBy(t => t.StateL).ToList();

                var query = (from transfer in transferCollection
                    // In memory to improve performance.
                    join whs in db.OWHS_Branch on transfer.Filler equals whs.WhsCode
                    orderby transfer.StateL
                    where whs == null || whs != null
                    select new
                    {
                        id = transfer.IdTransferL,
                        WarehouseName = whs.WhsName,

                    }).ToList();

                transferCollection.ForEach(t =>
                {
                    var w = query.FirstOrDefault(fn => fn.id == t.IdTransferL);
                    if (w != null) t.FillerName = w.WarehouseName ?? string.Empty;
                    
                });

                var querydetails = (from td in transferCollection.SelectMany(t => t.WTR1_TransferDetails)
                    join art in db.OITM_Articles on td.ItemCode equals art.ItemCode 
                    join cat in db.UCategories on art.U_categoria equals cat.Code
                    where art != null || art == null
                    select new
                    {
                        IdTransferDetail = td.IdTransferDetailL,
                       Article = art,
                        Category = cat
                    }).ToList();

                transferCollection.SelectMany(td => td.WTR1_TransferDetails).ToList().ForEach(td =>
                {
                    var res = querydetails.FirstOrDefault(ar => ar.IdTransferDetail == td.IdTransferDetailL);
                    if (res == null) return;
                    td.OITM_Articles = res.Article;
                    td.Category = res.Category;
                });
                
                    
               
                return new ObservableCollection<OWTR_Transfers>(transferCollection);

            }
        }

        public static void Update()
        {
            lock (Extensions.Locker)
            {

                var db = ContextFactory.GetDBContext();
                using (var scope = new TransactionScope())
                {
                    db.SaveChanges();
                    scope.Complete();
                }
            }
        }

        public static void SaveTransaction(OWTR_Transfers tranfer)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                var transfers = tranfer.WTR1_TransferDetails.Select(t => new OINM_Transaction(tranfer, t)).ToList();

                transfers.ForEach(t => db.OINM_Transaction.Add(t));
                
                ContextFactory.SaveChanges();


                // Actualizar existencias

                StoredCallbackProcessor.UpdateStock();
                ContextFactory.SaveChanges();

                //if (tranfer.Filler != Config.WhsCode)// Es transferencia de entrada
                //{
                    //var articles = (from detail in tranfer.WTR1_TransferDetails
                    //                join article in db.OITW_BranchArticles on detail.ItemCode equals article.ItemCode
                    //                where article.WhsCode == detail.WhsCode
                    //                select article).ToList();

                    //articles.ForEach(a =>
                    //{
                    //    var detail = tranfer.WTR1_TransferDetails.FirstOrDefault(d => d.ItemCode == a.ItemCode);
                    //    a.OnHand = a.OnHand + detail.Quantity;
                    //    a.OnHand1 = a.OnHand1 + detail.Quantity;
                    //});
                //}
                //else // Es transferencia de salida
                //{
                    //var articles = (from detail in tranfer.WTR1_TransferDetails
                    //                join article in db.OITW_BranchArticles on detail.ItemCode equals article.ItemCode
                    //                where article.WhsCode == Config.WhsCode // Todos los articulos salieron de la sucursal
                    //                select article).ToList();

                    //articles.ForEach(a =>
                    //{
                    //    var detail = tranfer.WTR1_TransferDetails.FirstOrDefault(d => d.ItemCode == a.ItemCode);
                    //    a.OnHand = a.OnHand - detail.Quantity;
                    //    a.OnHand1 = a.OnHand1 - detail.Quantity;
                    //});
                //}                
            }
        }
    }
}
