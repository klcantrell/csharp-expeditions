using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;
using TravelRecord.Model;
using TravelRecord.ViewModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TravelRecord
{
    public partial class NewTravelPage : ContentPage
    {
        NewTravelVM viewModel;

        public NewTravelPage()
        {
            InitializeComponent();
            viewModel = new NewTravelVM();

            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var request = new GeolocationRequest(GeolocationAccuracy.Medium);
            var location = await Geolocation.GetLocationAsync(request);

            var venues = await Venue.GetVenues(location.Latitude, location.Longitude);
            venueListView.ItemsSource = venues;
        }
    }
}
