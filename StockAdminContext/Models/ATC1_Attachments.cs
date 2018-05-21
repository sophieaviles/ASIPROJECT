using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace StockAdminContext.Models
{
    public partial class ATC1_Attachments : BaseModel
    {
        public ATC1_Attachments()
        {
            this.Date = DateTime.Now;
        }
        [Key]
        public int IdAtached { get; set; }
        public int AbsEntry { get; set; }
        public int Line { get; set; }
        public string srcPath { get; set; }
        public string trgtPath { get; set; }
        public string FileName { get; set; }
        public string FileExt { get; set; }
        public System.DateTime Date { get; set; }
        public int UsrID { get; set; }
        public string Copied { get; set; }
        public string Override { get; set; }
        public int IdEspecialOrder { get; set; }

        [JsonIgnore]
        public virtual ORDR_SpecialOrder ORDR_SpecialOrder { get; set; }
    }
}
