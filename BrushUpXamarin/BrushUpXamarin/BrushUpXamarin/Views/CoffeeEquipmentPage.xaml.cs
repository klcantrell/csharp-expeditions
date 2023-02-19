using System;
using System.Collections.Generic;
using System.Windows.Input;
using BrushUpXamarin.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BrushUpXamarin.Views
{
    public partial class CoffeeEquipmentPage : ContentPage
    {
        public CoffeeEquipmentPage()
        {
            InitializeComponent();
        }

        async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var coffee = ((ListView)sender).SelectedItem as Coffee;
            if (coffee == null)
            {
                return;
            }

            await DisplayAlert("Coffee Selected", coffee.Name, "OK");
        }

        void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        async void MenuItem_Clicked(object sender, EventArgs e)
        {
            if (sender is MenuItem)
            {
                var menuItem = (MenuItem)sender;
                var text = menuItem.Text;
                var coffee = menuItem.BindingContext as Coffee;
                if (coffee == null)
                {
                    return;
                }

                switch (text.ToLower())
                {
                    case "favorite":
                        await DisplayAlert("Coffee Favorited", coffee.Name, "OK");
                        break;
                    case "delete":
                        await DisplayAlert("Coffee Deleted", coffee.Name, "OK");
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
