using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockAdminContext.Models
{
    public partial class UserAuthorizationSet : BaseModel
    {
        [Key, Column(Order = 0)]
        public string ObjType { get; set; }

        [Key, Column(Order = 1)]
        public System.Guid UserId { get; set; }
        public Nullable<System.Guid> LocalUsers_UserId { get; set; }
        public virtual Document Document { get; set; }
        public virtual LocalUser LocalUser { get; set; }
    }
}
