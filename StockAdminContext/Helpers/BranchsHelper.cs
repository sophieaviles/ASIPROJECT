using System.ServiceModel.Configuration;
using System.Threading;
using StockAdminContext.Enums;
using StockAdminContext.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace StockAdminContext.Helpers
{
    public static class BranchsHelper
    {

        public static OWHS_Branch GetBranch(string branchCode, bool excludeNavigationProperties= false,bool forceRefresh = false)
        {
            var code = branchCode.Trim().ToLower();

           

            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                if (excludeNavigationProperties)
                {
                    return db.OWHS_Branch.FirstOrDefault(b => b.WhsCode.ToLower().Trim() == code);
                }

                if (forceRefresh) branch = null;
                return  branch ??
                        (branch=
                        db.OWHS_Branch.Include("OITW_BranchArticles").Include("OITW_BranchArticles.OITM_Articles.OITB_Groups")
                        .FirstOrDefault(b => b.WhsCode.ToLower().Trim() == code));

            }
        
        }


        public static void FixWhscodeCaseSensitive()
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                var codes = db.OITW_BranchArticles.Select(b => b.WhsCode).ToList();

                var lowerCaseList = codes.Where(c => c.Any(Char.IsLower)).ToList();
        
                var products = db.OITW_BranchArticles.Where(b => lowerCaseList.Contains(b.WhsCode)).ToList();

                products.ForEach(p =>
                                 {
                                     var code = p.WhsCode.ToUpper();
                                     p.WhsCode = code;
                                 });

                db.SaveChanges();
            }
        }

        /// <summary>
        ////Funciones de Grupos
        /// </summary>
        public static ObservableCollection<OITB_Groups> GetGroups()
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();


                 return
                    new ObservableCollection<OITB_Groups>(
                        db.OITB_Groups.Where(b => b.StateL == "1" && b.Locked =="N"));
            }
        }

        public static ObservableCollection<OITB_Groups> GetMandatoryGroups()
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();


                return
                   new ObservableCollection<OITB_Groups>(
                       db.OITB_Groups.Where(b => b.StateL == "1" && b.Locked == "N" && b.MandatoryCount =="1"));
            }
        }

        public static List<OITB_Groups> GroupList
        {
            get { return _groupList ?? (_groupList = new List<OITB_Groups>()); }
            set { _groupList = value; }
        }

        public static void SaveGroupList()
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();
                foreach (var art in GroupList)
                {
                    var itm = db.OITB_Groups.FirstOrDefault(a => a.ItmsGrpCod == art.ItmsGrpCod);
                    if (itm != null) itm.MandatoryCount = art.MandatoryCount;
                    db.SaveChanges();
                }
            }
        }

        private static List<OITB_Groups> _groupList;




        public static OWHS_Branch GetFiller(string branchCode)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();
                return db.OWHS_Branch.FirstOrDefault(b => b.WhsCode.ToLower() == branchCode.ToLower());

            }
        }


        public static ObservableCollection<OWHS_Branch> GetBranchs(string branch)
        {
            lock(Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                return
                    new ObservableCollection<OWHS_Branch>(
                        db.OWHS_Branch.Where(b => b.LockedForOrderL == false && b.WhsCode.ToLower() != branch.ToLower()));
            }
        }

        /// <summary>
        /// Lista estatica que guarda los productos en template que se han modificado
        /// </summary>
        public static List<OITM_Articles> TemplateList
        {
            get { return _templatedList ?? (_templatedList = new List<OITM_Articles>()); }
            set { _templatedList = value; }
        }

        public static void SaveTemplateArticles()
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();
                foreach (var art in TemplateList)
                {
                    var itm = db.OITM_Articles.FirstOrDefault(a => a.ItemCode == art.ItemCode);
                    if (itm != null) itm.TemplateL = art.TemplateL;
                    db.SaveChanges();
                }
            }
        }

        private static List<OITM_Articles> _templatedList;

        public static List<OITM_Articles> GetGenericBranchArticles(OWHS_Branch branch)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();
                return
                    branch.OITW_BranchArticles.Join(db.OITM_Articles,
                        ba => ba.ItemCode, a => a.ItemCode,
                        (ba, a) => new {branchArticle = ba, article = a})
                        .Select(obj => obj.article).ToList();                
            }
        }
    
        public static List<OWHS_Branch> GetBranchStores()
        {
            lock (Extensions.Locker)
            {
                if (branchStores != null && branchStores.Any()) return branchStores;

                var db = ContextFactory.GetDBContext();
                var locked = YesNo.N.ToString();
                branchStores =
                    db.OWHS_Branch.Include("OITW_BranchArticles")
                    .Where(b => b.WhsCode.ToLower() != Config.WhsCode &&
                               !b.LockedForOrderL && b.Locked.Contains(locked))
                        .ToList();

                return branchStores;
            }
        }



        private static List<OWHS_Branch> branchStores;      

        private static OWHS_Branch branch { get; set; }

        
    }
}
