using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TravelRecord
{
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();

            GetPermissions();
        }

        public async void GetPermissions()
        {
            try
            {
                var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
                if (status != PermissionStatus.Granted)
                {
                    if (Device.RuntimePlatform == Device.Android)
                    {
                        await DisplayAlert("Requesting your location", "Access to your location allows Travel to find experiences near you", "Ok");
                    }

                    status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                }

                if (status == PermissionStatus.Granted)
                {
                    locationsMap.IsShowingUser = true;
                }
                else
                {
                    await DisplayAlert("Please enable location services", "Access to your location allows Travel to find experiences near you", "Close");
                }
            }
            catch
            {
                await DisplayAlert("Error", "Something went wrong requesting your location", "Ok");
            }
        }
    }
}
