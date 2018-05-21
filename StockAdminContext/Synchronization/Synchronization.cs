using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Transactions;
using LinqKit;
using StockAdminContext.Helpers;
using StockAdminContext.Models;

using SigiFluent;
using TransactionScope = System.Transactions.TransactionScope;
using StockAdminContext.WebServices;

namespace StockAdminContext
{
    public static class Synchronization
    {
        static Synchronization()
        {
           
        }

        public static void Connect()
        {
            Bridge = new ConnectionBridge();
            Bridge.ConnectToServer();
            Bridge.OnFireSync += CheckForUpdates;
            Bridge.onFirstConnect += StartSync;
            
        }

       

        #region First Sync
 

        #endregion

        #region Sync Pooling Loop
         
        private static void StartSync()
        {
            Bridge.onFirstConnect -= StartSync;
            // TODO implement Toast Notify To Start Page.
            // Check for updates on start .
            CheckForUpdates();
            //AsyncHelper.DoAsync(() => SyncPoolingLoop());
            StoredCallbackProcessor.Run();
             
        }
        
        //private static void SyncPoolingLoop()
        //{
        //    // Wait for Start Db Contxt
        //    Thread.Sleep(new TimeSpan(0,0,10));

        //    Thread.CurrentThread.Priority = ThreadPriority.Lowest;
        //    while (true)
        //    { 
        //        // 5 min 
        //        Thread.Sleep(TimeSpan.FromSeconds(5));
                
        //        CheckForUpdates();
        //    }
        //}

        

        //public static bool IsRunningSync { get; set; }

        
        #endregion

        //Procedimiento principal de sincronización
        public static void CheckForUpdates()
        {
            try
            {
                //lock (Extensions.SyncLock)
                //{
                //    PendingChanges.Clear();

                //    if (!WebApiClient.IsAvailableConnection) return;
                //    if (OnBeginSync != null) OnBeginSync();
                //    //Buscar los cambios pendientes en SAP
                //    var version = SyncVersion.GetLatestVersionByType();

                //    var changesets = WebApiClient.GetNewChangesets(version.Item1, version.Item2, Config.WhsCode).Result;

                //    // ORder By Version CHanges

                //    changesets = changesets.OrderByDescending(o => o.version).ToList();

                //    //Llenar pila con cambios
                //    changesets
                //    .Where(p=> PendingChanges.All(c => c.version != p.version))
                //    .ToList().ForEach(c => PendingChanges.Push(c));

                //    changesetsCount = PendingChanges.Count();
                //    index = 0;
  
                //    //Sincronizar 
                //    GetPendingChanges();

                //    //UploadPendingChanges();

                //    if (onSyncComplete != null) onSyncComplete();
              //  }
            }
            catch (Exception ex)
            {
                if (onSyncComplete != null) onSyncComplete();
                PendingChanges.Clear();
                throw ex;
            }
             
        }

        #region DownLoadChange

        private static void GetPendingChanges()
        {
            if (!PendingChanges.Any()) return;

            var changeSet = PendingChanges.Peek();

            if (changeSet != null)
            {
                var doc = SyncVersion.GetDocumentTypeByCod(changeSet);

                // show Progress
                var percent = Convert.ToInt32(((double)index++ / (double)changesetsCount) * 100);

                ShowProgress(string.Format("{0}% {1}", percent, doc.DocumentName));

                if (doc != null)
                {
                    DoSync(doc, changeSet);
                    PendingChanges.Pop();
                }
                else
                {
                    //TODO handle null Documents locally.
                    PendingChanges.Pop();
                }


            }

            if (PendingChanges.Any()) GetPendingChanges();

        }

        private static void DoSync(Document doc, LOG_CHANGES changeset)
        {
            //try
            //{


            using (var scope = new TransactionScope(TransactionScopeOption.Required, 
                new TransactionOptions()
                {
                    Timeout = TimeSpan.FromMinutes(10), 
                    IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                }))
            {
                lock (Extensions.Locker)
                {

                    if (doc.DocumentType == typeof(OWTQ_TransferRequest)) GetTransferTransferRequestFromServer(changeset);
                    if (doc.DocumentType == typeof(OWTR_Transfers)) GetTransfersfromServer(changeset);
                    if (doc.DocumentType == typeof(ORDR_SpecialOrder)) GetSpecialOrdersFromServer(changeset);
                    if (doc.DocumentType == typeof(OCRD_BusinessPartner)) GetBusinessPartnerFromServer(changeset);
                    if (doc.DocumentType == typeof(ORPC_SupplierCreditNotes)) GetCreditNotesFromServe(changeset);
                    if (doc.DocumentType == typeof(OINV_Sales)) GetSalesFromServer(changeset);
                    if (doc.DocumentType == typeof(ODPI_DownPayment)) GetDownPayments(changeset);
                    if (doc.DocumentType == typeof(OIGE_GoodsIssues)) GetGoodIssues(changeset);
                    if (doc.DocumentType == typeof(OIGN_GoodsReceipt)) GetGoodReceipts(changeset);
                    if (doc.DocumentType == typeof(OITB_Groups)) GetGroupsFromServer(changeset);// TODO implement
                    if (doc.DocumentType == typeof(OPCH_Purchase)) GetPurchaseFromServer(changeset);
                    if (doc.DocumentType == typeof(OITM_Articles)) GetArticle(changeset);
                    if (doc.DocumentType == typeof(ORIN_ClientCreditNotes)) GetClientCreditNotes(changeset);
                    ContextFactory.GetDBContext().SaveChanges();
                    scope.Complete();
                }
            }
            if (onSyncItem != null) onSyncItem();

            //}
            //catch (DbEntityValidationException ex)
            //{
            //    LoggerHelper.WriteLog(ex);
            //    return false;
            //    throw ex;
            //}
            //catch (Exception ex)
            //{
            //    // TODO show Exception TO User
            //    //ContextFactory.RollBack();
            //    LoggerHelper.WriteLog(ex,"Error En Syncronizacion.");
            //    return false;
            //}

        }

        private static void GetArticle(LOG_CHANGES changeset)
        {
            var p = WebApiClient.GetArticleByKey(changeset.ListVal).Result;

            p.OITW_BranchArticles.ForEach(a=> a.LastSyncDateL= DateTime.Now);

            var db = ContextFactory.GetDBContext();

            var current = db.OITM_Articles.Include(a=> a.OITW_BranchArticles)
                .FirstOrDefault(prod => prod.ItemCode == p.ItemCode);

            if (current == null) db.OITM_Articles.Add(p);

            else
            {
                current.UpdateModelPropertiesFrom(p);
                if (current.OITW_BranchArticles.Count!= p.OITW_BranchArticles.Count)
                {
                    var localIds = current.OITW_BranchArticles
                                   .Select(a => Tuple.Create(a.ItemCode, a.WhsCode))
                                   .ToList();
                    var news =
                        p.OITW_BranchArticles.Where(a => !localIds.Contains(Tuple.Create(a.ItemCode, a.WhsCode)))
                            .ToList();
                    
                    news.ForEach(a=> current.OITW_BranchArticles.Add(a));

                }
            }
           // db.LOG_CHANGES.Add(changeset);
            changeset.AddChangeset();
        }

        private static void GetGroupsFromServer(LOG_CHANGES changeset)
        {
            // Collection.
            var group = WebApiClient.GetItemGroups(changeset.ListVal).Result;
            
            // TODO implement

        }

        private static void GetBusinessPartnerFromServer(LOG_CHANGES changeset)
        {
            var partner = WebApiClient.GetBusinessPartner(changeset.ListVal).Result;

            if (partner == null) return;

            var db = ContextFactory.GetDBContext();

            var businessPartner = db.OCRD_BusinessPartner.FirstOrDefault(b => b.CardCode == partner.CardCode);

            if (businessPartner == null) db.OCRD_BusinessPartner.Add(partner);

            else
            {
                businessPartner.UpdateModelPropertiesFrom(partner);

            }
           // db.LOG_CHANGES.Add(changeset);
            changeset.AddChangeset();
            db.SaveChanges();
            if (onBussinesPartnerUpdate != null) onBussinesPartnerUpdate();
        }

        private static void GetPurchaseFromServer(LOG_CHANGES changeset)
        {
            var purchase = WebApiClient.GetPurchase(changeset.ListVal).Result;

            var db = ContextFactory.GetDBContext();

            var id = Convert.ToInt32(changeset.ListVal);

            var localId = db.OPCH_Purchase.Include(c => c.PCH1_PurchaseDetail)
                .FirstOrDefault(c => c.IdPurchase == changeset.IdL.Value);

            var localDE = db.OPCH_Purchase.Include(c => c.PCH1_PurchaseDetail)
                    .FirstOrDefault(c => c.DocEntry == id);

            if (changeset.IdL.HasValue)// C1
            {
                if (localId == null) // C3
                {
                    db.OPCH_Purchase.Add(purchase);
                    PurchaseHelper.SaveTransaction(purchase);
                }
                else
                {
                    if (localId.StateL == LocalStatus.Procesado) // C4
                    {
                        if (localDE == null || localDE.DocEntry != localId.DocEntry) // C5
                        {
                            db.OPCH_Purchase.Add(purchase);
                            PurchaseHelper.SaveTransaction(purchase);
                        }
                        else
                        {
                            localId.UpdateModelPropertiesFrom(purchase);
                        }
                    }
                    else
                    {
                        localId.UpdateModelPropertiesFrom(purchase);
                        PurchaseHelper.SaveTransaction(purchase);
                    }
                }
            }
            else //  C2 -- El Idl no contenia un valor
            {
                if (localDE == null)
                {
                    db.OPCH_Purchase.Add(purchase);
                    PurchaseHelper.SaveTransaction(purchase);
                }
            }

           // db.LOG_CHANGES.Add(changeset);
            changeset.AddChangeset();

        }

        private static void GetClientCreditNotes(LOG_CHANGES changeset)
        {
            var note = WebApiClient.GetSaleCreditNote(changeset.ListVal).Result;

            var db = ContextFactory.GetDBContext();

            var id = Convert.ToInt32(changeset.ListVal);

            var localId = db.ORIN_ClientCreditNotes.Include(c => c.RIN1_ClientCreditNoteDetail)
                .FirstOrDefault(c => c.IdClientCreditNoteL == changeset.IdL.Value);

            var localDE = db.ORIN_ClientCreditNotes.Include(c => c.RIN1_ClientCreditNoteDetail)
                    .FirstOrDefault(c => c.DocEntry == id);

            if (changeset.IdL.HasValue)// C1
            {
                if (localId == null) // C3
                {
                    db.ORIN_ClientCreditNotes.Add(note);
                    ContextFactory.SaveChanges();
                    ClientCreditNoteHelper.SaveTransaction(note);
                }
                else
                {
                    if (localId.StateL == LocalStatus.Procesado) // C4
                    {
                        if (localDE == null || localDE.DocEntry != localId.DocEntry) // C5
                        {
                            db.ORIN_ClientCreditNotes.Add(note);
                            ClientCreditNoteHelper.SaveTransaction(note);
                        }
                        else
                        {
                            localId.UpdateModelPropertiesFrom(note);
                        }
                    }
                    else
                    {
                        localId.UpdateModelPropertiesFrom(note);
                        ClientCreditNoteHelper.SaveTransaction(note);
                    }
                }
            }
            else //  C2 -- El Idl no contenia un valor
            {
                if (localDE == null)
                {
                    db.ORIN_ClientCreditNotes.Add(note);
                    ClientCreditNoteHelper.SaveTransaction(note);
                }
            }

            //db.LOG_CHANGES.Add(changeset);
            changeset.AddChangeset();
        }

        private static void GetGoodReceipts(LOG_CHANGES changeset)
        {
            var receipt = WebApiClient.GetGoodReceipt(changeset.ListVal).Result;
            if (receipt == null) return;
            receipt.StateL = LocalStatus.Procesado;
             
            var db = ContextFactory.GetDBContext();

            var id = Convert.ToInt32(changeset.ListVal);

            var localId = db.OIGN_GoodsReceipt.Include(c => c.IGN1_GoodsReceiptDetail)
                .FirstOrDefault(c => c.IdGoodReceiptL == changeset.IdL.Value);

            var localDE = db.OIGN_GoodsReceipt.Include(c => c.IGN1_GoodsReceiptDetail)
                    .FirstOrDefault(c => c.DocEntry == id);

            if (changeset.IdL.HasValue)// C1
            {
                if (localId == null) // C3
                {
                    db.OIGN_GoodsReceipt.Add(receipt);
                    GoodsReceiptHelper.SaveTransaction(receipt);
                }
                else
                {
                    if (localId.StateL == LocalStatus.Procesado) // C4
                    {
                        if (localDE == null || localDE.DocEntry != localId.DocEntry) // C5
                        {
                            db.OIGN_GoodsReceipt.Add(receipt);
                            GoodsReceiptHelper.SaveTransaction(receipt);
                        }
                        else
                        {
                            localId.UpdateModelPropertiesFrom(receipt);
                        }
                    }
                    else
                    {
                        localId.UpdateModelPropertiesFrom(receipt);
                        GoodsReceiptHelper.SaveTransaction(receipt);
                    }
                }
            }
            else //  C2 -- El Idl no contenia un valor
            {
                if (localDE == null)
                {
                    db.OIGN_GoodsReceipt.Add(receipt);
                    GoodsReceiptHelper.SaveTransaction(receipt);
                }
            }

            //db.LOG_CHANGES.Add(changeset);           
            changeset.AddChangeset();
        }

        private static void GetGoodIssues(LOG_CHANGES changeset)
        {
            var gi = WebApiClient.GetGoodIssue(changeset.ListVal).Result;

            if (gi == null) return;
            gi.StateL = LocalStatus.Procesado;

            var db = ContextFactory.GetDBContext();

            var id = Convert.ToInt32(changeset.ListVal);

            var localId = db.OIGE_GoodsIssues.Include(c => c.IGE1_GoodsIssueDetail)
                .FirstOrDefault(c => c.IdGoodIssueL == changeset.IdL.Value);

            var localDE = db.OIGE_GoodsIssues.Include(c => c.IGE1_GoodsIssueDetail)
                    .FirstOrDefault(c => c.DocEntry == id);

            if (changeset.IdL.HasValue)// C1
            {
                if (localId == null) // C3
                {
                    db.OIGE_GoodsIssues.Add(gi);
                    GoodIssuesHelper.SaveTransaction(gi);
                }
                else
                {
                    if (localId.StateL == LocalStatus.Procesado) // C4
                    {
                        if (localDE == null || localDE.DocEntry != localId.DocEntry) // C5
                        {
                            db.OIGE_GoodsIssues.Add(gi);
                            GoodIssuesHelper.SaveTransaction(gi);
                        }
                        else
                        {
                            localId.UpdateModelPropertiesFrom(gi);
                        }
                    }
                    else
                    {
                        localId.UpdateModelPropertiesFrom(gi);
                        GoodIssuesHelper.SaveTransaction(gi);
                    }
                }
            }
            else //  C2 -- El Idl no contenia un valor
            {
                if (localDE == null)
                {
                    db.OIGE_GoodsIssues.Add(gi);
                    GoodIssuesHelper.SaveTransaction(gi);
                }
            }

            //db.LOG_CHANGES.Add(changeset);           
            changeset.AddChangeset();
            
        }

        private static void GetDownPayments(LOG_CHANGES changeset)
        {
            var downPayment = WebApiClient.GetDownPayment(changeset.ListVal).Result;
            if (downPayment == null) return;
            downPayment.StateL = LocalStatus.Procesado;

            var db = ContextFactory.GetDBContext();

            var id = Convert.ToInt32(changeset.ListVal);

            var localId = db.ODPI_DownPayment.Include(c => c.DPI1_DownPaymentDetail)
                .FirstOrDefault(c => c.IdDownPayment == changeset.IdL.Value);

            var localDE = db.ODPI_DownPayment.Include(c => c.DPI1_DownPaymentDetail)
                    .FirstOrDefault(c => c.DocEntry == id);

            if (changeset.IdL.HasValue)// C1
            {
                if (localId == null) // C3
                {
                    db.ODPI_DownPayment.Add(downPayment);
                }
                else
                {
                    if (localId.StateL == LocalStatus.Procesado) // C4
                    {
                        if (localDE == null || localDE.DocEntry != localId.DocEntry) // C5
                        {
                            db.ODPI_DownPayment.Add(downPayment);
                        }
                        else
                        {
                            localId.UpdateModelPropertiesFrom(downPayment);
                        }
                    }
                    else
                    {
                        localId.UpdateModelPropertiesFrom(downPayment);
                    }
                }
            }
            else //  C2 -- El Idl no contenia un valor
            {
                if (localDE == null)
                {
                    db.ODPI_DownPayment.Add(downPayment);
                }
            }

            //db.LOG_CHANGES.Add(changeset);           
            changeset.AddChangeset();
        }

        private static void GetSalesFromServer(LOG_CHANGES changeset)
        {
            var sale = WebApiClient.GetSale(changeset.ListVal).Result;
            if (sale == null) return;

            sale.StateL= LocalStatus.Procesado;

            var db = ContextFactory.GetDBContext();

            var id = Convert.ToInt32(changeset.ListVal);

            var localId = db.OINV_Sales.Include(c => c.INV1_SalesDetail)
                .FirstOrDefault(c => c.IdSaleL == changeset.IdL.Value);

            var localDE = db.OINV_Sales.Include(c => c.INV1_SalesDetail)
                    .FirstOrDefault(c => c.DocEntry == id);

            if (changeset.IdL.HasValue)// C1
            {
                if (localId == null) // C3
                {
                    db.OINV_Sales.Add(sale);
                    SalesHelper.SaveTransaction(sale);
                }
                else
                {
                    if (localId.StateL == LocalStatus.Procesado) // C4
                    {
                        if (localDE == null || localDE.DocEntry != localId.DocEntry) // C5
                        {
                            db.OINV_Sales.Add(sale);
                            SalesHelper.SaveTransaction(sale);
                        }
                        else
                        {
                            localId.UpdateModelPropertiesFrom(sale);
                        }
                    }
                    else
                    {
                        localId.UpdateModelPropertiesFrom(sale);
                        SalesHelper.SaveTransaction(sale);
                    }
                }
            }
            else //  C2 -- El Idl no contenia un valor
            {
                if (localDE == null)
                {
                    db.OINV_Sales.Add(sale);
                    SalesHelper.SaveTransaction(sale);
                }
            }

            //db.LOG_CHANGES.Add(changeset);                                 
            changeset.AddChangeset();
        }

        private static void GetCreditNotesFromServe(LOG_CHANGES changeset)
        {
            var note = WebApiClient.GetCreditNotes(changeset.ListVal).Result;
            if (note == null) return;
            note.StateL = LocalStatus.Procesado;
           
            var db = ContextFactory.GetDBContext();

            var id = Convert.ToInt32(changeset.ListVal);

            var localId = db.ORPC_SupplierCreditNotes.Include(c => c.RPC1_SupplierCreditNoteDetail)
                .FirstOrDefault(c => c.IdSupplierCreditNote == changeset.IdL.Value);

            var localDE = db.ORPC_SupplierCreditNotes.Include(c => c.RPC1_SupplierCreditNoteDetail)
                    .FirstOrDefault(c => c.DocEntry == id);

            if (changeset.IdL.HasValue)// C1
            {
                if (localId == null) // C3
                {
                    db.ORPC_SupplierCreditNotes.Add(note);
                    SupplierCreditNoteHelper.SaveTransaction(note);
                }
                else
                {
                    if (localId.StateL == LocalStatus.Procesado) // C4
                    {
                        if (localDE == null || localDE.DocEntry != localId.DocEntry) // C5
                        {
                            db.ORPC_SupplierCreditNotes.Add(note);
                            SupplierCreditNoteHelper.SaveTransaction(note);
                        }
                        else
                        {
                            localId.UpdateModelPropertiesFrom(note);
                        }
                    }
                    else
                    {
                        localId.UpdateModelPropertiesFrom(note);
                        SupplierCreditNoteHelper.SaveTransaction(note);
                    }
                }
            }
            else //  C2 -- El Idl no contenia un valor
            {
                if (localDE == null)
                {
                    db.ORPC_SupplierCreditNotes.Add(note);
                    SupplierCreditNoteHelper.SaveTransaction(note);
                }
            }

            //db.LOG_CHANGES.Add(changeset);         
            changeset.AddChangeset();
        } 

        public static void GetSpecialOrdersFromServer(LOG_CHANGES changeset)
        {
            var order = WebApiClient.GetSpecialOrder(changeset.ListVal).Result;

            if (order == null) return;
            order.StateL = LocalStatus.Procesado;

            var db = ContextFactory.GetDBContext();

            if (changeset.SyncType == SyncType.Add)
            {
                if (changeset.IdL.HasValue)
                {
                    // Get Local if Local == null NotifyException ()
                    var local =
                        db.ORDR_SpecialOrder.Include("RDR1_SpecialOrderDetail").Include("ATC1_Attachments")
                        .FirstOrDefault(o => o.IdSpecialOrder == changeset.IdL.Value);

                    if (local == null)
                    {
                        NotifyException(order);
                        //db.LOG_CHANGES.Add(changeset);
                        changeset.AddChangeset();
                        return;
                    }
                    // Verfiy != Procesado.
                    if (local.StateL != LocalStatus.Procesado)
                    {
                        local.UpdateModelPropertiesFrom(order);
                    }
                }
                else db.ORDR_SpecialOrder.Add(order);

            }
            if (changeset.SyncType == SyncType.Update)
            {
                var local = db.ORDR_SpecialOrder.Include("RDR1_SpecialOrderDetail").Include("ATC1_Attachments")
                           .FirstOrDefault(o => o.DocEntry == order.DocEntry);

                if (local == null)
                {
                    NotifyException(order);
                    //db.LOG_CHANGES.Add(changeset);
                    changeset.AddChangeset();
                    return;
                }


                if (local.RDR1_SpecialOrderDetail.Count == order.RDR1_SpecialOrderDetail.Count &&
                    local.ATC1_Attachments.Count == order.ATC1_Attachments.Count)
                {
                    local.UpdateModelPropertiesFrom(order);
                }

                if (local.RDR1_SpecialOrderDetail.Count != order.RDR1_SpecialOrderDetail.Count)
                {
                    local.RDR1_SpecialOrderDetail.ForEach(d=> d.RemoveAndSave());
                    local.RDR1_SpecialOrderDetail.AddRange(order.RDR1_SpecialOrderDetail);
                    local.UpdateModelPropertiesFrom(order,excludeComplexTypes:true);
                    db.SaveChanges();
                }
                if (local.ATC1_Attachments.Count != order.ATC1_Attachments.Count)
                {
                    local.ATC1_Attachments.ForEach(d=> d.RemoveAndSave());
                    local.ATC1_Attachments.AddRange(order.ATC1_Attachments);
                    local.UpdateModelPropertiesFrom(order,excludeComplexTypes:true);
                    db.SaveChanges();
                }
                
            }
            //db.LOG_CHANGES.Add(changeset);
            changeset.AddChangeset();

        }

        private static void GetTransferTransferRequestFromServer(LOG_CHANGES changeset)
        {

            var transfer = WebApiClient.GetTransferRequest(changeset.ListVal).Result;
           
            if (transfer == null) return;
            transfer.StateL = LocalStatus.Procesado;
            var db = ContextFactory.GetDBContext();

            var id = Convert.ToInt32(changeset.ListVal);

            var localId = db.OWTQ_TransferRequest.Include(c => c.WTQ1_TransferRequestDetails)
                .FirstOrDefault(c => c.IdTransferRequestL == changeset.IdL.Value);

            var localDE = db.OWTQ_TransferRequest.Include(c => c.WTQ1_TransferRequestDetails)
                    .FirstOrDefault(c => c.DocEntry == id);

            if (changeset.IdL.HasValue)// C1
            {
                if (localId == null) // C3
                {
                    db.OWTQ_TransferRequest.Add(transfer);
                }
                else
                {
                    if (localId.StateL == LocalStatus.Procesado) // C4
                    {
                        if (localDE == null || localDE.DocEntry != localId.DocEntry) // C5
                        {
                            db.OWTQ_TransferRequest.Add(transfer);
                        }
                        else
                        {
                            localId.UpdateModelPropertiesFrom(transfer);
                        }
                    }
                    else
                    {
                        localId.UpdateModelPropertiesFrom(transfer);
                    }
                }
            }
            else //  C2 -- El Idl no contenia un valor
            {
                if (localDE == null)
                {
                    db.OWTQ_TransferRequest.Add(transfer);
                }
            }

            //db.LOG_CHANGES.Add(changeset);  
            changeset.AddChangeset();


            //if (changeset.SyncType == SyncType.Add)
            //{
            //    // Actualizar si Changeset Tiene Idl y no esta procesado localmente

            //    if (changeset.IdL.HasValue)
            //    {
            //        var local =
            //            db.OWTQ_TransferRequest.Include("WTQ1_TransferRequestDetails")
            //            .FirstOrDefault(t => t.IdTransferRequestL == changeset.IdL.Value);

            //        if (local == null)
            //        {
            //            NotifyException(transfer);
            //            db.LOG_CHANGES.Add(changeset);
            //            db.SaveChanges();
            //            return;
            //        }

            //        // Verificar si no esta procesado
            //        if (local.StateL != LocalStatus.Procesado)
            //        {

            //            local.UpdateModelPropertiesFrom(transfer);

            //        }
            //    } // Si el objeto no tiene ID Local
            //    else db.OWTQ_TransferRequest.Add(transfer);   
            //}

            //if (changeset.SyncType == SyncType.Update)
            //{
            //    var local =
            //        db.OWTQ_TransferRequest.Include("WTQ1_TransferRequestDetails")
            //        .FirstOrDefault(t => t.DocEntry == transfer.DocEntry);

            //    if (local == null)
            //    {
            //        NotifyException(transfer);
            //        db.LOG_CHANGES.Add(changeset);
            //        db.SaveChanges();
            //        return;
            //    }

            //    if (local.WTQ1_TransferRequestDetails.Count != transfer.WTQ1_TransferRequestDetails.Count)
            //    {
            //        // Remove All Local Details.
            //        local.WTQ1_TransferRequestDetails.ForEach(d=> d.RemoveAndSave());
            //        local.WTQ1_TransferRequestDetails.AddRange(transfer.WTQ1_TransferRequestDetails);
            //        local.UpdateModelPropertiesFrom(transfer,excludeComplexTypes:true);
            //    }
            //    else local.UpdateModelPropertiesFrom(transfer);

            //}
            //db.LOG_CHANGES.Add(changeset);
            //db.SaveChanges();
        }

        private static void GetTransfersfromServer(LOG_CHANGES changeset)
        {
            var transfer = WebApiClient.GetTransfer(changeset.ListVal).Result;

            if (transfer == null) return;

            transfer.StateL = LocalStatus.Procesado;

            var db = ContextFactory.GetDBContext();

            var id = Convert.ToInt32(changeset.ListVal);

            var localId = db.OWTR_Transfers.Include(c => c.WTR1_TransferDetails)
                .FirstOrDefault(c => c.IdTransferL == changeset.IdL.Value);

            var localDE = db.OWTR_Transfers.Include(c => c.WTR1_TransferDetails)
                    .FirstOrDefault(c => c.DocEntry == id);

            if (changeset.IdL.HasValue)// C1
            {
                if (localId == null) // C3
                {
                    db.OWTR_Transfers.Add(transfer);
                    TransferHelper.SaveTransaction(transfer);
                }
                else
                {
                    if (localId.StateL == LocalStatus.Procesado) // C4
                    {
                        if (localDE == null || localDE.DocEntry != localId.DocEntry) // C5
                        {
                            db.OWTR_Transfers.Add(transfer);
                            TransferHelper.SaveTransaction(transfer);
                        }
                        else
                        {
                            localId.UpdateModelPropertiesFrom(transfer);
                        }
                    }
                    else
                    {
                        localId.UpdateModelPropertiesFrom(transfer);
                        TransferHelper.SaveTransaction(transfer);
                    }
                }
            }
            else //  C2 -- El Idl no contenia un valor
            {
                if (localDE == null)
                {
                    db.OWTR_Transfers.Add(transfer);
                    TransferHelper.SaveTransaction(transfer);
                }
            }

            //db.LOG_CHANGES.Add(changeset);                       
            changeset.AddChangeset();
        }
        
        #endregion

        #region UploadChanges

        //public static void UploadPendingChanges()
        //{
        //    if (!WebApiClient.IsAvailableConnection) return;

        //    //TransferRequestHelper.GetPendingToSyncOrders()
        //    //.ForEach(transfer=> SyncUpload(transfer));

        //    //PurchaseHelper.GetPendingToSyncPurchases()
        //    //.ForEach(purchase=> purchase.Upload(SyncUpload,true));

        //    //var sales = SalesHelper.GetPendingToSyncSales();
        //    //sales.ForEach(sale=> SyncUpload(sale));

        //    //var notes = SupplierCreditNoteHelper.GetPendingtoSyncNotes();
        //    //notes.ForEach(note => SyncUpload(note));

        //    //var receipts = GoodsReceiptHelper.GetPendingToSync();
        //    //receipts.ForEach(receipt=> SyncUpload(receipt));

        //    //var goodIssues = GoodIssuesHelper.GetPendingToSync();
        //    //goodIssues.ForEach(issue=> SyncUpload(issue)); 
            
        //    //var downPayments = DownPaymentHelper.GetPendingToSync();
        //    //downPayments.ForEach(dp=> SyncUpload(dp));

        //    //var inventoryCount = InventoryCountHelper.GetOpenInventoryCountForToday();

        //    //inventoryCount.ForEach(InventoryCountHelper.Process);
        //}       

        private static OWTQ_TransferRequest SyncUpload(OWTQ_TransferRequest transferRequest)
        
        { 
           var transfer = WebApiClient.AddTransfer(transferRequest).Result;
            
            transferRequest.UpdateModelPropertiesFrom(transfer.Model);
            transferRequest.StateL = LocalStatus.Procesado;
            transfer.UpdateEntityVersion();
            return transferRequest;
        }

        private static OPCH_Purchase SyncUpload(OPCH_Purchase purchase)
        {
            var result = WebApiClient.AddPurchase(purchase).Result;

            purchase.UpdateModelPropertiesFrom(result.Model);
            purchase.StateL = LocalStatus.Procesado;
            result.UpdateEntityVersion();
            PurchaseHelper.SaveTransaction(purchase);
            return purchase;
        }

        private static OINV_Sales SyncUpload(OINV_Sales sale)
        {

            
            var result = WebApiClient.AddSale(sale).Result;
            sale.UpdateModelPropertiesFrom(result.Model);
            sale.StateL = LocalStatus.Procesado;
            result.UpdateEntityVersion();

            
            SalesHelper.SaveTransaction(sale);
            return sale;
        }

        private static ORPC_SupplierCreditNotes SyncUpload(ORPC_SupplierCreditNotes note)
        {

            var result = WebApiClient.AddCreditNote(note).Result;
            note.UpdateModelPropertiesFrom(result.Model);
            note.StateL = LocalStatus.Procesado;
            result.UpdateEntityVersion();
            SupplierCreditNoteHelper.SaveTransaction(note);
            return note;
        }

        private static OIGN_GoodsReceipt SyncUpload(OIGN_GoodsReceipt receipt)
        {
            var result = WebApiClient.AddGoodReceipt(receipt).Result;
            receipt.UpdateModelPropertiesFrom(result.Model);
            receipt.StateL = LocalStatus.Procesado;
            result.UpdateEntityVersion();
            GoodsReceiptHelper.SaveTransaction(receipt);
            return receipt;
        }

        private static ODPI_DownPayment SyncUpload(ODPI_DownPayment downPayment)
        {
            var result = WebApiClient.AddDownPayment(downPayment).Result;
            downPayment.UpdateModelPropertiesFrom(result.Model);
            downPayment.StateL = LocalStatus.Procesado;
            result.UpdateEntityVersion();
            return downPayment;
        }

        private static OIGE_GoodsIssues SyncUpload(OIGE_GoodsIssues good)
        {
            var result = WebApiClient.AddGoodIssue(good).Result;
            good.UpdateModelPropertiesFrom(result.Model);
            good.StateL = LocalStatus.Procesado;
            result.UpdateEntityVersion();
            GoodIssuesHelper.SaveTransaction(good);
            return good;
        }

        private static ORIN_ClientCreditNotes SyncUpload(ORIN_ClientCreditNotes note)
        {
            var result = WebApiClient.AddSaleCreditNote(note).Result;
            note.UpdateModelPropertiesFrom(result.Model);
            note.StateL = LocalStatus.Procesado;
            result.UpdateEntityVersion();
            ClientCreditNoteHelper.SaveTransaction(note);
            return note;
        }
        // Common method   for View Modes.

        public static ORIN_ClientCreditNotes Synchronize(ORIN_ClientCreditNotes note)
        {
            CheckForUpdates();// Actualizar cambios antes de subir y verificar si el objeto no fue ya procesado
            note  = ClientCreditNoteHelper.Get(note.IdClientCreditNoteL);
            if (note.StateL != LocalStatus.Procesado) return note.Upload(SyncUpload);
            else return note;
        }

        public static OPCH_Purchase Synchronize(OPCH_Purchase model)  
        {
            
            CheckForUpdates();// Actualizar cambios antes de subir y verificar si el objeto no fue ya procesado
            model = PurchaseHelper.Get(model.IdPurchase);
            if (model.StateL != LocalStatus.Procesado) return model.Upload(SyncUpload);
            else return model;
        }
        
        public static OINV_Sales Synchronize(OINV_Sales model)
        {
            
            CheckForUpdates();// Actualizar cambios antes de subir y verificar si el objeto no fue ya procesado
            model = SalesHelper.Get(model.IdSaleL);
            if (model.StateL != LocalStatus.Procesado) return model.Upload(SyncUpload);
            else return model;
        }
        
        public static ORPC_SupplierCreditNotes Synchronize(ORPC_SupplierCreditNotes model)
        {
            
            CheckForUpdates();// Actualizar cambios antes de subir y verificar si el objeto no fue ya procesado
            model = SupplierCreditNoteHelper.Get(model.IdSupplierCreditNote);
            if (model.StateL != LocalStatus.Procesado) return model.Upload(SyncUpload);
            else return model;
        }
        
        public static OIGN_GoodsReceipt Synchronize(OIGN_GoodsReceipt model)
        {
            
            CheckForUpdates();// Actualizar cambios antes de subir y verificar si el objeto no fue ya procesado
            model = GoodsReceiptHelper.Get(model.IdGoodReceiptL);
            if (model.StateL != LocalStatus.Procesado) return model.Upload(SyncUpload);
            else return model;
        }
        
        public static ODPI_DownPayment Synchronize(ODPI_DownPayment model)
        {
            
            CheckForUpdates();// Actualizar cambios antes de subir y verificar si el objeto no fue ya procesado
            model = DownPaymentHelper.Get(model.IdDownPayment);
            if (model.StateL != LocalStatus.Procesado) return model.Upload(SyncUpload);
            else return model;
        }
        
        public static OIGE_GoodsIssues Synchronize(OIGE_GoodsIssues model)
        {
            CheckForUpdates();// Actualizar cambios antes de subir y verificar si el objeto no fue ya procesado
            model = GoodIssuesHelper.Get(model.IdGoodIssueL);
           if (model.StateL != LocalStatus.Procesado) return model.Upload(SyncUpload);
           else return model;
        } 
        
        public static OWTQ_TransferRequest Synchronize(OWTQ_TransferRequest model)
        {
            CheckForUpdates();// Actualizar cambios antes de subir y verificar si el objeto no fue ya procesado
            model = TransferRequestHelper.Get(model.IdTransferRequestL);
            if (model.StateL != LocalStatus.Procesado) return model.Upload(SyncUpload);
            else return model;
        }

        #endregion

        public static void NotifyException<Tmodel>(Tmodel model)
        {
            // Error no considerado en la app. siel objeto tiene ID local pero no se encuentra en la DB.
            // TODO implement  To notificaions panel . on Start Page. 
        }

        // This class Extension . to Upload
        private static Tmodel Upload<Tmodel>(this Tmodel model, Func<Tmodel, Tmodel> function, bool throwException = false)
        {
            //if (!WebApiClient.IsAvailableConnection)
            //{
            //    if (onConnectionError != null) onConnectionError(new ApiException
            //                                                     {
            //                                                        Message = "Error De Conexión",
            //                                                        ExceptionMessage = "Intentelo Cuando Se Restablezca",
            //                                                     });
            //    return model;
            //}
            try
            {
                lock (Extensions.SyncLock)
                {
                    return function(model);
                }
            }
            catch (Exception ex)
            {
                LoggerHelper.WriteLog(ex);
                if (throwException) throw ex;
                return model;
            }

        }
        
        private static Stack<LOG_CHANGES> PendingChanges = new Stack<LOG_CHANGES>();

        private static  int changesetsCount = 0;
        private static int index = 0;
        private static void ShowProgress(string text)
        {
            if (onReportProgress != null) onReportProgress(text);
        }
        public static event Action OnBeginSync;
        public static  event Action onSyncComplete;

        public static event Action onSyncItem;

        public static event Action<string> onReportProgress;

        public static event Action<ApiException> onConnectionError;

        private static ConnectionBridge Bridge;

        public  static event Action onBussinesPartnerUpdate;
    }
}
