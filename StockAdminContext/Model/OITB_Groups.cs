//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SigiApi.Model
{
    using System;
    using System.Collections.Generic;using Newtonsoft.Json;
    
    public partial class OITB_Groups : BaseModel
    {
        public OITB_Groups()
        {
            this.OITM_Articles = new HashSet<OITM_Articles>();
        }
    
        public short ItmsGrpCod { get; set; }
        public string ItmsGrpNam { get; set; }
        public System.DateTime LastSyncDateL { get; set; }
        public string Locked { get; set; }
        public string StateL { get; set; }
        public string MandatoryCount { get; set; }
    
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public virtual ICollection<OITM_Articles> OITM_Articles { get; set; }
    }
}