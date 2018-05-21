using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace StockAdminContext.Models
{
    public abstract class BaseModel : INotifyPropertyChanged
    {
        
        public event PropertyChangedEventHandler PropertyChanged = delegate { };


        protected void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }

        }        
    }
}
