using System.Collections.ObjectModel;
using TravelRecord.Model;

namespace TravelRecord.ViewModel
{
    public class HistoryVM
    {
        public ObservableCollection<Post> Posts { get; set; }

        public HistoryVM()
        {
            Posts = new ObservableCollection<Post>();
        }

        public async void UpdatePosts()
        {
            var posts = await Post.FindByUserId(App.user.Id);

            if (posts != null)
            {
                Posts.Clear();
                foreach (var post in posts)
                {
                    Posts.Add(post);
                }
            }
        }
    }
}
