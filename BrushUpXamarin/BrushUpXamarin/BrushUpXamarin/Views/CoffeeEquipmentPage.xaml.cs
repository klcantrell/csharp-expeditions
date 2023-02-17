using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace BrushUpXamarin.Views
{
    public partial class CoffeeEquipmentPage : ContentPage
    {
        int count = 0;

        public CoffeeEquipmentPage()
        {
            InitializeComponent();
        }

        void ButtonClick_Clicked(object sender, EventArgs e)
        {
            count += 1;
            LabelCount.Text = $"You clicked {count} time(s)";
        }
    }
}
