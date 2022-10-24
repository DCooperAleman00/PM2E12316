using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM2E12316
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaFotografias : ContentPage
    {
        public ListaFotografias()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            ListaFotos.ItemsSource = await App.BaseDatos.ListaFotos();
        }

        private async void ListaFotos_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            String sexResult = await DisplayActionSheet("Que desea hacer?", "Cancelar", null, "Ver Fotografia", "Ver ubicacion");

            if (sexResult == "Ver Fotografia")
            {
                Models.Fotografia item = (Models.Fotografia)e.Item;
                var newpage = new vista();
                newpage.BindingContext = item;
                await Navigation.PushAsync(newpage);
            }

            if (sexResult == "Ver ubicacion")
            {
                   Models.Fotografia item = (Models.Fotografia)e.Item;
                   var newpage = new mapa();
                   newpage.BindingContext = item;
                   await Navigation.PushAsync(newpage);
            }
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}