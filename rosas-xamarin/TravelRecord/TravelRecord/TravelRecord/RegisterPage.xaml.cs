 using System;
using System.Collections.Generic;
using TravelRecord.Model;
using Xamarin.Forms;

namespace TravelRecord
{
    public partial class RegisterPage : ContentPage
    {
        private readonly Users user;

        public RegisterPage()
        {
            InitializeComponent();

            user = new Users();
            containerStackLayout.BindingContext = user;
        }

        async void RegisterButton_Pressed(object sender, EventArgs e)
        {
            if (passwordEntry.Text == confirmPasswordEntry.Text)
            {
                await Users.Register(user);
                await Navigation.PushAsync(new HomePage());
            }
            else
            {
                await DisplayAlert("Error", "Passwords don't match", "Ok");
            }
        }
    }
}
