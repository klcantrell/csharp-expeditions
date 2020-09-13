using System;
using System.Collections.Generic;
using TravelRecord.Model;
using Xamarin.Forms;

namespace TravelRecord
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        async void RegisterButton_Pressed(object sender, EventArgs e)
        {
            if (passwordEntry.Text == confirmPasswordEntry.Text)
            {
                Users user = new Users()
                {
                    Email = emailEntry.Text,
                    Password = passwordEntry.Text,
                };
                await App.MobileService.GetTable<Users>().InsertAsync(user);

            }
            else
            {
                await DisplayAlert("Error", "Passwords don't match", "Ok");
            }
        }
    }
}
