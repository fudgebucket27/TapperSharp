

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
            _client.On("error", response =>
            {
                Console.WriteLine($"Error {response}");
            });
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
                case "deployments":
                    var deploymentsResponse = JsonSerializer.Deserialize<TapResponse<List<DeploymentResult>>>(jsonResponseGeneric);
                    if (_responseCompletionSources.TryRemove(deploymentsResponse!.CallId!, out var deploymentsCompletionSource))
                    {
                        deploymentsCompletionSource.TrySetResult(deploymentsResponse);
                    }
                    break;
                case "mintTokensLeft":
                    var mintTokensLeftResponse = JsonSerializer.Deserialize<TapResponse<string>>(jsonResponseGeneric);
                    if (_responseCompletionSources.TryRemove(mintTokensLeftResponse!.CallId!, out var mintTokensLeftCompletionSource))
                    {
                        mintTokensLeftCompletionSource.TrySetResult(mintTokensLeftResponse);
                    }
                    break;
                default:
                    Console.WriteLine("Not valid!");
                    break;
            }
        }

        public async Task<TapResponse<DeploymentResult>?> GetDeploymentAsync(string ticker)
        {
            var callId = Guid.NewGuid().ToString(); 

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
            var callId = Guid.NewGuid().ToString();

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

        public async Task<TapResponse<List<DeploymentResult>>?> GetDeploymentsAsync(int offset, int max)
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "deployments",
                Args = new object[] { offset, max },
                CallId = callId
            });
            var response = await completionSource.Task;
            return response as TapResponse<List<DeploymentResult>>;
        }

        public async Task<TapResponse<string>?> GetMintTokensLeftAsync(string ticker)
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "mintTokensLeft",
                Args = new[] { ticker },
                CallId = callId
            });
            var response = await completionSource.Task;
            return response as TapResponse<string>;
        }
    }
}