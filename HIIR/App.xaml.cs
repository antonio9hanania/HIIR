using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HIIR
{
    public partial class App : Application
    {
        public App()
        {
            Device.SetFlags(new string[] { "Brush_Experimental" });

            InitializeComponent();

            MainPage = new MainScreenTabbed();
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
