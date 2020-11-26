using System.ComponentModel;
using System.Threading.Tasks;
using DistanceBasedLocationUpdatesSample.Services;
using Xamarin.Essentials;

namespace DistanceBasedLocationUpdatesSample.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        IGeolocationService _geolocationService;

        public event PropertyChangedEventHandler PropertyChanged;

        public Location CurrentLocation { get; private set; }

        public MainViewModel(IGeolocationService geolocationService)
        {
            _geolocationService = geolocationService;
            _geolocationService.LocationUpdate += LocationUpdate;
            _geolocationService.StartUpdating(150);
            Initialize();
        }

        async Task Initialize()
        {
            CurrentLocation = await Geolocation.GetLastKnownLocationAsync();
        }

        private void LocationUpdate(object sender, GeolocationEventArgs e)
        {
            CurrentLocation = new Location(e.Latitude, e.Longitude);
            
        }
    }
}
