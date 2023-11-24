using SocketIOClient;
using System.Text.Json;
using System.Text.Json.Serialization;
using TapperSharp;
using TapperSharp.Services;

namespace TapperConsole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            SocketIOOptions socketIOOptions = new SocketIOOptions()
            {
                ReconnectionAttempts = 3,
                Reconnection = true,
                ReconnectionDelay = 500,
                ReconnectionDelayMax = 500,
                RandomizationFactor = 0                
            };
            var tapperClient = new TapperClient("https://tap.trac.network", socketIOOptions);
            try
            {
                await tapperClient.ConnectAsync();

                var result = await tapperClient!.GetTickerTradesFilledListAsync("buidl", 0,10);
                Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));

                await tapperClient.DisconnectAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}