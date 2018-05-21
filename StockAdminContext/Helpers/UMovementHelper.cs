using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using StockAdminContext.Models;
using System.Data.Entity;

namespace StockAdminContext.Helpers
{
    public static class UMovementHelper
    {
        public static ObservableCollection<UMovement> GetMovementsCollection(string tipo)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();              
                return new ObservableCollection<UMovement>(db.UMovements.Where(m=>m.U_TIPO == tipo).AsNoTracking());
            }
        }
    }
}
