using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockAdminContext.Models
{
    public  class Synchro<Tmodel>
    {
        public Synchro(Tmodel model, StockAdminContext.Models.LOG_CHANGES currentVersion)
        {
            Model = model;
            version = currentVersion;
        }

        public Synchro()
        {
            
        }


        public Tmodel Model { get; set; }

        public LOG_CHANGES version { get; set; }
    }
}
