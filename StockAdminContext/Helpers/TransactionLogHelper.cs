using System;
using System.Activities.Debugger;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StockAdminContext.Models;

namespace StockAdminContext.Helpers
{
    public static class TransactionLogHelper
    {

        public static short ConfirmationLog()
        {
            lock (Extensions.Locker)
            {
                var log = new TransactionLog
                          {
                              VerificationCode = (short) new Random().Next(1000,9999),
                              Description ="Registro de procesamiento de fin de Día",
                              Code = string.Empty,
                              CreatedBy = Config.CurrentUser,
                              Date = DateTime.Now,
                              State= string.Empty,
                          };
                var context = ContextFactory.GetDBContext();

                context.TransactionLogs.Add(log);
                ContextFactory.SaveChanges();

                return log.VerificationCode;
            }
        }
    }
}
