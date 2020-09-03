using System;
using System.Collections.Generic;
using SQLite;
using TravelRecord.Logic;
using TravelRecord.Model;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TravelRecord
{
    public partial class NewTravelPage : ContentPage
    {
        public NewTravelPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var request = new GeolocationRequest(GeolocationAccuracy.Medium);
            var location = await Geolocation.GetLocationAsync(request);

            var venues = VenueLogic.GetVenues(location.Latitude, location.Longitude);
        }

        private void Save_Clicked(object sender, EventArgs e)
        {
            Post post = new Post()
            {
                Experience = experienceEntry.Text
            };

            experienceEntry.Text = null;
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Post>();
                var rows = conn.Insert(post);

                if (rows > 0)
                    DisplayAlert("Success", "Experience successfully inserted", "Ok");
                else
                    DisplayAlert("Failure", "Experience failed to be inserted", "Ok");
            }
        }
    }
}
