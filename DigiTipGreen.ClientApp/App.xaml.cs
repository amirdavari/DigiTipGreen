using DigiTipGreen.ClientApp.Views;

namespace DigiTipGreen.ClientApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new LoginView();
        }
    }
}
