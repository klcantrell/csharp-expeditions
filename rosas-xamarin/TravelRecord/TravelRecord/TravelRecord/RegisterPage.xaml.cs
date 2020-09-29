 using System;
using System.Collections.Generic;
using TravelRecord.Model;
using TravelRecord.ViewModel;
using Xamarin.Forms;

namespace TravelRecord
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterVM registerVM;

        public RegisterPage()
        {
            InitializeComponent();

            registerVM = new RegisterVM();
            BindingContext = registerVM;
        }
    }
}
