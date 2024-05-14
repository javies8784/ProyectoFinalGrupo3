

using Microsoft.Maui.Devices.Sensors;
using System;

namespace ProyectoG3.Vistas;

public partial class VregistroMultas : ContentPage
{
    private CancellationTokenSource _cancelTokenSource;
    private bool _isCheckingLocation;

   


	public VregistroMultas()
	{
		InitializeComponent();
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

    }

    private void btEliminar_Clicked(object sender, EventArgs e)
    {

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
               // latitud = location.Latitude.ToString();
                string longitud = location.Longitude.ToString();
                
                Console.WriteLine("CSV string:      \"{0}\"", location.Latitude.ToString().Replace(',', '.') + ',' + location.Longitude.ToString().Replace(',', '.'));

                txtGps.Text = location.Latitude.ToString().Replace(',', '.') + ',' + location.Longitude.ToString().Replace(',', '.');

                //    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                //     Console.WriteLine("aqui javi "+ location.Latitude.ToString()+" lat "+ latitud);
                  Console.WriteLine("aqui javi longitud  " + longitud);
            }
        }
        // Catch one of the following exceptions:
        //   FeatureNotSupportedException
        //   FeatureNotEnabledException
        //   PermissionException
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
            }
        }
    }

    private void btGuardar_Clicked_1(object sender, EventArgs e)
    {

    }
}