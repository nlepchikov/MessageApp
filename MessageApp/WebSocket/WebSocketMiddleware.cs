using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;

namespace MessageApp.WebSocket;

public class WebSocketMiddleware
{
    private readonly RequestDelegate _next;
    private static System.Net.WebSockets.WebSocket? _sockets;

    public WebSocketMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public static async Task<System.Net.WebSockets.WebSocket> InvokeAsync(HttpContext context)
    {
        var webSocket = await context.WebSockets.AcceptWebSocketAsync();
        _sockets = webSocket;
        
        return webSocket;
    }
    
    public static async Task SendMessageAsync(string message)
    {
        var buffer = Encoding.UTF8.GetBytes(message);
        var segment = new ArraySegment<byte>(buffer);

        if (_sockets != null && _sockets.State == WebSocketState.Open)
        {
            await _sockets.SendAsync(segment, WebSocketMessageType.Text, true, CancellationToken.None);
            Console.WriteLine("Message sent to WebSocket client.");
        }
    }
}
