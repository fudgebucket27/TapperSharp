

using SocketIO;
using SocketIOClient;
using TapperSharp.Models;

namespace TapperSharp.Services
{
    public class TapperClient : ITapperClient
    {
        private readonly SocketIOClient.SocketIO _client;
        public TapperClient(string host, SocketIOOptions? socketIOOptions = null)
        {
            if (socketIOOptions == null)
            {
                _client = new SocketIOClient.SocketIO(host);
            }
            else
            {
                _client = new SocketIOClient.SocketIO(host, socketIOOptions);
            }
            _client.On("response", response =>
            {

                var msg = response.GetValue<string>();
                Console.WriteLine(msg);

            });
            _client.OnConnected += async (sender, e) =>
            {
                // Emit a string
                //await _client.EmitAsync("hi", "socket.io");
                Console.WriteLine($"Connected to {host}");
            };
            _client.OnDisconnected += async (sender, e) =>
            {
                Console.WriteLine($"Disconnected from {host}");
            };
        }

        public async Task ConnectAsync()
        {
            if (!_client.Connected)
            {
                await _client.ConnectAsync();
            }
        }

        public async Task DisconnectAsync()
        {
            if (_client.Connected)
            {
                await _client.DisconnectAsync();
            }
        }

        public async Task GetDeploymentAsync(string ticker)
        {
            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "deployment",
                Args = new[] {ticker},
                CallId = "getDeployment"
            });
        }
    }
}