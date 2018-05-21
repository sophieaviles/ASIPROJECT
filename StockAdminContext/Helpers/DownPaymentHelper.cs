using System;   
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using StockAdminContext.Models;

namespace StockAdminContext.Helpers
{
    public static class DownPaymentHelper
    {
        public static ObservableCollection<ODPI_DownPayment> GetDownPaymentsProcessed(OCRD_BusinessPartner selectedPartner, NNM1_Series serie)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();
                IQueryable<ODPI_DownPayment> downPayments = null;

                if (serie != null)
                {
                    if (serie.Series == 43)
                           downPayments =  db.ODPI_DownPayment.Include(dp => dp.DPI1_DownPaymentDetail )
                                  .Where(dp => dp.StateL == LocalStatus.Procesado && dp.CardCode 
                                      == selectedPartner.CardCode && (dp.IdSaleL == null || dp.IdSaleL == 0)
                                      && dp.Series == 31);
                    if (serie.Series == 42)
                         downPayments =  db.ODPI_DownPayment.Include(dp => dp.DPI1_DownPaymentDetail )
                                  .Where(dp => dp.StateL == LocalStatus.Procesado && dp.CardCode 
                                      == selectedPartner.CardCode && (dp.IdSaleL == null || dp.IdSaleL == 0)
                                      && dp.Series == 118);
                }


                var existing = from sale in db.OINV_Sales.Where(d => d.dpEntry != 0)
                    join dp in downPayments on sale.dpEntry equals dp.DocEntry
                    select dp;

                var existingByDetails = from detail in db.RIN1_ClientCreditNoteDetail
                    join dp in downPayments on detail.BaseEntry equals dp.DocEntry
                    select dp;

                downPayments = downPayments.Where(d => !existing.Contains(d) && !existingByDetails.Contains(d));

                //// TODO : implementar mejora de rendimiento hacer Tolist de todos los ids de la venta es lento  , implementar un Left JOin , not Exists
                //// para no mostrar anticipos cancelados .. 

                var downpaymentsLIst = downPayments.ToList();
                var queryNames = (from dp in downpaymentsLIst
                                  join srie in db.NNM1_Series on dp.Series equals srie.Series
                                  select new
                                  {
                                      DownPaymentId = dp.IdDownPayment,
                                      SerieTitle = srie.SeriesName + " " + srie.Remark
                                  }).ToList();
                
                downpaymentsLIst.ForEach(
                        trl =>
                        {
                            var filler = queryNames.FirstOrDefault(f => f.DownPaymentId == trl.IdDownPayment);
                            if (filler != null) trl.SeriesTitle = filler.SerieTitle;
                        });

                return new ObservableCollection<ODPI_DownPayment>(downpaymentsLIst);
            }
        }

        public static ObservableCollection<ODPI_DownPayment> GetDownPayments(
            NNM1_Series serie, DateTime? startDate, DateTime? endDate, string keyWords,LocalStatus? localstatus,
            bool IsAsNoTrackingForDownPayment)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                IQueryable<ODPI_DownPayment> downPayments = db.ODPI_DownPayment.Include("DPI1_DownPaymentDetail");

                if (serie!=null) downPayments = downPayments.Where(dp => dp.Series == serie.Series);

                if (!string.IsNullOrEmpty(keyWords))downPayments = downPayments.Where(dp => dp.Comments.Contains(keyWords));

                if (startDate.HasValue) downPayments = downPayments.Where(dp => dp.DocDate >= startDate.Value);

                if (endDate.HasValue) downPayments = downPayments.Where(dp => dp.DocDate <= endDate.Value);

                if (localstatus.HasValue) downPayments = downPayments.Where(dp => dp.StateL == localstatus.Value);


                var queryNames = (from dp in downPayments
                                  join srie in db.NNM1_Series on dp.Series equals srie.Series
                                  select new
                                  {
                                      DownPaymentId = dp.IdDownPayment,
                                      SerieTitle = srie.SeriesName + " " + srie.Remark
                                  }).ToList();

                // con el as notracking resuelve el bug  de ventas serie43
                var colecction =IsAsNoTrackingForDownPayment ? downPayments.AsNoTracking().ToList(): downPayments.ToList();
                colecction.ForEach(
                        trl =>
                        {
                            var filler = queryNames.FirstOrDefault(f => f.DownPaymentId == trl.IdDownPayment);
                            if (filler != null) trl.SeriesTitle = filler.SerieTitle;
                        });


                return new ObservableCollection<ODPI_DownPayment>(colecction);
            }
        }


        public static void Add(ODPI_DownPayment downpayment)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                if (downpayment.IdDownPayment == 0) db.ODPI_DownPayment.Add(downpayment);
                else
                {
                    downpayment.ModifiedByL = Config.CurrentUser;
                    downpayment.ModifiedDateL = DateTime.Now;
                }
            }
        }

        public static void Delete(ODPI_DownPayment selectedDownPayment)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();
                db.ODPI_DownPayment.Remove(selectedDownPayment);
            }
        }

        public static void DeleteDetail(DPI1_DownPaymentDetail selectedDownPaymentDetail)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();
                var detail =
                    db.DPI1_DownPaymentDetail.FirstOrDefault(
                        dt =>
                            dt.IdDownPaymentDetail == selectedDownPaymentDetail.IdDownPaymentL &&
                            dt.IdDownPaymentL == selectedDownPaymentDetail.IdDownPaymentL);
                if (detail != null) db.DPI1_DownPaymentDetail.Remove(detail);
            }
        }
        
        public static ODPI_DownPayment GetDownPaymentInSale(OINV_Sales sale,bool asNotrack)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();
                return asNotrack ?
                    db.ODPI_DownPayment.Include("DPI1_DownPaymentDetail").AsNoTracking()
                    .FirstOrDefault(dp => dp.CardCode == sale.CardCode && dp.DocEntry == sale.dpEntry):

                    db.ODPI_DownPayment.Include("DPI1_DownPaymentDetail")
                    .FirstOrDefault(dp => dp.CardCode == sale.CardCode && dp.DocEntry == sale.dpEntry);
            }
        }

        public static ODPI_DownPayment ForceUpdateToDataBase(this ODPI_DownPayment downPayment)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                return db.ODPI_DownPayment.Find(downPayment.IdDownPayment);
            }
        }

        public static List<ODPI_DownPayment> GetPendingToSync()
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();
                return
                    db.ODPI_DownPayment.Include("DPI1_DownPaymentDetail")
                        .Where(dp => dp.StateL == LocalStatus.Procesado ).ToList();
            }
        }

        public static ODPI_DownPayment Get(int id)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();
                return db.ODPI_DownPayment.Include("DPI1_DownPaymentDetail")
                       .FirstOrDefault(p => p.IdDownPayment == id);
            }
        }

      
    }
}
