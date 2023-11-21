using TapperSharp;
using TapperSharp.Services;

namespace TapperConsole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var tapperClient = new TapperClient("https://tap.trac.network");
            await tapperClient.ConnectAsync();
            await tapperClient.GetDeploymentAsync("tap");
            await tapperClient.DisconnectAsync();
   
        }
    }
}