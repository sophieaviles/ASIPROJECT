using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Transactions;
using StockAdminContext.Models;

namespace StockAdminContext.Helpers
{
    public static class SupplierCreditNoteHelper
    {
        public static ObservableCollection<ORPC_SupplierCreditNotes> GetPurchase(NNM1_Series serie, DateTime? startDate, DateTime? endDate, string keywords, LocalStatus? status)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();
                IQueryable<ORPC_SupplierCreditNotes> creditnotes = db.ORPC_SupplierCreditNotes.Include(ccn => ccn.RPC1_SupplierCreditNoteDetail).Include("RPC1_SupplierCreditNoteDetail.OITM_Articles");

                //var p = new OPCH_Purchase();

                

                if (!string.IsNullOrEmpty(keywords)) creditnotes = creditnotes.Where(t => t.Comments.Contains(keywords));

                if (startDate.HasValue) creditnotes = creditnotes.Where(d => d.DocDate >= startDate.Value);

                if (endDate.HasValue) creditnotes = creditnotes.Where(t => t.DocDate <= endDate.Value);

                if (serie != null) creditnotes = creditnotes.Where(d => d.Series == serie.Series);

                if (status.HasValue) creditnotes = creditnotes.Where(t => t.StateL == status.Value);

                var queryNames = (from note in creditnotes.ToList()
                    join srie in db.NNM1_Series on note.Series equals srie.Series
                    where serie!=null
                    select new
                           {
                               NoteId = note.IdSupplierCreditNote,
                               Title = serie.SeriesName+" "+ serie.Remark
                           }).ToList();

                creditnotes.ToList().ForEach(
                        trl =>
                        {
                            //var filler = db.NNM1_Series.FirstOrDefault(b => b.Series == trl.Series);
                            //if (filler != null) trl.SeriesTitle = filler.SeriesName + " " + filler.Remark;
                            var name = queryNames.FirstOrDefault(q => q.NoteId == trl.IdSupplierCreditNote);
                            trl.SeriesTitle = name != null ? name.Title : string.Empty;
                        });
                return new ObservableCollection<ORPC_SupplierCreditNotes>(creditnotes.OrderByDescending(t => t.ModifiedDateL).ThenBy(t => t.StateL));
            }
        }
        
        public static void Add(ORPC_SupplierCreditNotes creditNote)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();
                using (var scope = new TransactionScope())
                {
                    db.ORPC_SupplierCreditNotes.Add(creditNote);
                    db.SaveChanges();
                    scope.Complete();
                }
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

        public static void Delete(ORPC_SupplierCreditNotes creditNote)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();
                using (var scope = new TransactionScope())
                {
                    creditNote.RPC1_SupplierCreditNoteDetail.ToList().ForEach(t => db.RPC1_SupplierCreditNoteDetail.Remove(t));
                    db.ORPC_SupplierCreditNotes.Remove(creditNote);
                    db.SaveChanges();
                    scope.Complete();
                }
            }
        }

        public static void DeleteDetail(RPC1_SupplierCreditNoteDetail selectedtDetail)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                db.RPC1_SupplierCreditNoteDetail.Remove(selectedtDetail);
            }
        }

        public static List<ORPC_SupplierCreditNotes> GetPendingtoSyncNotes()
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                return db.ORPC_SupplierCreditNotes.Include(ccn => ccn.RPC1_SupplierCreditNoteDetail)
                    .Where(note=> note.StateL == LocalStatus.Pendiente)
                    .ToList();
            }
        }

        public static void SaveTransaction(ORPC_SupplierCreditNotes obj)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                var transfers = obj.RPC1_SupplierCreditNoteDetail.Select(t => new OINM_Transaction(obj, t)).ToList();

                transfers.ForEach(t => db.OINM_Transaction.Add(t));

                ContextFactory.SaveChanges();

                //Actualizar existencias

                //var articles = (from detail in obj.RPC1_SupplierCreditNoteDetail
                //                join article in db.OITW_BranchArticles on detail.ItemCode equals article.ItemCode
                //                where article.WhsCode == detail.WhsCode
                //                select article).ToList();

                //articles.ForEach(a =>
                //{
                //    var detail = obj.RPC1_SupplierCreditNoteDetail.FirstOrDefault(d => d.ItemCode == a.ItemCode);
                //    a.OnHand = a.OnHand - detail.Quantity;
                //    a.OnHand1 = a.OnHand1 - detail.Quantity;
                //});

                //ContextFactory.SaveChanges();

                StoredCallbackProcessor.UpdateStock();

            }
        }

        internal static ORPC_SupplierCreditNotes Get(int id)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();
                return db.ORPC_SupplierCreditNotes.Include("RPC1_SupplierCreditNoteDetail")
                       .FirstOrDefault(p => p.IdSupplierCreditNote == id);
            }
        }
    }
}
