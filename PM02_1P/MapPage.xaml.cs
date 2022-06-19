using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace PM02_1P
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        private Double Lat=0.0, Lon=0.0;
        public MapPage()
        {
            InitializeComponent();
        }

        public MapPage(Double lat, Double lon)
        {
            InitializeComponent();
            Lat = lat;
            Lon = lon;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                //var location = await Geolocation.GetLocationAsync();               

                if (Lat != 0.0 || Lon != 0.0)
                {
                    //Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    var pin = new Pin()
                    {
                        Position = new Position(Lat, Lon),
                        Label = "Mi hubicación"
                    };
                    mapSiteLocation.Pins.Add(pin);
                    mapSiteLocation.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(Lat, Lon), Distance.FromMeters(100.00)));
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }
    }
}