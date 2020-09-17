﻿using System;
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
        public NewTravelPage()
        {
            InitializeComponent();
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
                Post post = new Post()
                {
                    Experience = experienceEntry.Text,
                    CategoryId = firstCategory.id,
                    CategoryName = firstCategory.name,
                    Address = selectedVenue.location.address,
                    Distance = selectedVenue.location.distance,
                    Latitude = selectedVenue.location.lat,
                    Longitude = selectedVenue.location.lng,
                    VenueName = selectedVenue.name,
                    UserId = App.user.Id,
                };

                experienceEntry.Text = null;

                await Post.Insert(post);
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
