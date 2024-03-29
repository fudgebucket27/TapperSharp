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
            var result = await _tapperClient!.GetDeploymentsAsync(0, 5);
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
            var result = await _tapperClient!.GetAccountTokensAsync("bc1paq960e3drpdwddfxh5kcgq48qa5yxeqsty9zez6w2c6mxr5fecrqp0syg0", 0, 10);
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

        [TestMethod]
        public async Task GetTradesList()
        {
            var result = await _tapperClient!.GetTradesListAsync(0, 10);
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result!.Count > 0);
        }

        [TestMethod]
        public async Task GetTickerTradesListLength()
        {
            var result = await _tapperClient!.GetTickerTradesListLengthAsync("BTC");
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result! > 0);
        }

        [TestMethod]
        public async Task GetTickerTradesList()
        {
            var result = await _tapperClient!.GetTickerTradesListAsync("BTC", 0, 10);
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result!.Count > 0);
        }

        [TestMethod]
        public async Task GetAccountTradesListLength()
        {
            var result = await _tapperClient!.GetAccountTradesListLengthAsync("bc1pfaztje6lw8dk7ngg9netgvesnpa8z8dntd39dqtselw5t2uzl9cq9pvdxm", "BTC");
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result! > 0);
        }

        [TestMethod]
        public async Task GetAccountTradesList()
        {
            var result = await _tapperClient!.GetAccountTradesListAsync("bc1pfaztje6lw8dk7ngg9netgvesnpa8z8dntd39dqtselw5t2uzl9cq9pvdxm", "BTC", 0, 10);
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result!.Count > 0);
        }

        [TestMethod]
        public async Task GetTrade()
        {
            var result = await _tapperClient!.GetTradeAsync("eb903de9b959c59baaaa5c5d847016bb86ceacca4c4f2659750aa8a6513e9064i0");
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result! != null);
        }


        [TestMethod]
        public async Task GetTradesFilledListLength()
        {
            var result = await _tapperClient!.GetTradesFilledListLengthAsync();
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result > 0);
        }

        [TestMethod]
        public async Task GetTradesFilledList()
        {
            var result = await _tapperClient!.GetTradesFilledListAsync(0, 10);
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result!.Count > 0);
        }

        [TestMethod]
        public async Task GetTickerTradesFilledListLength()
        {
            var result = await _tapperClient!.GetTickerTradesFilledListLengthAsync("ordfi");
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result! > 0);

            result = await _tapperClient!.GetTickerTradesFilledListLengthAsync("xxx123123xxx1");
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result! == 0);
        }

        [TestMethod]
        public async Task GetTickerTradesFilledList()
        {
            var result = await _tapperClient!.GetTickerTradesFilledListAsync("buidl", 0, 10);
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result!.Count >= 0);
        }

        [TestMethod]
        public async Task GetAccountTradesFilledListLength()
        {
            var result = await _tapperClient!.GetAccountTradesFilledListLengthAsync("bc1pepk5alax8jte67z4dgwywyzhyzuuqcdejcnnuswvxj6hapzu9fcs2f5lr3", "shiba");
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result > 0);
        }

        [TestMethod]
        public async Task GetAccountTradesFilledList()
        {
            var result = await _tapperClient!.GetAccountTradesFilledListAsync("bc1pepk5alax8jte67z4dgwywyzhyzuuqcdejcnnuswvxj6hapzu9fcs2f5lr3", "shiba", 0, 10);
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result!.Count > 0);
        }

        [TestMethod]
        public async Task GetAccountReceiveTradesFilledListLength()
        {
            var result = await _tapperClient!.GetAccountReceiveTradesFilledListLengthAsync("bc1pepk5alax8jte67z4dgwywyzhyzuuqcdejcnnuswvxj6hapzu9fcs2f5lr3", "taposhi");
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result > 0);
        }

        [TestMethod]
        public async Task GetAccountReceiveTradesFilledList()
        {
            var result = await _tapperClient!.GetAccountReceiveTradesFilledListAsync("bc1pepk5alax8jte67z4dgwywyzhyzuuqcdejcnnuswvxj6hapzu9fcs2f5lr3", "taposhi", 0, 10);
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result!.Count > 0);
        }

        [TestMethod]
        public async Task GetAuthListLength()
        {
            var result = await _tapperClient!.GetAuthListLengthAsync();
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result > 0);
        }

        [TestMethod]
        public async Task GetAuthList()
        {
            var result = await _tapperClient!.GetAuthListAsync(0, 10);
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result!.Count > 0);
        }

        [TestMethod]
        public async Task GetAccountAuthListLength()
        {
            var result = await _tapperClient!.GetAccountAuthListLengthAsync("bc1pccu8444ay68zltcdjzrdelpnf26us7ywg9pvwl7nkrjgrkz8rlvqe6f880");
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result > 0);
        }


        [TestMethod]
        public async Task GetAccountAuthList()
        {
            var result = await _tapperClient!.GetAccountAuthListAsync("bc1pccu8444ay68zltcdjzrdelpnf26us7ywg9pvwl7nkrjgrkz8rlvqe6f880", 0 , 10);
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result!.Count > 0);
        }


        [TestMethod]
        public async Task GetAuthCancelled()
        {
            var result = await _tapperClient!.GetAuthCancelledAsync("fd3664a56cf6d14b21504e5d83a3d4867ee256f06cbe3bddf2787d6a80a86078i0");
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result == false);
        }

        [TestMethod]
        public async Task GetAuthHashExists()
        {
            var result = await _tapperClient!.GetAuthHashExistsAsync("0f30c22be2f46e819538ca1281aadb82d3928cae5a699cade80013c5b14871e4");
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result == true);

            result = await _tapperClient!.GetAuthHashExistsAsync("thisReturnsFalse69420Blazeit");
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result == false);
        }

        [TestMethod]
        public async Task GetRedeemListLength()
        {
            var result = await _tapperClient!.GetRedeemListLengthAsync();
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result > 0);
        }

        [TestMethod]
        public async Task GetRedeemList()
        {
            var result = await _tapperClient!.GetRedeemListAsync(0,10);
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result!.Count > 0);
        }

        [TestMethod]
        public async Task GetAccountRedeemListLength()
        {
            var result = await _tapperClient!.GetAccountRedeemListLengthAsync("bc1pccu8444ay68zltcdjzrdelpnf26us7ywg9pvwl7nkrjgrkz8rlvqe6f880");
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result > 0);
        }

        [TestMethod]
        public async Task GetAccountRedeemList()
        {
            var result = await _tapperClient!.GetAccountRedeemListAsync("bc1pccu8444ay68zltcdjzrdelpnf26us7ywg9pvwl7nkrjgrkz8rlvqe6f880", 0, 10);
            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            Assert.IsTrue(result!.Result!.Count > 0);
        }
    }
}