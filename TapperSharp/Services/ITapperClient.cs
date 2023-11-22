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
        Task ConnectAsync();
        Task DisconnectAsync();
        Task<TapResponse> GetDeploymentAsync(string ticker);
    }
}
