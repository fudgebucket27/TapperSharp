

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
        private readonly ConcurrentDictionary<string, TaskCompletionSource<object>> _responseCompletionSources = new ConcurrentDictionary<string, TaskCompletionSource<object>>();
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
                var jsonResponseBaseString = JsonSerializer.Serialize(response.GetValue<TapResponseBase>());
                var jsonResponseBaseObject = JsonSerializer.Deserialize<TapResponseBase>(jsonResponseBaseString);
                string func = jsonResponseBaseObject.Func;
                Type resultType = GetResultType(func);
                Type tapResponseType = typeof(TapResponse<>).MakeGenericType(resultType);
                var jsonResponseGeneric = JsonSerializer.Serialize(response.GetValue<object>());
                var tapResponse = JsonSerializer.Deserialize(jsonResponseGeneric, tapResponseType);

                if (tapResponse is TapResponse<object> genericResponse && _responseCompletionSources.TryRemove(genericResponse.CallId, out var completionSource))
                {
                    completionSource.TrySetResult(genericResponse);
                }
                else
                {
                    Console.WriteLine("Not valid!");
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

        private Type GetResultType(string func)
        {
            switch (func)
            {
                case "deployment":
                    return typeof(DeploymentResult);
                case "deploymentsLength":
                    return typeof(DeploymentsLengthResult);
                // Add more cases as needed for other func types
                default:
                    throw new InvalidOperationException("Unknown func type");
            }
        }

        public async Task<TapResponse<DeploymentResult>> GetDeploymentAsync(string ticker)
        {
            var callId = Guid.NewGuid().ToString(); // Generate a unique identifier

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "deployment",
                Args = new[] {ticker},
                CallId = callId
            });
            var response = await completionSource.Task;
            return response as TapResponse<DeploymentResult>;
        }

        public async Task<TapResponse<DeploymentsLengthResult>> GetDeploymentsLengthAsync()
        {
            var callId = Guid.NewGuid().ToString(); // Generate a unique identifier

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "deploymentsLength",
                Args = new string[0],
                CallId = callId
            });
            var response = await completionSource.Task;
            return response as TapResponse<DeploymentsLengthResult>;
        }
    }
}