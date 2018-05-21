using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading;
using StockAdminContext.Helpers;
using StockAdminContext.Models;

namespace StockAdminContext
{
    public static class AsyncHelper
    {

        #region ASyncMethods
        

        public static void DoAsync(Action func, Action onComplete = null,bool raiseException= false)
        {
            using (var async = new AsyncProcessor())
            {
                if (func != null) async.OnLoadContent += func;
                if (onComplete != null) async.OnLoadComplete += onComplete;
                async.Run();
            } 
        }
        

       
       
        #endregion


        private class AsyncProcessor : IDisposable
        {
            public AsyncProcessor()
            {
                worker = new BackgroundWorker();
                worker.DoWork += worker_DoWork;
                worker.WorkerSupportsCancellation = true;
                worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            }

            public void Run()
            {
                worker.RunWorkerAsync();
            }

            void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
            {


                if (OnLoadComplete != null)
                {
                    OnLoadComplete();
                }
                this.Dispose();
            }

            void worker_DoWork(object sender, DoWorkEventArgs e)
            {


                if (OnLoadContent != null)
                {
                    OnLoadContent();
                }
            }

            public event Action OnLoadContent;
            public event Action OnLoadComplete;

            private bool ShowLoader { get; set; }

            private BackgroundWorker worker;
            public void Dispose()
            {
                worker.Dispose();

            }
            private bool RunProcess(Func<bool> func, bool raiseException = false)
            {
                try
                {
                    return func();
                }
                catch (Exception ex)
                {
                    LoggerHelper.WriteLog(ex);

                    if (onException != null) onException(new ApiException()
                    {
                        Message = ex.Message,
                        ExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : "",
                        StackTrace = ex.StackTrace,
                    });

                    if (raiseException) throw ex;

                    return false;
                }
            }

            
        } 
        public static event Action<ApiException> onException;
    }

}
