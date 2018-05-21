using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using StockAdminContext;
using SigiFluent.ViewModels;

namespace SigiFluent.Helpers
{
    public static class NotificationsManager
    {

        public static ObservableCollection<NotificationItemMenu> GetNotifications()
        {
            var notifications = new List<NotificationItemMenu>();
            var endOfDay = StoredCallbackProcessor.CallDataSet("SP_Notifications");
            if (endOfDay != null)
            {
                foreach (DataTable table in endOfDay.Tables)
                {
                    notifications.AddRange(from DataRow row in table.Rows select new NotificationItemMenu(row.ItemArray[0].ToString(), row.ItemArray[1].ToString(), new RelayCommand(() => { })));
                }

            } 

            return new ObservableCollection<NotificationItemMenu>(notifications);
        }

        public static ObservableCollection<NotificationItemMenu> GetEndOfDayNotifications()
        {
            var notifications = new List<NotificationItemMenu>();

            var endOfDay = StoredCallbackProcessor.CallDataSet("SP_ValidateDay");
            if (endOfDay != null)
            {
                foreach (DataTable table in endOfDay.Tables)
                {
                    notifications.AddRange(from DataRow row in table.Rows select new NotificationItemMenu(row.ItemArray[2].ToString(), row.ItemArray[1].ToString(), new RelayCommand(() => { })));
                }
                   
            }
          
            return new ObservableCollection<NotificationItemMenu>(notifications);
        } 
    }
}
