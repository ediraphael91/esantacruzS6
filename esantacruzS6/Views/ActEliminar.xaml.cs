using esantacruzS6.Models;

namespace esantacruzS6.Views;

public partial class ActEliminar : ContentPage
{
	public ActEliminar(Estudiante datos)
	{
		InitializeComponent();
        txtCodigo.Text = datos.codigo.ToString();
        txtNombre.Text = datos.nombre;
        txtApellido.Text = datos.apellido;
        txtEdad.Text = datos.edad.ToString();
    }

    private void btnActualizar_Clicked(object sender, EventArgs e)
    {
		
    }

    private void btnEliminar_Clicked(object sender, EventArgs e)
    {

    }
}