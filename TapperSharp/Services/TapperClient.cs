

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
                var jsonResponseGenericString = JsonSerializer.Serialize(response.GetValue<object>());
                var func = jsonResponseBaseObject!.Func!;
                HandleResponseType(func, jsonResponseGenericString);
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

        private void HandleResponseType(string func, string jsonResponseGeneric)
        {
            switch (func)
            {
                case "deployment":
                    var deploymentResponse = JsonSerializer.Deserialize<TapResponse<DeploymentResult>>(jsonResponseGeneric);
                    if (_responseCompletionSources.TryRemove(deploymentResponse!.CallId!, out var deploymentCompletionSource))
                    {
                        deploymentCompletionSource.TrySetResult(deploymentResponse);
                    }
                    break;

                case "deploymentsLength":
                    var deploymentsLengthResponse = JsonSerializer.Deserialize<TapResponse<int>>(jsonResponseGeneric);
                    if (_responseCompletionSources.TryRemove(deploymentsLengthResponse!.CallId!, out var deploymentLengthsCompletionSource))
                    {
                        deploymentLengthsCompletionSource.TrySetResult(deploymentsLengthResponse);
                    }
                    break;
                default:
                    Console.WriteLine("Not valid!");
                    break;
            }
        }

        public async Task<TapResponse<DeploymentResult>?> GetDeploymentAsync(string ticker)
        {
            var callId = Guid.NewGuid().ToString(); // Generate a unique identifier

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "deployment",
                Args = new[] { ticker },
                CallId = callId
            });
            var response = await completionSource.Task;
            return response as TapResponse<DeploymentResult>;
        }

        public async Task<TapResponse<int>?> GetDeploymentsLengthAsync()
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
            return response as TapResponse<int>;
        }
    }
}