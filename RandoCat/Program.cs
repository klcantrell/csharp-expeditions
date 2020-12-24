using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace RandoCat
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            await SendRequest();
        }

        private static async Task SendRequest()
        {
            var response = await client.GetStringAsync("https://jsonplaceholder.typicode.com/todos/1");
            Console.WriteLine(response);
        }
    }
}
