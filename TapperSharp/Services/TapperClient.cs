

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
                var jsonResponseBaseString = JsonSerializer.Serialize(response.GetValue<TapErrorResponse>());
                var jsonResponseBaseObject = JsonSerializer.Deserialize<TapErrorResponse>(jsonResponseBaseString);
                if (_responseCompletionSources.TryRemove(jsonResponseBaseObject!.Cmd!.CallId!, out var completionSource))
                {
                    var errorException = new Exception($"Error received: {jsonResponseBaseString}");
                    completionSource.TrySetException(errorException);
                }
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

        /// <inheritdoc/>
        public async Task ConnectAsync()
        {
            if (!_client.Connected)
            {
                await _client.ConnectAsync();
            }
        }

        /// <inheritdoc/>
        public async Task DisconnectAsync()
        {
            if (_client.Connected)
            {
                await _client.DisconnectAsync();
            }
        }

        /// <summary>
        /// Handles the response type from server. Use to set the completion sources concurrent dictionary
        /// </summary>
        /// <param name="func">The function</param>
        /// <param name="jsonResponseGeneric">The json response from the server</param>
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
                    if (_responseCompletionSources.TryRemove(deploymentsLengthResponse!.CallId!, out var deploymentLengthCompletionSource))
                    {
                        deploymentLengthCompletionSource.TrySetResult(deploymentsLengthResponse);
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
                case "holdersLength":
                    var holdersLengthResponse = JsonSerializer.Deserialize<TapResponse<int>>(jsonResponseGeneric);
                    if (_responseCompletionSources.TryRemove(holdersLengthResponse!.CallId!, out var holdersLengthCompletionSource))
                    {
                        holdersLengthCompletionSource.TrySetResult(holdersLengthResponse);
                    }
                    break;
                case "holders":
                    var holdersResponse = JsonSerializer.Deserialize<TapResponse<List<HoldersResult>>>(jsonResponseGeneric);
                    if (_responseCompletionSources.TryRemove(holdersResponse!.CallId!, out var holdersCompletionSource))
                    {
                        holdersCompletionSource.TrySetResult(holdersResponse);
                    }
                    break;
                case "accountTokensLength":
                    var accountTokensLengthResponse = JsonSerializer.Deserialize<TapResponse<int>>(jsonResponseGeneric);
                    if (_responseCompletionSources.TryRemove(accountTokensLengthResponse!.CallId!, out var accountTokensLengthCompletionSource))
                    {
                        accountTokensLengthCompletionSource.TrySetResult(accountTokensLengthResponse);
                    }
                    break;
                case "balance":
                    var balanceResponse = JsonSerializer.Deserialize<TapResponse<string>>(jsonResponseGeneric);
                    if (_responseCompletionSources.TryRemove(balanceResponse!.CallId!, out var balanceCompletionSource))
                    {
                        balanceCompletionSource.TrySetResult(balanceResponse);
                    }
                    break;
                case "transferable":
                    var transferableResponse = JsonSerializer.Deserialize<TapResponse<string>>(jsonResponseGeneric);
                    if (_responseCompletionSources.TryRemove(transferableResponse!.CallId!, out var transferableCompletionSource))
                    {
                        transferableCompletionSource.TrySetResult(transferableResponse);
                    }
                    break;
                case "accountTokens":
                    var accountTokensResponse = JsonSerializer.Deserialize<TapResponse<List<string>>>(jsonResponseGeneric);
                    if (_responseCompletionSources.TryRemove(accountTokensResponse!.CallId!, out var accountTokensCompletionSource))
                    {
                        accountTokensCompletionSource.TrySetResult(accountTokensResponse);
                    }
                    break;
                case "accountMintListLength":
                    var accountMintListLengthResponse = JsonSerializer.Deserialize<TapResponse<long>>(jsonResponseGeneric);
                    if (_responseCompletionSources.TryRemove(accountMintListLengthResponse!.CallId!, out var accountMintListLengthCompletionSource))
                    {
                        accountMintListLengthCompletionSource.TrySetResult(accountMintListLengthResponse);
                    }
                    break;
                case "accountMintList":
                    var accountMintListResponse = JsonSerializer.Deserialize<TapResponse<List<AccountMintListResult>>>(jsonResponseGeneric);
                    if (_responseCompletionSources.TryRemove(accountMintListResponse!.CallId!, out var accountMintListCompletionSource))
                    {
                        accountMintListCompletionSource.TrySetResult(accountMintListResponse);
                    }
                    break;
                case "tickerMintListLength":
                    var tickerMintListLengthResponse = JsonSerializer.Deserialize<TapResponse<long>>(jsonResponseGeneric);
                    if (_responseCompletionSources.TryRemove(tickerMintListLengthResponse!.CallId!, out var tickerMintListLengthCompletionSource))
                    {
                        tickerMintListLengthCompletionSource.TrySetResult(tickerMintListLengthResponse);
                    }
                    break;
                default:
                    Console.WriteLine("Not valid!");
                    break;
            }
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public async Task<TapResponse<int>?> GetHoldersLengthAsync(string ticker)
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "holdersLength",
                Args = new[] { ticker },
                CallId = callId
            });
            var response = await completionSource.Task;
            return response as TapResponse<int>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<List<HoldersResult>>?> GetHoldersAsync(string ticker, int offset, int max)
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "holders",
                Args = new object[] { ticker, offset, max },
                CallId = callId
            });
            var response = await completionSource.Task;
            return response as TapResponse<List<HoldersResult>>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<int>?> GetAccountTokensLengthAsync(string address)
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "accountTokensLength",
                Args = new[] { address },
                CallId = callId
            });
            var response = await completionSource.Task;
            return response as TapResponse<int>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<string>?> GetBalanceAsync(string address, string ticker)
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "balance",
                Args = new[] { address, ticker },
                CallId = callId
            });
            var response = await completionSource.Task;
            return response as TapResponse<string>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<string>?> GetTransferableAsync(string address, string ticker)
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "transferable",
                Args = new[] { address, ticker },
                CallId = callId
            });
            var response = await completionSource.Task;
            return response as TapResponse<string>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<List<string>>?> GetAccountTokensAsync(string address, int offset, int max)
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "accountTokens",
                Args = new object[] { address, offset, max },
                CallId = callId
            });
            var response = await completionSource.Task;
            return response as TapResponse<List<string>>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<long>?> GetAccountMintListLengthAsync(string address, string ticker)
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "accountMintListLength",
                Args = new[] { address, ticker },
                CallId = callId
            });
            var response = await completionSource.Task;
            return response as TapResponse<long>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<List<AccountMintListResult>>?> GetAccountMintListAsync(string address, string ticker, int offset, int max)
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "accountMintList",
                Args = new object[] { address, ticker, offset, max},
                CallId = callId
            });
            var response = await completionSource.Task;
            return response as TapResponse<List<AccountMintListResult>>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<long>?> GetTickerMintListLengthAsync(string ticker)
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "tickerMintListLength",
                Args = new object[] { ticker },
                CallId = callId
            });
            var response = await completionSource.Task;
            return response as TapResponse<long>;
        }
    }
}