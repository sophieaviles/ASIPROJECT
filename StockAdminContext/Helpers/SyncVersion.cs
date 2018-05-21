using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using StockAdminContext.Models;

namespace StockAdminContext.Helpers
{
    public static class SyncVersion
    {
        public static Tuple<int,int> GetLatestVersionByType()
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();


                int dVersion = 0, mVersion = 0;
                if (db.LOG_CHANGES.Any())
                {
                    dVersion = db.LOG_CHANGES.Where(d => d.TypeSIGI == "D")
                        .OrderByDescending(l => l.version)
                        .Select(l => l.version)
                        .FirstOrDefault();
                    mVersion = db.LOG_CHANGES.Where(d => d.TypeSIGI == "M")
                        .OrderByDescending(l => l.version)
                        .Select(l => l.version).FirstOrDefault();

                }

                return Tuple.Create(dVersion, mVersion);
            }

        }

        public static Document GetDocumentTypeByCod(LOG_CHANGES log)
        {

            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                return db.Documents.FirstOrDefault(d => d.ObjType == log.ObjType);
            }

        }
    }
}
