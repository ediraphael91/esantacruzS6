using esantacruzS6.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;


namespace esantacruzS6.Views;

public partial class vEstudiante : ContentPage
{
    private const string Url = "http://192.168.100.8/semana6/estudiantews.php";
    private readonly HttpClient cliente = new HttpClient();
    private ObservableCollection<Models.Estudiante> est;
    public vEstudiante()
    {
        InitializeComponent();
        //consumir el metodo mostrar
        mostrar();

        MessagingCenter.Subscribe<ActEliminar>(this, "ActualizarLista", sender =>
        {
            mostrar();
        });

    }

    public async void mostrar()
    {
        var content = await cliente.GetStringAsync(Url);
        List<Models.Estudiante> mostrar = JsonConvert.DeserializeObject<List<Models.Estudiante>>(content);
        est = new ObservableCollection<Models.Estudiante>(mostrar);
        listaEstudiantes.ItemsSource = est;
    }

    private void btnEstudiante_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Views.vAgregar());
    }

    private void listaEstudiantes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var objEstudiante = (Estudiante)e.SelectedItem;
        Navigation.PushAsync(new ActEliminar(objEstudiante));

    }
}