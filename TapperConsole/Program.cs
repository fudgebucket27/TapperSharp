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
            await tapperClient.ConnectAsync();
            var tapDeployment = await tapperClient.GetDeploymentAsync("tap");
            Console.WriteLine(JsonSerializer.Serialize(tapDeployment,  new JsonSerializerOptions() { WriteIndented = true}));
            var deploymentsLength = await tapperClient.GetDeploymentsLengthAsync();
            Console.WriteLine(JsonSerializer.Serialize(deploymentsLength, new JsonSerializerOptions() { WriteIndented = true }));
            await tapperClient.DisconnectAsync();
        }
    }
}