using System;
using System.Diagnostics;
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
            Process.Start(new ProcessStartInfo() { FileName = "https://swapi.dev/api/people/1/", UseShellExecute = true, });
        }

        private static async Task SendRequest()
        {
            var response = await client.GetStringAsync("https://jsonplaceholder.typicode.com/todos/1");
            Console.WriteLine(response);
        }
    }
}
