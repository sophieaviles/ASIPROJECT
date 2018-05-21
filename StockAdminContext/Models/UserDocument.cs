using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.SymbolStore;

namespace StockAdminContext.Models
{
    public partial class UserDocument : BaseModel
    {

        public UserDocument()
        {
            IsAvailable = true;
        }

        //[Key, Column(Order = 0)]
        //public string ObjType { get; set; }
        //[Key, Column(Order = 1)]

        [Key]
        public int Id { get; set; }
        public string ObjType { get; set; }
        public int IdUser { get; set; }
        public string TypeAccess { get; set; }

        [NotMapped]
        public bool IsAvailable { get; set; }

        public virtual Document Document { get; set; }
        public virtual LocalUser LocalUser { get; set; }
    }
}
