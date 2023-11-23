﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapperSharp.Models;

namespace TapperSharp.Services
{
    public interface ITapperClient
    {
        /// <summary>
        /// Connect to TAP endpoint
        /// </summary>
        /// <returns></returns>
        Task ConnectAsync();

        /// <summary>
        /// Disconnect from TAP endpoint
        /// </summary>
        /// <returns></returns>
        Task DisconnectAsync();

        /// <summary>
        /// Get deployment of ticker passed in
        /// </summary>
        /// <param name="ticker">The ticker, ie "tap"</param>
        /// <returns>The deployment result for the ticker</returns>
        /// <exception cref="Exception">Thrown when an error occurs</exception>
        Task<TapResponse<DeploymentResult>?> GetDeploymentAsync(string ticker);

        /// <summary>
        /// Get the amount of deployments
        /// </summary>
        /// <returns>The amount of deployments on the network</returns>
        /// <exception cref="Exception">Thrown when an error occurs</exception>
        Task<TapResponse<long>?> GetDeploymentsLengthAsync();

        /// <summary>
        /// Get deployments based on an offset and max value. Max value limit is 500
        /// </summary>
        /// <param name="offset">The offset to start getting deployments from, ie 0</param>
        /// <param name="max">The max amount of deployments to get per request, ie 10. Limit is 500</param>
        /// <returns>The deployments</returns>
        /// <exception cref="Exception">Thrown when an error occurs</exception>
        Task<TapResponse<List<DeploymentResult>>?> GetDeploymentsAsync(int offset, int max);

        /// <summary>
        /// Get the amount of tokens left to mint for a ticker
        /// </summary>
        /// <param name="ticker">The ticker, ie "tap"</param>
        /// <returns>The amount of tokens left to mint for the ticker</returns>
        /// <exception cref="Exception">Thrown when an error occurs</exception>
        Task<TapResponse<string>?> GetMintTokensLeftAsync(string ticker);

        /// <summary>
        /// Get the holders for the given ticker, it will also return holders that once owned but no longer hold
        /// </summary>
        /// <param name="ticker">The ticker, ie "tap"</param>
        /// <returns>The amount of holders</returns>
        /// <exception cref="Exception">Thrown when an error occurs</exception>
        Task<TapResponse<long>?> GetHoldersLengthAsync(string ticker);

        /// <summary>
        /// Get holders based on a ticker, offset and max value. Max value limit is 500
        /// </summary>
        /// <param name="ticker">The ticker, ie "tap"</param>
        /// <param name="offset">The offset to start getting holders from, ie 0</param>
        /// <param name="max">The max amount of deployments to get per request, ie 10. Limit is 500</param>
        /// <returns>The holders</returns>
        /// <exception cref="Exception">Thrown when an error occurs</exception>
        Task<TapResponse<List<HoldersResult>>?> GetHoldersAsync(string ticker, int offset, int max);

        /// <summary>
        /// Get the amount of tokens held for the given address, it will also return tokens that the address once owned but no longer hold
        /// </summary>
        /// <param name="address">The address</param>
        /// <returns>The amount of account tokens owned by the address</returns>
        /// <exception cref="Exception">Thrown when an error occurs</exception>
        Task<TapResponse<long>?> GetAccountTokensLengthAsync(string address);


        /// <summary>
        /// Get the balance of a ticker for a given address, 
        /// Tokens that the address once owned but no longer hold are also returned
        /// </summary>
        /// <param name="address">The address</param>
        /// <param name="ticker">The ticker, ie "tap"</param>
        /// <returns>The balance in big integer format</returns>
        /// <exception cref="Exception">Thrown when an error occurs</exception>
        Task<TapResponse<string>?> GetBalanceAsync(string address, string ticker);

        /// <summary>
        /// Get account tokens based on a address, offset and max value. Max value limit is 500.
        /// Tokens once owned but no longer held are also returned.
        /// </summary>
        /// <param name="address">The address</param>
        /// <param name="offset">The offset to start getting tokens from, ie 0</param>
        /// <param name="max">The max amount of deployments to get per request, ie 10. Limit is 500</param>
        /// <returns>A list of tickers the address holds/held</returns>
        /// <exception cref="Exception">Thrown when an error occurs</exception>
        Task<TapResponse<List<string>>?> GetAccountTokensAsync(string address, int offset, int max);

        /// <summary>
        /// Get the amount of mints for a ticker by an address.
        /// </summary>
        /// <param name="address">The address</param>
        /// <param name="ticker">The ticker, ie "tap"</param>
        /// <returns>The amount of mints ever performed by the address for the ticker</returns>
        /// <exception cref="Exception">Thrown when an error occurs</exception>
        Task<TapResponse<long>?> GetAccountMintListLengthAsync(string address, string ticker);

        /// <summary>
        /// Get account mints for a token based on address, ticker and offset and max value. Max value limit is 500.
        /// Failed mints also show.
        /// </summary>
        /// <param name="address">The address</param>
        /// <param name="ticker">The ticker, ie "tap"<</param>
        /// <param name="offset">The offset to start getting data from, ie 0</param>
        /// <param name="max">The max amount of mint objects to get per request, ie 10. Limit is 500</param>
        /// <returns>The list of mints for the ticker</returns>
        /// <exception cref="Exception">Thrown when an error occurs</exception>
        Task<TapResponse<List<MintListResult>>?> GetAccountMintListAsync(string address, string ticker, int offset, int max);

        /// <summary>
        /// Get the amount of mints for a ticker. Inlcudes mints that failed.
        /// </summary>
        /// <param name="ticker">The ticker, ie "tap"</param>
        /// <returns>The amount of mints ever performed for the ticker</returns>
        /// <exception cref="Exception">Thrown when an error occurs</exception>
        Task<TapResponse<long>?> GetTickerMintListLengthAsync(string ticker);

        /// <summary>
        /// Get mints for a ticker based on it's ticker and offset and max value. Max value limit is 500.
        /// Failed mints also show.
        /// </summary>
        /// <param name="ticker">The ticker, ie "tap"<</param>
        /// <param name="offset">The offset to start getting data from, ie 0</param>
        /// <param name="max">The max amount of mint objects to get per request, ie 10. Limit is 500</param>
        /// <returns>The list of mints for the ticker</returns>
        /// <exception cref="Exception">Thrown when an error occurs</exception>
        Task<TapResponse<List<MintListResult>>?> GetTickerMintListAsync(string ticker, int offset, int max);

        /// <summary>
        /// Get amount of mints ever performed. Includes mints that failed
        /// Failed mints also show.
        /// </summary>
        /// <returns>The amount of mints ever performed</returns>
        /// <exception cref="Exception">Thrown when an error occurs</exception>
        Task<TapResponse<long>?> GetMintListLengthAsync();

        /// <summary>
        /// Get mints based on a offset and max value. Max value limit is 500.
        /// Failed mints also show.
        /// </summary>
        /// <param name="offset">The offset to start getting data from, ie 0</param>
        /// <param name="max">The max amount of mint objects to get per request, ie 10. Limit is 500</param>
        /// <returns>The list of mints</returns>
        /// <exception cref="Exception">Thrown when an error occurs</exception>
        Task<TapResponse<List<MintListResult>>?> GetMintListAsync(int offset, int max);

        /// <summary>
        /// Get the amount of transfer-inscribes for a ticker by an address.
        /// </summary>
        /// <param name="address">The address</param>
        /// <param name="ticker">The ticker, ie "tap"</param>
        /// <returns>The amount of transfer-inscribes</returns>
        /// <exception cref="Exception">Thrown when an error occurs</exception>
        Task<TapResponse<long>?> GetAccountTransferListLengthAsync(string address, string ticker);

        /// <summary>
        /// Get transfer list based on a offset and max value. Max value limit is 500.
        /// Failed transfers also show.
        /// </summary>
        /// <param name="offset">The offset to start getting data from, ie 0</param>
        /// <param name="max">The max amount of objects to get per request, ie 10. Limit is 500</param>
        /// <returns>The list of transfer-inscribe objects</returns>
        /// <exception cref="Exception">Thrown when an error occurs</exception>
        Task<TapResponse<List<TransferListResult>>?> GetTransferListAsync(int offset, int max);

        /// <summary>
        /// Get transfer list based on address, ticker, offset and max value. Max value limit is 500.
        /// Failed transfers also show.
        /// </summary>
        /// <param name="address">The address</param>
        /// <param name="ticker">The ticker, ie "tap".</param>
        /// <param name="offset">The offset to start getting data from, ie 0</param>
        /// <param name="max">The max amount of objects to get per request, ie 10. Limit is 500</param>
        /// <returns>The list of transfer-inscribe objects</returns>
        /// <exception cref="Exception">Thrown when an error occurs</exception>
        Task<TapResponse<List<TransferListResult>>?> GetAccountTransferListAsync(string address, string ticker, int offset, int max);

        /// <summary>
        /// Get transfer list based on a ticker, offset and max value. Max value limit is 500.
        /// Failed transfer also show.
        /// </summary>
        /// <param name="ticker">The ticker ie, "tap"</param>
        /// <param name="offset">The offset to start getting data from, ie 0</param>
        /// <param name="max">The max amount of objects to get per request, ie 10. Limit is 500</param>
        /// <returns>The list of transfer-inscribe objects</returns>
        /// <exception cref="Exception">Thrown when an error occurs</exception>
        Task<TapResponse<List<TransferListResult>>?> GetTickerTransferListAsync(string ticker, int offset, int max);

        /// <summary>
        /// Get the amount of transfer-inscribes for a ticker
        /// </summary>
        /// <param name="ticker">The ticker, ie "tap"</param>
        /// <returns>The amount of transfer-inscribes</returns>
        /// <exception cref="Exception">Thrown when an error occurs</exception>
        Task<TapResponse<long>?> GetTickerTransferListLengthAsync(string ticker);
    }
}
