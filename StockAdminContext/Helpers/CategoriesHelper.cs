 

using System.Data.Common;
using System.Text.RegularExpressions;
using StockAdminContext.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;


namespace StockAdminContext.Helpers
{
    public static class CategoriesHelper
    {

        public static ObservableCollection<CategoryResume> GetCategoryResume(ObservableCollection<WTQ1_TransferRequestDetails> details, string branchcode)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                if (categories == null) categories = db.UCategories.ToList();
                var cat = categories.Join(details, c => c.Code, d => d.LineCode, (c, d) => new { c, d }).Select(j => j.c).Distinct();

                var recs = cat.Select(uc => new CategoryResume
                {
                    Category = uc.Name,
                    Stock = db.OITW_BranchArticles
                    .Where(ba => ba.OITM_Articles.U_categoria == uc.Code && ba.OWHS_Branch.WhsCode == branchcode).Sum(ar => ar.OnHand1),
                    Quantity = details.Where(d => d.LineCode == uc.Code).Sum(d => d.IQuantity)
                }).ToList();

                return new ObservableCollection<CategoryResume>(recs); 
               
            }
        }

        public static ObservableCollection<CategoryResume> GetCategoryResume(List<WTR1_TransferDetails> details, string branchcode)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();
                
                if (categories == null) categories = db.UCategories.ToList();
                var cat = categories.Join(details, c => c.Code, d => d.LineCode, (c, d) => new { c, d }).Select(j => j.c).Distinct();

                var recs = cat.Select(uc => new CategoryResume
                {
                    Category = uc.Name,
                    Stock = db.OITW_BranchArticles
                    .Where(ba => ba.OITM_Articles.U_categoria == uc.Code && ba.OWHS_Branch.WhsCode == branchcode).Sum(ar => ar.OnHand1),
                    Quantity = details.Where(d => d.LineCode == uc.Code).Sum(d => d.Quantity)
                }).ToList();

                return new ObservableCollection<CategoryResume>(recs);

            }
        }

        public static UCategory GetCategory(string idCategory)
        {
            
               return ContextFactory.GetDBContext().UCategories.FirstOrDefault(c => c.Code.Contains(idCategory));
           
        }

        private static List<UCategory> categories = null;
    }
}
