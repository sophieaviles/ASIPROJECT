using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockAdminContext.Models
{
    public partial class OCRD_BusinessPartner : BaseModel
    {
        
        
        [Key]
        public string CardCode { get; set; }
        public string CardName { get; set; }// Nombre
        public int GroupCode { get; set; }
        public string AddId { get; set; } // NRC

        public string Notes { get; set; }// GIRO
        public string Address { get; set; }
        
        // Si es cliente o Proveedor 
        // Cliente =C , Proveedor =S
        public string CardType { get; set; } 
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Cellular { get; set; }
        public string Email { get; set; }
        public string ValidFor { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public string U_NIT { get; set; }

        public string VatGroup { get; set; }
    }
}
