using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TravelRecord
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        void RegisterButton_Pressed(object sender, EventArgs e)
        {
            if (passwordEntry.Text == confirmPasswordEntry.Text)
            {

            }
            else
            {
                DisplayAlert("Error", "Passwords don't match", "Ok");
            }
        }
    }
}
