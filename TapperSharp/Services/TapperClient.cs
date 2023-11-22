

using SocketIO;
using SocketIOClient;
using System.Collections.Concurrent;
using System.Text.Json;
using TapperSharp.Models;

namespace TapperSharp.Services
{
    public class TapperClient : ITapperClient
    {
        private readonly SocketIOClient.SocketIO _client;
        private readonly ConcurrentDictionary<string, TaskCompletionSource<TapResponse>> _responseCompletionSources = new ConcurrentDictionary<string, TaskCompletionSource<TapResponse>>();
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
                var jsonResponse = JsonSerializer.Serialize(response.GetValue<TapResponse>());
                var tapResponse = JsonSerializer.Deserialize<TapResponse>(jsonResponse);
                if (_responseCompletionSources.TryRemove(tapResponse.CallId, out var completionSource))
                {
                    completionSource.TrySetResult(response.GetValue<TapResponse>());
                }
            });
            _client.OnConnected += (sender, e) =>
            {
                Console.WriteLine($"Connected to {host}");
            };
            _client.OnDisconnected += (sender, e) =>
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

        public async Task<TapResponse> GetDeploymentAsync(string ticker)
        {
            var callId = Guid.NewGuid().ToString(); // Generate a unique identifier

            var completionSource = new TaskCompletionSource<TapResponse>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "deployment",
                Args = new[] {ticker},
                CallId = callId
            });
            return await completionSource.Task;
        }

        public async Task<TapResponse> GetDeploymentsLengthAsync()
        {
            var callId = Guid.NewGuid().ToString(); // Generate a unique identifier

            var completionSource = new TaskCompletionSource<TapResponse>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "deploymentsLength",
                Args = new string[0],
                CallId = callId
            });
            return await completionSource.Task;
        }
    }
}