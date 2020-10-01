using System;
using System.Windows.Input;
using TravelRecord.Model;

namespace TravelRecord.ViewModel.Commands
{
    public class PostCommand : ICommand
    {
        NewTravelVM viewModel;

        public PostCommand(NewTravelVM viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            var post = parameter as Post;

            return !string.IsNullOrEmpty(post?.Experience)
                && post.Venue != null;
        }

        public async void Execute(object parameter)
        {
            var post = parameter as Post;
            await viewModel.PublishPost(post);
        }
    }
}
