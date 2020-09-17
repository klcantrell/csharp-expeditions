using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecord.Model;
using Xamarin.Forms;

namespace TravelRecord
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            var assembly = typeof(MainPage);
            iconImage.Source = ImageSource.FromResource("TravelRecord.Assets.Images.plane.png", assembly);
        }

        async void LoginButton_Pressed(object sender, EventArgs e)
        {
            switch (await Users.Login(emailEntry.Text, passwordEntry.Text))
            {
                case LoginResult.Success:
                    await Navigation.PushAsync(new HomePage());
                    break;
                case LoginResult.MissingField:
                    await DisplayAlert("Error", "You need to enter both an email and password", "Ok");
                    break;
                case LoginResult.WrongPassword:
                    await DisplayAlert("Error", "Password is incorrect", "Ok");
                    break;
                case LoginResult.Failure:
                    await DisplayAlert("Error", "There was an error logging you in", "Ok");
                    break;
            }
        }

        void RegisterButton_Pressed(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }
    }
}
