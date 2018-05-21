using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using StockAdminContext.Helpers;

namespace StockAdminContext.Models
{
    public static class ContextFactory
    {
        private static ApplicationContext Context
        {
            get; set;
        }

        public static ApplicationContext GetDBContext()
        {
            return Context ?? (Context = new ApplicationContext());
        }

        public static void UpdateFieldsFrom<Tmodel>(this Tmodel currentModel, Tmodel updateFrom ) where Tmodel : BaseModel
        {

            var entity = Context.Entry(currentModel);

            //updateFrom.Id = currentModel.Id;
            entity.OriginalValues.SetValues(currentModel);
            entity.CurrentValues.SetValues(updateFrom);

        }

        public static void SetUnchanged<Tmodel>(this Tmodel model) where Tmodel : class
        {

            Context.Entry(model).State = EntityState.Detached;
        }
        public static void UpdateEntityVersion<Tmodel>(this Synchro<Tmodel> synchroModel) where Tmodel : class
        {
            lock (locker)
            {
                if (synchroModel == null) return;

               if(!Context.LOG_CHANGES.Any(l=> l.version== synchroModel.version.version))
                {
                    Context.LOG_CHANGES.Add(synchroModel.version);
                } 
                //SetModified(synchroModel.Model);

                Context.SaveChanges();

              //  var log = WebApiClient.ConfirmChangeset(synchroModel.version).Result;
            }
        }

        public static void AddChangeset(this LOG_CHANGES changeset)
        {                        
            var db = ContextFactory.GetDBContext();                       
            if (!db.LOG_CHANGES.Any(l => l.version == changeset.version))
            {
                Context.LOG_CHANGES.Add(changeset);
               // var log = WebApiClient.ConfirmChangeset(changeset).Result;
            }
            else
            {
                //var log =WebApiClient.ConfirmChangeset(changeset).Result;
            }
        }

        public static void UpdateModelPropertiesFrom<Tmodel>(this Tmodel model, Tmodel FromModel,bool excludeComplexTypes = false)
            where Tmodel : class 
        {

            var properties = model.GetType().GetProperties().ToList();

            if (excludeComplexTypes)
            {
                // Exlcude if is Custom class for naviation Properties 
               properties= (from property in model.GetType().GetProperties()
                              let name = property.Name
                              let type = property.PropertyType
                              let value = property.GetValue(model,
                                          (BindingFlags.GetProperty | BindingFlags.GetField | BindingFlags.Public),
                                          null, null, null)
                              where (type.IsPrimitive || type.IsEnum || type.IsValueType || type.IsSerializable)
                                select property).ToList();
            }

            foreach (var property in properties)
            {

                PropertyInfo current = model.GetType().GetProperty(property.Name);

                PropertyInfo source = FromModel.GetType().GetProperty(property.Name);

                var updateFromValue = source.GetValue(FromModel, null);

                var originalValue = current.GetValue(model, null);

                var value = updateFromValue ?? originalValue;

                
                //Refresh Complex Types IEnumerable items.

                 if(!current.CanWrite)continue;

                 if (updateFromValue != null && updateFromValue is IEnumerable<object>
                                           && originalValue is IEnumerable<object>)
                 {
                     if ((originalValue as IEnumerable<object>).Count() ==
                         (updateFromValue as IEnumerable<object>).Count())
                     {
                         var items = CombineWith(originalValue as IEnumerable<object>,
                             updateFromValue as IEnumerable<object>);

                        // items.ForEach(i => i.Item1.UpdateModelPropertiesFrom(i.Item2, excludeComplexTypes: true));

                         current.SetValue(model, originalValue, null);

                         continue;
                     }
                     else continue;
                 }


                if (updateFromValue!=null &&  updateFromValue.GetType() == typeof (int))
                {
                    value = updateFromValue != null && (int) updateFromValue != 0 ? updateFromValue : originalValue;
                }

                if (updateFromValue != null && updateFromValue.GetType() == typeof(decimal?))
                {
                    value = updateFromValue != null && (decimal?)updateFromValue > 0 ? updateFromValue : originalValue;
                }

                if (updateFromValue != null &&
                    updateFromValue.GetType() == typeof (string))
                {
                    value = !string.IsNullOrEmpty((string) updateFromValue) ? updateFromValue : originalValue;
                }

                if (updateFromValue!=null && ( updateFromValue.GetType() == typeof (DateTime)
                    || updateFromValue.GetType() == typeof (DateTime?)))
                {
                    if (updateFromValue != null && !updateFromValue.Equals(new DateTime()))
                        current.SetValue(model, value, null);
                    else current.SetValue(model, originalValue, null);
                }
                else
                {
                    current.SetValue(model, value, null);
                }

                if (current.GetType() == typeof (DateTime) && current.GetValue(model, null) == null)
                {
                    current.SetValue(model,DateTime.Now,null);
                }
            }
        }


        private static IEnumerable<Tuple<T, U>> CombineWith<T, U>(this IEnumerable<T> first, IEnumerable<U> second)
        {
            using (var firstEnumerator = first.GetEnumerator())
            using (var secondEnumerator = second.GetEnumerator())
            {
                bool hasFirst = true;
                bool hasSecond = true;

                while (
                    // Only call MoveNext if it didn't fail last time.
                    (hasFirst && (hasFirst = firstEnumerator.MoveNext()))
                    | // WARNING: Do NOT change to ||.
                    (hasSecond && (hasSecond = secondEnumerator.MoveNext()))
                    )
                {
                    yield return Tuple.Create(
                            hasFirst ? firstEnumerator.Current : default(T),
                            hasSecond ? secondEnumerator.Current : default(U)
                        );
                }
            }
        }

        public static  void RollBack()
        {
             
                var changedEntries =
                    Context.ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged).ToList();

                foreach (var entry in changedEntries.Where(x => x.State == EntityState.Modified))
                {
                    entry.CurrentValues.SetValues(entry.OriginalValues);
                    entry.State = EntityState.Unchanged;
                }

                foreach (var entry in changedEntries.Where(x => x.State == EntityState.Added))
                {
                    entry.State = EntityState.Detached;
                }

                foreach (var entry in changedEntries.Where(x => x.State == EntityState.Deleted))
                {
                    entry.State = EntityState.Unchanged;
                }
            
        }

        public static bool HasPendingChanges()
        {
            lock (Extensions.Locker)
            {
                return Context.ChangeTracker.Entries().Any(x => x.State != EntityState.Unchanged);
            }
        }

        public static void ChangeEntityState<Tmodel>(this Tmodel model, EntityState state) where Tmodel : BaseModel
        {
            var currentState = Context.Entry(model).State;

            if (currentState != EntityState.Added  ) Context.Entry(model).State = state;
        }


        public static void Reload<Tmodel>(this Tmodel model) where Tmodel : BaseModel
        {
            var entity  = Context.Entry(model);

            entity.Reload();
        }

        public static void SetUnchangedEntity<Tmodel>(this Tmodel model) where Tmodel:BaseModel
        {
            model.ChangeEntityState(EntityState.Unchanged);
        }
        //public static void RemoveAndSave<Tmodel>(this Tmodel models) where Tmodel : class
        //{
        //    foreach (var model in (IEnumerable<Tmodel>)models)
        //    {
        //        Context.Entry(model).State = (EntityState.Deleted);
        //        Context.SaveChanges();
        //    }

        //} 

        public static void RemoveAndSave<Tmodel>(this Tmodel model) where Tmodel : BaseModel
        {
            model.ChangeEntityState(EntityState.Deleted);
            Context.SaveChanges();
        }


        public static void SaveChanges()
        {
            lock (Extensions.Locker)
            {
                try
                {
                    ContextFactory.GetDBContext().SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    LoggerHelper.WriteLog(ex);
                }
                catch (Exception ex)
                {
                    LoggerHelper.WriteLog(ex);
                    ContextFactory.RollBack();
                }
            }
        }


        

        private  static object locker = new object();

         
    }
}
