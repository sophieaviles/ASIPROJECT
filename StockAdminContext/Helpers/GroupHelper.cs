using StockAdminContext.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace StockAdminContext.Helpers
{
    public class GroupHelper
    {
        public static ObservableCollection<OITB_Groups> GetGroups() 
        {
            lock (Extensions.Locker)
            {
            var db = ContextFactory.GetDBContext();

            //var groups = (from g in db.OITB_Groups
            //             join a in db.OITM_Articles
            //             on g.ItmsGrpCod equals a.ItmsGrpCod
            //             join b in db.OITW_BranchArticles
            //             on a.ItemCode equals b.ItemCode
            //             where g.Locked == "N" && g.StateL == "1" && b.WhsCode == Config.WhsCode                                        
            //             select g).Distinct().AsNoTracking().ToList();
                var groups = db.OITB_Groups
                    .Where(g => g.Locked == "N" && g.StateL == "1").Distinct().AsNoTracking();
             
            return new ObservableCollection<OITB_Groups>(groups);
            }
        }
        public static string GetGroupName(short? groupCode)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                return (from g in db.OITB_Groups
                              join a in db.OITM_Articles
                              on g.ItmsGrpCod equals a.ItmsGrpCod
                              join b in db.OITW_BranchArticles
                              on a.ItemCode equals b.ItemCode
                              where g.Locked == "N" && g.StateL == "1" && b.WhsCode == Config.WhsCode
                              where g.ItmsGrpCod== groupCode
                              select g.ItmsGrpNam).Distinct().AsNoTracking().FirstOrDefault()
                              ;
 
            }
        }
    }
}
