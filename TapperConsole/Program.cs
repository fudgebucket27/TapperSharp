﻿using TapperSharp;

namespace TapperConsole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var client = new TapperClient();
            await client.ConnectAsync(new Uri("wss://tap.trac.network"));

        }
    }
}