using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using StockAdminContext.Models;

namespace StockAdminContext.Helpers
{
    public static class SalesHelper
    {
        public static ObservableCollection<OINV_Sales> GetSales( NNM1_Series serie,
            DateTime? startDate, DateTime? endDate, string keywords, LocalStatus? status)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                IQueryable<OINV_Sales> sales = db.OINV_Sales.Include(s =>s.PaymentType).Include(s => s.RoyaltyPaymentType)
                                               .Include(s=> s.OWHS_Branch).Include(s => s.INV1_SalesDetail).Include("INV1_SalesDetail.OITM_Articles");

                if (serie!=null) sales = sales.Where(t => t.Series == serie.Series);

                if (!string.IsNullOrEmpty(keywords)) sales = sales.Where(t => t.Comments.Contains(keywords));

                if (startDate.HasValue) sales = sales.Where(d => d.DocDate >= startDate.Value);

                if (endDate.HasValue) sales = sales.Where(t => t.DocDate <= endDate.Value);

                if (status.HasValue) sales = sales.Where(t => t.StateL == status.Value);

                
                var queryNames = (from sale in sales
                                  join srie in db.NNM1_Series on sale.Series equals srie.Series
                                  select new
                                  {
                                      saleId = sale.IdSaleL,
                                      SerieTitle = srie.SeriesName + " " + srie.Remark
                                  }).ToList();

                sales.ToList().ForEach(
                        trl =>
                        {
                            var filler = queryNames.FirstOrDefault(f => f.saleId == trl.IdSaleL);
                            if (filler != null) trl.SeriesTitle = filler.SerieTitle;
                        });

                return new ObservableCollection<OINV_Sales>(sales);
            }
        }

        public static ObservableCollection<PaymentType> GetPaymentTypes(bool isRoyality= false)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                var payments = db.PaymentTypes.Where(p => p.IsRoyalty == isRoyality);

                return new ObservableCollection<PaymentType>(payments);
            }
        }

        public static ObservableCollection<PaymentType> GetOnlyPaymentTypes()
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                var payments = db.PaymentTypes.Where(p => p.IsRoyalty == false && p.IdPaymentType != 5);

                return new ObservableCollection<PaymentType>(payments);
            }
        }

        public static void AddSale(OINV_Sales selectedSale)
        {
            if (selectedSale == null) return;
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();
                if(selectedSale.IdSaleL==0)db.OINV_Sales.Add(selectedSale);
                else 
                {
                    selectedSale.ModifiedDateL = DateTime.Now;
                    selectedSale.ModifiedByL = Config.CurrentUser;
                }
            }
        }

        public static void DeleteSale(OINV_Sales selectedSale)
        {
            if (selectedSale == null) return;

            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                db.OINV_Sales.Remove(selectedSale);
            }
        }

        public static void DeleteDetail(INV1_SalesDetail selectedSaleDetail)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                db.INV1_SalesDetail.Remove(selectedSaleDetail);
            }
        }

        public static List<OINV_Sales> GetPendingToSyncSales()
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                return db.OINV_Sales.Include("INV1_SalesDetail")
                    .Where(s => s.StateL == LocalStatus.Pendiente).ToList();
            }
        }

        public static void SaveTransaction(OINV_Sales obj)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                var transfers = obj.INV1_SalesDetail.Select(t => new OINM_Transaction(obj, t)).ToList();

                transfers.ForEach(t => db.OINM_Transaction.Add(t));

                ContextFactory.SaveChanges();

                //Actualizar existencias

                //var articles = (from detail in obj.INV1_SalesDetail
                //                join article in db.OITW_BranchArticles on detail.ItemCode equals article.ItemCode
                //                where article.WhsCode == detail.WhsCode
                //                select article).ToList();

                //articles.ForEach(a =>
                //{
                //    var detail = obj.INV1_SalesDetail.FirstOrDefault(d => d.ItemCode == a.ItemCode);
                //    a.OnHand = a.OnHand - detail.Quantity;
                //    a.OnHand1 = a.OnHand1 - detail.Quantity;
                //});

                //ContextFactory.SaveChanges();
                StoredCallbackProcessor.UpdateStock();
            }
        }

        public static OINV_Sales Get(int id)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();
                return db.OINV_Sales.Include("INV1_SalesDetail")
                       .FirstOrDefault(p => p.IdSaleL == id);
            }
        }

        public static bool VerifyNumAtCard(OINV_Sales selectedSale)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                return db.OINV_Sales.Any(s => s.NumAtCard == selectedSale.NumAtCard && s.IdSaleL!=selectedSale.IdSaleL);
            }
        }

        public static List<InvoicePayment> GetiInvoicePayments(OINV_Sales sale)
        {

            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                var cashAccount = db.PaymentTypes
                    .FirstOrDefault(p=> p.IdPaymentType == 1); // Cuenta para efectivo

                var creditAccount = db.PaymentTypes
                    .FirstOrDefault(p => p.IdPaymentType == 2); // Cuenta para tarjeta..


                var TDate = sale.DocDate;


                var pagosaloha = (from inaloha in db.InvoiceALOHAs
                             where inaloha.Date == TDate
                             select new
                             {
                                 alohaid = inaloha.IdInvoiceALOHA,
                                 cash = inaloha.Cash,
                                 credit = inaloha.Credit,
                             }).FirstOrDefault();

                var pagos = new List<InvoicePayment>();

                //pago en efectivo
                var pagoe = new InvoicePayment();
                pagoe.WhsCode = Config.WhsCode;
                pagoe.Amount = pagosaloha.cash;
                pagoe.PaymentType = cashAccount;

                pagos.Add(pagoe);

                //pago tarjeta
                var pagot = new InvoicePayment();
                pagot.WhsCode = Config.WhsCode;
                pagot.Amount = pagosaloha.credit;
                pagot.PaymentType = creditAccount;

                pagos.Add(pagot);

                return pagos;            
            }
        } 
    }
}
