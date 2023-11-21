

using SocketIO;
using SocketIOClient;

namespace TapperSharp
{
    public class TapperClient : ITapperClient
    { 
        private readonly SocketIOClient.SocketIO _client;
        public TapperClient(string host, SocketIOOptions? socketIOOptions = null)
        {
            if(socketIOOptions == null)
            {
                _client = new SocketIOClient.SocketIO(host);
            }
            else
            {
                _client = new SocketIOClient.SocketIO(host, socketIOOptions);
            }
            _client.On("response", response =>
            {
 
                Console.WriteLine(response);

                string text = response.GetValue<string>();

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
            await _client.ConnectAsync();
        }

        public async Task DisconnectAsync()
        {
            await _client.DisconnectAsync();
        }
    }
}