using System;
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
        Task<TapResponse<long?>?> GetDeploymentsLengthAsync();

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
        /// Get the holders for the given ticker, it will also return holders that once owned but no long?er hold
        /// </summary>
        /// <param name="ticker">The ticker, ie "tap"</param>
        /// <returns>The amount of holders</returns>
        /// <exception cref="Exception">Thrown when an error occurs</exception>
        Task<TapResponse<long?>?> GetHoldersLengthAsync(string ticker);

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
        /// Get the amount of tokens held for the given address, it will also return tokens that the address once owned but no long?er hold
        /// </summary>
        /// <param name="address">The address</param>
        /// <returns>The amount of account tokens owned by the address</returns>
        /// <exception cref="Exception">Thrown when an error occurs</exception>
        Task<TapResponse<long?>?> GetAccountTokensLengthAsync(string address);


        /// <summary>
        /// Get the balance of a ticker for a given address, 
        /// Tokens that the address once owned but no long?er hold are also returned
        /// </summary>
        /// <param name="address">The address</param>
        /// <param name="ticker">The ticker, ie "tap"</param>
        /// <returns>The balance in big integer format</returns>
        /// <exception cref="Exception">Thrown when an error occurs</exception>
        Task<TapResponse<string>?> GetBalanceAsync(string address, string ticker);

        /// <summary>
        /// Get account tokens based on a address, offset and max value. Max value limit is 500.
        /// Tokens once owned but no long?er held are also returned.
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
        Task<TapResponse<long?>?> GetAccountMintListLengthAsync(string address, string ticker);

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
        Task<TapResponse<long?>?> GetTickerMintListLengthAsync(string ticker);

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
        Task<TapResponse<long?>?> GetMintListLengthAsync();

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
        Task<TapResponse<long?>?> GetAccountTransferListLengthAsync(string address, string ticker);

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
        Task<TapResponse<long?>?> GetTickerTransferListLengthAsync(string ticker);

        /// <summary>
        /// Get amount of transfer-inscribes ever performed. Includes failed
        /// </summary>
        /// <returns>The amount of transfer-inscribes ever performed</returns>
        /// <exception cref="Exception">Thrown when an error occurs</exception>
        Task<TapResponse<long?>?> GetTransferListLengthAsync();

        /// <summary>
        /// Get amount sent based on address and ticker
        /// Failed sends also show.
        /// </summary>
        /// <param name="address">The address</param>
        /// <param name="ticker">The ticker, ie "tap".</param>
        /// <returns>The amount of sends</returns>
        /// <exception cref="Exception">Thrown when an error occurs</exception>
        Task<TapResponse<long?>?> GetAccountSentListLengthAsync(string address, string ticker);

        /// <summary>
        /// Get sent list based on address, ticker,offset and max value. Max value limit is 500.
        /// Failed sends also show.
        /// </summary>
        /// <param name="address">The address</param>
        /// <param name="ticker">The ticker, ie "tap".</param>
        /// <param name="offset">The offset to start getting data from, ie 0</param>
        /// <param name="max">The max amount of objects to get per request, ie 10. Limit is 500</param>
        /// <returns>The send list</returns>
        /// <exception cref="Exception">Thrown when an error occurs</exception>
        Task<TapResponse<List<SendListResult>>?> GetAccountSentListAsync(string address, string ticker, int offset, int max);

        /// <summary>
        /// Get amount sent based on ticker
        /// Failed sends also show.
        /// </summary>
        /// <param name="ticker">The ticker, ie "tap".</param>
        /// <returns>The amount of sends</returns>
        /// <exception cref="Exception">Thrown when an error occurs</exception>
        Task<TapResponse<long?>?> GetTickerSentListLengthAsync(string ticker);

        /// <summary>
        /// Get sent list based on ticker,offset and max value. Max value limit is 500.
        /// Failed sends also show.
        /// </summary>
        /// <param name="ticker">The ticker, ie "tap".</param>
        /// <param name="offset">The offset to start getting data from, ie 0</param>
        /// <param name="max">The max amount of objects to get per request, ie 10. Limit is 500</param>
        /// <returns>The send list</returns>
        /// <exception cref="Exception">Thrown when an error occurs</exception>
        Task<TapResponse<List<SendListResult>>?> GetTickerSentListAsync(string ticker, int offset, int max);

        /// <summary>
        /// Get amount of sends ever performed. Includes failed
        /// </summary>
        /// <returns>The amount of sends ever performed</returns>
        /// <exception cref="Exception">Thrown when an error occurs</exception>
        Task<TapResponse<long?>?> GetSentListLengthAsync();

        /// <summary>
        /// Get sent list based on offset and max value. Max value limit is 500.
        /// Failed sends also show.
        /// </summary>
        /// <param name="offset">The offset to start getting data from, ie 0</param>
        /// <param name="max">The max amount of objects to get per request, ie 10. Limit is 500</param>
        /// <returns>The send list</returns>
        /// <exception cref="Exception">Thrown when an error occurs</exception>
        Task<TapResponse<List<SendListResult>>?> GetSentListAsync(int offset, int max);

        /// <summary>
        /// Get amount of tokens ever recieved based on address and ticker
        /// Failed recieved also show.
        /// </summary>
        /// <param name="address">The address</param>
        /// <param name="ticker">The ticker</param>
        /// <returns>The amount of tokens recieved</returns>
        /// <exception cref="Exception">Thrown when an error occurs</exception>
        Task<TapResponse<long?>?> GetAccountReceiveListLengthAsync(string address, string ticker);

        /// <summary>
        /// Get tokens recieved based on address, ticker,offset and max value. Max value limit is 500.
        /// Failed recieved also show.
        /// </summary>
        /// <param name="address">The address</param>
        /// <param name="ticker">The ticker</param>
        /// <param name="offset">The offset to start getting data from, ie 0</param>
        /// <param name="max">The max amount of objects to get per request, ie 10. Limit is 500</param>
        /// <returns>The tokens recieved</returns>
        /// <exception cref="Exception">Thrown when an error occurs</exception>
        Task<TapResponse<List<AccountRecieveListResult>>?> GetAccountReceiveListAsync(string address, string ticker, int offset, int max);


        /// <summary>
        /// Get accumlator object for internal functions, including the inscribed json object
        /// Returns null if the accumulator object doesn't exist.
        /// </summary>
        /// <param name="inscriptionId">The inscription id</param>
        /// <returns>The accumulator</returns>
        /// <exception cref="Exception">Thrown when an error occurs</exception>
        Task<TapResponse<AccumulatorListResult>?> GetAccumulatorAsync(string inscriptionId);

        /// <summary>
        /// Get accumulator list based on offset and max value. Max value limit is 500.
        /// Items  that may have been tapped already are shown
        /// </summary>
        /// <param name="offset">The offset to start getting data from, ie 0</param>
        /// <param name="max">The max amount of objects to get per request, ie 10. Limit is 500</param>
        /// <returns>The accumulator list</returns>
        /// <exception cref="Exception">Thrown when an error occurs</exception>
        Task<TapResponse<List<AccumulatorListResult>>?> GetAccumulatorListAsync(int offset, int max);

        /// <summary>
        /// Get amount of accumulated items based on address
        /// Items that may have been tapped already are included
        /// </summary>
        /// <param name="address">The address</param>
        /// <returns>The amount of accumulated items</returns>
        /// <exception cref="Exception">Thrown when an error occurs</exception>
        Task<TapResponse<long?>?> GetAccountAccumulatorListLengthAsync(string address);

        /// <summary>
        /// Get amount of accumulated items across all address
        /// Items that may have been tapped already are included
        /// </summary>
        /// <returns>The amount of accumulated items</returns>
        /// <exception cref="Exception">Thrown when an error occurs</exception>
        Task<TapResponse<long?>?> GetAccumulatorListLengthAsync();

        /// <summary>
        /// Get accumulator list based on address, offset and max value. Max value limit is 500.
        /// Items  that may have been tapped already are shown
        /// </summary>
        /// <param name="address">The address</param>
        /// <param name="offset">The offset to start getting data from, ie 0</param>
        /// <param name="max">The max amount of objects to get per request, ie 10. Limit is 500</param>
        /// <returns>The accumulator list</returns>
        /// <exception cref="Exception">Thrown when an error occurs</exception>
        Task<TapResponse<List<AccumulatorListResult>>?> GetAccountAccumulatorListAsync(string address, int offset, int max);


        /// <summary>
        /// Get amount of trades
        /// </summary>
        /// <returns>The amount of trades</returns>
        /// <exception cref="Exception">Thrown when an error occurs</exception>
        Task<TapResponse<long?>?> GetTradesListLengthAsync();

        /// <summary>
        /// Get trade list based on offset and max value. Max value limit is 500.
        /// </summary>
        /// <param name="offset">The offset to start getting data from, ie 0</param>
        /// <param name="max">The max amount of objects to get per request, ie 10. Limit is 500</param>
        /// <returns>The trade list</returns>
        /// <exception cref="Exception">Thrown when an error occurs</exception>
        Task<TapResponse<List<TradeListResult>>?> GetTradesListAsync(int offset, int max);

        /// <summary>
        /// Get trades based on ticker
        /// </summary>
        /// <param name="ticker">The ticker, ie "tap"</param>
        /// <returns>The trades</returns>
        /// <exception cref="Exception">Thrown when an error occurs</exception>
        Task<TapResponse<long?>?> GetTickerTradesListLengthAsync(string ticker);


        /// <summary>
        /// Get trades based on ticker, offset and max value. Max value limit is 500.
        /// </summary>
        /// <param name="ticker">The ticker, ie "tap"</param>
        /// <param name="offset">The offset to start getting data from, ie 0</param>
        /// <param name="max">The max amount of objects to get per request, ie 10. Limit is 500</param>
        /// <returns>The trades</returns>
        /// <exception cref="Exception">Thrown when an error occurs</exception>
        Task<TapResponse<List<TradeListResult>>?> GetTickerTradesListAsync(string ticker, int offset, int max);

        /// <summary>
        /// Get trades amount based on address and ticker
        /// </summary>
        /// <param name="address">The address</param>
        /// <param name="ticker">The ticker, ie "tap"</param>
        /// <returns>The trades amount</returns>
        /// <exception cref="Exception">Thrown when an error occurs</exception>
        Task<TapResponse<long?>?> GetAccountTradesListLengthAsync(string address, string ticker);

        /// <summary>
        /// Get rades amount based on address, ticker,offset and max value. Max value limit is 500.
        /// </summary>
        /// <param name="address">The address</param>
        /// <param name="ticker">The ticker, ie "tap"</param>
        /// <param name="offset">The offset to start getting data from, ie 0</param>
        /// <param name="max">The max amount of objects to get per request, ie 10. Limit is 500</param>
        /// <returns>The trades </returns>
        /// <exception cref="Exception">Thrown when an error occurs</exception>
        Task<TapResponse<List<TradeListResult>>?> GetAccountTradesListAsync(string address, string ticker, int offset, int max);


        /// <summary>
        /// Get trade based on inscriptionId
        /// Returns null if the accumulator object doesn't exist.
        /// </summary>
        /// <param name="inscriptionId">The inscription id</param>
        /// <returns>The trade</returns>
        /// <exception cref="Exception">Thrown when an error occurs</exception>
        Task<TapResponse<TradeResult>?> GetTradeAsync(string inscriptionId);


        /// <summary>
        /// Get amount of trades filled
        /// </summary>
        /// <returns>The amount of trades filled</returns>
        /// <exception cref="Exception">Thrown when an error occurs</exception>
        Task<TapResponse<long?>?> GetTradesFilledListLengthAsync();

        /// <summary>
        /// Get trades filled list based on offset and max value. Max value limit is 500.
        /// </summary>
        /// <param name="offset">The offset to start getting data from, ie 0</param>
        /// <param name="max">The max amount of objects to get per request, ie 10. Limit is 500</param>
        /// <returns>The trade filled list</returns>
        /// <exception cref="Exception">Thrown when an error occurs</exception>
        Task<TapResponse<List<TradesFilledListResult>>?> GetTradesFilledListAsync(int offset, int max);

        /// <summary>
        /// Get trades filled based on ticker
        /// </summary>
        /// <param name="ticker">The ticker, ie "tap"</param>
        /// <returns>The trades</returns>
        /// <exception cref="Exception">Thrown when an error occurs</exception>
        Task<TapResponse<long?>?> GetTickerTradesFilledListLengthAsync(string ticker);

        /// <summary>
        /// Get trades filled based on ticker, offset and max value. Max value limit is 500.
        /// </summary>
        /// <param name="ticker">The ticker, ie "tap"</param>
        /// <param name="offset">The offset to start getting data from, ie 0</param>
        /// <param name="max">The max amount of objects to get per request, ie 10. Limit is 500</param>
        /// <returns>The trades</returns>
        /// <exception cref="Exception">Thrown when an error occurs</exception>
        Task<TapResponse<List<TradesFilledListResult>>?> GetTickerTradesFilledListAsync(string ticker, int offset, int max);

        /// <summary>
        /// Get account trades filled amount
        /// </summary>
        /// <param name="address">The address</param>
        /// <param name="ticker">The ticker, ie "tap"</param>
        /// <returns>The trades filled amount</returns>
        /// <exception cref="Exception">Thrown when an error occurs</exception>
        Task<TapResponse<long?>?> GetAccountTradesFilledListLengthAsync(string address, string ticker);

        /// <summary>
        /// Get account trades filled
        /// </summary>
        /// <param name="address">The address</param>
        /// <param name="ticker">The ticker, ie "tap"</param>
        /// <param name="offset">The offset to start getting data from, ie 0</param>
        /// <param name="max">The max amount of objects to get per request, ie 10. Limit is 500</param>
        /// <returns>The trades</returns>
        /// <exception cref="Exception">Thrown when an error occurs</exception>
        Task<TapResponse<List<TradesFilledListResult>>?> GetAccountTradesFilledListAsync(string address, string ticker, int offset, int max);

    }
}
