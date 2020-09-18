using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel;

namespace TravelRecord.Model
{
    public class Post : INotifyPropertyChanged
    {
        private string id;

        public string Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        private string experience;

        public string Experience
        {
            get => experience;
            set
            {
                experience = value;
                OnPropertyChanged("Experience");
            }
        }

        private string venueName;

        public string VenueName
        {
            get => venueName;
            set
            {
                venueName = value;
                OnPropertyChanged("VenueName");
            }
        }

        private string categoryId;

        public string CategoryId
        {
            get => categoryId;
            set
            {
                categoryId = value;
                OnPropertyChanged("CategoryId");
            }
        }

        private string categoryName;

        public string CategoryName
        {
            get => categoryName;
            set
            {
                categoryName = value;
                OnPropertyChanged("CategoryName");
            }
        }

        private string address;

        public string Address {
            get => address;
            set
            {
                address = value;
                OnPropertyChanged("Address");
            }
        }

        private double latitude;

        public double Latitude
        {
            get => latitude;
            set
            {
                latitude = value;
                OnPropertyChanged("Latitude");
            }
        }

        private double longitude;

        public double Longitude
        {
            get => longitude;
            set
            {
                longitude = value;
                OnPropertyChanged("Longitude");
            }
        }

        private int distance;

        public int Distance
        {
            get => distance;
            set
            {
                distance = value;
                OnPropertyChanged("Distance");
            }
        }

        private string userId;

        public string UserId
        {
            get => userId;
            set
            {
                userId = value;
                OnPropertyChanged("UserId");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public static async Task Insert(Post post)
        {
            await App.MobileService.GetTable<Post>().InsertAsync(post);
        }

        public static async Task<List<Post>> FindByUserId(string userId)
        {
            return await App.MobileService
                .GetTable<Post>()
                .Where(p => p.UserId == userId)
                .ToListAsync();
        }

        public static async Task Update(Post post)
        {
            await App.MobileService
                .GetTable<Post>()
                .UpdateAsync(post);
        }

        public static async Task Delete(Post post)
        {
            await App.MobileService
                .GetTable<Post>()
                .DeleteAsync(post);
        }

        public static Dictionary<string, int> GetPostCategoriesCount(List<Post> posts)
        {
            Dictionary<String, int> categoriesCount = new Dictionary<string, int>();

            var categories = (from p in posts
                              orderby p.CategoryId
                              select p.CategoryName).Distinct();

            foreach (var category in categories)
            {
                var count = (from p in posts
                             where p.CategoryName == category
                             select p).ToList().Count;

                categoriesCount.Add(category, count);
            }

            return categoriesCount;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
