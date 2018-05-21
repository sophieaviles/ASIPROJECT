using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockAdminContext.Models
{
   public  class InvoicePayment
    {
       public string  WhsCode { get; set; }

       public int AlohaId { get; set; }

       public double Amount { get; set; }

       public  PaymentType PaymentType { get; set; }


       // Al hacer el pago de la factura , busque la tabla y por el id local recupere las formas de pago . 
    }
}
