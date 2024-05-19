using Microsoft.Maui.ApplicationModel.Communication;
using ProyectoG3.Conexion;
using ProyectoG3.Models;
using System.Collections.ObjectModel;
using System.Net;
using System.Text;
using System.Text.Json;

namespace ProyectoG3.Vistas;

public partial class Vregistrousuario : ContentPage
{
    private string ips = IP.Ipconexion();

    public Vregistrousuario()
	{
		InitializeComponent();
	}

    private void btRegistrar_Clicked(object sender, EventArgs e)
    {
        insertar();
        
    }

    public async void insertar()
    {
        string user = txtusuario.Text;
        string password = txtcontasena.Text;
        string mail = txtcorreo.Text;

        try
        {
            // URL base de la API
            string baseUrl = $"{ips}/api/usuario/verificarusuario/datos";

            // Crear instancia de HttpClient
            HttpClient cliente = new HttpClient();

            string parametros = "{\"usuario\":\"manuel\",\"contrasena\":\"dos\"}";

            // Convertir la cadena JSON a un objeto JSON que puedas manipular
            var objetoJson = JsonSerializer.Deserialize<Dictionary<string, string>>(parametros);

            // Cambiar el valor del campo "usuario" con la variable
            objetoJson["usuario"] = user;

            // Convertir el objeto JSON de vuelta a una cadena JSON
            string parametrosActualizados = JsonSerializer.Serialize(objetoJson);



            var contenido = new StringContent(parametrosActualizados, Encoding.UTF8, "application/json");


            // Realizar la solicitud POST
            HttpResponseMessage respuesta = await cliente.PostAsync(baseUrl, contenido);
            //await DisplayAlert("Respuesta del servidor", respuesta.IsSuccessStatusCode.ToString(), "OK");
            if (respuesta.IsSuccessStatusCode)

            {

                // Leer el contenido de la respuesta
                string contenidoRespuesta = await respuesta.Content.ReadAsStringAsync();

                // Mostrar el resultado en un mensaje
                await DisplayAlert("Verificación", "El usuario ya existe", "OK");
                limpiar();
            }
            else
            {
                insertaregistro();
                // Si la solicitud no fue exitosa, mostrar un mensaje de error
                //await DisplayAlert("Error", " Error usuario o Contraceña incorrecta", "OK");
            }
        }
        catch (Exception ex)
        {
            // Manejar cualquier excepción que ocurra durante la solicitud
            await DisplayAlert("Error", ex.Message, "OK");
        }

        

        
        
    }

    

    public void insertaregistro() {

        string user = txtusuario.Text;
        string password = txtcontasena.Text;
        string mail = txtcorreo.Text;
        try
        {
            WebClient cliente = new WebClient();
            var parametros = new System.Collections.Specialized.NameValueCollection();
            parametros.Add("usuario", user);
            parametros.Add("contrasena", password);
            parametros.Add("email", mail);

            // Verificar si el usuario ya existe antes de insertarlo
            string url = $"{ips}/api/usuario";
            var respuesta = cliente.UploadValues(url, "POST", parametros);
            string respuestaString = System.Text.Encoding.UTF8.GetString(respuesta);

            DisplayAlert("Alerta", "Usuario Insertado", "Cerrar");

            limpiar();            
            Application.Current.MainPage = new Vistas.Vlogin();

        }
        catch (Exception ex)
        {

            DisplayAlert("Alerta", ex.Message, "Cerrar");
        }

    }

 

   
    private void Button_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new Vistas.Vlogin();
    }

    public void limpiar() {
        txtusuario.Text ="";
        txtcontasena.Text="";
        txtcorreo.Text= ""  ;

    }
}