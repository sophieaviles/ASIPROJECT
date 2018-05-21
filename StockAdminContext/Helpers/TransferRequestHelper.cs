 
using System.Data.Entity;
using LinqKit;
 
using StockAdminContext.Enums;
using StockAdminContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Transactions;

namespace StockAdminContext.Helpers
{
    public static class TransferRequestHelper
    {
        public static ObservableCollection<OWTQ_TransferRequest> GetTransferRequests(string warehouse,
            DateTime? startDate, DateTime? endDate, string keywords,LocalStatus? status)
        {
            lock (Extensions.Locker)
            { 
                var db = ContextFactory.GetDBContext();

                IQueryable<OWTQ_TransferRequest> transfers =
                    db.OWTQ_TransferRequest.Include("WTQ1_TransferRequestDetails");

                if (!string.IsNullOrEmpty(warehouse)) transfers = transfers.Where(t => t.Filler == warehouse);

                if (!string.IsNullOrEmpty(keywords)) transfers = transfers.Where(t => t.Comments.Contains(keywords));

                if (startDate.HasValue) transfers = transfers.Where(d => d.DocDueDate >= startDate.Value);

                if (endDate.HasValue) transfers = transfers.Where(t => t.DocDueDate <= endDate.Value);

                if (status.HasValue) transfers = transfers.Where(t => t.StateL == status.Value);

                var transferCollection = transfers.OrderBy(t => t.StateL).ToList();

                var query = (from transfer in transferCollection // In memory to improve performance.
                             join whs in db.OWHS_Branch on transfer.Filler equals whs.WhsCode
                             orderby transfer.StateL
                             where whs == null || whs != null
                             select new
                             {
                                 id = transfer.IdTransferRequestL,
                                 WarehouseName = whs.WhsName,

                             }).ToList();

               transferCollection.ForEach(t=>
                                          {
                                              var w = query.FirstOrDefault(fn => fn.id == t.IdTransferRequestL);
                                              if (w != null)t.FillerName =w.WarehouseName?? string.Empty;
                                          });

               return new ObservableCollection<OWTQ_TransferRequest>(transferCollection);
                
            }
        }

        public static OWTQ_TransferRequest Get(int id)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();
                return db.OWTQ_TransferRequest.Include("WTQ1_TransferRequestDetails")
                       .FirstOrDefault(p=> p.IdTransferRequestL==id);
            }
        }

        public static OWTQ_TransferRequest GetTransfer(int id)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                return db.OWTQ_TransferRequest.Include("WTQ1_TransferRequestDetails").FirstOrDefault(t=> t.IdTransferRequestL== id);
            }
        }

        public static ObservableCollection<OWTQ_TransferRequest> GetTransferRequests(string branch)
        {
            lock (Extensions.Locker)
            {
              
                var db = ContextFactory.GetDBContext();
                 var transferRequestList = db.OWTQ_TransferRequest.Include("WTQ1_TransferRequestDetails").Include("OWHS_Branch")
                        .Where(trl => trl.WhsCode == branch).ToList();
                     
                 return new ObservableCollection<OWTQ_TransferRequest>(transferRequestList);
                
            }
        }      
       
        public static ObservableCollection<WTQ1_TransferRequestDetails> GetFilteredTransferRequestDetails(OWHS_Branch selectedBranchStore, OWTQ_TransferRequest transferRequest = null)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();
              
                //de la branch
                 
                var branchDetails = (

                    from t2 in db.OITW_BranchArticles// sucursal a la que se le pide
                    join t1 in db.OITW_BranchArticles on t2.ItemCode equals t1.ItemCode //sucursal que pide
                    join t0 in db.OITM_Articles on t2.ItemCode equals t0.ItemCode
                    join t3 in db.UCategories on t0.U_categoria equals t3.Code
                    join t4 in db.UWarehouseOrders on t3.Code equals t4.IdCategory
                    where !string.IsNullOrEmpty(t0.TemplateL ) && t0.TemplateL.ToLower().Contains("y") && t4.Whscode == selectedBranchStore.WhsCode
                    && t0.validFor.Contains("Y") && t1.Locked.Contains("N") && t2.Locked.Contains("N") && t0.InvntItem.Contains("Y") //Todos los filtros de los articulos
                    && t1.WhsCode == Config.WhsCode
                    && t2.WhsCode == selectedBranchStore.WhsCode
                    select new 
                    {
                        WhsCode = Config.WhsCode,
                        InvntryUom = t0.InvntryUom,
                        ItemCode = t2.ItemCode,
                        ItemName = t0.ItemName,
                        OnHand1 = t1.OnHand1 ?? 0,
                        Line = t3 != null ? t3.Name : string.Empty,
                        LineCode = t3 != null ? t3.Code : string.Empty,
                        Quantity = 0
                    }).ToList();

                if (transferRequest.IdTransferRequestL > 0)
                {
                    var collection = (from trd in branchDetails
                                      join strd in transferRequest.WTQ1_TransferRequestDetails
                                          on trd.ItemCode equals strd.ItemCode into jtrds
                                      from jtrd in jtrds.DefaultIfEmpty()
                                      select new
                                      {
                                          ItemCode = trd.ItemCode,
                                          Quantity = jtrd != null ? jtrd.Quantity : trd.Quantity,
                                          WhsCode = trd.WhsCode,
                                          ItemName = trd.ItemName,
                                          InvntryUom = trd.InvntryUom,
                                          OnHand1 = trd.OnHand1,
                                          Line = trd.Line,
                                          LineCode = trd.LineCode,
                                          CreatedDateL = jtrd != null ? jtrd.CreatedDateL : DateTime.Now,
                                          CreatedByL = jtrd != null ? jtrd.CreatedByL : Config.CurrentUser,
                                          ModifiedByL = jtrd != null ? jtrd.ModifiedByL : Config.CurrentUser,
                                          ModifiedDateL = jtrd != null ? jtrd.ModifiedDateL : DateTime.Now,
                                      }).ToList();

                    var savedDetils = collection.Select(v => new WTQ1_TransferRequestDetails()
                                                             {
                                                                 ItemCode = v.ItemCode,
                                                                 Quantity = v.Quantity,
                                                                 WhsCode = v.WhsCode,
                                                                 ItemName = v.ItemName,
                                                                 InvntryUom = v.InvntryUom,
                                                                 OnHand1 = v.OnHand1,
                                                                 Line = v.Line,
                                                                 LineCode = v.LineCode,
                                                                 CreatedDateL = v.CreatedDateL,
                                                                 CreatedByL = v.CreatedByL,
                                                                 ModifiedByL = v.ModifiedByL,
                                                                 ModifiedDateL = v.ModifiedDateL
                                                             }).ToList();
                    return new ObservableCollection<WTQ1_TransferRequestDetails>(savedDetils);
                }
                var details = branchDetails.Select(v => new WTQ1_TransferRequestDetails()
                                                        {
                                                            InvntryUom = v.InvntryUom,
                                                            ItemCode = v.ItemCode,
                                                            ItemName = v.ItemName,
                                                            OnHand1 = v.OnHand1,
                                                            Line = v.Line,
                                                            LineCode = v.LineCode,
                                                            Quantity = v.Quantity
                                                        }).ToList();
                return new ObservableCollection<WTQ1_TransferRequestDetails>(details);

            }
        }

        // CODIGO CHAMPERO REMOVIDO :)
        //public static void Update(OWTQ_TransferRequest transferRequest, List<WTQ1_TransferRequestDetails> details) {
        //    lock (Extensions.Locker)
        //    {

        //        var db = ContextFactory.GetDBContext();
        //        using (var scope = new TransactionScope())
        //        {
        //            if (transferRequest.WTQ1_TransferRequestDetails.Any())
        //            {

        //                db.Database.ExecuteSqlCommand(
        //                    string.Format("DELETE FROM WTQ1_TransferRequestDetails WHERE IdTransferRequestL = {0}",
        //                        transferRequest.IdTransferRequestL));
        //               // db.SaveChanges();

        //                foreach(var detail in details){ transferRequest.WTQ1_TransferRequestDetails.Add(detail);}
                        
        //            }
        //            db.SaveChanges();
        //            scope.Complete();
        //        }
        //    }
        //}
        public static void AddOrUpdate(OWTQ_TransferRequest transfer)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                if (transfer.IdTransferRequestL == 0) db.OWTQ_TransferRequest.Add(transfer);
                else transfer.ChangeEntityState(EntityState.Modified);
            }
        }
        public static void Update() {
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

        public static void Add(OWTQ_TransferRequest transferRequest)
        {
            lock (Extensions.Locker)
            {

                var db = ContextFactory.GetDBContext();
                using (var scope = new TransactionScope())
                {
                    db.OWTQ_TransferRequest.Add(transferRequest);
                    db.SaveChanges();
                    scope.Complete();
                }
            }
        }

        public static void Delete(OWTQ_TransferRequest transferRequest)
        {
            lock (Extensions.Locker)
            {

                var db = ContextFactory.GetDBContext();
                using (var scope = new TransactionScope())
                {
                    //transferRequest.WTQ1_TransferRequestDetails.ToList().ForEach(t => db.WTQ1_TransferRequestDetails.Remove(t));
                    if (transferRequest.IdTransferRequestL != 0)
                    {
                        db.OWTQ_TransferRequest.Remove(transferRequest);
  
                        db.SaveChanges();
                        scope.Complete();
                    }
                    
                    
                }
            }
        }

        public static Tuple<string,string>  GetInventoryRange(string itemcode , decimal qty)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                var article = db.OITW_BranchArticles.FirstOrDefault(i => i.ItemCode == itemcode && i.WhsCode== Config.WhsCode);

                var validRange =
                    string.Format(
                        "Valor minimo  Sugerido de inventario {0}\n Valor Maximo Sugerido  de inventario: {1}",
                        article.MaxStock, article.MinStock);

                if (qty + article.OnHand1> article.MaxStock)
                {
                    return Tuple.Create ("Los inventarios quedaran arriba del maximo",validRange);


                }
                if (qty +article.OnHand1 < article.MinStock)
                {
                    return Tuple.Create ("Los inventarios quedaran abajo del minimo",validRange);
                    
                }

                return Tuple.Create(string.Empty,validRange);

            }
        }


        public static List<OWTQ_TransferRequest> GetPendingToSyncOrders()
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                return db.OWTQ_TransferRequest.Include("WTQ1_TransferRequestDetails")
                    .Where(o=> o.StateL== LocalStatus.Pendiente).ToList();
            }
        }

        public static void DeleteZeroQtyDetails(List<WTQ1_TransferRequestDetails> details)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();
                details.ForEach(d =>
                                {
                                    var detail =
                                        db.WTQ1_TransferRequestDetails.FirstOrDefault(
                                            td => td.IdTransferRequestDetailL == d.IdTransferRequestDetailL);

                                    if (detail != null) db.WTQ1_TransferRequestDetails.Remove(detail);
                                });
            }
        }
    }
}



