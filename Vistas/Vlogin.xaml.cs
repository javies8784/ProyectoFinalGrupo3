using Microsoft.Maui;
using ProyectoG3.Conexion;
using System.Text;

namespace ProyectoG3.Vistas;

public partial class Vlogin : ContentPage
{
    private string ips = IP.Ipconexion();
    public Vlogin()
	{
		InitializeComponent();
	}

    private void btIngresar_Clicked(object sender, EventArgs e)
    {
        //Navigation.PushAsync(new Vistas.Vprincipal());        
        //Application.Current.MainPage = new AppShell();
        Ingresar();




    }

    public async void Ingresar()
    {
        string user = txtUsario.Text;
        string password = txtContrsena.Text;

        try
        {
            // URL base de la API
            string baseUrl = $"{ips}/api/usuario/verificarusuario/datos";

            // Crear instancia de HttpClient
            HttpClient cliente = new HttpClient();
            string parametros = "{\"usuario\":\"" + user + "\",\"contrasena\":\"" + password + "\"}";
            var contenido = new StringContent(parametros, Encoding.UTF8, "application/json");


            // Realizar la solicitud POST
            HttpResponseMessage respuesta = await cliente.PostAsync(baseUrl, contenido);

            if (respuesta.IsSuccessStatusCode)

            {

                
                string contenidoRespuesta = await respuesta.Content.ReadAsStringAsync();
                Application.Current.MainPage = new AppShell(contenidoRespuesta);
                await DisplayAlert("Message", "Bienvenido "+ user, "OK");


            }
            else
            {
                
                await DisplayAlert("Error", " Usuario o Contraseña Incorrecta", "OK");
            }
        }
        catch (Exception ex)
        {
            // Manejar cualquier excepción que ocurra durante la solicitud
            await DisplayAlert("Error", ex.Message, "OK");
        }

    }

    private void btRegistrar_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new Vistas.Vregistrousuario();

    }
}