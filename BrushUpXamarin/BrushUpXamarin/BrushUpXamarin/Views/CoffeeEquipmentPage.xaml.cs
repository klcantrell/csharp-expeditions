using System;
using System.Collections.Generic;

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

        int count = 0;

        string countDisplay = "Click the button below!";
        public string CountDisplay
        {
            get => countDisplay;
            set
            {
                if (value == countDisplay)
                {
                    return;
                }
                else
                {
                    countDisplay = value;
                    OnPropertyChanged();
                }
            }
        }

        void ButtonClick_Clicked(object sender, EventArgs e)
        {
            count += 1;
            CountDisplay = $"You clicked {count} time(s)";
        }
    }
}
