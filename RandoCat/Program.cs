using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net.Http;

namespace RandoCat
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            await SendRequest();

            const string url = "https://swapi.dev/api/people/1/";

            string commandToStart;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                commandToStart = "xdg-open";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                commandToStart = "open";
            }
            else
            {
                // assuming OS is Windows
                commandToStart = "explorer";
            }

            if (commandToStart == null)
            {
                throw new Exception("Unsupported OS. Must be one of [Linux, OSX, Windows]");
            }

            Process.Start(new ProcessStartInfo(commandToStart, url) { RedirectStandardOutput = true, RedirectStandardError = true, });
        }

        private static async Task SendRequest()
        {
            var response = await client.GetStringAsync("https://jsonplaceholder.typicode.com/todos/1");
            Console.WriteLine(response);
        }
    }
}
