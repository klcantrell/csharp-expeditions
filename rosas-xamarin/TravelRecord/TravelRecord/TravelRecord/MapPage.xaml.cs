using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.Geolocator;
using SQLite;
using TravelRecord.Model;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

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

                    await GetLocation();
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

            await GetLocation();

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Post>();
                var posts = conn.Table<Post>().ToList();

                DisplayInMap(posts);
            }
        }

        protected override async void OnDisappearing()
        {
            var locator = CrossGeolocator.Current;

            await locator.StopListeningAsync();

            locator.PositionChanged -= Locator_PositionChanged;
        }

        private void Locator_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            Xamarin.Essentials.Location location = new Xamarin.Essentials.Location()
            {
                Latitude = e.Position.Latitude,
                Longitude = e.Position.Longitude,
            };
            MoveMap(location);
        }

        private async Task GetLocation()
        {
            if (hasLocationPermission)
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                var location = await Geolocation.GetLocationAsync(request);

                MoveMap(location);
            }
        }

        private void MoveMap(Xamarin.Essentials.Location location)
        {
            var center = new Position(location.Latitude, location.Longitude);
            var mapSpan = new MapSpan(center, 1, 1);
            locationsMap.MoveToRegion(mapSpan);
        }

        private void DisplayInMap(List<Post> posts)
        {
            foreach (var post in posts)
            {
                try
                {
                    var position = new Position(post.Latitude, post.Longitude);
                    var pin = new Pin()
                    {
                        Type = PinType.SavedPin,
                        Position = position,
                        Label = post.VenueName,
                        Address = post.Address,
                    };
                    locationsMap.Pins.Add(pin);
                }
                catch(NullReferenceException nre) {}
                catch(Exception ex) {}
            }
        }
    }
}
