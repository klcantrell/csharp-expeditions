using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace TravelRecord.Model
{
    public class Post
    {
        [PrimaryKey]
        public string Id { get; set; }

        [MaxLength(250)]
        public string Experience { get; set; }

        public string VenueName { get; set; }

        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Distance { get; set; }
        public string UserId { get; set; }

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
    }
}
