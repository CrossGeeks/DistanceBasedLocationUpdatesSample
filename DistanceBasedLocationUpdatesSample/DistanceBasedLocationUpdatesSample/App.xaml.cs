using System;
using DistanceBasedLocationUpdatesSample.Services;
using DistanceBasedLocationUpdatesSample.Views;
using DistanceBasedLocationUpdatesSample.ViewModels;
using Xamarin.Forms;

namespace DistanceBasedLocationUpdatesSample
{
    public partial class App : Application
    {
        public App(IGeolocationService geolocationService)
        {
            InitializeComponent();

            MainPage = new MainPage()
            {
                BindingContext = new MainViewModel(geolocationService)
            };
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
