using System.Windows.Input;

namespace ItemsBasket.UI.ViewModels
{
    public class LogInOutViewModel : BaseViewModel
    {
        private bool _isLoggedIn;
        private string _userName;
        private string _password;

        private ICommand _loginCommand;

        public bool IsLoggedIn
        {
            get { return _isLoggedIn; }
            set { SetField(ref _isLoggedIn, value); }
        }

        public string UserName
        {
            get { return _userName; }
            set { SetField(ref _userName, value); }
        }

        public string Password
        {
            get { return _password; }
            set { SetField(ref _password, value); }
        }

        public ICommand LoginCommand
        {
            get { return _loginCommand; }
            set { SetField(ref _loginCommand, value); }
        }

        public LogInOutViewModel()
        {
            LoginCommand = new RelayCommand(p => true, p => { });
        }
    }
}