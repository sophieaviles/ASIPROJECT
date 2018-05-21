using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using StockAdminContext.Helpers;
using StockAdminContext.Models;

namespace SigiFluent.ViewModels
{
    public class MembershipViewModel : BaseViewModel
    {
        public event Action<bool> OnSelect;
        public event Action OnClose;

        #region METODOS PARA VALIDADOR DE PRECIOS
        public ICommand CheckUserCommand
        {
            get
            {
                return new RelayCommand<PasswordBox>(UserIsValid);
            }
        }

        public ObservableCollection<LocalUser> PriceValidationUsers
        {
            get { return StockAdminContext.Helpers.Membership.GetPriceValidationUsers(); }

        }

        public LocalUser PriceValidatorUser
        {
            get { return _pricevalidatoruser; }
            set
            {
                _pricevalidatoruser = value;
                RaisePropertyChanged("PriceValidatorUser");
            }
        }

        public override void OnCloseModal()
        {
            if (OnClose != null) OnClose();
        }

        private void UserIsValid(PasswordBox parameter)
        {
            var passwordBox = parameter;
            var psw = passwordBox.Password;

            if (Membership.ValidateUser(PriceValidatorUser.NickName, psw))
            {

                if (OnSelect != null) OnSelect(true);
                IsModalVisible = false;
                
            }
            else
            {
                ErrorMessage = "Error en autenticación de usario, por favor verifique la clave del usuario.";
            }
        }

        private LocalUser _pricevalidatoruser;
        #endregion METODOS PARA VALIDADOR DE PRECIOS

    }
}
