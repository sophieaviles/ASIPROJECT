using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StockAdminContext.Models;

namespace StockAdminContext.Helpers
{
    public static class VersionHelper
    {

        public static void UpgradeVersion<Tmodel>(Synchro<Tmodel> item )
        {
            
            var docId = item.GetDocumentId();

            
        }

        private static string GetDocumentId<Tmodel>(this Synchro<Tmodel> model)
        {
            var db = ContextFactory.GetDBContext();

            var document = db.Documents.FirstOrDefault(d => d.DocumentType == model.Model.GetType());

            return  document != null ? document.ObjType : string.Empty;
        }
    }
}
