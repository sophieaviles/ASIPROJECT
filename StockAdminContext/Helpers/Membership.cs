using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using StockAdminContext.Models;

namespace StockAdminContext.Helpers
{
    public class Membership
    {
        static Membership()
        {
#if DEBUG
            Config.SetCurrentUserName("Admin");
#endif
        }

        

        public static bool AddUser(string username, string password)
        { 
            return false;
        }

        public static bool Login(string username, string password)
        {
            var isloggedIn = ValidateUser(username, password);

            if(isloggedIn) Config.SetCurrentUserName(username);

            return isloggedIn;
        }

        public static bool ValidateUser(string username, string password)
        {
            using (var db = new ApplicationContext())
            {
                return db
               .LocalUsers.Any(u => u.NickName == username && u.Password == password);
            }
        }

        public static  ObservableCollection<LocalUser> GetCashiers()
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                return new ObservableCollection<LocalUser>(db.LocalUsers.ToList());
            }
        }

        public static ObservableCollection<LocalUser> GetPricesChangers()
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                return new ObservableCollection<LocalUser>(db.LocalUsers.Where(lu => lu.AllowChangePrices).ToList());
            }
        }

        public static  ObservableCollection<LocalUser> GetShopManagers()
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                return new ObservableCollection<LocalUser>(db.LocalUsers.ToList());
            }
        }


        public static ObservableCollection<LocalUser> GetPriceValidationUsers()
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                return new ObservableCollection<LocalUser>(db.LocalUsers.Where( u=> u.AllowChangePrices));
            }
        }

        #region Local Config Settings

        public static List<LocalUser> CreateDefaultUsers()
        {
            var users = new List<LocalUser>();
            var admin = new LocalUser()
                        {
                            AllowChangePrices = true,
                            AllowProcessing = true,
                            ModifiedDateL = DateTime.Now,
                           CreatedByL =  "Desarrollador de Software",
                           CreatedDateL = DateTime.Now,
                           NickName = "Admin",
                           Password = "1234567",
                           Name = "Administrador de Sistema",

                        };

            users.Add(admin);

            return users;
        } 

        #endregion

        public static ObservableCollection<LocalUser> GetUsers()
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                return new ObservableCollection<LocalUser>(db.LocalUsers.Include(u=>u.UserDocuments));
            }
        }

        public static List<Document> GetDocumentsForUser()
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();

                return db.Documents.ToList();
            }
        }

        public static void AddOrUpdate(LocalUser user)
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();



                if (user.Id == 0)
                {
                    var toRemove = user.UserDocuments.Where(d => !d.IsAvailable).ToList();
                    toRemove.ForEach(d=> user.UserDocuments.Remove(d));
                    db.LocalUsers.Add(user);
                }
                else
                {
                    user.ChangeEntityState(EntityState.Modified);
                    var toRemove = user.UserDocuments.Where(d => !d.IsAvailable).ToList();
                    toRemove.ForEach(d=> d.ChangeEntityState(EntityState.Deleted));
                }
            }

        }

        public static void DeleteUser(LocalUser selectedUser)
        {
            lock (Extensions.Locker)
            {
                if(selectedUser.Id>0) selectedUser.ChangeEntityState(EntityState.Deleted);
            }
        }
    } 

   
    

}
