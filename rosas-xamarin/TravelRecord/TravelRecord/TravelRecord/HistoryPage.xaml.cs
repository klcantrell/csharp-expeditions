using TravelRecord.Model;
using TravelRecord.ViewModel;
using Xamarin.Forms;

namespace TravelRecord
{
    public partial class HistoryPage : ContentPage
    {
        HistoryVM viewModel;

        public HistoryPage()
        {
            InitializeComponent();
            viewModel = new HistoryVM();
            BindingContext = viewModel;
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.UpdatePosts();
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedPost = postListView.SelectedItem as Post;

            if (selectedPost != null)
            {
                Navigation.PushAsync(new PostDetailPage(selectedPost));
            }
        }
    }
}
