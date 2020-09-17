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
            switch (await Users.Register(emailEntry.Text, passwordEntry.Text, confirmPasswordEntry.Text))
            {
                case RegisterResult.Success:
                    await Navigation.PushAsync(new HomePage());
                    break;
                case RegisterResult.Failure:
                    await DisplayAlert("Error", "Passwords don't match", "Ok");
                    break;
            }
        }
    }
}
