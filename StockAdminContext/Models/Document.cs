using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace StockAdminContext.Models
{
    public partial class Document : BaseModel
    {
        public Document()
        {
            this.UserAuthorizationSets = new List<UserAuthorizationSet>();
            this.UserDocuments = new List<UserDocument>();
        }

        [Key]
        public string ObjType { get; set; }
        public string DocumentName { get; set; }

        public string type{ get; set;}

        [NotMapped]
        public Type DocumentType
        {
            get
            {
                return JsonConvert.DeserializeObject<Type>(type);
            }
            set
            {
                type = JsonConvert.SerializeObject(value);
                RaisePropertyChanged("DocumentType");
            }

        }

        public virtual ICollection<UserAuthorizationSet> UserAuthorizationSets { get; set; }
        public virtual ICollection<UserDocument> UserDocuments { get; set; }
    }
}
