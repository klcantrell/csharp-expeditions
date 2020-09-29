using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;
using TravelRecord.Model;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TravelRecord
{
    public partial class NewTravelPage : ContentPage
    {
        private readonly Post post;


        public NewTravelPage()
        {
            InitializeComponent();

            post = new Post();
            containerStackLayout.BindingContext = post;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var request = new GeolocationRequest(GeolocationAccuracy.Medium);
            var location = await Geolocation.GetLocationAsync(request);

            var venues = await Venue.GetVenues(location.Latitude, location.Longitude);
            venueListView.ItemsSource = venues;
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            try
            {
                var selectedVenue = venueListView.SelectedItem as Venue;
                var firstCategory = selectedVenue.categories.FirstOrDefault();

                post.CategoryId = firstCategory.id;
                post.CategoryName = firstCategory.name;
                post.Address = selectedVenue.location.address;
                post.Distance = selectedVenue.location.distance;
                post.Latitude = selectedVenue.location.lat;
                post.Longitude = selectedVenue.location.lng;
                post.VenueName = selectedVenue.name;
                post.UserId = App.user.Id;

                await Post.Insert(post);
                experienceEntry.Text = null;

                await DisplayAlert("Success", "Experience successfully inserted", "Ok");
            }
            catch(NullReferenceException nre)
            {
                await DisplayAlert("Failure", "Experience failed to be inserted", "Ok");
            }
            catch(Exception ex)
            {
                await DisplayAlert("Failure", "Experience failed to be inserted", "Ok");
            }
        }
    }
}
