
namespace ProyectoG3.Vistas;

public partial class Vprincipal : ContentPage
{
   
    public Vprincipal()
	{
		InitializeComponent();
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
}