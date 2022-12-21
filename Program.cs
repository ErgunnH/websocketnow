// See https://aka.ms/new-console-template for more information


using System.Text;
using System.Net.WebSockets;
using System;
using Microsoft.AspNetCore.SignalR;

MyHub kk = new MyHub();
await kk.Connect();
Console.WriteLine("finish");
//string a = "";
//byte[] b = new byte[256];
//var ws = new ClientWebSocket();
//await ws.ConnectAsync(new Uri("wss://nbstream.binance.com/eoptions/stream?streams=BTC-210630-9000-P@trade/BTC-210630-9000-P@ticker"),CancellationToken.None);
//Console.WriteLine(ws.State);

//var result = await ws.ReceiveAsync(b, CancellationToken.None);
//Console.WriteLine(ws.State);
//Console.WriteLine(result.Count);
//Console.WriteLine(ws.CloseStatusDescription);
//Console.WriteLine(ws.CloseStatus);
//Console.WriteLine(Encoding.ASCII.GetString(b, 0, result.Count));



//using (var ws = new ClientWebSocket())
//{
//    Console.WriteLine("using içinde");
//    await ws.ConnectAsync(new Uri("wss://nbstream.binance.com/eoptions/ws/BTC-210630-9000-P@ticker"), CancellationToken.None);
//    var buffer = new byte[256];
//    Console.WriteLine("while girecek");
//    while (ws.State == WebSocketState.Open)
//    {
//        Console.WriteLine("while girdi");
//        var result = await ws.ReceiveAsync(buffer, CancellationToken.None);
//        Console.WriteLine("ife girecek");
        
//        if (result.MessageType == WebSocketMessageType.Close)
//        {
//            Console.WriteLine("if girdi");
//            await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);
//        }
//        else
//        {
//            Console.WriteLine("elseye girdi");
//            Console.WriteLine(Encoding.ASCII.GetString(buffer, 0, result.Count));
//        }
//    }
//    Console.WriteLine("while dan çıktı");
//}
//Console.WriteLine("usingden çıktı");