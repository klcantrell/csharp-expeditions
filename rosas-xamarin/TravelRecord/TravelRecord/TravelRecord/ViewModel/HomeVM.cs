using System;
using TravelRecord.ViewModel.Commands;

namespace TravelRecord.ViewModel
{
    public class HomeVM
    {
        public NavigationCommand NavigationCommand { get; set; }

        public HomeVM()
        {
            NavigationCommand = new NavigationCommand(this);
        }

        public async void Navigate()
        {
            await App.Current.MainPage.Navigation.PushAsync(new NewTravelPage());
        }
    }
}
