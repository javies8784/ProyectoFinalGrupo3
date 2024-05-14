
using ProyectoG3.Vistas;

namespace ProyectoG3
{
    public partial class AppShell : Shell
    {
        public Shell ShellInstance { get; private set; }

        public AppShell()
        {
            InitializeComponent();
            //Routing.RegisterRoute("monkeydetails", typeof(Vregistro));
            ShellInstance = this;

        }

        private async void OnGoToVregistroClicked()
        {
            var appShell = new AppShell();
            Application.Current.MainPage = appShell;

            // Navegar a la página Vregistro
            await appShell.ShellInstance.GoToAsync("//Vregistro");
        }

    }
}
