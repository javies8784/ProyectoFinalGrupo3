using Newtonsoft.Json;
using ProyectoG3.Conexion;
using ProyectoG3.Models;
using System.Collections.ObjectModel;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProyectoG3.Vistas;

public partial class VmultasEditElim : ContentPage
{
    private CancellationTokenSource _cancelTokenSource;
    private bool _isCheckingLocation;

    private string ips = IP.Ipconexion();


    private List<Tipomulta> _listaTiposMultas;
    private Dictionary<string, int> _diccionarioIds;

    private int tipomultas; 

    public VmultasEditElim()
	{
		InitializeComponent();
         _diccionarioIds = new Dictionary<string, int>();
        CargarDatosTipoMultas();
	}
    public VmultasEditElim(RegistroMultas datos)
    {
        InitializeComponent();
        //////////////////  datos del registro  /////////////
        txtNticket.Text = datos.id.ToString();
        txtGps.Text = datos.coordenadas;        
        Pkfecha.Date = DateTime.Parse(datos.fecha.Date.ToString("dd/MM/yyyy"));        
        TimeSpan time = TimeSpan.Parse(datos.hora);
        Pkhora.Time = time;
        tipomultas = datos.tipomulta;
        txtPlaca.Text = datos.numeroplaca;

        // Establecer la ruta de la imagen
        Foto.Source = ImageSource.FromFile(datos.imagen_base64.ToString());



        _diccionarioIds = new Dictionary<string, int>();
        CargarDatosTipoMultas();

        // Buscar el índice del elemento con el id deseado
        //DisplayAlert("Alerta",datos.tipomulta.ToString(),"ok");
       
        


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

            // Buscar el índice del elemento con el id deseado
            if (tipomultas != 0)
            {
                int indexToSelect = -1;
                for (int i = 0; i < _listaTiposMultas.Count; i++)
                {
                    if (_listaTiposMultas[i].id == tipomultas)
                    {
                        indexToSelect = i;
                        break;
                    }
                }

                // Seleccionar el ítem deseado si se encontró su índice
                if (indexToSelect != -1)
                {
                    pkTiposMultas.SelectedIndex = indexToSelect;
                }
            }

        }
        catch (Exception ex)
        {
            // Manejar cualquier excepción que ocurra durante la carga de datos
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }


    private void btEliminar_Clicked(object sender, EventArgs e)
    {

        try
        {
            WebClient cliente = new WebClient();
            string url = $"{ips}/api/registromulta/{txtNticket.Text}";
            var parametros = new System.Collections.Specialized.NameValueCollection();            
            parametros.Add("id", txtNticket.Text);
            cliente.UploadValues(url, "DELETE", parametros);
            DisplayAlert("Alerta", "Item Eliminado", "Cerrar");
            OnGoToVregistroClicked();

        }
        catch (Exception ex)
        {

            DisplayAlert("Alerta", ex.Message, "Cerrar");
        }

    }

    private void btActualizar_Clicked(object sender, EventArgs e)
    {
        //Navigation.PushAsync(new Vistas.Vregistro());
        //Application.Current.MainPage = new AppShell();
        //
        update();
        OnGoToVregistroClicked();

    }
    private async void OnGoToVregistroClicked()
    {
        //await Shell.Current.GoToAsync("//Vregistro");
        var appShell = new AppShell();
        Application.Current.MainPage = appShell;

        // Navegar a la página Vregistro
        await appShell.GoToAsync("//Vregistro");
    }

    private void btGPS_Clicked(object sender, EventArgs e)
    {
        var gps = GetCurrentLocation();

        CancelRequest();

    }

    private void TomarFoto_Clicked(object sender, EventArgs e)
    {
        TakePhoto();

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

    public void update()
    {
        try
        {
            WebClient cliente = new WebClient();
            string url = $"{ips}/api/registromulta/{txtNticket.Text}";
            var parametros = new System.Collections.Specialized.NameValueCollection();
            
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
            parametros.Add("id",txtNticket.Text);

            cliente.UploadValues(url, "PUT", parametros);
            DisplayAlert("Alerta", "Item Actualizado", "Cerrar");

        }
        catch (Exception ex)
        {

            DisplayAlert("Alerta", ex.Message, "Cerrar");
        }

    }
}