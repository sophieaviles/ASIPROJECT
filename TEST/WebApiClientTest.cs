using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SigiApi.Model;
using SigiApi.WebServices;

namespace TEST
{
    [TestClass]
    public class WebApiClientTest
    {
        [TestMethod]
        public void TestArticles()
        {


           
            Task.WaitAll(new Task(() =>
                                  {
                                     var  products =  WebApiClient.GetArticles();

                                      ;
                                  }));
             

        }

         
        //[TestMethod]
        //public void TestBranches()
        //{
        //    var bran
        //}
    }
}
