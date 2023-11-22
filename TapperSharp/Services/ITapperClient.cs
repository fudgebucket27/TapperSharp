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
        Task<TapResponse<DeploymentResult>?> GetDeploymentAsync(string ticker);
        /// <summary>
        /// Get the amount of deployments
        /// </summary>
        /// <returns>The amount of deployments on the network</returns>
        Task<TapResponse<int>?> GetDeploymentsLengthAsync();
        /// <summary>
        /// Get deployments based on an offset and max value. Max value limit is 500
        /// </summary>
        /// <param name="offset">The offset to start getting deployments from, ie 0</param>
        /// <param name="max">The max amount of deployments to get per request, ie 10. Limit is 500</param>
        /// <returns></returns>
        Task<TapResponse<List<DeploymentResult>>?> GetDeploymentsAsync(int offset, int max);
        /// <summary>
        /// Get the amount of tokens left to mint for a ticker
        /// </summary>
        /// <param name="ticker">The ticker, ie "tap"</param>
        /// <returns>The amount of tokens left to mint for the ticker</returns>
        Task<TapResponse<string>?> GetMintTokensLeftAsync(string ticker);
    }
}
