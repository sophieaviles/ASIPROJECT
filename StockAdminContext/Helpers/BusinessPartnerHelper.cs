using System.Collections.ObjectModel;
using StockAdminContext.Models;
using System.Collections.Generic;
using System.Linq;

namespace StockAdminContext.Helpers
{
    public static class BusinessPartnerHelper
    {
        public static OCRD_BusinessPartner GetBusinessPartner(string partner)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();
                return db.OCRD_BusinessPartner.FirstOrDefault(b => b.CardCode == partner);
            }

        }

        public static  ObservableCollection<OCRD_BusinessPartner> GetBusinessPartners()
        {
            lock (Extensions.Locker)
            {
                var dbConxtex = ContextFactory.GetDBContext();
                return new ObservableCollection<OCRD_BusinessPartner>(dbConxtex.OCRD_BusinessPartner);
            }
        }

        public static ObservableCollection<OCRD_BusinessPartner> GetCustomers(string searchKeyword)
        {
            lock (Extensions.Locker)
            {
                
                if (_customers == null)
                {
                    var db = ContextFactory.GetDBContext();
                    _customers=  db.OCRD_BusinessPartner.AsNoTracking().Where(c=> c.CardType=="C" && (c.GroupCode == 100 || c.CardCode == Config.BusinessPartner )).ToList();
                }
                
                if (string.IsNullOrEmpty(searchKeyword)) return new ObservableCollection<OCRD_BusinessPartner>();
                var list = _customers.Where(c => c.CardName.ToLower().Contains(searchKeyword.ToLower()) || c.CardCode.ToLower().Contains(searchKeyword.ToLower())).ToList();
                return  new ObservableCollection<OCRD_BusinessPartner>(list);
            }
        }

        public static ObservableCollection<OCRD_BusinessPartner> GetSupplier(string searchKeyword)
        {
            lock (Extensions.Locker)
            {

                if (_suppliercustomer == null)
                {
                    var db = ContextFactory.GetDBContext();
                    _suppliercustomer = db.OCRD_BusinessPartner.AsNoTracking().Where(c => c.CardType == "S").ToList();
                }

                if (string.IsNullOrEmpty(searchKeyword)) return new ObservableCollection<OCRD_BusinessPartner>();
                var list = _suppliercustomer.Where(c => c.CardName.ToLower().Contains(searchKeyword.ToLower()) || c.CardCode.ToLower().Contains(searchKeyword.ToLower())).ToList();
                return new ObservableCollection<OCRD_BusinessPartner>(list);
            }
        }

        private static List<OCRD_BusinessPartner> _customers;
        private static List<OCRD_BusinessPartner> _suppliercustomer;
    }
}
