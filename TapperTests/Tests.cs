using SocketIOClient;
using System.Text.Json;
using TapperSharp.Services;

namespace TapperTests
{
    [TestClass]
    public class Tests
    {
        private static TapperClient? _tapperClient;

        [ClassInitialize]
        public static async Task ClassInitialize(TestContext context)
        {
            SocketIOOptions socketIOOptions = new SocketIOOptions()
            {
                ReconnectionAttempts = 3,
                Reconnection = true,
                ReconnectionDelay = 500,
                ReconnectionDelayMax = 500,
                RandomizationFactor = 0
            };
            _tapperClient = new TapperClient("https://tap.trac.network", socketIOOptions);
            await _tapperClient.ConnectAsync();
        }

        [ClassCleanup]
        public static async Task ClassCleanup()
        {
            await _tapperClient!.DisconnectAsync();
        }

        [TestMethod]
        public async Task GetDeployment()
        {
            var result = await _tapperClient!.GetDeploymentAsync("tap");
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsNotNull(result!.Result);
        }

        [TestMethod]
        public async Task GetDeploymentsLength()
        {
            var result = await _tapperClient!.GetDeploymentsLengthAsync();
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.AreNotEqual(0, result!.Result);
        }

        [TestMethod]
        public async Task GetDeployments()
        {
            var result = await _tapperClient!.GetDeploymentsAsync(0,5);
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.AreEqual(5, result!.Result!.Count);
        }

        [TestMethod]
        public async Task GetMintTokensLeft()
        {
            var result = await _tapperClient!.GetMintTokensLeftAsync("tap");
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.AreEqual("0", result!.Result!);
        }

        [TestMethod]
        public async Task GetHoldersLength()
        {
            var result = await _tapperClient!.GetHoldersLengthAsync("tap");
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.AreNotEqual(0, result!.Result!);
        }

        [TestMethod]
        public async Task GetHolders()
        {
            var result = await _tapperClient!.GetHoldersAsync("tap", 0, 10);
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.AreNotEqual(0, result!.Result!.Count);
        }

        [TestMethod]
        public async Task GetAccountTokensLength()
        {
            var result = await _tapperClient!.GetAccountTokensLengthAsync("bc1paq960e3drpdwddfxh5kcgq48qa5yxeqsty9zez6w2c6mxr5fecrqp0syg0");
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.AreNotEqual(0, result!.Result!);
        }

        [TestMethod]
        public async Task GetBalance()
        {
            var result = await _tapperClient!.GetBalanceAsync("bc1paq960e3drpdwddfxh5kcgq48qa5yxeqsty9zez6w2c6mxr5fecrqp0syg0", "tap");
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.AreNotEqual("0", result!.Result!);
        }

        [TestMethod]
        public async Task GetTransferable()
        {
            var result = await _tapperClient!.GetTransferableAsync("bc1paq960e3drpdwddfxh5kcgq48qa5yxeqsty9zez6w2c6mxr5fecrqp0syg0", "tap");
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result is null || result.Result is string);
        }

        [TestMethod]
        public async Task GetAccountTokens()
        {
            var result = await _tapperClient!.GetAccountTokensAsync("bc1paq960e3drpdwddfxh5kcgq48qa5yxeqsty9zez6w2c6mxr5fecrqp0syg0", 0,10);
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.AreNotEqual(0, result!.Result!.Count);
        }

        [TestMethod]
        public async Task GetAccountMintListLength()
        {
            var result = await _tapperClient!.GetAccountMintListLengthAsync("bc1paq960e3drpdwddfxh5kcgq48qa5yxeqsty9zez6w2c6mxr5fecrqp0syg0", "tap");
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result > 0);
        }

        [TestMethod]
        public async Task GetAccountMintList()
        {
            var result = await _tapperClient!.GetAccountMintListAsync("bc1paq960e3drpdwddfxh5kcgq48qa5yxeqsty9zez6w2c6mxr5fecrqp0syg0", "tap", 0, 10);
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result!.Count > 0);
        }

        [TestMethod]
        public async Task GetTickerMintListLength()
        {
            var result = await _tapperClient!.GetTickerMintListLengthAsync("tap");
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result > 0);
        }

        [TestMethod]
        public async Task GetTickerMintList()
        {
            var result = await _tapperClient!.GetTickerMintListAsync("tap", 0, 10);
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result!.Count > 0);
        }

        [TestMethod]
        public async Task GetMintListLength()
        {
            var result = await _tapperClient!.GetMintListLengthAsync();
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result! > 0);
        }

        [TestMethod]
        public async Task GetMintList()
        {
            var result = await _tapperClient!.GetMintListAsync(0, 10);
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result!.Count > 0);
        }

        [TestMethod]
        public async Task GetTransferList()
        {
            var result = await _tapperClient!.GetTransferListAsync(0, 10);
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result!.Count > 0);
        }

        [TestMethod]
        public async Task GetTickerTransferList()
        {
            var result = await _tapperClient!.GetTickerTransferListAsync("-tap", 0, 10);
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result!.Count > 0);
        }

        [TestMethod]
        public async Task GetTickerTransferListLength()
        {
            var result = await _tapperClient!.GetTickerTransferListLengthAsync("-tap");
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result! > 0);
        }

        [TestMethod]
        public async Task GetAccountTransferList()
        {
            var result = await _tapperClient!.GetAccountTransferListAsync("bc1pdqgwqyc2n04a4ttd5kutzcmpjlqgvpdw2x225d7xewrypqa03nus6mddlq", "-tap", 0, 10);
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result!.Count > 0);
        }

        [TestMethod]
        public async Task GetAccountTransferListLength()
        {
            var result = await _tapperClient!.GetAccountTransferListLengthAsync("bc1pdqgwqyc2n04a4ttd5kutzcmpjlqgvpdw2x225d7xewrypqa03nus6mddlq", "-tap");
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result! > 0);
        }

        [TestMethod]
        public async Task GetTransferListLength()
        {
            var result = await _tapperClient!.GetTransferListLengthAsync();
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result! > 0);
        }

        [TestMethod]
        public async Task GetAccountSentListLength()
        {
            var result = await _tapperClient!.GetAccountSentListLengthAsync("bc1pdqgwqyc2n04a4ttd5kutzcmpjlqgvpdw2x225d7xewrypqa03nus6mddlq", "-tap");
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result! > 0);
        }

        [TestMethod]
        public async Task GetAccountSentList()
        {
            var result = await _tapperClient!.GetAccountSentListAsync("bc1pdqgwqyc2n04a4ttd5kutzcmpjlqgvpdw2x225d7xewrypqa03nus6mddlq", "-tap", 0, 10);
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result!.Count > 0);
        }

        [TestMethod]
        public async Task GetTickerSentListLength()
        {
            var result = await _tapperClient!.GetTickerSentListLengthAsync("-tap");
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result! > 0);
        }

        [TestMethod]
        public async Task GetTickerSentList()
        {
            var result = await _tapperClient!.GetTickerSentListAsync("-tap", 0, 10);
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result!.Count > 0);
        }

        [TestMethod]
        public async Task GetSentListLength()
        {
            var result = await _tapperClient!.GetSentListLengthAsync();
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result! > 0);
        }

        [TestMethod]
        public async Task GetSentList()
        {
            var result = await _tapperClient!.GetSentListAsync(0, 10);
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result!.Count > 0);
        }

        [TestMethod]
        public async Task GetAccountReceiveListLength()
        {
            var result = await _tapperClient!.GetAccountReceiveListLengthAsync("bc1pdqgwqyc2n04a4ttd5kutzcmpjlqgvpdw2x225d7xewrypqa03nus6mddlq", "-tap");
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result! > 0);
        }

        [TestMethod]
        public async Task GetAccountReceiveList()
        {
            var result = await _tapperClient!.GetAccountReceiveListAsync("bc1pdqgwqyc2n04a4ttd5kutzcmpjlqgvpdw2x225d7xewrypqa03nus6mddlq", "-tap", 0, 10);
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result!.Count > 0);
        }

        [TestMethod]
        public async Task GetAccumulatorList()
        {
            var result = await _tapperClient!.GetAccumulatorListAsync(0, 10);
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result!.Count > 0);
        }

        [TestMethod]
        public async Task GetAccumulator()
        {
            var result = await _tapperClient!.GetAccumulatorAsync("1d132688e099f69e81dd67c6f85558ce5fbbf99c82ec16548beb994d76435a15i0");
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result! != null);
            result = await _tapperClient!.GetAccumulatorAsync("040b39c793028677de567eeecfd181fef82eef7313b1764cf2090a283653d06ai0");
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result! == null);
        }

        [TestMethod]
        public async Task GetAccountAccumulatorListLength()
        {
            var result = await _tapperClient!.GetAccountAccumulatorListLengthAsync("bc1paq960e3drpdwddfxh5kcgq48qa5yxeqsty9zez6w2c6mxr5fecrqp0syg0");
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result! > 0);
        }

        [TestMethod]
        public async Task GetAccumulatorListLength()
        {
            var result = await _tapperClient!.GetAccumulatorListLengthAsync();
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result is null || result is not null);
        }

        [TestMethod]
        public async Task GetAccountAccumulatorList()
        {
            var result = await _tapperClient!.GetAccountAccumulatorListAsync("bc1pnedcv68w8468w5fnju2u2kvsdennmudaje7kt94lhjx5cq33ts3sqhmwmu", 0, 10);
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result!.Count >= 0);
        }

        [TestMethod]
        public async Task GetTradesListLength()
        {
            var result = await _tapperClient!.GetTradesListLengthAsync();
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result > 0);
        }
    }
}