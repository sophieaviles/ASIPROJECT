using System.Windows.Controls;
using System.Windows.Input;

namespace SigiFluent.ViewModels
{
    public class LoginVM : BaseViewModel
    {
        

        public LoginVM()
        {
            ShowLoginError = false;
        }

        public string ErrorMsj { get; set; }

        public bool IsLoggedIn
        {
            get { return isLogged; }
            set
            {
                //isLogged = BackOfficeHelper.Login(UserName, ApiSecret);
                RaisePropertyChanged("IsLoggedIn");
            }
        }

        public bool ShowLoginError
        {
            get { return showLoginError; }
            set
            {
                showLoginError = value;
                RaisePropertyChanged("ShowLoginError");
            } 
        }

        public string UserName
        {
            get { return user; }
            set 
            {
                user = value;
                RaisePropertyChanged("UserName");
            }
        }

        public string ApiSecret 
        { 
            get { return apiSecret; }
            set
            {
                apiSecret = value;
                RaisePropertyChanged("ApiSecret");
            }
        }

        public ICommand LoginCommand { get { return new RelayCommand<PasswordBox>(  CheckLogin,Canlogin ); } }

        
        private bool Canlogin(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            return !string.IsNullOrEmpty(UserName) &&
                   !string.IsNullOrEmpty(passwordBox.Password);
        }


        private void CheckLogin(object parameter)
        {
            //control de valores 
            var passwordBox = parameter as PasswordBox;
            apiSecret = passwordBox.Password;
            //IsLoggedIn = BackOfficeHelper.Login(user, apiSecret);
            ShowLoginError = false;

            if (IsLoggedIn)
            {   
                //LoginHelper.SaveLogin(UserName, ApiSecret);

                //Helpers.ControlAssociated.windowControl.Logged();
                //Helpers.ControlAssociated.listControls.First(c => c.Key == Helpers.ControlType.Login).Value.Close();
                
            }
            else
            {
              ShowLoginError = true;
            }
        }


      

        private bool showLoginError { get; set; }
        private bool isLogged { get; set; }
        private string user { get; set; }
        private string apiSecret { get; set; }
       
        

    }
}
