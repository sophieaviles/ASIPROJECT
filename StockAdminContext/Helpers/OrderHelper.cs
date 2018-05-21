using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockAdminContext.Helpers
{
    public static class  OrderHelper
    {
        static OrderHelper()
        {
            
        }



        //public static bool AddOrder(OWTQ_TransferRequest transfer)
        //{
        //    var company = PackageManager.GetCompany();
        //    SAPbobsCOM.Documents sapOrder;

        //    sapOrder = company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oOrders);
        //    sapOrder.CardCode = transfer.CardCode;

        //    //sapOrder.DocEntry = transfer.DocEntry;
        //    sapOrder.DocDueDate = DateTime.Now;

        //    sapOrder.Lines.ItemCode = "A00001";
        //    sapOrder.Lines.Quantity = transfer.WTQ1_OrderDetails.Count;



        //    var IsAdded = sapOrder.Add() != 0;

        //    var orderNumber = sapOrder.DocNum;

        //    int err = 0;
        //    string errorMessage = string.Empty;
        //    company.GetLastError(out err, out errorMessage);

        //    company.Disconnect();

        //    return IsAdded;
        //}

        public static  void AddOrder(){

        }
    }
}
