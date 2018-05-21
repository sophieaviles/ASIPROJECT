using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using StockAdminContext.Models;

namespace StockAdminContext.Helpers
{
    public static class ArticlesHelper
    {
        public static OITM_Articles GetArticle(string itemCode)
        {
             lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                var product =  db.OITM_Articles.FirstOrDefault(a => a.ItemCode.ToLower() == itemCode.ToLower());

                 //product.ChangeEntityState(EntityState.Unchanged);

                return product;
            }
        }

        public static ObservableCollection<OITM_Articles> GetArticlesCollection()
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                var articles = db.OITM_Articles.ToList();

                //articles.ForEach(a=> a.ChangeEntityState(EntityState.Unchanged));

                return new ObservableCollection<OITM_Articles>(articles);
            }
        }

        public static ObservableCollection<OITM_Articles> GetInventarioArticles(string movementId, short groupId)
        {            
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                var articles = (from a in db.OITM_Articles
                                join m in db.UMovements_Acc
                                on a.ItemCode equals m.U_ITEMCODE
                                join b in db.OITW_BranchArticles
                                on a.ItemCode equals b.ItemCode
                                where a.InvntItem.Contains("Y") &&
                                      m.U_IDMOVIMIENTO == movementId &&
                                      a.ItmsGrpCod == groupId &&
                                      b.WhsCode == Config.WhsCode
                                select new
                                {
                                    a.ItemCode,
                                    a.ItemName,
                                    a.InvntryUom,
                                    m.U_CUENTA,
                                    b
                                }).AsNoTracking().ToList();

                var articlesCollection = articles.Select(a => new OITM_Articles()
                {
                    ItemCode = a.ItemCode,
                    ItemName = a.ItemName,
                    InvntryUom = a.InvntryUom,
                    AccCount = a.U_CUENTA, 
                    OITW_BranchArticles = new List<OITW_BranchArticles>() {a.b}
                }).ToList();

               //articlesCollection.ForEach(a=> a.ChangeEntityState(EntityState.Unchanged));

                return new ObservableCollection<OITM_Articles>(articlesCollection);
            }
        }

        public static ObservableCollection<OITM_Articles> GetSalesArticles()
        {

            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                //var articles = db.OITM_Articles.Where(a => a.SellItem.Contains("Y") && a.OITW_BranchArticles.Any() && a.OITW_BranchArticles.FirstOrDefault().WhsCode == Config.WhsCode);
                
                var articles = (from t0 in db.OITM_Articles
                                join t1 in db.OITW_BranchArticles
                                on t0.ItemCode equals t1.ItemCode
                                where t0.SellItem.Contains("Y")
                                && t0.validFor.Contains("Y")
                                && t1.Locked.Contains("N")
                                && t1.WhsCode == Config.WhsCode
                                select new
                                {
                                    t0.ItemCode,
                                    t0.ItemName,
                                    t0.InvntryUom,
                                    t1
                                }).AsNoTracking().ToList();

                  var articlesCollection = articles.Select(a => new OITM_Articles()
                {
                    ItemCode = a.ItemCode,
                    ItemName = a.ItemName,
                    InvntryUom = a.InvntryUom,
                    //AccCount = a.U_CUENTA, 
                    OITW_BranchArticles = new List<OITW_BranchArticles>() {a.t1}
                }).ToList();

               //articlesCollection.ForEach(a=> a.ChangeEntityState(EntityState.Unchanged));

                return new ObservableCollection<OITM_Articles>(articlesCollection);
            }
        }
        
        public static ObservableCollection<OITM_Articles> GetSalesArticles(List<string> itemCodes )
        {

            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                //var articles = db.OITM_Articles.Where(a => a.SellItem.Contains("Y") && a.OITW_BranchArticles.Any() && a.OITW_BranchArticles.FirstOrDefault().WhsCode == Config.WhsCode);

                var articles = (from t0 in db.OITM_Articles.Where(a=> itemCodes.Contains(a.ItemCode))
                                join t1 in db.OITW_BranchArticles
                                on t0.ItemCode equals t1.ItemCode
                                where t0.SellItem.Contains("Y")
                                && t0.validFor.Contains("Y")
                                && t1.Locked.Contains("N")
                                && t1.WhsCode == Config.WhsCode
                                select new
                                {
                                    t0.ItemCode,
                                    t0.ItemName,
                                    t0.InvntryUom,
                                    t1
                                }).AsNoTracking().ToList();

                var articlesCollection = articles.Select(a => new OITM_Articles()
                {
                    ItemCode = a.ItemCode,
                    ItemName = a.ItemName,
                    InvntryUom = a.InvntryUom,
                    //AccCount = a.U_CUENTA, 
                    OITW_BranchArticles = new List<OITW_BranchArticles>() { a.t1 }
                }).ToList();

                //articlesCollection.ForEach(a=> a.ChangeEntityState(EntityState.Unchanged));

                return new ObservableCollection<OITM_Articles>(articlesCollection);
            }
        }

        public static ObservableCollection<OITM_Articles> GetPurchaseArticles()
        {
            
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                var cuentas = (from  m in db.UMovements_Acc where m.U_IDMOVIMIENTO.Equals("0")
                               select new
                               {
                                   m.U_ITEMCODE,
                                   m.U_CUENTA
                               }
                                   );

                var articles = (from t0 in db.OITM_Articles                                
                                join t1 in db.OITW_BranchArticles
                                on t0.ItemCode equals t1.ItemCode
                                join m in cuentas
                                on t0.ItemCode equals m.U_ITEMCODE into lcategoria
                                from acc in lcategoria.DefaultIfEmpty()
                                where t0.PrchseItem.Contains("Y")
                                && t0.validFor.Contains("Y")
                                && t1.Locked.Contains("N")
                                && t1.WhsCode == Config.WhsCode
                                select new
                                {
                                    t0.ItemCode,
                                    t0.ItemName,
                                    t0.InvntryUom,
                                    acc.U_CUENTA
                                }).AsNoTracking().ToList();

                var articlesCollection = articles.Select(a => new OITM_Articles()
                {
                    ItemCode = a.ItemCode,
                    ItemName = a.ItemName,
                    InvntryUom = a.InvntryUom,
                    AccCount = a.U_CUENTA ?? string.Empty,
                }).ToList();

                return new ObservableCollection<OITM_Articles>(articlesCollection);
            }
        }

        public static ObservableCollection<ALOHA_Articles> GetArticlesAlohaCollection()
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                var articles = db.ALOHA_Articles.ToList();

                //articles.ForEach(a=> a.ChangeEntityState(EntityState.Unchanged));

                return new ObservableCollection<ALOHA_Articles>(articles);
            }
        }

        public static double? GetAlohaPrice(string productcode)
        {
            if (string.IsNullOrEmpty(productcode)) return (double?) null;

            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();
                
               // var aloha = db.ALOHA_Articles.FirstOrDefault(a => a.IdALOHA_Articles ==  id);
                var aloha = (from article in db.ALOHA_Articles
                    join oitm in db.OITM_ALOHA on article.IdALOHA_Articles equals oitm.IdALOHA_Articles
                    where oitm.ItemCode == productcode
                    select article).OrderByDescending(product=> product.Price)
                    .FirstOrDefault();

                double price;

                if (aloha!=null && double.TryParse(aloha.Price, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out price))
                {
                    return price;
                }

                return (double) 0.0;
            }
        }

        public static ObservableCollection<OITM_Articles> GetSpecialOrdersProducts(ORDR_SpecialOrder specialOrder)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                IQueryable<OITM_Articles> products = db.OITM_Articles;

                if (specialOrder == null || (specialOrder.Style==null &&specialOrder.Cake== null && specialOrder.Especiality==null))
                    return  new ObservableCollection<OITM_Articles>();

                if (specialOrder.Style != null) products = products.Where(p => p.U_estilo == specialOrder.Style.Code);

                if (specialOrder.Cake != null) products = products.Where(p => p.U_torta == specialOrder.Cake.Code);

                if (specialOrder.Especiality != null)
                    products = products.Where(p => p.U_Especialidad == specialOrder.Especiality.Code);

                var prods = products.ToList();

                //prods.ForEach(p=> p.ChangeEntityState(EntityState.Unchanged));

                return new ObservableCollection<OITM_Articles>(prods);
            }
        }


        public static List<string> GetProductsOnInventory(List<string> ids)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                // InvtItem  productos inventariables. 
               return  db.OITM_Articles.Where(i => ids.Contains(i.ItemCode) && i.InvntItem.Contains("N") )
                    .Select(i => i.ItemCode).ToList();
            }
        }


        public static List<OITW_BranchArticles> GetArticlesFor(List<string> select)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                var products = db.OITW_BranchArticles
                    .Where(i => select.Contains(i.ItemCode) && i.WhsCode == Config.WhsCode)
                    .Select(p => new
                                 {
                                     ItemCode = p.ItemCode,
                                     OnHand1 = p.OnHand1,
                                     WhsCode = p.WhsCode,

                                 }).ToList();


                return products.Select(p => new OITW_BranchArticles()
                                            {
                                                ItemCode = p.ItemCode,
                                                WhsCode = p.WhsCode,
                                                OnHand1 = p.OnHand1,
                                            }).ToList();
            }
        }

        public static decimal GetOnHandFor(string itemcode)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                var products = db.OITW_BranchArticles
                    .Where(i => i.ItemCode  == itemcode && i.WhsCode == Config.WhsCode)
                    .Select(p => new
                    {
                        ItemCode = p.ItemCode,
                        OnHand1 = p.OnHand1,
                        WhsCode = p.WhsCode,

                    }).ToList();


                var article=  products.Select(p => new OITW_BranchArticles()
                {
                    ItemCode = p.ItemCode,
                    WhsCode = p.WhsCode,
                    OnHand1 = p.OnHand1,
                }).FirstOrDefault();

                 

                return article.OnHand1.HasValue ? article.OnHand1.Value : (decimal)0;
            }
        }
    }
}
