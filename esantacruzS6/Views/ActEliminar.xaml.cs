using esantacruzS6.Models;

namespace esantacruzS6.Views;

public partial class ActEliminar : ContentPage
{
    private const string Url = "http://192.168.100.8/semana6/estudiantews.php";
    private readonly HttpClient client = new HttpClient();

    public ActEliminar(Estudiante datos)
    {
        InitializeComponent();
        txtCodigo.Text = datos.codigo.ToString();
        txtNombre.Text = datos.nombre;
        txtApellido.Text = datos.apellido;
        txtEdad.Text = datos.edad.ToString();
    }

    private async void btnActualizar_Clicked(object sender, EventArgs e)
    {
        try
        {
            int codigo = int.Parse(txtCodigo.Text);

            var datosEstudiante = new Dictionary<string, string>
         {
             {"codigo", codigo.ToString()},
             {"nombre", txtNombre.Text},
             {"apellido", txtApellido.Text},
             {"edad",  txtEdad.Text}
         };


            var query = new FormUrlEncodedContent(datosEstudiante);
            string url = "http://192.168.100.8/semana6/estudiantews.php?" + await query.ReadAsStringAsync();

            HttpClient client = new HttpClient();

            var response = await client.PutAsync(url, null);

            // Verificar la respuesta
            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Éxito", "Datos actualizados correctamente", "OK");
                await Navigation.PushAsync(new vEstudiante());
            }
            else
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                await DisplayAlert("Error", $"No se pudo actualizar los datos: {responseBody}", "OK");
            }


        }
        catch (Exception ex)
        {
            await DisplayAlert("Alerta", ex.Message, "OK");
        }
    }


    private void btnEliminar_Clicked(object sender, EventArgs e)
    {
        try
        {
            var codigo = txtCodigo.Text;

            var response = client.DeleteAsync($"{Url}?codigo={codigo}").Result;
            var responseString = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode)
            {
                DisplayAlert("Éxito", "Estudiante eliminado correctamente", "OK");
                MessagingCenter.Send(this, "ActualizarLista"); // Envía un mensaje para actualizar la lista
                Navigation.PopAsync();
            }
            else
            {
                DisplayAlert("Error", $"No se pudo eliminar el estudiante: {responseString}", "OK");
            }
        }
        catch (Exception ex)
        {
            DisplayAlert("Alerta", ex.Message, "OK");
        }
    }
}

