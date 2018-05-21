using StockAdminContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Data.Entity;

namespace StockAdminContext.Helpers
{
    public static class GoodsReceiptHelper
    {
        public static ObservableCollection<OIGN_GoodsReceipt> GetGoodsReceipts(
            DateTime? startDate, DateTime? endDate, short groudId = -1, string keywords = "", LocalStatus? status = null)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();
                IQueryable<OIGN_GoodsReceipt> GoodsReceipt = db.OIGN_GoodsReceipt.Include("Movement").Include("IGN1_GoodsReceiptDetail.OITM_Articles")
                .Include(e => e.IGN1_GoodsReceiptDetail);

                if (startDate.HasValue) GoodsReceipt = GoodsReceipt.Where(d => d.DocDate >= startDate.Value);

                if (endDate.HasValue) GoodsReceipt = GoodsReceipt.Where(t => t.DocDate <= endDate.Value);

                if (groudId != -1) GoodsReceipt = GoodsReceipt.Where(t => t.ItmsGrpCod == groudId);

                if (!String.IsNullOrEmpty(keywords)) GoodsReceipt = GoodsReceipt.Where(t => t.Comments.Contains(keywords));

                if (status.HasValue) GoodsReceipt = GoodsReceipt.Where(t => t.StateL == status);


                GoodsReceipt.ToList().ForEach(
                       trl =>
                       {
                           var filler = db.OWHS_Branch.FirstOrDefault(b => b.WhsCode == trl.WhsCode);
                           if (filler != null) trl.FillerTitle = trl.WhsCode + " " + filler.WhsName;
                       });
                
                return new ObservableCollection<OIGN_GoodsReceipt>(GoodsReceipt.OrderByDescending(t => t.ModifiedDateL).ThenBy(t => t.StateL));
                
            }
        }

        public static void DeleteGoodsReceipt(OIGN_GoodsReceipt selectedGoodsReceipt)
        {
            if (selectedGoodsReceipt == null) return;

            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                if (selectedGoodsReceipt.IdGoodReceiptL > 0)
                {
                   // selectedGoodsReceipt.IGN1_GoodsReceiptDetail.ForEach(d => d.ChangeEntityState(EntityState.Deleted));

                    db.OIGN_GoodsReceipt.Remove(selectedGoodsReceipt);
                }
            }
        }

        public static void DeleteGoodsReceiptDetail(IGN1_GoodsReceiptDetail selectedGoodsReceiptsDetail)
        {
            if (selectedGoodsReceiptsDetail == null) return;

            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                db.IGN1_GoodsReceiptDetail.Remove(selectedGoodsReceiptsDetail);
            }
        }

        public static bool SaveNewDetailsCommand(OIGN_GoodsReceipt SelectedGoodsReceipt, OITB_Groups SelectedGroup, UMovement SelectedMovement)
        {
            bool needUpdate = false;
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                SelectedGoodsReceipt.ItmsGrpCod = SelectedGroup.ItmsGrpCod;

                SelectedGoodsReceipt.IdMovement = SelectedMovement.Code;

                //SelectedGoodsReceipt.IGN1_GoodsReceiptDetail = GoodsReceiptsDetails;

                //if (!db.OIGN_GoodsReceipt.Any(g => g.IdGoodReceiptL == SelectedGoodsReceipt.IdGoodReceiptL))
                //{
                //    //GoodsReceiptsDetails.ToList().ForEach(d => SelectedGoodsReceipt.IGN1_GoodsReceiptDetail.Add(d));
                //    needUpdate = true;
                //    db.OIGN_GoodsReceipt.Add(SelectedGoodsReceipt);
                //}
                if (SelectedGoodsReceipt.IdGoodReceiptL == 0)
                {
                    db.OIGN_GoodsReceipt.Add(SelectedGoodsReceipt);
                }
                else if (db.Entry(SelectedGoodsReceipt).State == EntityState.Modified)
                {
                    SelectedGoodsReceipt.ModifiedDateL = DateTime.Now;
                    SelectedGoodsReceipt.ModifiedByL = Config.CurrentUser;
                }

                ContextFactory.SaveChanges();                       
            }

            return needUpdate;
        }

        public static List<OIGN_GoodsReceipt> GetPendingToSync()
        {
            lock (Extensions.Locker)
            {
                return ContextFactory.GetDBContext()
                    .OIGN_GoodsReceipt.Include("IGN1_GoodsReceiptDetail")
                    .Where(r => r.StateL == LocalStatus.Pendiente).ToList();
            }
        }

        public static void DeleteDetail(IGN1_GoodsReceiptDetail selectedArticleDetail)
        {
            lock (Extensions.Locker)
            {
                if (selectedArticleDetail.IdGoodReceiptDetail > 0)
                {
                    selectedArticleDetail.ChangeEntityState(EntityState.Deleted);
                }
            }
        }

        public static void SaveTransaction(OIGN_GoodsReceipt obj)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                var transfers = obj.IGN1_GoodsReceiptDetail.Select(t => new OINM_Transaction(obj, t)).ToList();

                transfers.ForEach(t => db.OINM_Transaction.Add(t));

                ContextFactory.SaveChanges();

                //Actualizar existencias

                //var articles = (from detail in obj.IGN1_GoodsReceiptDetail
                //                join article in db.OITW_BranchArticles on detail.ItemCode equals article.ItemCode
                //                where article.WhsCode == detail.WhsCode
                //                select article).ToList();

                //articles.ForEach(a =>
                //{
                //    var detail = obj.IGN1_GoodsReceiptDetail.FirstOrDefault(d => d.ItemCode == a.ItemCode);
                //    a.OnHand = a.OnHand + detail.Quantity;
                //    a.OnHand1 = a.OnHand1 + detail.Quantity;
                //});

                //ContextFactory.SaveChanges();
                StoredCallbackProcessor.UpdateStock();
            }
        }

        internal static OIGN_GoodsReceipt Get(int id)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();
                return db.OIGN_GoodsReceipt.Include("IGN1_GoodsReceiptDetail")
                       .FirstOrDefault(p => p.IdGoodReceiptL == id);
            }
        }
    }
}
