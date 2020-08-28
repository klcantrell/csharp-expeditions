using System;
using System.Collections.Generic;
using SQLite;
using TravelRecord.Model;
using Xamarin.Forms;

namespace TravelRecord
{
    public partial class NewTravelPage : ContentPage
    {
        public NewTravelPage()
        {
            InitializeComponent();
        }

        void Save_Clicked(object sender, EventArgs e)
        {
            Post post = new Post()
            {
                Experience = experienceEntry.Text
            };

            experienceEntry.Text = null;
            SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation);
            conn.CreateTable<Post>();
            var rows = conn.Insert(post);
            conn.Close();

            if (rows > 0)
                DisplayAlert("Success", "Experience successfully inserted", "Ok");
            else
                DisplayAlert("Failure", "Experience failed to be inserted", "Ok");
        }
    }
}
