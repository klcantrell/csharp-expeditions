using System;
using System.Collections.Generic;
using TravelRecord.ViewModel;
using Xamarin.Forms;

namespace TravelRecord
{
    public partial class HomePage : TabbedPage
    {
        public HomeVM viewModel;

        public HomePage()
        {
            InitializeComponent();

            viewModel = new HomeVM();
            BindingContext = viewModel; 
        }
    }
}
