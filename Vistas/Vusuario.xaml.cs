using ProyectoG3.Models;
using System.Net;

namespace ProyectoG3.Vistas;

public partial class Vusuario : ContentPage
{
    private string respuestaServidor;

    private Usuario usuarioConectado;

    public Vusuario()
    {
        InitializeComponent();
        respuestaServidor = AppShell.RespuestaServidor;

        
        //DisplayAlert("Alerta", respuestaServidor, "ok");


    }

   
    public void update()
    {
        try
        {
            WebClient cliente = new WebClient();
            var parametros = new System.Collections.Specialized.NameValueCollection();
            parametros.Add("id", "4");
            parametros.Add("nombre", "anita");
            parametros.Add("contrasena", "contrasena");
            parametros.Add("email", "email");
            cliente.UploadValues("http://192.168.1.35:3300/api/usuario/{}", "POST", parametros);
            DisplayAlert("Alerta", "Cliente Insertado", "Cerrar");

        }
        catch (Exception ex)
        {

            DisplayAlert("Alerta", ex.Message, "Cerrar");
        }

    }

    private void btnActualizar_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new AppShell();

    }
}