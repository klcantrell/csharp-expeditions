using System;
using System.Linq;
using System.Collections.Generic;
using SQLite;
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

            var posts = await (App.MobileService
                .GetTable<Post>()
                .Where(p => p.UserId == App.user.Id).ToListAsync());


            var categories = (from p in posts
                                orderby p.CategoryId
                                select p.CategoryName).Distinct();

            Dictionary<String, int> categoriesCount = new Dictionary<string, int>();

            foreach(var category in categories)
            {
                var count = (from p in posts
                                where p.CategoryName == category
                                select p).ToList().Count;

                categoriesCount.Add(category, count);
            }

            categoriesListView.ItemsSource = categoriesCount;

            postCountLabel.Text = posts.Count.ToString();
        }
    }
}
