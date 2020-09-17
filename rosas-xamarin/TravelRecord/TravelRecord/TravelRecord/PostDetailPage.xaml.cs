using System;
using System.Collections.Generic;
using SQLite;
using TravelRecord.Model;
using Xamarin.Forms;

namespace TravelRecord
{
    public partial class PostDetailPage : ContentPage
    {
        readonly Post selectedPost;

        public PostDetailPage(Post selectedPost)
        {
            InitializeComponent();
            this.selectedPost = selectedPost;

            experienceEntry.Text = selectedPost.Experience;
            venueLabel.Text = selectedPost.VenueName;
            categoryLabel.Text = selectedPost.CategoryName;
            addressLabel.Text = selectedPost.Address;
            coordinatesLabel.Text = $"{selectedPost.Latitude}, {selectedPost.Longitude}";
            distanceLabel.Text = $"{selectedPost.Distance} m";
        }

        async void updateButton_Pressed(object sender, EventArgs e)
        {
            selectedPost.Experience = experienceEntry.Text;

            try
            {
                await Post.Update(selectedPost);
                await DisplayAlert("Success", "Experience successfully updated", "Ok");
            }
            catch
            {
                await DisplayAlert("Failure", "Experience failed to be updated", "Ok");
            }
        }

        async void deleteButton_Pressed(object sender, EventArgs e)
        {
            try
            {
                await Post.Delete(selectedPost);
                await Navigation.PopAsync();
            }
            catch
            {
                await DisplayAlert("Failure", "Experience failed to be deleted", "Ok");
            }
        }
    }
}
