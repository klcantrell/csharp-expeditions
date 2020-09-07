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
                                 select p.CategoryName).Distinct();

                Dictionary<String, int> categoriesCount = new Dictionary<string, int>();

                foreach(var category in categories)
                {
                    var count = (from p in posts
                                 where p.CategoryName == category
                                 select p).ToList().Count;

                    categoriesCount.Add(category, count);
                }

                postCountLabel.Text = posts.Count.ToString();
            }
        }
    }
}
