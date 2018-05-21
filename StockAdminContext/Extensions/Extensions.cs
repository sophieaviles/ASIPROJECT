using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
//using System.Web.WebPages.Html;
using StockAdminContext.Models;

namespace StockAdminContext
{

    public static class Extensions
    {
        public static int ExecuteSqlCommandSmart(this Database self, string storedProcedure,
            object parameters = null)
        {
            if (self == null)
                throw new ArgumentNullException("self");
            if (string.IsNullOrEmpty(storedProcedure))
                throw new ArgumentException("storedProcedure");

            var arguments = PrepareArguments(storedProcedure, parameters);
            return self.ExecuteSqlCommand(arguments.Item1, arguments.Item2);
        }

        public static IEnumerable<TElement> SqlQuerySmart<TElement>(this Database self, string storedProcedure,
            object parameters = null)
        {
            if (self == null)
                throw new ArgumentNullException("self");
            if (string.IsNullOrEmpty(storedProcedure))
                throw new ArgumentException("storedProcedure");

            var arguments = PrepareArguments(storedProcedure, parameters);
            return self.SqlQuery<TElement>(arguments.Item1, arguments.Item2);
        }

        public static IEnumerable SqlQuerySmart(this Database self, Type elementType, string storedProcedure,
            object parameters = null)
        {
            if (self == null)
                throw new ArgumentNullException("self");
            if (elementType == null)
                throw new ArgumentNullException("elementType");
            if (string.IsNullOrEmpty(storedProcedure))
                throw new ArgumentException("storedProcedure");

            var arguments = PrepareArguments(storedProcedure, parameters);
            return self.SqlQuery(elementType, arguments.Item1, arguments.Item2);
        }

        private static Tuple<string, object[]> PrepareArguments(string storedProcedure, object parameters)
        {
            var parameterNames = new List<string>();
            var parameterParameters = new List<object>();

            if (parameters != null)
            {
                foreach (PropertyInfo propertyInfo in parameters.GetType().GetProperties())
                {
                    string name = "@" + propertyInfo.Name;
                    object value = propertyInfo.GetValue(parameters, null);

                    parameterNames.Add(name);
                    parameterParameters.Add(new SqlParameter(name, value ?? DBNull.Value));
                }
            }

            if (parameterNames.Count > 0)
                storedProcedure += " " + string.Join(", ", parameterNames);

            return new Tuple<string, object[]>(storedProcedure, parameterParameters.ToArray());
        }


        public static object Locker = new object();

        // Para Ejecutar una sincronizacion a la vez.
        public static object SyncLock = new object();

        //#region filters

        //public static IQueryable<T> Filter<TContext, T>(this IQueryable<T> query, FilterContainer<TContext, T> filterContainer)
        //    where TContext : System.Data.Entity.DbContext
        //{
        //    return filterContainer.ApplyTo(query);
        //}

       
        //#endregion

        //public static void UpdateModelPropertiesFrom<Tmodel>(this Tmodel model, Tmodel FromModel)
        //    where Tmodel : BaseModel
        //{

        //    foreach (var property in model.GetType().GetProperties())
        //    {
        //        PropertyInfo current = model.GetType().GetProperty(property.Name);

        //        PropertyInfo source = FromModel.GetType().GetProperty(property.Name);

        //        var updateFromValue = source.GetValue(FromModel, null);

        //        var originalValue = current.GetValue(model, null);

        //        var value = updateFromValue ?? originalValue;

        //        //  TODO for dates Values.

        //        if (updateFromValue.GetType() == typeof (DateTime)
        //            || updateFromValue.GetType() == typeof (DateTime?))
        //        {
        //            if (updateFromValue != null && !updateFromValue.Equals(new DateTime()))
        //                current.SetValue(model, value, null);
        //            else current.SetValue(model,originalValue,null);
        //        }
        //        else
        //        {
        //            current.SetValue(model, value, null);
        //        }
        //    }
        //}
    }

}
