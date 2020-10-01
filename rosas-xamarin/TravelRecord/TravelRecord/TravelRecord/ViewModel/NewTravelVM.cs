using System;
using System.ComponentModel;
using System.Threading.Tasks;
using TravelRecord.Model;
using TravelRecord.ViewModel.Commands;

namespace TravelRecord.ViewModel
{
    public class NewTravelVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public PostCommand PostCommand { get; set; }

        private Post post;
        public Post Post
        {
            get => post;
            set
            {
                post = value;
                OnPropertyChanged("Post");
            }
        }

        private string experience;
        public string Experience
        {
            get => experience;
            set
            {
                experience = value;
                Post = new Post()
                {
                    Experience = this.Experience,
                    Venue = this.Venue,
                };
                OnPropertyChanged("Experience");
            }
        }

        private Venue venue;
        public Venue Venue
        {
            get => venue;
            set
            {
                venue = value;
                Post = new Post()
                {
                    Experience = this.Experience,
                    Venue = this.Venue,
                };
                OnPropertyChanged("Venue");
            }
        }

        public NewTravelVM()
        {
            PostCommand = new PostCommand(this);
            Post = new Post();
            Venue = new Venue();
        }

        public async Task PublishPost(Post post)
        {
            try
            {
                await Post.Insert(post);

                await App.Current.MainPage.DisplayAlert("Success", "Experience successfully inserted", "Ok");
            }
            catch (NullReferenceException nre)
            {
                await App.Current.MainPage.DisplayAlert("Failure", "Experience failed to be inserted", "Ok");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Failure", "Experience failed to be inserted", "Ok");
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
