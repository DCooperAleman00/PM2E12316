using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using PM2E12316.Controllers;

namespace PM2E12316
{
    public partial class App : Application
    {

        static FotoController basedatos;

        public static FotoController BaseDatos
        {
            get
            {
                if (basedatos == null)
                {
                    basedatos = new FotoController(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "EmpleDB.db3"));
                }
                return basedatos;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
