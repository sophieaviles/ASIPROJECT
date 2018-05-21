using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StockAdminContext.Models
{
    public partial class AuthorizationLog : BaseModel
    {   
        [Key]
        public int IdAuthorizationLog { get; set; }
        public string ObjType { get; set; }
        public System.Guid UserId { get; set; }
        public int DocEntry { get; set; }
        public System.DateTime Date { get; set; }
    }
}
