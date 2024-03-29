﻿

using SocketIO;
using SocketIOClient;
using System.Collections.Concurrent;
using System.Net;
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
        /// <param name="jsonResponse">The json response from the server</param>
        private void HandleResponseType(string func, string jsonResponse)
        {
            var jsonResponseBase = JsonSerializer.Deserialize<TapResponseBase>(jsonResponse);
            if (jsonResponseBase == null || string.IsNullOrEmpty(jsonResponseBase.CallId))
            {
                // Handle error: Invalid response or missing CallId
                return;
            }
            switch (func)
            {
                case "deployment":
                    HandleGenericResponse<DeploymentResult>(jsonResponse, jsonResponseBase.CallId);
                    break;
                case "deployments":
                    HandleGenericResponse<List<DeploymentResult>>(jsonResponse, jsonResponseBase.CallId);
                    break;
                case "deploymentsLength":
                case "holdersLength":
                case "accountTokensLength":
                case "accountMintListLength":
                case "tickerMintListLength":
                case "mintListLength":
                case "accountTransferListLength":
                case "tickerTransferListLength":
                case "transferListLength":
                case "accountSentListLength":
                case "tickerSentListLength":
                case "sentListLength":
                case "accountReceiveListLength":
                case "accountAccumulatorListLength":
                case "accumulatorListLength":
                case "tradesListLength":
                case "tickerTradesListLength":
                case "accountTradesListLength":
                case "tradesFilledListLength":
                case "tickerTradesFilledListLength":
                case "accountTradesFilledListLength":
                case "accountReceiveTradesFilledListLength":
                case "authListLength":
                case "accountAuthListLength":
                case "redeemListLength":
                case "accountRedeemListLength":
                    HandleGenericResponse<long?>(jsonResponse, jsonResponseBase.CallId);
                    break;          
                case "holders":
                    HandleGenericResponse<List<HoldersResult>>(jsonResponse, jsonResponseBase.CallId);
                    break;
                case "mintTokensLeft":
                case "transferable":
                case "balance":
                    HandleGenericResponse<string>(jsonResponse, jsonResponseBase.CallId);
                    break;
                case "authCancelled":
                case "authHashExists":
                    HandleGenericResponse<bool>(jsonResponse, jsonResponseBase.CallId);
                    break;
                case "accountTokens":
                    HandleGenericResponse<List<string>>(jsonResponse, jsonResponseBase.CallId);
                    break;               
                case "accountMintList":
                case "tickerMintList":
                case "mintList":
                    HandleGenericResponse<List<MintListResult>>(jsonResponse, jsonResponseBase.CallId);
                    break;  
                case "transferList":
                case "tickerTransferList":
                case "accountTransferList":
                    HandleGenericResponse<List<TransferListResult>>(jsonResponse, jsonResponseBase.CallId);
                    break;
                case "accountSentList":
                case "tickerSentList":
                case "sentList":
                    HandleGenericResponse<List<SendListResult>>(jsonResponse, jsonResponseBase.CallId);
                    break;
                case "accountReceiveList":
                    HandleGenericResponse<List<AccountRecieveListResult>>(jsonResponse, jsonResponseBase.CallId);
                    break;
                case "accumulatorList":
                case "accountAccumulatorList":
                    HandleGenericResponse<List<AccumulatorListResult>>(jsonResponse, jsonResponseBase.CallId);
                    break;
                case "accumulator":
                    HandleGenericResponse<AccumulatorListResult>(jsonResponse, jsonResponseBase.CallId);
                    break;
                case "tradesList":
                case "tickerTradesList":
                case "accountTradesList":
                    HandleGenericResponse<List<TradeListResult>>(jsonResponse, jsonResponseBase.CallId);
                    break;
                case "trade":
                    HandleGenericResponse<TradeResult>(jsonResponse, jsonResponseBase.CallId);
                    break;
                case "tradesFilledList":
                case "tickerTradesFilledList":
                case "accountTradesFilledList":
                    HandleGenericResponse<List<TradesFilledListResult>>(jsonResponse, jsonResponseBase.CallId);
                    break;
                case "accountReceiveTradesFilledList":
                    HandleGenericResponse<List<AccountRecieveTradesFilledListResult>>(jsonResponse, jsonResponseBase.CallId);
                    break;
                case "authList":
                case "accountAuthList":
                    HandleGenericResponse<List<AuthListResult>>(jsonResponse, jsonResponseBase.CallId);
                    break;
                case "redeemList":
                case "accountRedeemList":
                    HandleGenericResponse<List<RedeemListResult>>(jsonResponse, jsonResponseBase.CallId);
                    break;
                default:
                    Console.WriteLine("Unhandled function type: " + func);
                    Console.WriteLine("Response: " + jsonResponse);
                    break;
            }
        }

        /// <summary>
        /// Handles the generic response and sets the completion soruce
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonResponse"></param>
        /// <param name="callId"></param>
        private void HandleGenericResponse<T>(string jsonResponse, string callId)
        {
            var response = JsonSerializer.Deserialize<TapResponse<T>>(jsonResponse);
            if (response != null && _responseCompletionSources.TryRemove(callId, out var completionSource))
            {
                completionSource.TrySetResult(response);
            }
            else
            {
                // TO DO: Handle the case where deserialization fails or callId is not found
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
        public async Task<TapResponse<long?>?> GetDeploymentsLengthAsync()
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
            return response as TapResponse<long?>;
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
        public async Task<TapResponse<long?>?> GetHoldersLengthAsync(string ticker)
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
            return response as TapResponse<long?>;
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
        public async Task<TapResponse<long?>?> GetAccountTokensLengthAsync(string address)
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
            return response as TapResponse<long?>;
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
        public async Task<TapResponse<long?>?> GetAccountMintListLengthAsync(string address, string ticker)
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
            return response as TapResponse<long?>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<List<MintListResult>>?> GetAccountMintListAsync(string address, string ticker, int offset, int max)
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
            return response as TapResponse<List<MintListResult>>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<long?>?> GetTickerMintListLengthAsync(string ticker)
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
            return response as TapResponse<long?>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<List<MintListResult>>?> GetTickerMintListAsync(string ticker, int offset, int max)
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "tickerMintList",
                Args = new object[] { ticker, offset, max },
                CallId = callId
            });
            var response = await completionSource.Task;
            return response as TapResponse<List<MintListResult>>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<long?>?> GetMintListLengthAsync()
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "mintListLength",
                Args = new string[0],
                CallId = callId
            });
            var response = await completionSource.Task;
            return response as TapResponse<long?>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<List<MintListResult>>?> GetMintListAsync(int offset, int max)
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "mintList",
                Args = new object[] {offset, max },
                CallId = callId
            });
            var response = await completionSource.Task;
            return response as TapResponse<List<MintListResult>>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<long?>?> GetAccountTransferListLengthAsync(string address, string ticker)
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "accountTransferListLength",
                Args = new string[] {address , ticker},
                CallId = callId
            });
            var response = await completionSource.Task;
            return response as TapResponse<long?>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<List<TransferListResult>>?> GetTransferListAsync(int offset, int max)
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "transferList",
                Args = new object[] { offset, max },
                CallId = callId
            });
            var response = await completionSource.Task;
            return response as TapResponse<List<TransferListResult>>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<List<TransferListResult>>?> GetTickerTransferListAsync(string ticker, int offset, int max)
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "tickerTransferList",
                Args = new object[] { ticker, offset, max },
                CallId = callId
            });
            var response = await completionSource.Task;
            return response as TapResponse<List<TransferListResult>>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<long?>?> GetTickerTransferListLengthAsync(string ticker)
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "tickerTransferListLength",
                Args = new string[] { ticker },
                CallId = callId
            });
            var response = await completionSource.Task;
            return response as TapResponse<long?>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<List<TransferListResult>>?> GetAccountTransferListAsync(string address, string ticker, int offset, int max)
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "accountTransferList",
                Args = new object[] { address, ticker, offset, max },
                CallId = callId
            });
            var response = await completionSource.Task;
            return response as TapResponse<List<TransferListResult>>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<long?>?> GetTransferListLengthAsync()
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "transferListLength",
                Args = new string[0],
                CallId = callId
            });
            var response = await completionSource.Task;
            return response as TapResponse<long?>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<long?>?> GetAccountSentListLengthAsync(string address, string ticker)
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "accountSentListLength",
                Args = new object[] { address, ticker },
                CallId = callId
            });
            var response = await completionSource.Task;
            return response as TapResponse<long?>;
        }
        
        /// <inheritdoc/>
        public async Task<TapResponse<List<SendListResult>>?> GetAccountSentListAsync(string address, string ticker, int offset, int max)
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "accountSentList",
                Args = new object[] { address, ticker, offset, max },
                CallId = callId
            });
            var response = await completionSource.Task;
            return response as TapResponse<List<SendListResult>>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<long?>?> GetTickerSentListLengthAsync(string ticker)
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "tickerSentListLength",
                Args = new object[] { ticker },
                CallId = callId
            });
            var response = await completionSource.Task;
            return response as TapResponse<long?>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<List<SendListResult>>?> GetTickerSentListAsync(string ticker, int offset, int max)
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "tickerSentList",
                Args = new object[] { ticker, offset, max },
                CallId = callId
            });
            var response = await completionSource.Task;
            return response as TapResponse<List<SendListResult>>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<long?>?> GetSentListLengthAsync()
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "sentListLength",
                Args = new string[0],
                CallId = callId
            });
            var response = await completionSource.Task;
            return response as TapResponse<long?>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<List<SendListResult>>?> GetSentListAsync(int offset, int max)
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "sentList",
                Args = new object[] { offset, max },
                CallId = callId
            });
            var response = await completionSource.Task;
            return response as TapResponse<List<SendListResult>>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<long?>?> GetAccountReceiveListLengthAsync(string address, string ticker)
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "accountReceiveListLength",
                Args = new object[] { address, ticker },
                CallId = callId
            });
            var response = await completionSource.Task;
            return response as TapResponse<long?>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<List<AccountRecieveListResult>>?> GetAccountReceiveListAsync(string address, string ticker, int offset, int max)
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "accountReceiveList",
                Args = new object[] { address, ticker, offset, max },
                CallId = callId
            });
            var response = await completionSource.Task;
            return response as TapResponse<List<AccountRecieveListResult>>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<AccumulatorListResult>?> GetAccumulatorAsync(string inscriptionId)
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "accumulator",
                Args = new object[] { inscriptionId },
                CallId = callId
            });
            var response = await completionSource.Task;
            return response as TapResponse<AccumulatorListResult>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<List<AccumulatorListResult>>?> GetAccumulatorListAsync(int offset, int max)
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "accumulatorList",
                Args = new object[] { offset, max },
                CallId = callId
            });
            var response = await completionSource.Task;
            return response as TapResponse<List<AccumulatorListResult>>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<long?>?> GetAccountAccumulatorListLengthAsync(string address)
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "accountAccumulatorListLength",
                Args = new object[] {address},
                CallId = callId
            });
            var response = await completionSource.Task;
            return response as TapResponse<long?>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<long?>?> GetAccumulatorListLengthAsync()
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "accumulatorListLength",
                Args = new string[0],
                CallId = callId
            });
            var response = await completionSource.Task;
            return response as TapResponse<long?>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<List<AccumulatorListResult>>?> GetAccountAccumulatorListAsync(string address, int offset, int max)
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "accountAccumulatorList",
                Args = new object[] { address, offset, max },
                CallId = callId
            });
            var response = await completionSource.Task;
            return response as TapResponse<List<AccumulatorListResult>>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<long?>?> GetTradesListLengthAsync()
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "tradesListLength",
                Args = new string[0],
                CallId = callId
            });
            var response = await completionSource.Task;
            return response as TapResponse<long?>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<List<TradeListResult>>?> GetTradesListAsync(int offset, int max)
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "tradesList",
                Args = new object[] { offset, max },
                CallId = callId
            });
            var response = await completionSource.Task;
            return response as TapResponse<List<TradeListResult>>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<long?>?> GetTickerTradesListLengthAsync(string ticker)
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "tickerTradesListLength",
                Args = new object[] { ticker},
                CallId = callId
            });
            var response = await completionSource.Task;
            return response as TapResponse<long?>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<List<TradeListResult>>?> GetTickerTradesListAsync(string ticker, int offset, int max)
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "tickerTradesList",
                Args = new object[] { ticker, offset, max },
                CallId = callId
            });
            var response = await completionSource.Task;
            return response as TapResponse<List<TradeListResult>>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<long?>?> GetAccountTradesListLengthAsync(string address, string ticker)
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "accountTradesListLength",
                Args = new object[] { address, ticker },
                CallId = callId
            });
            var response = await completionSource.Task;
            return response as TapResponse<long?>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<List<TradeListResult>>?> GetAccountTradesListAsync(string address, string ticker, int offset, int max)
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "accountTradesList",
                Args = new object[] { address, ticker, offset, max },
                CallId = callId
            });
            var response = await completionSource.Task;
            return response as TapResponse<List<TradeListResult>>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<TradeResult>?> GetTradeAsync(string inscriptionId)
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "trade",
                Args = new object[] { inscriptionId },
                CallId = callId
            });
            var response = await completionSource.Task;
            return response as TapResponse<TradeResult>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<long?>?> GetTradesFilledListLengthAsync()
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "tradesFilledListLength",
                Args = new string[0] ,
                CallId = callId
            });
            var response = await completionSource.Task;
            return response as TapResponse<long?>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<List<TradesFilledListResult>>?> GetTradesFilledListAsync(int offset, int max)
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "tradesFilledList",
                Args = new object[] { offset, max },
                CallId = callId
            });
            var response = await completionSource.Task;
            return response as TapResponse<List<TradesFilledListResult>>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<long?>?> GetTickerTradesFilledListLengthAsync(string ticker)
        {

            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "tickerTradesFilledListLength",
                Args = new object[] { ticker },
                CallId = callId
            });
            var response = await completionSource.Task;
            return response as TapResponse<long?>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<List<TradesFilledListResult>>?> GetTickerTradesFilledListAsync(string ticker, int offset, int max)
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "tickerTradesFilledList",
                Args = new object[] { ticker, offset, max },
                CallId = callId
            });
            var response = await completionSource.Task;
            return response as TapResponse<List<TradesFilledListResult>>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<long?>?> GetAccountTradesFilledListLengthAsync(string address, string ticker)
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "accountTradesFilledListLength",
                Args = new object[] { address, ticker },
                CallId = callId
            });
            var response = await completionSource.Task;
            return response as TapResponse<long?>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<List<TradesFilledListResult>>?> GetAccountTradesFilledListAsync(string address, string ticker, int offset, int max)
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "accountTradesFilledList",
                Args = new object[] { address, ticker, offset, max },
                CallId = callId
            });
            var response = await completionSource.Task;
            return response as TapResponse<List<TradesFilledListResult>>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<long?>?> GetAccountReceiveTradesFilledListLengthAsync(string address, string ticker)
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "accountReceiveTradesFilledListLength",
                Args = new object[] { address, ticker },
                CallId = callId
            });
            var response = await completionSource.Task;
            return response as TapResponse<long?>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<List<AccountRecieveTradesFilledListResult>>?> GetAccountReceiveTradesFilledListAsync(string address, string ticker, int offset, int max)
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "accountReceiveTradesFilledList",
                Args = new object[] { address, ticker, offset, max },
                CallId = callId
            });
            var response = await completionSource.Task;
            return response as TapResponse<List<AccountRecieveTradesFilledListResult>>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<long?>?> GetAuthListLengthAsync()
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "authListLength",
                Args = new string[0],
                CallId = callId
            });

            var response = await completionSource.Task;
            return response as TapResponse<long?>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<List<AuthListResult>>?> GetAuthListAsync(int offset, int max)
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "authList",
                Args = new object[] {offset, max},
                CallId = callId
            });

            var response = await completionSource.Task;
            return response as TapResponse<List<AuthListResult>>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<long?>?> GetAccountAuthListLengthAsync(string address)
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "accountAuthListLength",
                Args = new object[] {address},
                CallId = callId
            });

            var response = await completionSource.Task;
            return response as TapResponse<long?>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<List<AuthListResult>>?> GetAccountAuthListAsync(string address, int offset, int max)
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "accountAuthList",
                Args = new object[] {address, offset, max },
                CallId = callId
            });

            var response = await completionSource.Task;
            return response as TapResponse<List<AuthListResult>>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<bool>?> GetAuthCancelledAsync(string inscriptionId)
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "authCancelled",
                Args = new object[] { inscriptionId },
                CallId = callId
            });

            var response = await completionSource.Task;
            return response as TapResponse<bool>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<bool>?> GetAuthHashExistsAsync(string signatureHash)
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "authHashExists",
                Args = new object[] { signatureHash },
                CallId = callId
            });

            var response = await completionSource.Task;
            return response as TapResponse<bool>;
        }

        /// <inheritdoc/>
        public async Task<TapResponse<long?>?> GetRedeemListLengthAsync()
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "redeemListLength",
                Args = new string[0],
                CallId = callId
            });

            var response = await completionSource.Task;
            return response as TapResponse<long?>;
        }

        public async Task<TapResponse<List<RedeemListResult>>?> GetRedeemListAsync(int offset, int max)
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "redeemList",
                Args = new object[] {offset, max},
                CallId = callId
            });

            var response = await completionSource.Task;
            return response as TapResponse<List<RedeemListResult>>;
        }

        public async Task<TapResponse<long?>?> GetAccountRedeemListLengthAsync(string address)
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "accountRedeemListLength",
                Args = new object[] {address},
                CallId = callId
            });

            var response = await completionSource.Task;
            return response as TapResponse<long?>;
        }

        public async Task<TapResponse<List<RedeemListResult>>?> GetAccountRedeemListAsync(string address, int offset, int max)
        {
            var callId = Guid.NewGuid().ToString();

            var completionSource = new TaskCompletionSource<object>();
            _responseCompletionSources[callId] = completionSource;

            await _client.EmitAsync("get", new TapRequest()
            {
                Func = "accountRedeemList",
                Args = new object[] { address, offset, max },
                CallId = callId
            });

            var response = await completionSource.Task;
            return response as TapResponse<List<RedeemListResult>>;
        }
    }    
}