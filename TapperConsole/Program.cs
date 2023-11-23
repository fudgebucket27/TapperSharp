﻿using SocketIOClient;
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
            await tapperClient.ConnectAsync();


            var holders = await tapperClient.GetHoldersAsync("tap", 0, 10);
            Console.WriteLine(JsonSerializer.Serialize(holders, new JsonSerializerOptions() { WriteIndented = true }));
            var accountTokensLength = await tapperClient.GetAccountTokensLengthAsync(holders!.Result![0]!.Address!);
            Console.WriteLine(JsonSerializer.Serialize(accountTokensLength, new JsonSerializerOptions() { WriteIndented = true }));
            var balance = await tapperClient.GetBalanceAsync(holders!.Result[0]!.Address!, "tap");
            Console.WriteLine(JsonSerializer.Serialize(balance, new JsonSerializerOptions() { WriteIndented = true }));
            var transferable = await tapperClient.GetTransferableAsync(holders!.Result[0]!.Address!, "tap");
            Console.WriteLine(JsonSerializer.Serialize(transferable, new JsonSerializerOptions() { WriteIndented = true }));

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
    }
}