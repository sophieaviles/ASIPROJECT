using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StockAdminContext;

namespace SigiFluent
{
    public class ToolMenuItem
    {
        public ToolMenuItem(string actionName, string groupName, RelayCommand relayCommand, ToolGroupRegion region,bool isChecked = false)
        {
            this.ActionName = actionName;
            this.GroupName = groupName;
            this.ActionCommand = relayCommand;
            IsVisible = true;
            this.IsChecked = isChecked;
            this.Region = region;
        }

        public string ActionName { get; set; }
        public string GroupName { get; set; }
        public RelayCommand ActionCommand { get; set; }

        public string Group { get; set; }

        public bool IsVisible { get; set; }

        public bool IsChecked { get; set; }

        public ToolGroupRegion Region { get; set; }
    }
    public enum ToolGroupRegion
    {
        Supply = 0,
        Purchases = 1,
        Sales = 2,
        Inventory = 3,
        EndOfDay = 4,
        CheckBook = 5,
        Reports = 6,
    }
   
}
