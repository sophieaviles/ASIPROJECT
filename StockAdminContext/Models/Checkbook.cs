using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace StockAdminContext.Models
{
    public partial class Checkbook : BaseModel
    {
        public Checkbook()
        {
            this.SeriesCheckbook = new List<NNM1_Series>();
        }

        [Key] 
        public int IdCheckbookL { get; set; }
        public System.DateTime DateL { get; set; }
        public string SerieL { get; set; }
        public int InitialNumL { get; set; }
        public int NextNumberL { get; set; }
        public int LastNumL { get; set; }
        public CheckBookStatus StateL { get; set; }
        public System.DateTime CreatedDateL { get; set; }
        public string ModifiedByL { get; set; }
        public string CreatedByL { get; set; }
        public System.DateTime ModifiedDateL { get; set; }
        public virtual List<NNM1_Series> SeriesCheckbook { get; set; }

        public List<CanceledCheckBook> CanceledCheckBooks { get; set; }
    }


    /// <summary>
    /// (int)Error =  0 sin errores, 1 no tiene asignado talonario, 2 no hay talonario activo cerrar formulario, 3 error general
    /// (string)ErrorMessage = se muestra cuando error != 0
    /// (int)Number = numero a asignar
    /// </summary>
    public class CheckBookNumberItem
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public int Number { get; set; }

    }
    
}
