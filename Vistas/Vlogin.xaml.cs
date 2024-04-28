using Microsoft.Maui;

namespace ProyectoG3.Vistas;

public partial class Vlogin : ContentPage
{
	public Vlogin()
	{
		InitializeComponent();
	}

    private void btIngresar_Clicked(object sender, EventArgs e)
    {
        //Navigation.PushAsync(new Vistas.Vprincipal());        
        Application.Current.MainPage = new AppShell();
        


    }

    private void btRegistrar_Clicked(object sender, EventArgs e)
    {

    }
}