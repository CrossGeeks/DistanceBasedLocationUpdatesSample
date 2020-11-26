using System;
using System.Linq;
using CoreLocation;
using DistanceBasedLocationUpdatesSample.Services;

namespace DistanceBasedLocationUpdatesSample.iOS.Services
{
    public class GeolocationService : IGeolocationService
    {
        public event EventHandler<GeolocationEventArgs> LocationUpdate;

        const string CurrentLocationIdentifier = nameof(CurrentLocationIdentifier);

        CLLocationManager _locationManager;
        double _distance;
        CLRegion _currentLocationRegion;

        public bool IsUpdating { get; private set; }

        public GeolocationService()
        {
            _locationManager = new CLLocationManager()
            {
                AllowsBackgroundLocationUpdates = true,
                PausesLocationUpdatesAutomatically = false
            };
        }

        private void LocationsUpdated(object sender, CLLocationsUpdatedEventArgs e)
        {
            var currentLocation = e.Locations.LastOrDefault();

            if (currentLocation != null && currentLocation.HorizontalAccuracy > 0)
            {
                _currentLocationRegion = new CLCircularRegion(_locationManager.Location.Coordinate, _distance, CurrentLocationIdentifier);
                _locationManager.StartMonitoring(_currentLocationRegion);
                LocationUpdate?.Invoke(this, new GeolocationEventArgs(currentLocation.Coordinate.Latitude, currentLocation.Coordinate.Longitude));
            }
  
        }

        private void RegionLeft(object sender, CLRegionEventArgs e)
        {
            _locationManager.RequestLocation();
        }

        public void StartUpdating(double distance)
        {
            if(IsUpdating)
            {
                StopUpdating();
            }

            _distance = distance;

            _locationManager.RegionLeft += RegionLeft;
            _locationManager.LocationsUpdated += LocationsUpdated;
            _locationManager.RequestLocation();

            IsUpdating = true;
        }

        public void StopUpdating()
        {
            _locationManager.RegionLeft -= RegionLeft;
            _locationManager.LocationsUpdated -= LocationsUpdated;

            IsUpdating = false;

            if (_currentLocationRegion != null)
            {
                _locationManager.StopMonitoring(_currentLocationRegion);
            }
           
        }
    }
}