using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StockAdminContext.Models
{
    public partial class Parameter : BaseModel
    {
        public Parameter()
        {
             
        }

        [Key]
        public string ParameterKey { get; set; }
        public string Parameter1 { get; set; }
        public string Value1 { get; set; }
        public string Value2 { get; set; }
        public System.DateTime CreatedDateL { get; set; }
        public string CreatedByL { get; set; }
        public string ModifiedByL { get; set; }
        public System.DateTime ModifiedDateL { get; set; }
    }
}
