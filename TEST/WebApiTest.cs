using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockAdminContext.WebServices;

namespace TEST
{
    [TestClass]
    public class WebApiTest
    {
        [TestMethod]
        public void TestArticles()
        {

            var art = WebApiClient.GetArticles().Result;

            Assert.IsTrue(art.Count> 0);
        }

        [TestMethod]
        public void TestFileUpload()
        {
           // WebApiClient.AddSpecialOrder()
        }


        [TestMethod]
        public void TestBranches()
        {
            
        }
    }
}
