using Newtonsoft.Json;
using ProyectoG3.Conexion;
using ProyectoG3.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Text.Json.Nodes;

namespace ProyectoG3.Vistas;

public partial class Vregistro : ContentPage
{
    
    private string ips = IP.Ipconexion();
    //private const string url = $"http://{ip}:3300/api/registromulta";
    private readonly HttpClient cliente = new HttpClient();
    private ObservableCollection<RegistroMultas> usr;

    public Vregistro()
	{
		InitializeComponent();
        listardatos();


    }
  

    public void listardatos() {
        ObtenerDatos();
    }

    public async void ObtenerDatos()
    {
        
        string url = $"{ips}/api/registromulta";

        var content = await cliente.GetStringAsync(url);
        
        List<RegistroMultas> mostrar1 = JsonConvert.DeserializeObject<List<RegistroMultas>>(content);
        usr = new ObservableCollection<RegistroMultas>(mostrar1);
        
        //foreach (var registromultas in mostrar1)
       // {
         //   await DisplayAlert("Respuesta del servidor", registromultas.coordenadas, "OK");
            //DisplayAlert("Alerta", "Cliente Insertado"+ tipoMulta, "Cerrar");
            //pkTiposMultas.Items.Add(tipoMulta.nombre);
            //_diccionarioIds.Add(tipoMulta.nombre, tipoMulta.id);
        //}


        listRegistroMultas.ItemsSource = usr;

    }

    public void insertar() {
        try
        {
            WebClient cliente = new WebClient();
            var  parametros = new System.Collections.Specialized.NameValueCollection();
            parametros.Add("usuario", "anita");
            parametros.Add("contrasena", "contrasena");
            parametros.Add("email", "email");
            cliente.UploadValues($"{ips}/api/usuario","POST", parametros);
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
            parametros.Add("fecha","" );
            parametros.Add("tipomulta", "anita");
            parametros.Add("numeroplaca", "contrasena");
            parametros.Add("coordenadas", "email");
            parametros.Add("imagen_base64", "email");
            parametros.Add("hora", "email");
            parametros.Add("id", "email");
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
            string baseUrl = "http://192.168.1.24:3300/api/usuario/verificarusuario/datos";

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
        //insertar();
        listRegistroMultas.ItemsSource = null;
        listRegistroMultas.SelectedItem = null;

        listardatos();

    }

    private async void btActualizar_Clicked(object sender, EventArgs e)
    {
        //buscar();
        //VerificaUsuario();
        //VerificaUsuario2();
        try
        {
            // URL base de la API
            string baseUrl = "http://192.168.1.24:3300/api/usuario/verificarusuario/datos";

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

    private void listRegistroMultas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {

        var objRegistroMultas = (RegistroMultas)e.SelectedItem;
        Navigation.PushAsync(new Vistas.VmultasEditElim(objRegistroMultas));

    }
}