using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using StockAdminContext.Models;

namespace StockAdminContext.Helpers
{
    public  static class SpecialOrdersHelper
    {

        public static ObservableCollection<ORDR_SpecialOrder> GetOrders(string warehouse,
            DateTime? startDate, DateTime? endDate, string keywords,LocalStatus? status)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                IQueryable<ORDR_SpecialOrder> orders = db.ORDR_SpecialOrder.Include(d=> d.RDR1_SpecialOrderDetail)
                                                      .Include("ATC1_Attachments")
                                                      .Include("RDR1_SpecialOrderDetail.OITM_Articles")
                                                      .Include("Cake")
                                                      .Include("Category").Include("ColorCovert").Include("ColorFlower")
                                                      .Include("ColorRibbon")
                                                      .Include("ColorTop")
                                                      .Include("Cover").Include("Especiality").Include("Flower")
                                                      .Include("Style")
                                                      .Include("filler").Include("ColorHeight");

                                                    


                //if (!string.IsNullOrEmpty(warehouse)) orders = orders.Where(t => t.Filler == warehouse);

                if (!string.IsNullOrEmpty(keywords)) orders = orders.Where(t => t.Comments.Contains(keywords));

                if (startDate.HasValue) orders = orders.Where(d => d.DocDate >= startDate.Value);

                if (endDate.HasValue) orders = orders.Where(t => t.DocDate <= endDate.Value);

                if (status.HasValue) orders = orders.Where(o => o.StateL == status.Value);

                var items = orders.ToList();
                return new ObservableCollection<ORDR_SpecialOrder>(items);
            }
        }

        public static ORDR_SpecialOrder GetOrder(int id)
        {
            lock (Extensions.Locker)
            {
                return ContextFactory.GetDBContext()
                    .ORDR_SpecialOrder.Include("RDR1_SpecialOrderDetail")
                    .Include("ATC1_Attachments").Include("RDR1_SpecialOrderDetail.OITM_Articles")
                    .FirstOrDefault(o => o.IdSpecialOrder == id);                                ;
            }
        }

        public static ObservableCollection<UCake> GetCakes()
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                return new ObservableCollection<UCake>(db.UCakes.ToList());
            }
        }

        public static ObservableCollection<UCategory> GetCategories()
        {
            lock (Extensions.Locker)
            {
                return  new ObservableCollection<UCategory>(ContextFactory.GetDBContext().UCategories.ToList());
            }
        }

        public static ObservableCollection<UColor_Covert> GetColorCoverts()
        {
            lock (Extensions.Locker)
            {
                return new ObservableCollection<UColor_Covert>(ContextFactory.GetDBContext().UColor_Covert.ToList());
            }
        }

        public static ObservableCollection<UColor_Flower> GetColorFlowers()
        {
            lock (Extensions.Locker)
            {
                return  new ObservableCollection<UColor_Flower>(ContextFactory.GetDBContext().UColor_Flower.ToList());
            }
        }

        public static ObservableCollection<UColor_Ribbon> GetColorRibbon()
        {
            lock (Extensions.Locker)
            {
                return new ObservableCollection<UColor_Ribbon>(ContextFactory.GetDBContext().UColor_Ribbon.ToList());
            }
        }

        public static ObservableCollection<UColor_top> GetColorTops()
        {
            lock (Extensions.Locker)
            {
                return new ObservableCollection<UColor_top>(ContextFactory.GetDBContext().UColor_top.ToList ());
            }
        }

        public static ObservableCollection<UCover> GetCovers()
        {
            lock (Extensions.Locker)
            {
                return new ObservableCollection<UCover>(ContextFactory.GetDBContext().UCovers.ToList());
            }
        }

        public static ObservableCollection<ULaza> GetLazas()
        {
            lock (Extensions.Locker)
            {
                return new ObservableCollection<ULaza>(ContextFactory.GetDBContext().ULaza.ToList());
            }
        }

        public static ObservableCollection<UGuirnalda> GetGuirnaldas()
        {
            lock (Extensions.Locker)
            {
                return new ObservableCollection<UGuirnalda>(ContextFactory.GetDBContext().UGuirnalda.ToList());
            }
        }

        public static ObservableCollection<UEspecialty> GetSpecialities()
        {
            lock (Extensions.Locker)
            {
                return new ObservableCollection<UEspecialty>(ContextFactory.GetDBContext().UEspecialties.ToList());
            }
        }

        public static ObservableCollection<UFlower> GetFlowers()
        {
            lock (Extensions.Locker)
            {
                return new ObservableCollection<UFlower>(ContextFactory.GetDBContext().UFlowers.ToList());
            }
        }

        public static ObservableCollection<UStyle> GetStyles()
        {
            lock (Extensions.Locker)
            {
                return new ObservableCollection<UStyle>(ContextFactory.GetDBContext().UStyles.ToList());
            }
        }

        public static ObservableCollection<Ufiller> GetFillers()
        {
            lock (Extensions.Locker)
            {
                return new ObservableCollection<Ufiller>(ContextFactory.GetDBContext().Ufillers.ToList());

            }
        }

        public static ObservableCollection<UColor_Height> GetColorHeights()
        {
            lock (Extensions.Locker)
            {
                return new ObservableCollection<UColor_Height>(ContextFactory.GetDBContext().UColor_Height.ToList());
            }
        }

        public static void AddOrder(ORDR_SpecialOrder selectedOrder)
        {
            lock (Extensions.Locker)
            {

                if (selectedOrder.IdSpecialOrder == 0)
                {
                    ContextFactory.GetDBContext().ORDR_SpecialOrder.Add(selectedOrder);
                }
                else selectedOrder.ChangeEntityState(EntityState.Modified);
            }
        }

        public static void DeleteOrder(ORDR_SpecialOrder selectedOrder)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                if (db.Entry(selectedOrder).State != EntityState.Added)
                {
                    db.ORDR_SpecialOrder.Remove(selectedOrder);
                }
            }
        }

        public static void DeleteDetail(RDR1_SpecialOrderDetail selectedSpecialOrderDetails)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                db.RDR1_SpecialOrderDetail.Remove(selectedSpecialOrderDetails);
            }
        }
    }
}
