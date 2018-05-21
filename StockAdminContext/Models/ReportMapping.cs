using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace StockAdminContext.Models
{
    public class ReportMapping
    {
        [Key]
        public int Id { get; set; }


        public string DisplayName { get; set; }

        public string ReportFileName { get; set; }

        public string StoredProcedureName { get; set; }

        public int ParameterType { get; set; }
    }
}
