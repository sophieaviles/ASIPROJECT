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
    
    public partial class UserDocument : BaseModel
    {
        public string ObjType { get; set; }
        public System.Guid UserId { get; set; }
        public string TypeAccess { get; set; }
    
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public virtual Documents Documents { get; set; }
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public virtual LocalUsers LocalUsers { get; set; }
    }
}
