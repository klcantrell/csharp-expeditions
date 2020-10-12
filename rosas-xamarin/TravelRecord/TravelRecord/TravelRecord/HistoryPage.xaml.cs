using System;
using TravelRecord.Helpers;
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
        
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            viewModel.UpdatePosts();

            await AzureAppServiceHelper.SyncAsync();
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedPost = postListView.SelectedItem as Post;

            if (selectedPost != null)
            {
                Navigation.PushAsync(new PostDetailPage(selectedPost));
            }
        }

        private void Delete_Clicked(object sender, EventArgs e)
        {
            var post = (sender as MenuItem).CommandParameter as Post;
            viewModel.DeletePost(post);

            viewModel.UpdatePosts();
        }

        private async void postListView_Refreshing(object sender, EventArgs e)
        {
            viewModel.UpdatePosts();
            postListView.IsRefreshing = false;

            await AzureAppServiceHelper.SyncAsync();
        }
    }
}
