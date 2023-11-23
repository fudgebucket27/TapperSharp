using SocketIOClient;
using System.Text.Json;
using System.Text.Json.Serialization;
using TapperSharp;
using TapperSharp.Services;

namespace TapperConsole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            SocketIOOptions socketIOOptions = new SocketIOOptions()
            {
                ReconnectionAttempts = 3,
                Reconnection = true,
                ReconnectionDelay = 500,
                ReconnectionDelayMax = 500,
                RandomizationFactor = 0
            };
            var tapperClient = new TapperClient("https://tap.trac.network", socketIOOptions);
            try
            {
                await tapperClient.ConnectAsync();


                var tickerSentList = await tapperClient.GetSentListAsync(0, 10);
                Console.WriteLine(JsonSerializer.Serialize(tickerSentList, new JsonSerializerOptions() { WriteIndented = true }));

                //var tickerSentList = await tapperClient.GetTickerSentListAsync("-tap", 0 , 10);
                //Console.WriteLine(JsonSerializer.Serialize(tickerSentList, new JsonSerializerOptions() { WriteIndented = true }));

                //var tickerSentListLength = await tapperClient.GetTickerSentListLengthAsync("-tap");
                //Console.WriteLine(JsonSerializer.Serialize(tickerSentListLength, new JsonSerializerOptions() { WriteIndented = true }));
                //var accountSentList = await tapperClient.GetAccountSentListAsync("bc1pdqgwqyc2n04a4ttd5kutzcmpjlqgvpdw2x225d7xewrypqa03nus6mddlq", "-tap", 0 , 10);
                //Console.WriteLine(JsonSerializer.Serialize(accountSentList, new JsonSerializerOptions() { WriteIndented = true }));
                //var accountSentListLength = await tapperClient.GetAccountSentListLengthAsync("bc1pdqgwqyc2n04a4ttd5kutzcmpjlqgvpdw2x225d7xewrypqa03nus6mddlq", "-tap");
                //Console.WriteLine(JsonSerializer.Serialize(accountSentListLength, new JsonSerializerOptions() { WriteIndented = true }));
                //var accountTransferList = await tapperClient.GetAccountTransferListAsync("bc1pdqgwqyc2n04a4ttd5kutzcmpjlqgvpdw2x225d7xewrypqa03nus6mddlq", "-tap", 0, 10);
                //Console.WriteLine(JsonSerializer.Serialize(accountTransferList, new JsonSerializerOptions() { WriteIndented = true }));
                //var tickerTransferListLength = await tapperClient.GetTickerTransferListLengthAsync("-tap");
                //Console.WriteLine(JsonSerializer.Serialize(tickerTransferListLength, new JsonSerializerOptions() { WriteIndented = true }));
                //var tickerTransferList = await tapperClient.GetTickerTransferListAsync("-tap", 0, 10);
                //Console.WriteLine(JsonSerializer.Serialize(tickerTransferList, new JsonSerializerOptions() { WriteIndented = true }));
                //var transferList = await tapperClient.GetTransferListAsync(0, 10);
                //Console.WriteLine(JsonSerializer.Serialize(transferList, new JsonSerializerOptions() { WriteIndented = true }));
                //var accountTransferListLength = await tapperClient.GetAccountTransferListLengthAsync("bc1paq960e3drpdwddfxh5kcgq48qa5yxeqsty9zez6w2c6mxr5fecrqp0syg0", "tap");
                //Console.WriteLine(JsonSerializer.Serialize(accountTransferListLength, new JsonSerializerOptions() { WriteIndented = true }));
                //var mintList = await tapperClient.GetMintListAsync(0, 10);
                //Console.WriteLine(JsonSerializer.Serialize(mintList, new JsonSerializerOptions() { WriteIndented = true }));
                //var mintListLength = await tapperClient.GetMintListLengthAsync();
                //Console.WriteLine(JsonSerializer.Serialize(mintListLength, new JsonSerializerOptions() { WriteIndented = true }));
                //var tickerMintList = await tapperClient.GetTickerMintListAsync("tap", 0, 10);
                //Console.WriteLine(JsonSerializer.Serialize(tickerMintList, new JsonSerializerOptions() { WriteIndented = true }));
                //var ticketMintListLength = await tapperClient.GetTickerMintListLengthAsync("tap");
                //Console.WriteLine(JsonSerializer.Serialize(ticketMintListLength, new JsonSerializerOptions() { WriteIndented = true }));
                //var accountTokensLength = await tapperClient.GetAccountTokensLengthAsync(holders!.Result![0]!.Address!);
                //var holders = await tapperClient.GetHoldersAsync("tap", 0, 10);
                //Console.WriteLine(JsonSerializer.Serialize(holders, new JsonSerializerOptions() { WriteIndented = true }));
                //var accountTokensLength = await tapperClient.GetAccountTokensLengthAsync(holders!.Result![0]!.Address!);
                //Console.WriteLine(JsonSerializer.Serialize(accountTokensLength, new JsonSerializerOptions() { WriteIndented = true }));
                //var balance = await tapperClient.GetBalanceAsync(holders!.Result[0]!.Address!, "tap");
                //Console.WriteLine(JsonSerializer.Serialize(balance, new JsonSerializerOptions() { WriteIndented = true }));
                //var transferable = await tapperClient.GetTransferableAsync(holders!.Result[0]!.Address!, "tap");
                //Console.WriteLine(JsonSerializer.Serialize(transferable, new JsonSerializerOptions() { WriteIndented = true }));
                //var accountTokens = await tapperClient.GetAccountTokensAsync("bc1paq960e3drpdwddfxh5kcgq48qa5yxeqsty9zez6w2c6mxr5fecrqp0syg0", 0, 10);
                //Console.WriteLine(JsonSerializer.Serialize(accountTokens, new JsonSerializerOptions() { WriteIndented = true }));
                //var accountMintListLength = await tapperClient.GetAccountMintListLengthAsync("bc1paq960e3drpdwddfxh5kcgq48qa5yxeqsty9zez6w2c6mxr5fecrqp0syg0", "tap");
                //Console.WriteLine(JsonSerializer.Serialize(accountMintListLength, new JsonSerializerOptions() { WriteIndented = true }));
                //var accountMintList = await tapperClient.GetAccountMintListAsync("bc1paq960e3drpdwddfxh5kcgq48qa5yxeqsty9zez6w2c6mxr5fecrqp0syg0", "tap", 0, 10);
                //Console.WriteLine(JsonSerializer.Serialize(accountMintList, new JsonSerializerOptions() { WriteIndented = true }));
                //var holdersLength = await tapperClient.GetHoldersLengthAsync("tap");
                //Console.WriteLine(JsonSerializer.Serialize(holdersLength, new JsonSerializerOptions() { WriteIndented = true }));
                //var tapDeployment = await tapperClient.GetDeploymentAsync("tap");
                //Console.WriteLine(JsonSerializer.Serialize(tapDeployment,  new JsonSerializerOptions() { WriteIndented = true}));
                //var deploymentsLength = await tapperClient.GetDeploymentsLengthAsync();
                //Console.WriteLine(JsonSerializer.Serialize(deploymentsLength, new JsonSerializerOptions() { WriteIndented = true }));
                //var deployments = await tapperClient.GetDeploymentsAsync(0,10);
                //Console.WriteLine(JsonSerializer.Serialize(deployments, new JsonSerializerOptions() { WriteIndented = true }));
                //var mintTokensLeft = await tapperClient.GetMintTokensLeftAsync("tap");
                //Console.WriteLine(JsonSerializer.Serialize(mintTokensLeft, new JsonSerializerOptions() { WriteIndented = true }));
                await tapperClient.DisconnectAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}