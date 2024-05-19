
namespace ProyectoG3.Vistas;

public partial class Vprincipal : ContentPage
{
   
    public Vprincipal()
	{
		InitializeComponent();

	}
    public Vprincipal(string usuario)
    {
        InitializeComponent();
        lblUsuario.Text = usuario;
        DisplayAlert("datos", usuario, "ok");
    }

    private void btMenu_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Vistas.VregistroMultas());

    }

    private void MtipoMultas_Clicked(object sender, EventArgs e)
    {

    }

    private void listarMultas_Clicked(object sender, EventArgs e)
    {

    }

    private void RegistrarMultas_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Vistas.VregistroMultas());
    }

    private async void btnSalir_Clicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Salir", "¿Estás seguro de que deseas salir?", "Sí", "No");
        if (answer)
        {

            Application.Current.MainPage = new Vistas.Vlogin();

        }
    }
}