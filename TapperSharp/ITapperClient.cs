using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TapperSharp
{
    public interface ITapperClient
    {
        Task ConnectAsync(Uri serverUri);
        Task SendAsync(string message);
        Task<string> ReceiveAsync();
        Task DisconnectAsync();
    }
}
