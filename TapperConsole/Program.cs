using TapperSharp;

namespace TapperConsole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var client = new TapperClient("https://tap.trac.network");
            await client.ConnectAsync();
            await client.DisconnectAsync();
        }
    }
}