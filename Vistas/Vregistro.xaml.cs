using Newtonsoft.Json;
using ProyectoG3.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Text.Json.Nodes;

namespace ProyectoG3.Vistas;

public partial class Vregistro : ContentPage
{
    private const string url = "http://192.168.1.12:3300/api/usuario";
    private readonly HttpClient cliente = new HttpClient();
    private ObservableCollection<Usuario> usr;

    public Vregistro()
	{
		InitializeComponent();
        ObtenerDatos();

    }

    public async void ObtenerDatos()
    {
        
        var content = await cliente.GetStringAsync(url);
        
        List<Usuario> mostrar = JsonConvert.DeserializeObject<List<Usuario>>(content);
        usr = new ObservableCollection<Usuario>(mostrar);


        usr.Add(new Usuario() { id = 1, nombre = "NOMBRE",contrasena="APELLIDO",email="EDAD" });


        //groupedAnimal.Add(dogs);
        //groupedAnimal.Add(cats);

        listEstudiantes.ItemsSource = usr;

    }

    public void insertar() {
        try
        {
            WebClient cliente = new WebClient();
            var  parametros = new System.Collections.Specialized.NameValueCollection();
            parametros.Add("usuario", "anita");
            parametros.Add("contrasena", "contrasena");
            parametros.Add("email", "email");
            cliente.UploadValues("http://192.168.1.12:3300/api/usuario","POST", parametros);
            DisplayAlert("Alerta", "Cliente Insertado", "Cerrar");

        }
        catch (Exception ex)
        {

            DisplayAlert("Alerta",ex.Message,"Cerrar");
        }

    }

    public void update()
    {
        try
        {
            WebClient cliente = new WebClient();
            var parametros = new System.Collections.Specialized.NameValueCollection();
            parametros.Add("id","4");
            parametros.Add("usuario", "anita");
            parametros.Add("contrasena", "contrasena");
            parametros.Add("email", "email");
            cliente.UploadValues("http://192.168.1.12:3300/api/usuario/4", "POST", parametros);
            DisplayAlert("Alerta", "Cliente Insertado", "Cerrar");

        }
        catch (Exception ex)
        {

            DisplayAlert("Alerta", ex.Message, "Cerrar");
        }

    }

    public void buscar()
    {
        try
        {
            WebClient cliente = new WebClient();
            var parametros = new System.Collections.Specialized.NameValueCollection();
            //parametros.Add("id", "4");
            parametros.Add("usuario", "javier");
            parametros.Add("contrasena", "javier");
            
            var respuesta = cliente.UploadValues("http://localhost:3300/api/usuario/verificarusuario/datos", "POST", parametros);
            DisplayAlert("Alerta", "Cliente confiram" + respuesta.ToString(), "Cerrar");

        }
        catch (Exception ex)
        {

            DisplayAlert("Alerta", ex.Message, "Cerrar");
        }



    }


    public async void VerificaUsuario2()
    {

        try
        {
            // URL base de la API
            string baseUrl = "http://192.168.1.12:3300/api/usuario/verificarusuario/datos";

            // Crear instancia de HttpClient
            HttpClient cliente = new HttpClient();
           string parametros = "{\"usuario\":\"javier\",\"contrasena\":\"javier\"}";            
            var contenido = new StringContent(parametros, Encoding.UTF8, "application/json");

            // Realizar la solicitud POST
            HttpResponseMessage respuesta = await cliente.PostAsync(baseUrl, contenido);

            // Verificar si la solicitud fue exitosa
            if (respuesta.IsSuccessStatusCode)
            {
                // Leer el contenido de la respuesta
                string contenidoRespuesta = await respuesta.Content.ReadAsStringAsync();

                // Mostrar el resultado en un mensaje
                await DisplayAlert("Respuesta del servidor", contenidoRespuesta, "OK");
            }
            else
            {
                // Si la solicitud no fue exitosa, mostrar un mensaje de error
                await DisplayAlert("Error", "La solicitud no fue exitosa", "OK");
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
        insertar();

    }

    private async void btActualizar_Clicked(object sender, EventArgs e)
    {
        //buscar();
        //VerificaUsuario();
        //VerificaUsuario2();
        try
        {
            // URL base de la API
            string baseUrl = "http://192.168.1.12:3300/api/usuario/verificarusuario/datos";

            // Crear instancia de HttpClient
            HttpClient cliente = new HttpClient();
            string parametros = "{\"usuario\":\"javier\",\"contrasena\":\"javier\"}";
            var contenido = new StringContent(parametros, Encoding.UTF8, "application/json");

            // Realizar la solicitud POST
            HttpResponseMessage respuesta = await cliente.PostAsync(baseUrl, contenido);

            // Verificar si la solicitud fue exitosa
            if (respuesta.IsSuccessStatusCode)
            {
                // Leer el contenido de la respuesta
                string contenidoRespuesta = await respuesta.Content.ReadAsStringAsync();

                // Mostrar el resultado en un mensaje
                await DisplayAlert("Respuesta del servidor", contenidoRespuesta, "OK");
            }
            else
            {
                // Si la solicitud no fue exitosa, mostrar un mensaje de error
                await DisplayAlert("Error", "La solicitud no fue exitosa", "OK");
            }
        }
        catch (Exception ex)
        {
            // Manejar cualquier excepción que ocurra durante la solicitud
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}