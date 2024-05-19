

using Microsoft.Maui.Devices.Sensors;
using Newtonsoft.Json;
using ProyectoG3.Conexion;
using ProyectoG3.Models;
using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Text.Json.Nodes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProyectoG3.Vistas;

public partial class VregistroMultas : ContentPage
{
    private CancellationTokenSource _cancelTokenSource;
    private bool _isCheckingLocation;

    private string ips = IP.Ipconexion();
    

    private List<Tipomulta> _listaTiposMultas;
    private Dictionary<string, int> _diccionarioIds;

    public VregistroMultas()
    {
        InitializeComponent();
        _diccionarioIds = new Dictionary<string, int>();
        CargarDatosTipoMultas();
    }

    

    public void cargarPicker(){
        CargarDatosTipoMultas();
}


    private async void CargarDatosTipoMultas()
    {
        try
        {
            
            HttpClient cliente = new HttpClient();
            string url = $"{ips}/api/tipomulta/";
            string content = await cliente.GetStringAsync(url);
            _listaTiposMultas = JsonConvert.DeserializeObject<List<Tipomulta>>(content);
            pkTiposMultas.Items.Clear();
            _diccionarioIds.Clear();
            foreach (var tipoMulta in _listaTiposMultas)
            {
                pkTiposMultas.Items.Add(tipoMulta.nombre);
                _diccionarioIds.Add(tipoMulta.nombre, tipoMulta.id);
            }
            
        }
        catch (Exception ex)
        {
            // Manejar cualquier excepción que ocurra durante la carga de datos
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private void pickerTiposMultas_SelectedIndexChanged(object sender, EventArgs e)
    {
        var selectedIndex = pkTiposMultas.SelectedIndex;
        if (selectedIndex != -1)
        {
            // Obtener el nombre seleccionado
            var nombreSeleccionado = pkTiposMultas.SelectedItem.ToString();

            // Obtener el ID asociado al nombre seleccionado
            if (_diccionarioIds.ContainsKey(nombreSeleccionado))
            {
                var idSeleccionado = _diccionarioIds[nombreSeleccionado];
                // Hacer algo con el ID seleccionado
                // Por ejemplo, almacenarlo en una variable o utilizarlo en otra parte de la aplicación
            }
        }
    }


    private void btGPS_Clicked(object sender, EventArgs e)
    {
       var gps= GetCurrentLocation();

        CancelRequest();



    }

    private void TomarFoto_Clicked(object sender, EventArgs e)
    {
        TakePhoto();

    }

    private void btGuardar_Clicked(object sender, EventArgs e)
    {
        //CargarDatosTipoMultas();
        insertar();
        limpiar();
    }
    public void insertar()
    {
        try
        {


            WebClient cliente = new WebClient();
            var parametros = new System.Collections.Specialized.NameValueCollection();
            //parametros.Add("id", "anita");
            string url = $"{ips}/api/registromulta";

            parametros.Add("fecha", Pkfecha.Date.ToString("yyyy-MM-dd"));
            var selectedIndex = pkTiposMultas.SelectedIndex;
            if (selectedIndex != -1)
            {

                var nombreSeleccionado = pkTiposMultas.SelectedItem.ToString();
                if (_diccionarioIds.ContainsKey(nombreSeleccionado))
                {
                    var idSeleccionado = _diccionarioIds[nombreSeleccionado];
                    parametros.Add("tipomulta", idSeleccionado.ToString());

                }
            }

            parametros.Add("numeroplaca", txtPlaca.Text);
            parametros.Add("coordenadas", txtGps.Text);

            var source = Foto.Source;
            if (source is FileImageSource fileSource)
            {
                var imagePath = fileSource.File; // Aquí tienes la ruta de la imagen
                parametros.Add("imagen_base64", imagePath);
            }

            parametros.Add("hora", Pkhora.Time.ToString());
            cliente.UploadValues(url, "POST", parametros);
            DisplayAlert("Alerta", "Item Insertado", "Cerrar");


        }
        catch (Exception ex)
        {

            DisplayAlert("Alerta", ex.Message, "Cerrar");
        }

    }



    private void btEliminar_Clicked(object sender, EventArgs e)
    {

        var selectedIndex = pkTiposMultas.SelectedIndex;
        if (selectedIndex != -1)
        {
            var nombreSeleccionado = pkTiposMultas.SelectedItem.ToString();
            if (_diccionarioIds.ContainsKey(nombreSeleccionado))
            {
                var idSeleccionado = _diccionarioIds[nombreSeleccionado];
                DisplayAlert("Alerta", "Item Insertado " + idSeleccionado.ToString(), "Cerrar");
            }
        }

    }


    public async Task GetCurrentLocation()
    {
        try
        {
            _isCheckingLocation = true;

            GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

            _cancelTokenSource = new CancellationTokenSource();

            Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

            if (location != null)
            { 
                string longitud = location.Longitude.ToString();                
                Console.WriteLine("CSV string:      \"{0}\"", location.Latitude.ToString().Replace(',', '.') + ',' + location.Longitude.ToString().Replace(',', '.'));
                txtGps.Text = location.Latitude.ToString().Replace(',', '.') + ',' + location.Longitude.ToString().Replace(',', '.');
                  Console.WriteLine("aqui javi longitud  " + longitud);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            // Unable to get location
        }
        finally
        {
            _isCheckingLocation = false;
        }
    }

    public void CancelRequest()
    {
        if (_isCheckingLocation && _cancelTokenSource != null && _cancelTokenSource.IsCancellationRequested == false)
            _cancelTokenSource.Cancel();
    }

    public async void TakePhoto()
    {
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

            if (photo != null)
            {
                // save the file into local storage
                string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                using Stream sourceStream = await photo.OpenReadAsync();
                using FileStream localFileStream = File.OpenWrite(localFilePath);

                await sourceStream.CopyToAsync(localFileStream);
                var imagen = new Image
                {
                    HeightRequest = 120,
                    VerticalOptions = LayoutOptions.Center
                };

                // Establecer la ruta de la imagen
                Foto.Source = ImageSource.FromFile(localFilePath.ToString());

                //Foto.Source(localFilePath.ToString());
                //await DisplayAlert("Respuesta del servidor", localFilePath, "OK");
            }
        }
    }


    public void limpiar()
    {
        txtNticket.Text = "";
        txtGps.Text = "";
        txtPlaca.Text = "";
        pkTiposMultas.SelectedIndex = -1;
        Foto.Source = ImageSource.FromFile("carro.jpg");



    }

   
}