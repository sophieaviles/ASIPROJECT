using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TEST
{
    [TestClass]
    public class UnitTest1
    {


        [TestMethod]
        public void SerializeTest()
        {

            var db = ContextFactory.GetDBContext();

            var tranfers = db.OWTQ_TransferRequest.Where(t => t.StateL == LocalStatus.Pendiente).ToList();

            tranfers.ForEach(t => db.OWTQ_TransferRequest.Remove(t));

            db.SaveChanges();
            //var order = new OWTQ_TransferRequest();

            //var result = WebApiClient.AddOrder(order);

            //Assert.IsTrue(result);
        }

        [TestMethod]
        public void AddTestOrderToDataBse()
        {

            //var deleteOrder = WebApiClient.DeleteOrder(23);

            //Assert.IsTrue(deleteOrder);

            //var result = SigiApi.Helpers.TransferRequestHelper.AddOrderToDataBase();

            //Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestInitializeMembership()
        {

            ////Membership.InitializeMembership();

            //var products =  PackageManager.GetProducts();

            

        }
        [TestMethod]
        public void TestDb()
        {

            ////Membership.InitializeMembership();
            
            //var products =  PackageManager.GetProducts();
           // var db = ContextFactory.GetDBContext();
            //branch =  "SUC-001";

            //var branch = db.OWHS_Branch.FirstOrDefault(b => b.WhsCode == "SUC-001");

           // ContextFactory.GetDBContext().OWHS_Branch.FirstOrDefault(b => b.WhsCode == branch);

           // var prods = GetBranchArticles(branch);

        }
        public static List<OITM_Articles> GetBranchArticles(OWHS_Branch branch)
        {
            var loop = branch.OITW_BranchArticles;
            return loop.Select(ba => ba.OITM_Articles).ToList();
        }

        [TestMethod]
        public void TestDatabase()
        {
            var db = ContextFactory.GetDBContext();

            var articles = db.OITM_Articles.ToList();


            var warehouses = db.OWHS_Branch.ToList();

            var branchArticles = db.OITW_BranchArticles.ToList();

            Assert.IsNotNull(articles);

            Assert.IsNotNull(warehouses);
            Assert.IsNotNull(branchArticles);
        }


        [TestMethod]
        public void UpdateFromWebApiTest()
        {
            

            var trans = ContextFactory.GetDBContext().OWTQ_TransferRequest.Include("WTQ1_TransferRequestDetails")
                .FirstOrDefault(t => t.IdTransferRequestL == 13);

            var db = ContextFactory.GetDBContext();

            var transfer = WebApiClient.GetTransferRequest("144").Result;

           trans.UpdateModelPropertiesFrom(transfer); 

            db.SaveChanges();


            //var details = db.WTQ1_TransferRequestDetails.Where(t => t.IdTransferRequestL == 13).ToList();

            //details.UpdateModelPropertiesFrom(trans.WTQ1_TransferRequestDetails);

            //db.SaveChanges();
        }


        [TestMethod]
        public void Series()
        {


            var trans = SeriesHelper.GetSeries("18");
        }

        [TestMethod]
        public void TestSerialize()
        {
            //var order = SpecialOrdersHelper.GetOrder(9);

            //var orderString = JsonConvert.SerializeObject(order);


            var result =  WebApiClient.Upload("SUC-001",
                @"C:\Users\Jorge\Documents\Visual Studio 2012\Projects\SIGI\Web API\WebApiSigi\Upload\TestFile.PDF");

        }

        [TestMethod]
        public void TestBrachtProducts()
        {

            var db = ContextFactory.GetDBContext();

            var Code = "SUC-016";

            var products = BranchsHelper.GetBranch(Code);

            Assert.IsTrue( products.OITW_BranchArticles.Count== 411);

        }
       
    }
}
