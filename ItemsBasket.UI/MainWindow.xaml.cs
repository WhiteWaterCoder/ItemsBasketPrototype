using FirstFloor.ModernUI.Windows.Controls;

namespace ItemsBasket.UI
{
    public partial class MainWindow : ModernWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            var a = SillyServiceLocator.LogInOutViewModel;
        }
    }
}