﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Xamarin.Forms.Maps;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;



namespace PM2E12316
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    
    public partial class mapa : ContentPage
    {
        String lat;
        String lon;
        String des;
        public mapa()
        {
            InitializeComponent();
            
        }
        protected override async void OnAppearing()
        {
            lat = lblLat.Text;
            lon = lblLong.Text;
            des = lblDesc.Text;
            base.OnAppearing();
            Pin ubicacion = new Pin();
            ubicacion.Label = "Ubicación Seleccionada";
            ubicacion.Address = des;
            ubicacion.Position = new Position( Convert.ToDouble( lat) , Convert.ToDouble( lon ));
            Mapa.Pins.Add(ubicacion);

            Mapa.MoveToRegion(new MapSpan(new Position(Convert.ToDouble(lat), Convert.ToDouble(lon)), 1, 1));
            var localizacion = CrossGeolocator.Current;
            if (localizacion != null)
            {
                localizacion.PositionChanged += localizacion_positionChanged;
                if (!localizacion.IsListening)
                {
                    await localizacion.StartListeningAsync(TimeSpan.FromSeconds(10), 100);
                }
            }
        }
        private void localizacion_positionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            var posicion_mapa = new Position(e.Position.Latitude, e.Position.Longitude);
            Mapa.MoveToRegion(new MapSpan(posicion_mapa, 1, 1));
        }
    }
}