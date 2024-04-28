using ProyectoG3.Vistas;

namespace ProyectoG3
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var navigationPage = new NavigationPage(new Vlogin()); //AppShell();
            //MainPage = new AppShell();
            navigationPage.Background = Colors.White;
            navigationPage.BarTextColor= Colors.White;            
            MainPage = navigationPage;
        }
    }
}
