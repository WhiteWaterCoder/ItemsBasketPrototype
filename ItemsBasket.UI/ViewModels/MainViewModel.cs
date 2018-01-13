namespace ItemsBasket.UI.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private string _logInOutTitle;

        public string LogInOutTitle
        {
            get { return _logInOutTitle; }
            set { SetField(ref _logInOutTitle, value); }
        }
    }
}