using System;
namespace DistanceBasedLocationUpdatesSample.Services
{
    public interface IGeolocationService
    {
        event EventHandler<GeolocationEventArgs> LocationUpdate;
        void StartUpdating(double distance);
        void StopUpdating();
        bool IsUpdating { get; }
    }

    public class GeolocationEventArgs : EventArgs
    {
        public GeolocationEventArgs(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public double Latitude { get; }
        public double Longitude { get; }
    }
}
