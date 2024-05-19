using ProyectoG3.Conexion;
using System.Net;

namespace ProyectoG3.Vistas;

public partial class VtipoMultas : ContentPage
{
    private string ips = IP.Ipconexion();
    public VtipoMultas()
	{
		InitializeComponent();
	}

    private void btGuardar_Clicked(object sender, EventArgs e)
    {
        insertar();
    }

    public void insertar()
    {
        try
        {
            WebClient cliente = new WebClient();
            var parametros = new System.Collections.Specialized.NameValueCollection();
            //parametros.Add("id", "anita");
            string url = $"{ips}/api/tipomulta";
            parametros.Add("nombre", txtNnombre.Text);
            parametros.Add("obsarvacion", txtObservacion.Text);
            cliente.UploadValues(url, "POST", parametros);
            DisplayAlert("Alerta", "Item Insertado", "Cerrar");
            limpiar();

        }
        catch (Exception ex)
        {

            DisplayAlert("Alerta", ex.Message, "Cerrar");
        }

    }
    public void limpiar() {
        txtNnombre.Text = "";
        txtObservacion.Text = "";
    }
}