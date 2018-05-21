using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using SigiApi.Model;
using SigiFluent.Commands;

namespace SigiFluent.ViewModels
{
    public class VMAdminOptions: BaseModel
    {
        #region COMMAND


        public ICommand CmdSaveBusinessPartner
        {
            get { return new RelayCommand(SaveTemplate); }
        }

        private void SaveTemplate(object obj)
        {
            ContextFactory.GetDBContext().SaveChanges();
        }

        #endregion COMMAND
    }
}
