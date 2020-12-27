using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TestDisableReflection
{
    class Program
    {
        private static readonly Random random = new Random();
        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            await SendRequest();
        }

        private static async Task SendRequest()
        {
            int randomId;
            do
            {
                randomId = random.Next(1, 84);
            } while (randomId == 17);

            var response = await client.GetStringAsync($"https://swapi.dev/api/people/{randomId}/");
            Console.WriteLine($"Your random Star Wars character of the day:");
            Console.WriteLine(response);
        }
    }
}
