
using System.Net.WebSockets;
using System.Text;

namespace TapperSharp
{
    public class TapperClient : ITapperClient
    {
        private readonly ClientWebSocket _client = new ClientWebSocket();

        public async Task ConnectAsync(Uri serverUri)
        {
            try
            {
                await _client.ConnectAsync(serverUri, CancellationToken.None);
                Console.WriteLine("Connected!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during connection: {ex.Message}");
            }
        }

        public async Task SendAsync(string message)
        {
            var encoded = Encoding.UTF8.GetBytes(message);
            var buffer = new ArraySegment<Byte>(encoded, 0, encoded.Length);

            await _client.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public async Task<string> ReceiveAsync()
        {
            var buffer = new ArraySegment<byte>(new byte[2048]);
            WebSocketReceiveResult result = await _client.ReceiveAsync(buffer, CancellationToken.None);

            return Encoding.UTF8.GetString(buffer.Array, 0, result.Count);
        }

        public async Task DisconnectAsync()
        {
            if (_client.State == WebSocketState.Open)
            {
                await _client.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                Console.WriteLine("Disconnected!");
            }
        }
    }
}