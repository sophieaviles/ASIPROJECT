using System;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using System.Linq;
using StockAdminContext.Models;
using System.Collections.ObjectModel;

namespace StockAdminContext.Helpers
{
    public static class CheckBookHelper
    {
        public static void Add(Checkbook checkBook)
        {
            var db = ContextFactory.GetDBContext();
            using (var scope = new TransactionScope())
            {
                db.Checkbooks.Add(checkBook);
                db.SaveChanges();

                scope.Complete();
            }

        }

        public static ObservableCollection<Checkbook> GetCollection()
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();
                return new ObservableCollection<Checkbook>(db.Checkbooks);
                //todo: verificar si filtra por whscode (ese campo no existe)
            }
        }

        public static void Update()
        {
            lock (Extensions.Locker)
            {

                var db = ContextFactory.GetDBContext();
                using (var scope = new TransactionScope())
                {
                    db.SaveChanges();
                    scope.Complete();
                }
            }
        }

        public static Checkbook GetActiveCheckBook()
        {
            lock (Extensions.Locker)
            {
                var db = ContextFactory.GetDBContext();
                return db.Checkbooks.FirstOrDefault(cb => cb.StateL == CheckBookStatus.Activo);
            }
        }

        /// <summary>
        /// Obtiene el numero de talonario correspondiente. Function = 'C' => Consultar
        /// </summary>
        /// <param name="series">la serie a asignar</param>
        /// <returns>Estructura con Número a asignar</returns>
        public static CheckBookNumberItem GetCheckBookNumber(int series)
        {


            lock (Extensions.Locker)
            {
                #region TEST DE CODIGOS FALLIDOS

                /*
                 * La forma recomendada no retorna ningun valor ¿por qué?
                 * --------------------------------------------
                const string query = "SP_GetCheckBookNumber @SERIES, @FUNCION, @NEXT";
                var serie = new SqlParameter("SERIES", series);
                var funcion = new SqlParameter("FUNCION", 'C');
                var next = new SqlParameter("NEXT", 1);
                //no retorna nimi
                currentNumber = db.Database.SqlQuery<CheckBookNumberItem>(query, serie, funcion, next).FirstOrDefault();
                */

                /*
                 * la forma 2 semichampera tampoco
                 * -------------------------------------------
                db.Database.Connection.Open();
                var command = db.Database.Connection.CreateCommand();
                command.CommandText = "SP_GetCheckBookNumber";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("SERIES", series));
                command.Parameters.Add(new SqlParameter("FUNCION", 'C'));
                command.Parameters.Add(new SqlParameter("NEXT", 2));
                using (var reader = command.ExecuteReader())
                {
                    tasks = StoredProcedureHelper.DataReaderToObjectList<CheckBookNumberItem>(reader);
                }*/

                #endregion TEST DE CODIGOS FALLIDOS

                /*
                 * la forma champera sí retorna valores misteriosamente
                 * ------------------------------------------
                 */
                //todo: tratar de erfactorizar este código a algo mas entendible
                using (var con = new SqlConnection(Config.CurrentConnection))
                {
                    var ds = new DataSet();

                    var cmd = new SqlCommand("SP_GetCheckBookNumber", con)
                    {
                        CommandType = CommandType.StoredProcedure,
                        Connection = con
                    };
                    cmd.Parameters.AddWithValue("@SERIES", series);
                    cmd.Parameters.AddWithValue("@FUNCION", 'C');
                    cmd.Parameters.AddWithValue("@NEXT", 0);

                    cmd.Connection.Open();
                    cmd.Prepare();
                    var adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    cmd.Connection.Close();

                    if (ds.Tables.Count <= 0)
                        return new CheckBookNumberItem()
                        {
                            ErrorCode = 3,
                            ErrorMessage = "No se ejecutado la operación",
                            Number = 0
                        };
                    return new CheckBookNumberItem
                    {
                        ErrorCode = (int) ds.Tables[0].Rows[0].ItemArray[0],
                        ErrorMessage =
                            ds.Tables[0].Rows[0].ItemArray[1].ToString(),
                        Number = (int) ds.Tables[0].Rows[0].ItemArray[2]
                    };
                }
            }
        }

        public static CheckBookNumberItem SetNextCheckBookNumber(short? series, string currentNumber,Action<string> onErrorAction)
        {
            try
            {
                var serie = Convert.ToInt32(series);
                var number = Convert.ToInt32(currentNumber);

                return SetNextCheckBookNumber(serie, number);

            }
            catch (Exception ex)
            {
               LoggerHelper.WriteLog(ex);
                if (onErrorAction != null) onErrorAction(ex.Message);
                return null;
            }
        }

    public static CheckBookNumberItem SetNextCheckBookNumber(int series, int currentNumber)
        {
            lock (Extensions.Locker)
            {
                /*
                 * la forma champera sí tetorna valores misteriosamente
                 * ------------------------------------------
                 */
                //todo: tratar de erfactorizar este código a algo mas entendible
                using (var con = new SqlConnection(Config.CurrentConnection))
                {
                    var ds = new DataSet();

                    var cmd = new SqlCommand("SP_GetCheckBookNumber", con)
                              {
                                  CommandType = CommandType.StoredProcedure,
                                  Connection = con
                              };
                    cmd.Parameters.AddWithValue("@SERIES", series);
                    cmd.Parameters.AddWithValue("@FUNCION", 'A');
                    cmd.Parameters.AddWithValue("@NEXT", currentNumber);

                    cmd.Connection.Open();
                    cmd.Prepare();
                    var adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    cmd.Connection.Close();

                    if (ds.Tables.Count <= 0)
                        return new CheckBookNumberItem()
                               {
                                   ErrorCode = 3,
                                   ErrorMessage = "No se ejecutado la operación",
                                   Number = 0
                               };
                    return new CheckBookNumberItem
                           {
                               ErrorCode = (int) ds.Tables[0].Rows[0].ItemArray[0],
                               ErrorMessage = ds.Tables[0].Rows[0].ItemArray[1].ToString(),
                               Number = (int) ds.Tables[0].Rows[0].ItemArray[2]
                           };
                }
            }
        }

        
    }
}
