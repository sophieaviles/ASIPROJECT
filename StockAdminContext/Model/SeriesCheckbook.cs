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
    
    public partial class SeriesCheckbook : BaseModel
    {
        public short Series { get; set; }
        public int IdCheckbookL { get; set; }
    
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public virtual NNM1_Series NNM1_Series { get; set; }
    }
}
