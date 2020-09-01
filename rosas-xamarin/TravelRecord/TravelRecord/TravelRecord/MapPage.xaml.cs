using System;
using System.Collections.Generic;
using Plugin.Geolocator;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TravelRecord
{
    public partial class MapPage : ContentPage
    {
        private bool hasLocationPermission = false;

        public MapPage()
        {
            InitializeComponent();

            GetPermissions();
        }

        public async void GetPermissions()
        {
            try
            {
                var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
                if (status != PermissionStatus.Granted)
                {
                    if (Device.RuntimePlatform == Device.Android)
                    {
                        await DisplayAlert("Requesting your location", "Access to your location allows Travel to find experiences near you", "Ok");
                    }

                    status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                }

                if (status == PermissionStatus.Granted)
                {
                    hasLocationPermission = true;
                    locationsMap.IsShowingUser = true;

                    GetLocation();
                }
                else
                {
                    await DisplayAlert("Please enable location services", "Access to your location allows Travel to find experiences near you", "Close");
                }
            }
            catch
            {
                await DisplayAlert("Error", "Something went wrong requesting your location", "Ok");
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (hasLocationPermission)
            {
                var locator = CrossGeolocator.Current;

                locator.PositionChanged += Locator_PositionChanged;

                await locator.StartListeningAsync(TimeSpan.Zero, 100);
            }

            GetLocation();
        }

        protected override async void OnDisappearing()
        {
            var locator = CrossGeolocator.Current;

            await locator.StopListeningAsync();

            locator.PositionChanged -= Locator_PositionChanged;
        }

        private void Locator_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            Location location = new Location()
            {
                Latitude = e.Position.Latitude,
                Longitude = e.Position.Longitude,
            };
            MoveMap(location);
        }

        private async void GetLocation()
        {
            if (hasLocationPermission)
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                var location = await Geolocation.GetLocationAsync(request);

                MoveMap(location);
            }
        }

        private void MoveMap(Location location)
        {
            var center = new Xamarin.Forms.Maps.Position(location.Latitude, location.Longitude);
            var mapSpan = new Xamarin.Forms.Maps.MapSpan(center, 1, 1);
            locationsMap.MoveToRegion(mapSpan);
        }
    }
}
