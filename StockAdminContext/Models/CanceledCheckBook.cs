using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Transactions;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Metadata.Edm;
using System.ComponentModel;

namespace StockAdminContext.Models
{
    public class CanceledCheckBook :BaseModel
    {
        [Key]
        public int IdL { get; set; }
        public int IdCheckBookL { get; set; } 

        public int InitialNumL { get; set; }

        public int LastNumL { get; set; }

        public System.DateTime CreatedDateL { get; set; }
        public string CreatedByL { get; set; }
        public string ModifiedByL { get; set; }
        public System.DateTime ModifiedDateL { get; set; }
        public CanceledCheckBookCategories CategoryL { get; set; }

        public bool Processed { get; set; }

        [JsonIgnore]
        public Checkbook Checkbook { get; set; }

        [NotMapped]
        public StockAdminContext.EnumExtension.EnumMember Category
        {
            set {
                CategoryL = (CanceledCheckBookCategories)EnumHelper.GetEnumValue(typeof(CanceledCheckBookCategories), value);                    
            }           
        }

        [NotMapped]
        public string CategoryName {
            get {        
                    return EnumHelper.GetDescription(CategoryL);
            }
        }


        public static void Update()
        {
            lock (Extensions.Locker)
            {

                var db = ContextFactory.GetDBContext();
                using (var scope = new TransactionScope())
                {
                    db.SaveChanges();
                    scope.Complete();
                }
            }
        }
    }
}
