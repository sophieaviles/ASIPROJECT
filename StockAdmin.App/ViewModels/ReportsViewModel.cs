using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using StockAdminContext;
using StockAdminContext.Models;
//using SigiFluent.Views.Reports;

namespace SigiFluent.ViewModels
{
    public class ReportsViewModel:BaseViewModel
    {

        private void BuildReport()
        {
            var mapping = ViewModelManager.ActionBarViewModel.ReportType;

            if (mapping == null) return;

            var parameters = GetParameters(mapping);

            var dataSet = StoredCallbackProcessor.CallDataSet(mapping.StoredProcedureName, parameters);
            //var report = new ReportContainer(mapping.ReportFileName, dataSet);

            //ViewModelManager.LoadReport(report);
        }

        private void BuildReportNameAjusteInventarios()
        {

            
            //var report = new ReportContainer("rptAjustedeInventarios.rpt", StoredCallbackProcessor.CallDataSet("SP_INV_REP_AJUSTES",Parameters));

            //ViewModelManager.LoadReport(report);
        }
          
        private Dictionary<string, string> GetParameters(ReportMapping mapping)
        {
            
                var parametes = new Dictionary<string, string>();
                if (StartDate.HasValue)
                    parametes.Add("@inicio", StartDate.Value.ToShortDateString());
                if (LastDate.HasValue)
                    parametes.Add("@fin", LastDate.Value.ToShortDateString());

                parametes.Add("@whscode",Config.WhsCode);

               
                if(mapping.ParameterType==1) parametes.Add("@ItmsGrpCod",GetItmsGrpCode());

                return parametes;
            
        }

        private string GetItmsGrpCode()
        {
            var group = ViewModelManager.ActionBarViewModel.SelectedGroup;

            return group != null ? group.ItmsGrpCod.ToString() : string.Empty;
        }
  
         
        #region Overrides 

        public override void ExecuteDoProcess()
        {
            BuildReport();
        }

        public override bool CanExecuteDoProcess()
        {
            return StartDate.HasValue && LastDate.HasValue;
        }

        #endregion 
    }
}
