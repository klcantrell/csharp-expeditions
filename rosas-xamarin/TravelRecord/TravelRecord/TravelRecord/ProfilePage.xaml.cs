using System;
using System.Linq;
using System.Collections.Generic;
using TravelRecord.Model;
using Xamarin.Forms;

namespace TravelRecord
{
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var posts = await Post.FindByUserId(App.user.Id);

            var categoriesCount = Post.GetPostCategoriesCount(posts);

            categoriesListView.ItemsSource = categoriesCount;

            postCountLabel.Text = posts.Count.ToString();
        }
    }
}
