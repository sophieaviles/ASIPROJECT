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
    
    public partial class OITM_ALOHA : BaseModel
    {
        public int IdALOHA_Articles { get; set; }
        public string ItemCode { get; set; }
    
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public virtual OITM_Articles OITM_Articles { get; set; }
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public virtual ALOHA_Articles ALOHA_Articles { get; set; }
    }
}