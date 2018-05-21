using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockAdminContext.Models
{
     public class BusinessPartnerModel: BaseModel
    {
        public string CardName { get; set; }// Nombre
       
        public string AddId { get; set; } // NRC

        public string Notes { get; set; }// GIRO

         public string U_NIT { get; set; }
         public string Address { get; set; }

         public string Phone1 { get; set; }
    }
}
