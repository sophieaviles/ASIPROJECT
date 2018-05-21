using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace StockAdminContext.Models
{
    public class LOG_CHANGES
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int version { get; set; }
        public string FromWhscode { get; set; }
        public string ToWhscode { get; set; }
        public string ObjType { get; set; }
        public string TransType { get; set; }
        public string ListKey { get; set; }
        public string ListVal { get; set; }
        public string TypeSIGI { get; set; }
        public LogStatus StatusL { get; set; }

        [Timestamp][JsonIgnore]
        public byte[] RowVersion { get; set; }

        [NotMapped]
        [JsonIgnore]
        public SyncType SyncType
        {
            get
            {
                var val = TransType.Trim();

                if (val.Contains("A")) return SyncType.Add;
                
                return SyncType.Update;
            }
        }


        public int? IdL { get; set; }
        public bool Confirmed { get; set; }
    }
}
