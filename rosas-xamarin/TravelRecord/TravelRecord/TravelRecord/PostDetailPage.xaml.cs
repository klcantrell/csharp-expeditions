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
        }

        void updateButton_Pressed(object sender, EventArgs e)
        {
            selectedPost.Experience = experienceEntry.Text;

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Post>();
                var rows = conn.Update(selectedPost);

                if (rows > 0)
                    DisplayAlert("Success", "Experience successfully updated", "Ok");
                else
                    DisplayAlert("Failure", "Experience failed to be updated", "Ok");
            }
        }
        void deleteButton_Pressed(object sender, EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Post>();
                var rows = conn.Delete(selectedPost);

                if (rows > 0)
                    DisplayAlert("Success", "Experience successfully deleted", "Ok");
                else
                    DisplayAlert("Failure", "Experience failed to be deleted", "Ok");
            }
        }
    }
}
