# TapperSharp
C# Socket.io Client Wrapper around Benny's TAP Public Endpoints: https://github.com/BennyTheDev/trac-tap-public-endpoint

WIP. Expect breaking changes.

# Install into your project
Via Nuget

```bash
NuGet\Install-Package TapperSharp -Version 0.0.1
```

# Client usage
```csharp
SocketIOOptions socketIOOptions = new SocketIOOptions()
{
ReconnectionAttempts = 3,
Reconnection = true,
ReconnectionDelay = 500,
 ReconnectionDelayMax = 500,
RandomizationFactor = 0
 };
var _tapperClient = new TapperClient("https://tap.trac.network", socketIOOptions); // Initialize client
await _tapperClient.ConnectAsync(); // Connect
var holders = await _tapperClient!.GetHoldersLengthAsync("tap"); // Example usage of get holders length of a token
await _tapperClient!.DisconnectAsync(); // Disconnect
```

Check the [tests](https://github.com/fudgebucket27/TapperSharp/blob/master/TapperTests/Tests.cs) for usage of all methods.
