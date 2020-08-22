using System;
using Xamarin.Forms;

namespace HelloWorld
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        void Button_Pressed(object sender, EventArgs e)
        {
            var name = nameEntry.Text;
            var greeting = $"Hello {name}!";
            greetingLabel.Text = greeting;
            nameEntry.Text = null;
        }
    }
}
