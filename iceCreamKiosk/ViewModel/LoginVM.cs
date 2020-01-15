using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using iceCreamKiosk.model;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace iceCreamKiosk.ViewModel
{
    class LoginVM : ViewModelBase
    {
        private string userName;

        public string UserName { get { return userName; } set { Set(ref userName, value); } }
        public SecureString SecurePassword { private get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public SnackbarMessageQueue SnackbarMessageQueue { get; set; } = new SnackbarMessageQueue();

        public LoginVM()
        {
            LoginCommand = new RelayCommand<object>(ExecuteLoginCommand);
            CloseCommand = new RelayCommand(()=>MessengerInstance.Send<bool>(true));
        }

        private bool CanExecuteLoginCommand(object arg)
        {
            var passwordBox = arg as PasswordBox;

            using (SecureString password = passwordBox.SecurePassword)
            {
                return !(string.IsNullOrEmpty(UserName) 
                    || string.IsNullOrEmpty(password.ToString()));

            }
        }

        void ExecuteLoginCommand(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            string password = passwordBox.Password;

            
                if (string.IsNullOrEmpty(UserName))
                {
                    SnackbarMessageQueue.Enqueue("Username is Requierd");
                }
                else if (string.IsNullOrEmpty(password))
                {
                    SnackbarMessageQueue.Enqueue("Password is Requierd");
                }
                else if (UserName == "Avremi" && password == "1234")
                {
                    MessengerInstance.Send<ViewModelBase>(new StoresCollectionForAdminVM());
                    MessengerInstance.Send<bool>(true);
                }
                else
                {
                    SnackbarMessageQueue.Enqueue("Incorrect Username or Password");
                }
            

        }
    }
}
