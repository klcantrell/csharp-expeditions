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
            var isEmailEmpty = string.IsNullOrEmpty(emailEntry.Text);
            var isPasswordEmpty = string.IsNullOrEmpty(passwordEntry.Text);

            if (isEmailEmpty || isPasswordEmpty)
            {
                await DisplayAlert("Error", "You need to enter both an email and password", "Ok");
            }
            else
            {
                var user = (await App.MobileService.GetTable<Users>().Where(u => u.Email == emailEntry.Text).ToListAsync()).FirstOrDefault();
                if (user != null)
                {
                    App.user = user;
                    if (user.Password == passwordEntry.Text)
                    {
                        await Navigation.PushAsync(new HomePage());
                    }
                    else
                    {
                        await DisplayAlert("Error", "Password is incorrect", "Ok");
                    }
                }
                else
                {
                    await DisplayAlert("Error", "There was an error logging you in", "Ok");
                }
            }
        }

        void RegisterButton_Pressed(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }
    }
}
