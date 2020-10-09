using System;
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
        
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await viewModel.UpdatePosts();
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedPost = postListView.SelectedItem as Post;

            if (selectedPost != null)
            {
                Navigation.PushAsync(new PostDetailPage(selectedPost));
            }
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            var post = (sender as MenuItem).CommandParameter as Post;
            viewModel.DeletePost(post);

            await viewModel.UpdatePosts();
        }

        private async void postListView_Refreshing(object sender, EventArgs e)
        {
            await viewModel.UpdatePosts();
            postListView.IsRefreshing = false;
        }
    }
}
