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
    
    public partial class UCakes : BaseModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int DocEntry { get; set; }
        public string Canceled { get; set; }
        public string Object { get; set; }
        public Nullable<int> LogInst { get; set; }
        public Nullable<int> UserSign { get; set; }
        public string Transfered { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<short> CreateTime { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<short> UpdateTime { get; set; }
        public string DataSource { get; set; }
    }
}
