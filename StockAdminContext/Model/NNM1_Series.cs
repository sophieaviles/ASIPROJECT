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
    
    public partial class NNM1_Series : BaseModel
    {
        public string ObjectCode { get; set; }
        public short Series { get; set; }
        public string SeriesName { get; set; }
        public string Remark { get; set; }
        public string Locked { get; set; }
        public string StateL { get; set; }
    }
}
