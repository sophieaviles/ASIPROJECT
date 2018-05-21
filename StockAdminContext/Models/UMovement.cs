using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace StockAdminContext.Models
{
    public partial class UMovement : BaseModel
    {
        [Key]
        public string Code { get; set; }
        public string Name { get; set; }              
        public string U_TIPO { get; set; }
 
    }
}
