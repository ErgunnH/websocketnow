using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class MyHub : Hub
{
    private const string API_URL = "wss://stream.bybit.com/realtime";

    private ClientWebSocket _ws;
    private CancellationTokenSource _cts;

    public async Task Connect()
    {
        _ws = new ClientWebSocket();
        _cts = new CancellationTokenSource();

        await _ws.ConnectAsync(new Uri(API_URL), _cts.Token);

        var subscriptionMessage = "{\"op\": \"subscribe\", \"args\": [\"trade.BTCUSD\"]}";
        await _ws.SendAsync(Encoding.UTF8.GetBytes(subscriptionMessage), WebSocketMessageType.Text, true, _cts.Token);

        await Task.Factory.StartNew(
            async () =>
            {
                var buffer = new byte[1024 * 4];

                while (_ws.State == WebSocketState.Open)
                {
                    var result = await _ws.ReceiveAsync(new ArraySegment<byte>(buffer), _cts.Token);

                    Console.WriteLine(Encoding.ASCII.GetString(buffer, 0, result.Count));
                    
                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        await _ws.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, _cts.Token);
                        _cts.Cancel();
                    }
                    else
                    {
                        var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                        var bybitMessage = JsonConvert.DeserializeObject<ByBitMessage>(message);
                        if (bybitMessage.Type == "data")
                        {
                            await Clients.All.SendAsync("ReceiveMessage", bybitMessage.Data);

                        }
                    }
                }
            }, _cts.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
    }

    public async Task Disconnect()
    {
        _cts.Cancel();
        await _ws.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, _cts.Token);
        _ws.Dispose();
    }
}

public class ByBitMessage
{
    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("data")]
    public object Data { get; set; }
}