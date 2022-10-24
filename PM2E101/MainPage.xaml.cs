using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


using Plugin.Media;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Plugin.Geolocator;
using PM2E12316.Models;
using PM2E12316;

namespace PM2E12316
{
    public partial class MainPage : ContentPage
    {

        string ruta="";
        public MainPage()
        {
            InitializeComponent();
            InizializatePlugins();
        }

        private async void btnFoto_Clicked(object sender, EventArgs e)
        {
            Opcion();

        }
        private void btnAgregar_Clicked(object sender, EventArgs e)
        {
            guardar();
        }


        private async void InizializatePlugins()
        {

            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                     txtLatitud.Text = location.Latitude.ToString();
                    txtLongitud.Text = location.Longitude.ToString();
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
             }
            catch (FeatureNotEnabledException fneEx)
            {
             }
            catch (PermissionException pEx)
            {
             }
            catch (Exception ex)
            {
 
            }
        }

        private async void tomar()
        {
            var takepic = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "PhotoApp",
                Name = "TEST.jpg"
            });
            ruta = takepic.Path;


            if (takepic != null)
            {
                foto.Source = ImageSource.FromStream(() => { return takepic.GetStream(); });

            }

            var Sharephoto = takepic.Path;
            await Share.RequestAsync(new ShareFileRequest
            {
                Title = "Foto",
                File = new ShareFile(Sharephoto)
            });
        }
        private async void GaleriaFoto()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("No soportado", "Error de permisos", "Aceptar");
                return;

            }

            var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,

            });


            ruta = file.Path;

            if (file != null)
            {
                foto.Source = ImageSource.FromStream(() => { return file.GetStream(); });
                return;
            }

        }

        private async void Opcion()
        {
            String sexResult = await DisplayActionSheet("Seleccione una opción ", "Cancelar", null, "Tomar Foto", "Abrir Galeria");

            if (sexResult == "Tomar Foto")
            {
                tomar();
            }

            if (sexResult == "Abrir Galeria")
            {
                GaleriaFoto();
            }

        }

        private async void guardar (){
           
            if ( ruta==""  || String.IsNullOrWhiteSpace(txtDescripcion.Text) || String.IsNullOrWhiteSpace(txtLatitud.Text) || String.IsNullOrWhiteSpace(txtLongitud.Text )) { 
                await DisplayAlert("Warning", "Llene todos los campo", "Aceptar");
            }
            else
            {
                var emple = new Fotografia
                {
                    latitud = txtLatitud.Text,
                    longitud = txtLongitud.Text,
                    descripcion= txtDescripcion.Text ,
                    imgRuta = ruta
                };
                var resultado = await App.BaseDatos.GuardarFoto(emple);
                if (resultado != 0)
                {
                    await DisplayAlert("Warning", "Empleado Ingresado con exito!", "Aceptar");
                    foto.Source = ("camara2.png");
                    ruta = "";
                    txtDescripcion.Text = "";
                    InizializatePlugins();

                }
                else
                {
                    await DisplayAlert("Warning", "Error!", "Aceptar");
                }

            }
        }

        private async void btnLista_Clicked(object sender, EventArgs e)
        {
            var newpage = new ListaFotografias();
            await Navigation.PushAsync(newpage);
        }
    }
}
