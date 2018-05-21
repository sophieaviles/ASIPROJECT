using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StockAdminContext.Models
{
    public partial class LocalUser : BaseModel
    {
        public LocalUser()
        {
            this.UserDocuments = new List<UserDocument>();
            this.UserAuthorizationSets = new List<UserAuthorizationSet>();
            CreatedByL = Config.CurrentUser;
            CreatedDateL = DateTime.Now;
            ModifiedDateL = DateTime.Now;

        }

        //[Key]
        //public System.Guid UserId { get; set; }
        [Key]
        public int Id { get; set; }
        public string NickName { get; set; }
        public string Name { get; set; } 
        public string LastName { get; set; } 
        public string Password { get; set; }
        public string Position { get; set; }
        public string Locked { get; set; }
        public string CreatedByL { get; set; }
        public System.DateTime CreatedDateL { get; set; }
        public string ModifiedByL { get; set; }
        public System.DateTime ModifiedDateL { get; set; }
        public bool AllowChangePrices { get; set; }
        public bool AllowProcessing { get; set; }

       // public string Email { get; set; }
        public virtual ICollection<UserDocument> UserDocuments { get; set; }
        public virtual ICollection<UserAuthorizationSet> UserAuthorizationSets { get; set; }

        public List<Deposit> Deposits { get; set; }

        public List<Deposit> AuthoriezdDeposits { get; set; }

        public List<CashMissing> CashMissings { get; set; } 
    }
}
