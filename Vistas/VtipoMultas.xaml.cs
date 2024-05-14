using System.Net;

namespace ProyectoG3.Vistas;

public partial class VtipoMultas : ContentPage
{
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
            parametros.Add("nombre", txtNnombre.Text);
            parametros.Add("obsarvacion", txtObservacion.Text);
            cliente.UploadValues("http://192.168.1.12:3300/api/tipomulta/", "POST", parametros);
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