using SocketIOClient;
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
            Assert.IsNotNull(result!.Result);
        }

        [TestMethod]
        public async Task GetDeploymentsLength()
        {
            var result = await _tapperClient!.GetDeploymentsLengthAsync();
            Assert.AreNotEqual(0, result!.Result);
        }

        [TestMethod]
        public async Task GetDeployments()
        {
            var result = await _tapperClient!.GetDeploymentsAsync(0,5);
            Assert.AreEqual(5, result!.Result!.Count);
        }

        [TestMethod]
        public async Task GetMintTokensLeft()
        {
            var result = await _tapperClient!.GetMintTokensLeftAsync("tap");
            Assert.AreEqual("0", result!.Result!);
        }

        [TestMethod]
        public async Task GetHoldersLength()
        {
            var result = await _tapperClient!.GetHoldersLengthAsync("tap");
            Assert.AreNotEqual(0, result!.Result!);
        }

        [TestMethod]
        public async Task GetHolders()
        {
            var result = await _tapperClient!.GetHoldersAsync("tap", 0, 10);
            Assert.AreNotEqual(0, result!.Result!.Count);
        }
    }
}