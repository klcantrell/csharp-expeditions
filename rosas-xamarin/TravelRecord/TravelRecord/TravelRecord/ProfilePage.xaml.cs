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

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using(SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                var posts = conn.Table<Post>().ToList();
                var categories = (from p in posts
                                 orderby p.CategoryId
                                 select p.CategoryName).Distinct().ToList();

                postCountLabel.Text = posts.Count.ToString();
            }
        }
    }
}
