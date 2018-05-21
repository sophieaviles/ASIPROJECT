using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Transactions;
using StockAdminContext.Models;
using System.Data.Linq;
using RefreshMode = System.Data.Entity.Core.Objects.RefreshMode;

namespace StockAdminContext.Helpers
{
    public static class ClientCreditNoteHelper
    {
        public static ObservableCollection<ORIN_ClientCreditNotes> GetCreditNotes(ClientCreditNoteType type, NNM1_Series serie, DateTime? startDate, DateTime? endDate, string keywords, LocalStatus? status)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();
                var creditnotes = db.ORIN_ClientCreditNotes.Include(ccn => ccn.RIN1_ClientCreditNoteDetail);

                
                if (!string.IsNullOrEmpty(keywords)) creditnotes = creditnotes.Where(t => t.Comments.Contains(keywords));

                if (startDate.HasValue) creditnotes = creditnotes.Where(d => d.DocDate >= startDate.Value);

                if (endDate.HasValue) creditnotes = creditnotes.Where(t => t.DocDate <= endDate.Value);

                if (serie != null) creditnotes = creditnotes.Where(d => d.Series == serie.Series);

                if (status.HasValue) creditnotes = creditnotes.Where(t => t.StateL == status.Value);

                var queryNames = (from note in creditnotes.ToList()
                                  join srie in db.NNM1_Series on note.Series equals srie.Series
                                  where serie != null
                                  select new
                                  {
                                      NoteId = note.IdClientCreditNoteL,
                                      Title = serie.SeriesName + " " + serie.Remark
                                  }).ToList();

                creditnotes.ToList().ForEach(
                        trl =>
                        {
                            var name = queryNames.FirstOrDefault(q => q.NoteId == trl.IdClientCreditNoteL);
                            trl.SeriesTitle = name != null ? name.Title : string.Empty;
                        });
                return new ObservableCollection<ORIN_ClientCreditNotes>(creditnotes.OrderByDescending(t => t.ModifiedDateL).ThenBy(t => t.StateL));
            }
        }
        public static void Add(ORIN_ClientCreditNotes creditNote)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();
                using (var scope = new TransactionScope())
                {
                    db.ORIN_ClientCreditNotes.Add(creditNote);
                    db.SaveChanges();
                    scope.Complete();
                }
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

        public static void Delete(ORIN_ClientCreditNotes creditNote)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();
                using (var scope = new TransactionScope())
                {
                    creditNote.RIN1_ClientCreditNoteDetail.ToList().ForEach(t => db.RIN1_ClientCreditNoteDetail.Remove(t));
                    db.ORIN_ClientCreditNotes.Remove(creditNote);
                    db.SaveChanges();
                    scope.Complete();
                }
            }
        }
        
        public static void DeleteDetail(RIN1_ClientCreditNoteDetail selectedtDetail)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                db.RIN1_ClientCreditNoteDetail.Remove(selectedtDetail);
            }
        }

        public static List<ORIN_ClientCreditNotes> GetPendingtoSyncNotes()
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                return db.ORIN_ClientCreditNotes.Include(ccn => ccn.RIN1_ClientCreditNoteDetail)
                    .Where(note => note.StateL == LocalStatus.Pendiente)
                    .ToList();
            }
        }

        public static void SaveTransaction(ORIN_ClientCreditNotes obj)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                var transfers = obj.RIN1_ClientCreditNoteDetail.Select(t => new OINM_Transaction(obj, t)).ToList();

                transfers.ForEach(t => db.OINM_Transaction.Add(t));

                ContextFactory.SaveChanges();

                //Actualizar existencias

                //var articles = (from detail in obj.RIN1_ClientCreditNoteDetail
                //                join article in db.OITW_BranchArticles on detail.ItemCode equals article.ItemCode
                //                where article.WhsCode == detail.WhsCode
                //                select article).ToList();

                //articles.ForEach(a =>
                //{
                //    var detail = obj.RIN1_ClientCreditNoteDetail.FirstOrDefault(d => d.ItemCode == a.ItemCode);
                //    a.OnHand = a.OnHand + detail.Quantity;
                //    a.OnHand1 = a.OnHand1 + detail.Quantity;
                //});

                //ContextFactory.SaveChanges();

                StoredCallbackProcessor.UpdateStock();                
            }
        }

        public static bool ExistCreditNote(OINV_Sales SelectedSale)
        {
           var ex = ContextFactory.GetDBContext()
                .RIN1_ClientCreditNoteDetail.Any(
                    ccd => ccd.BaseDocNum == SelectedSale.DocNum && ccd.BaseEntry == SelectedSale.DocEntry);

            return ex;
        }

        public static ORIN_ClientCreditNotes Get(int idClientCreditNoteL)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                return db.ORIN_ClientCreditNotes.Include(c => c.RIN1_ClientCreditNoteDetail)
                    .FirstOrDefault(n => n.IdClientCreditNoteL == idClientCreditNoteL);
            }
        }
    }
}
