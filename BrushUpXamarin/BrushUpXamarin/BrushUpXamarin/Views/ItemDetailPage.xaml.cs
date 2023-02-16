using System.ComponentModel;
using Xamarin.Forms;
using BrushUpXamarin.ViewModels;

namespace BrushUpXamarin.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}
