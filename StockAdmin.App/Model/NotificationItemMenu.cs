using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StockAdminContext.Models;

namespace SigiFluent.ViewModels
{
   public   class NotificationItemMenu :BaseModel
    {

       public NotificationItemMenu(string notificationName, string notificationCount, RelayCommand actionCommand)
       {
           this.NotificationCount = notificationCount;
           this.NotificationName = notificationName;
           this.ActionCommand = actionCommand;
           this.IsVisible = true;

       }
       public string NotificationName { get; set; }
        public string NotificationCount { get; set; }
        public RelayCommand ActionCommand { get; set; }

         

        public bool IsVisible { get; set; }

        

    }
}
