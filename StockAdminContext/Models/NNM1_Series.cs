using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ServiceModel.Description;
using Newtonsoft.Json;

namespace StockAdminContext.Models
{
    public partial class NNM1_Series : BaseModel
    {
        public NNM1_Series()
        {
            this.Checkbooks = new List<Checkbook>();
        }

        public string ObjectCode { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Series { get; set; }
        public string SeriesName { get; set; }
        public string Remark { get; set; }
        public string Locked { get; set; }
        public string StateL { get; set; }
        public string IsCheckBooks { get; set; }
        public virtual ICollection<Checkbook> Checkbooks { get; set; }

        [JsonIgnore]
        public List<Deposit> Desposits { get; set; }
            
            
        [NotMapped]
        public string SeriesTitle { get { return string.Format("{0} {1}", SeriesName, Remark); } }

      
    }
}
