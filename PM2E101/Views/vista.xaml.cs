using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PM2E12316.Models;

namespace PM2E12316
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class vista : ContentPage
    {
        public vista()
        {
            InitializeComponent();
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            var emple = new Fotografia
            {
                codigo = Convert.ToInt32(lblCodigo.Text)
            };
            var resultado = await App.BaseDatos.BorrarFoto(emple);
            if (resultado != 0)
            {
                await DisplayAlert("Warning", "Foto Eliminada", "Aceptar");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Warning", "Error!", "Aceptar");
            }

        }
    }
}