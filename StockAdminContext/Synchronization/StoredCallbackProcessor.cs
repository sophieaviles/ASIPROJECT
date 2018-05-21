using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LinqKit;
using StockAdminContext.Models;
using System.Data.SqlClient;
using System.Data;

namespace StockAdminContext
{
   public  static   class StoredCallbackProcessor
    {

       static StoredCallbackProcessor()
       {
           
       }

       public static void Run()
       {
           
           // TODO conect to Param Table.
           // Initialize Async Process.
            StartAlohaSync();

           //Procedimiento para actualizar información de SIGI con SAP
            CallBack("SP_Synchronization");

       }

       public static void StartAlohaSync(bool ignoreAsync= false)
       {
           lock (Extensions.Locker)
           {
               var db = ContextFactory.GetDBContext();
               // Aloha Sync
               var alohaExe = db.Parameters.FirstOrDefault(p => p.ParameterKey == "7");

               if (alohaExe == null) return;

               var exePath = alohaExe != null ? alohaExe.Value1 : string.Empty;


               var alohaLoopDelay = db.Parameters.FirstOrDefault(p => p.ParameterKey == "1");

               if (alohaLoopDelay == null) return;

               var minutes = !string.IsNullOrEmpty(alohaLoopDelay.Value1)
                   ? Convert.ToInt32(alohaLoopDelay.Value1)
                   : 5; // 5 Minutes Default value 
               // Set CallBack

               if (!ignoreAsync) CallBackLoopAsync("SP_UpdateInvoice", TimeSpan.FromMinutes(minutes), true, exePath);
               else AlohaSync("SP_UpdateInvoice", TimeSpan.FromMinutes(minutes), false, exePath);
           } 
       }


       #region CallBacks

       #region Aloha Sync
       private static void CallBackLoopAsync(string storedProcedureName ,TimeSpan delay,bool infiniteLoop, string ExeName ="")
       {
           AsyncHelper.DoAsync(()=> AlohaSync(storedProcedureName,delay,infiniteLoop,ExeName));

       }

       private static bool AlohaSync(string storedProcedureName, TimeSpan delay,bool infiniteLoop,string ExeName="")
       {


           if (!infiniteLoop|| IsRunningAsync)
           { //Launch External app
               if (!string.IsNullOrEmpty(ExeName) && File.Exists(ExeName)) LaunchApplication(ExeName);
               CallBack(storedProcedureName);
               if (OnUpdate != null) OnUpdate();
               return true;
           }

          while(true){
                IsRunningAsync = true;
              //Launch External app
               if (!string.IsNullOrEmpty(ExeName) && File.Exists(ExeName)) LaunchApplication(ExeName);
               CallBack(storedProcedureName);
               if (OnUpdate != null) OnUpdate();
               Thread.Sleep(delay);
               
           }  // infinite loop

           return true;
       }

       public static  void CallBack(string storedProcedureName)
       {
           try
           {
               using (var con = new SqlConnection(Config.CurrentConnection))
               {
                   var ds = new DataSet();

                   var cmd = new SqlCommand(storedProcedureName, con)
                             {
                                 CommandType = CommandType.StoredProcedure,
                                 Connection = con
                             };
                   cmd.Connection.Open();
                   cmd.Prepare();
                   var adapter = new SqlDataAdapter(cmd);
                   adapter.Fill(ds);
                   cmd.Connection.Close();
               }
           }
           catch (Exception ex)
           {
              
           }
           finally { }
       }

       #endregion


       public static DataSet CallDataSet(string storedProcedureName)
       {
            try
            {

           using (var conn = new SqlConnection(Config.CurrentConnection))
           {
               var ds = new DataSet();

               var cmd = new SqlCommand(storedProcedureName, conn)
               {
                   CommandType = CommandType.StoredProcedure,
                   Connection = conn
               };   
               cmd.Connection.Open();
               cmd.Prepare();
               var adapter = new SqlDataAdapter(cmd);
               adapter.Fill(ds);
               cmd.Connection.Close();

               return ds;
           }
           }
            catch(Exception ex)
            {
                // todo log this
                return null;
            }
            finally
            {

            }
       }

       public static void UpdateStock()
       {
           CallDataSet("SP_UpdateOnhand");
       }

       public static DataSet CallDataSet(string storedProcedureName ,Dictionary<string,string> parameters )
       {
           using (var con = new SqlConnection(Config.CurrentConnection))
           {
               var ds = new DataSet();

               var cmd = new SqlCommand(storedProcedureName, con)
               {
                   CommandType = CommandType.StoredProcedure,
                   Connection = con
               };

               parameters.ForEach(dic=> cmd.Parameters.AddWithValue(dic.Key,dic.Value));
               cmd.Connection.Open();
               cmd.Prepare();
               var adapter = new SqlDataAdapter(cmd);
               adapter.Fill(ds);
               cmd.Connection.Close();

               return ds;
           }
       }
       public static DataSet CallDataSet(string storedProcedureName, Dictionary<string, DateTime> parameters)
       {
           using (var con = new SqlConnection(Config.CurrentConnection))
           {
               var ds = new DataSet();

               var cmd = new SqlCommand(storedProcedureName, con)
               {
                   CommandType = CommandType.StoredProcedure,
                   Connection = con
               };

               parameters.ForEach(dic => cmd.Parameters.AddWithValue(dic.Key, dic.Value));
               cmd.Connection.Open();
               cmd.Prepare();
               var adapter = new SqlDataAdapter(cmd);
               adapter.Fill(ds);
               cmd.Connection.Close();

               return ds;
           }
       }
       private static void LaunchApplication(string ExeName)
       {
           lock (applicationLocker)
           {
           // Prepare the process to run
           ProcessStartInfo start = new ProcessStartInfo();
           // Enter in the command line arguments, everything you would enter after the executable name itself
          // start.Arguments = arguments;
           // Enter the executable to run, including the complete path
           start.FileName = ExeName;
           // Do you want to show a console window?
           start.WindowStyle = ProcessWindowStyle.Hidden;
           start.CreateNoWindow = true;
          
           // Run the external process & wait for it to finish
           
               using (Process proc = Process.Start(start))
               {
                   proc.WaitForExit();
                 
               }
           }
       }

       private static  object applicationLocker = new object();
       private static bool IsRunningAsync = false;
       #endregion Callbacks


       public static event Action OnUpdate;
    }
}
