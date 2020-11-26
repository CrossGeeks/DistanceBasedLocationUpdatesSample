using System;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace DistanceBasedLocationUpdatesSample.Controls
{
    public class CustomMap : Xamarin.Forms.Maps.Map
    {
        public static readonly BindableProperty CurrentLocationProperty = BindableProperty.Create(nameof(CurrentLocation), typeof(Location), typeof(CustomMap), null, BindingMode.TwoWay, null , UpdatedLocation);

        public Location CurrentLocation
        {
            get => (Location)GetValue(CurrentLocationProperty);
            set => SetValue(CurrentLocationProperty, value);
        }

        static void UpdatedLocation(BindableObject bindable, object oldValue, object newValue)
        {
            if(bindable is CustomMap map)
            {
                var mapSpan = MapSpan.FromCenterAndRadius(new Position(map.CurrentLocation.Latitude, map.CurrentLocation.Longitude), Distance.FromKilometers(0.5));
                map.MoveToRegion(mapSpan);

                var element2 = map.MapElements.FirstOrDefault(m => !string.IsNullOrEmpty(m.ClassId) && m.ClassId.Equals("CurrentLocation", StringComparison.OrdinalIgnoreCase));

                if (element2 != null)
                {
                    map.MapElements.Remove(element2);
                }

                map.MapElements.Add(new Circle
                {
                    ClassId = "CurrentLocation",
                    Center = mapSpan.Center,
                    Radius = new Distance(100),
                    StrokeColor = Color.Blue,

                    StrokeWidth = 2,
                    FillColor = Color.FromHex("#550000FF")
                });
            }
        }
    }
}

