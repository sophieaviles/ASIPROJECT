using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StockAdminContext.Helpers;
using StockAdminContext.Models;


namespace StockAdminContext.WebServices
{
    public static class WebApiClient
    {

        static WebApiClient()
        {


            SetConfig();

        }

        #region  Transfer Requests

        public static Task<OWTQ_TransferRequest> GetTransferRequest(string key)
        {
            return GetAsync<OWTQ_TransferRequest>(string.Format("api/orders?key={0}", key));
        }

        public static Task<Synchro<OWTQ_TransferRequest>> AddTransfer(OWTQ_TransferRequest order)
        {
          return PostAsync<OWTQ_TransferRequest,Synchro<OWTQ_TransferRequest>>( "api/orders", order); 

        }

        #endregion

        #region Articles

        public static Task<List<OITM_Articles>> GetArticles()
        {
            return GetAsync<List<OITM_Articles>>("api/Articles");
        }

        public static Task<OITM_Articles> GetArticleByKey(string key)
        {
            return GetAsync<OITM_Articles>(string.Format("api/Articles?key={0}", key));
        }

        #endregion

        #region DownPayment

        public static Task<ODPI_DownPayment> GetDownPayment(string key)
        {
            return GetAsync<ODPI_DownPayment>(string.Format("api/DownPayments?key={0}", key));
        }

        public static Task<Synchro<ODPI_DownPayment>> AddDownPayment(ODPI_DownPayment downPayment)
        {
            return PostAsync<ODPI_DownPayment, Synchro<ODPI_DownPayment>>("api/DownPayments", downPayment);
        }

        #endregion

        #region GoodIssues

        public static Task<OIGE_GoodsIssues> GetGoodIssue(string key)
        {
            return GetAsync<OIGE_GoodsIssues>(string.Format("api/GoodsIssues?key={0}", key));
        }

        public static Task<Synchro<OIGE_GoodsIssues>> AddGoodIssue(OIGE_GoodsIssues goodIssue)
        {
            return PostAsync<OIGE_GoodsIssues, Synchro<OIGE_GoodsIssues>>("api/GoodsIssues", goodIssue);
        }

        #endregion
        
        #region GoodReceipts
        public static Task<OIGN_GoodsReceipt> GetGoodReceipt(string key)
        {
            return GetAsync<OIGN_GoodsReceipt>(string.Format("api/GoodsReceipts?key={0}", key));
        }

        public static Task<Synchro<OIGN_GoodsReceipt>> AddGoodReceipt(OIGN_GoodsReceipt receipt)
        {
            return PostAsync<OIGN_GoodsReceipt, Synchro<OIGN_GoodsReceipt>>("api/GoodsReceipts", receipt);
        }
        #endregion 
        #region ItemGroups

        public static Task<List<OITB_Groups>> GetItemGroups(string key)
        {
            return GetAsync<List<OITB_Groups>>(string.Format("api/ItemGroups?key={0}", key));
        }

        public static Task<bool> AddItemGroup(OITB_Groups itemgroups)
        {
            return PostAsync<OITB_Groups, bool>("api/ItemGroups", itemgroups);
        }


        #endregion

        #region CreditNotes

        public static Task<ORPC_SupplierCreditNotes> GetCreditNotes(string key)
        {
            return GetAsync<ORPC_SupplierCreditNotes>(string.Format("api/SupplierCreditNotes?key={0}", key));
        }

        public static Task<Synchro<ORPC_SupplierCreditNotes>> AddCreditNote(ORPC_SupplierCreditNotes creditNote)
        {
            return PostAsync<ORPC_SupplierCreditNotes, Synchro<ORPC_SupplierCreditNotes>>("api/SupplierCreditNotes", creditNote);
        }

        #endregion

        #region Sales Creditnote

        public static Task<ORIN_ClientCreditNotes> GetSaleCreditNote(string key)
        {
            return GetAsync<ORIN_ClientCreditNotes>(string.Format("api/SalesCreditNotes?key={0}", key));
        }

        public static Task<Synchro<ORIN_ClientCreditNotes>> AddSaleCreditNote(ORIN_ClientCreditNotes note)
        {
            return PostAsync<ORIN_ClientCreditNotes, Synchro<ORIN_ClientCreditNotes>>("api/SalesCreditNotes", note);
        }

        #endregion

        #region WareHouses
        public static Task<List<OWHS_Branch>> GetWarehouses()
        {
            return GetAsync<List< OWHS_Branch>>("api/Warehouse");
        }

        public static Task<OWHS_Branch> GetWarehouse(string key)
        {
            return GetAsync<OWHS_Branch>(string.Format("api/Warehouse?key={0}", key));
        }
        #endregion

        #region Syncho Logs

        public static Task<List<LOG_CHANGES>> GetNewChangesets(int dVersion,int mVersion, string whsCode)
        {
            var route = string.Format("api/Synchronization?dVersion={0}&mVersion={1}&whscode={2}", dVersion, mVersion, whsCode);
            return GetAsync<List<LOG_CHANGES>>(route);
        }

        #endregion 

        #region tranfers
        public static Task<OWTR_Transfers> GetTransfer(string key)
        {
            return GetAsync<OWTR_Transfers>(string.Format("api/Transfers?key={0}", key));
        }

        #endregion

        #region SpecialOrders

        public static Task<Synchro<ORDR_SpecialOrder>> AddSpecialOrder(ORDR_SpecialOrder order)
        {

            //order.FileName = order.ReportFileName;
            //var path = string.Format("api/SpecialOrder?filename={0}",order.ReportFileName);

            //PutAsync<ORDR_SpecialOrder, bool>("api/SpecialOrder", order);

            return PostAsync<ORDR_SpecialOrder, Synchro<ORDR_SpecialOrder>>("api/SpecialOrder", order);

        }

        public static Task<ORDR_SpecialOrder> GetSpecialOrder(string key)
        {
            return GetAsync<ORDR_SpecialOrder>(string.Format("api/SpecialOrder?key={0}",key));
        }

        #endregion

        #region AddPurchases

        public static Task<Synchro<OPCH_Purchase>> AddPurchase(OPCH_Purchase purchase)
        {
            return PostAsync<OPCH_Purchase, Synchro<OPCH_Purchase>>("api/Purchases", purchase); 
        }

        public static Task<OPCH_Purchase> GetPurchase(string key)
        {
            return GetAsync<OPCH_Purchase>(string.Format("api/Purchases?key={0}", key));
        }

        #endregion addPurchases

        #region Sale

        public static Task<Synchro<OINV_Sales>> AddSale(OINV_Sales sale)
        {
            return PostAsync<OINV_Sales,Synchro<OINV_Sales>>("api/sales",sale);
        }

        public static Task<OINV_Sales> GetSale(string key)
        {
            return GetAsync<OINV_Sales>(string.Format("api/Sales?key={0}", key));
        }

        public static Task<Synchro<List<InvoicePayment>>> AddInvoicePayment(List<InvoicePayment> payments)
        {
            return PutAsync<List<InvoicePayment>,
                Synchro<List<InvoicePayment>>>("api/Sales", payments);
        } 

        #endregion

        #region BusinessPartner

        public static Task<OCRD_BusinessPartner> GetBusinessPartner(string key)
        {
            return GetAsync<OCRD_BusinessPartner>(string.Format("api/BusinessPartner?key={0}", key));
        } 

        #endregion


        #region File Upload

        //public static bool Upload(string whscode, string filePath)
        //{

        //    var apiRoute = string.Format("api/upload?whscode={0}", whscode);
        //    // Create a progress notification handler
        //    ProgressMessageHandler progress = new ProgressMessageHandler();
        //   // progress.HttpSendProgress += ProgressEventHandler;

        //    // Create an HttpClient and wire up the progress handler
        //    HttpClient client = HttpClientFactory.Create(progress);
        //    client.BaseAddress = new Uri(UrlWebApi);

        //    // Set the request timeout as large uploads can take longer than the default 2 minute timeout
        //    client.Timeout = TimeSpan.FromMinutes(20);

        //    // Open the file we want to upload and submit it
        //    using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, BufferSize, useAsync: true))
        //    {
        //        // Create a stream content for the file
        //        StreamContent content = new StreamContent(fileStream, BufferSize);

        //        // Create Multipart form data content, add our submitter data and our stream content
        //        MultipartFormDataContent formData = new MultipartFormDataContent();
        //        formData.Add(new StringContent(whscode), "submitter");
        //        formData.Add(content, "filename", Path.GetFileName(filePath));

        //        // Post the MIME multipart form data upload with the file

        //        HttpResponseMessage response = client.PostAsync(apiRoute, formData).Result;

        //        FileResult result =   response.Content.ReadAsAsync<FileResult>().Result;

        //        if (response.IsSuccessStatusCode)
        //        {
        //            Console.WriteLine("{0}Result:{0}  Filename:  {1}{0}  Submitter: {2}", Environment.NewLine,
        //                result.FileNames.FirstOrDefault(), result.Submitter);
        //        }

        //        return result != null;
        //    }
        //}

        #endregion

      

        public static Task<bool> ConfirmChangeset(LOG_CHANGES changeset)
        {
            return PostAsync<LOG_CHANGES, bool>("api/Synchronization", changeset);
        }

        public static bool IsAvailableConnection
        {
            get
            {
                ////return ConnectionBridge.StateConnection == ConnectionState.Connected;
                //try
                //{
                //    using (var client = new WebClient())
                //    using (var stream = client.OpenRead("http://172.16.0.2:83/"))
                //    {
                //        return true;
                //    }
                //}
                //catch
                //{
                //    return false;
                //}
                return true;
            }
           
        }


        private static async  Task<Tmodel> GetAsync<Tmodel>(string path)  
        {
            try
            {
                using (var client = GetHttpClient())
                {

                    HttpResponseMessage response = client.GetAsync(path).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var resulstring =    response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<Tmodel>(resulstring);

                    }
                    // TODO  Log Error Message
                    var error = string.Format("{0} ({1})", (int) response.StatusCode, response.ReasonPhrase);

                    var resultstring = response.Content.ReadAsStringAsync().Result;

                    var errorModel = JsonConvert.DeserializeObject<ApiException>(resultstring);

                    if (errorModel != null && OnWebApiError != null) OnWebApiError(errorModel);

                    ReturnNullFromServerError();
                    LoggerHelper.WriteLog(error);
                    

                }
            }
            catch (Exception ex)
            {
                LoggerHelper.WriteLog(ex);
              
                
            }
            return JsonConvert.DeserializeObject<Tmodel>(string.Empty);
        }

               
        private static Task<Tresult> PostAsync<Tmodel, Tresult>(string path, Tmodel model)
        {
            try
            {
                //using (var client = GetHttpClient())
                //{
                //    var response = client.PostAsJsonAsync(path, model).Result;

                //    if (response.IsSuccessStatusCode)
                //    {
                //        return response.Content.ReadAsAsync<Tresult>();
                //    }

                //    //TODO  log error message.
                //    var error = string.Format("{0} ({1}) Request Message:{2}", (int) response.StatusCode,
                //        response.ReasonPhrase, response.RequestMessage);

                //    var errorContent = response.Content.ReadAsStringAsync().Result;

                //    var errorModel = response.Content.ReadAsAsync<ApiException>().Result;

                //    if (errorModel != null && OnWebApiError!=null) OnWebApiError(errorModel);
                    
                //    ReturnNullFromServerError();
                //    LoggerHelper.WriteLog(errorContent);
                //    //LoggerHelper.WriteLog(error);
                    
                  return null;
                //}
            }
            catch (Exception ex)
            {
                LoggerHelper.WriteLog(ex);
                return null;
            }
        }

        private static Task<Tresult> PutAsync<Tmodel, Tresult>(string path, Tmodel model)
        {
            using (var client = GetHttpClient())
            {
                //var response = client.PutAsJsonAsync(path, model).Result;

                //if (response.IsSuccessStatusCode)
                //{
                //    return response.Content.ReadAsAsync<Tresult>();
                //}

                ////TODO  log error message.
                //var error = string.Format("{0} ({1}) Request Message:{2}", (int)response.StatusCode,
                //    response.ReasonPhrase, response.RequestMessage);
                //LoggerHelper.WriteLog(error);
                return null;
            } 
        }

        private static void ReturnNullFromServerError()
        {
            if (OnWebApiError == null) return;

                OnWebApiError(new ApiException
                              {
                                  ExceptionMessage = "Error Al intentar Sincronizar con El servidor... intentelo nuevamente.",
                                  
                              });
        }

        private static HttpClient GetHttpClient()
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback +=
                (sender, certificate, chain, errors) => true;

            var client =  new HttpClient()

            {  
                BaseAddress = new Uri(UrlWebApi),

                Timeout = TimeSpan.FromMinutes(1)

            };
            client.DefaultRequestHeaders.Add("WhsCode",Config.WhsCode);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            return client;
        }

        private static void SetConfig()
        {
            urlWebApi = ConfigurationManager.AppSettings["WebApiUrl"];
        }

        private static string UrlWebApi
        {
            get
            {
                return urlWebApi;
            }
        }

        private static string urlWebApi;

        const int BufferSize = 1024;

        public static event Action<ApiException> OnWebApiError;
    }
}
