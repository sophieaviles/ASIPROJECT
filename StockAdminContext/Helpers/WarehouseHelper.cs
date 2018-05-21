using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace StockAdminContext.Helpers
{
    public class WarehouseHelper
    {
        //public static List<OWHS_Branch> GetWareHouses()
        //{
        //    var branches = new List<OWHS_Branch>();

        //    var company = PackageManager.GetCompany();

        //    SAPbobsCOM.SBObob oSBObob;
        //    SAPbobsCOM.Recordset oRecordSet;
        //    try
        //    {
        //        oSBObob = company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoBridge);

        //        oRecordSet = company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

        //        oRecordSet = oSBObob.GetWareHouseList();


        //        while (!oRecordSet.EoF == true)
        //        {
        //            var branch = new OWHS_Branch()
        //                         {
        //                             WhsCode =  oRecordSet.Fields.Item(0).Value,
        //                             WhsName =  oRecordSet.Fields.Item(1).Value,
        //                         };
        //            branches.Add(branch);
        //            oRecordSet.MoveNext();

        //        }

        //        return branches;
        //    }
        //    catch (Exception ex)
        //    {
        //        // TODO log this Exception
        //        return new List<OWHS_Branch>();
        //    }

        //}

        //public static List<string> GetWareHouseObjects()
        //{
        //    var branches = new List<string>();

        //    var company = PackageManager.GetCompany();

        //    SAPbobsCOM.SBObob oSBObob;
        //    SAPbobsCOM.Recordset oRecordSet;
        //    try
        //    {
        //        oSBObob = company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoBridge);

        //        oRecordSet = company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

        //        oRecordSet = oSBObob.GetWareHouseList();

                


        //        while (!oRecordSet.EoF == true)
        //        {
        //            var item = oRecordSet.Fields.Item(0).Value;
        //            var jsonFormat = JsonConvert.SerializeObject(item);
        //            branches.Add(jsonFormat);
        //            oRecordSet.MoveNext();
        //        }

        //        return branches;
        //    }
        //    catch (Exception ex)
        //    {
        //        // TODO log this Exception
        //        return new List<string>();
        //    }
        //}
    }
}
