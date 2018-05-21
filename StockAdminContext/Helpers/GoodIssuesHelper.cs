using System.Collections.Generic;
using StockAdminContext.Models;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Data.Entity;

namespace StockAdminContext.Helpers
{
    public static class GoodIssuesHelper
    {
        public static ObservableCollection<OIGE_GoodsIssues> GetGoodsIssues(
               DateTime? startDate, DateTime? endDate, short groudId = -1, string keywords = "", LocalStatus? status = null)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();
                IQueryable<OIGE_GoodsIssues> goodsUsIssueses = db.OIGE_GoodsIssues.Include("Movement").Include("IGE1_GoodsIssueDetail.OITM_Articles")
                    .Include(e => e.IGE1_GoodsIssueDetail);

                if (startDate.HasValue) goodsUsIssueses = goodsUsIssueses.Where(d => d.DocDate >= startDate.Value);

                if (endDate.HasValue) goodsUsIssueses = goodsUsIssueses.Where(t => t.DocDate <= endDate.Value);

                if (groudId != -1) goodsUsIssueses = goodsUsIssueses.Where(t => t.ItmsGrpCod == groudId);

                if (!String.IsNullOrEmpty(keywords)) goodsUsIssueses = goodsUsIssueses.Where(t => t.Comments.Contains(keywords));

                if (status.HasValue)  goodsUsIssueses = goodsUsIssueses.Where(t => t.StateL == status.Value);



                goodsUsIssueses.ToList().ForEach(
                       trl =>
                       {
                           var filler = db.OWHS_Branch.FirstOrDefault(b => b.WhsCode == trl.WhsCode);
                           if (filler != null) trl.FillerTitle = trl.WhsCode + " " + filler.WhsName;
                       });

                return new ObservableCollection<OIGE_GoodsIssues>(goodsUsIssueses.OrderByDescending(t => t.ModifiedDateL).ThenBy(t => t.StateL));

            }
        }

        public static void DeleteGoodsReceipt(OIGE_GoodsIssues selectedGoodsIssues)
        {
            if (selectedGoodsIssues == null) return;

            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                if(selectedGoodsIssues.IdGoodIssueL!=0) db.OIGE_GoodsIssues.Remove(selectedGoodsIssues);
            }
        }

        public static void DeleteGoodsIssuesDetail(IGE1_GoodsIssueDetail selectedGoodsIssuesDetail)
        {
            if (selectedGoodsIssuesDetail == null) return;

            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                db.IGE1_GoodsIssueDetail.Remove(selectedGoodsIssuesDetail);
            }
        }

        public static bool SaveDetails(OIGE_GoodsIssues SelectedGoodsIssues, OITB_Groups SelectedGroup,UMovement SelectedMovement)
        {
            bool needUpdate = false;
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                SelectedGoodsIssues.ItmsGrpCod = SelectedGroup.ItmsGrpCod;

                SelectedGoodsIssues.IdMovement = SelectedMovement.Code;

                //SelectedGoodsIssues.IGN1_GoodsReceiptDetail = GoodsIssuesDetails;

                //if (!db.OIGE_GoodsIssues.Any(g => g.IdGoodIssueL == SelectedGoodsIssues.IdGoodIssueL))
                //{
                //    //GoodsIssuesDetails.ToList().ForEach(d => SelectedGoodsIssues.IGE1_GoodsIssueDetail.Add(d));
                //    needUpdate = true;
                //    db.OIGE_GoodsIssues.Add(SelectedGoodsIssues);
                //}
                if (SelectedGoodsIssues.IdGoodIssueL == 0)
                {
                    db.OIGE_GoodsIssues.Add(SelectedGoodsIssues);
                }
                else if (db.Entry(SelectedGoodsIssues).State == EntityState.Modified)
                {
                    SelectedGoodsIssues.ModifiedDateL = DateTime.Now;
                    SelectedGoodsIssues.ModifiedByL = Config.CurrentUser;
                }

               ContextFactory.SaveChanges();
                               
            }
            return needUpdate;
        }
    
        public static List<OIGE_GoodsIssues> GetPendingToSync()
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                return db.OIGE_GoodsIssues.Include("Movement").Include(e => e.IGE1_GoodsIssueDetail)
                    .Where(e => e.StateL == LocalStatus.Pendiente).ToList();
            }
        }

        public static void DeleteDetail(IGE1_GoodsIssueDetail selectedArticleDetail)
        {
            lock (Extensions.Locker)
            {
                if (selectedArticleDetail!=null&&  selectedArticleDetail.IdGoodIssueDetail > 0)
                {
                    selectedArticleDetail.ChangeEntityState(EntityState.Deleted);
                }
            }

        }

        public static void SaveTransaction(OIGE_GoodsIssues obj)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                var transfers = obj.IGE1_GoodsIssueDetail.Select(t => new OINM_Transaction(obj, t)).ToList();

                transfers.ForEach(t => db.OINM_Transaction.Add(t));

                ContextFactory.SaveChanges();

                //Actualizar existencias

                //var articles = (from detail in obj.IGE1_GoodsIssueDetail
                //                join article in db.OITW_BranchArticles on detail.ItemCode equals article.ItemCode
                //                where article.WhsCode == detail.WhsCode
                //                select article).ToList();

                //articles.ForEach(a =>
                //{
                //    var detail = obj.IGE1_GoodsIssueDetail.FirstOrDefault(d => d.ItemCode == a.ItemCode);
                //    a.OnHand = a.OnHand - detail.Quantity;
                //    a.OnHand1 = a.OnHand1 - detail.Quantity;
                //});

                //ContextFactory.SaveChanges();

                StoredCallbackProcessor.UpdateStock();
            }
        }

        public static OIGE_GoodsIssues Get(int id)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();
                return db.OIGE_GoodsIssues.Include("IGE1_GoodsIssueDetail")
                       .FirstOrDefault(p=> p.IdGoodIssueL==id);
            }
        }
    }
}
