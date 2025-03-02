using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using MessageApp.Models;
using MessageApp.Services;
using MessageApp.WebSocket; 

namespace MessageApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessagesController : ControllerBase
{
    private readonly MessageService _messageService;

    public MessagesController(MessageService messageService)
    {
        _messageService = messageService;
    }

    [HttpPost]
    public async Task<IActionResult> PostMessage([FromBody] Messages.MessageRequest request)
    {
        await _messageService.AddMessageAsync(request.Content, request.SequenceNumber);

        var message = new
        {
            content = request.Content,
            timestamp = DateTime.UtcNow,
            sequenceNumber = request.SequenceNumber
        };
        
        await WebSocketMiddleware.SendMessageAsync(JsonSerializer.Serialize(message));

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetMessages([FromQuery] DateTime start, [FromQuery] DateTime end)
    {
        var messages = await _messageService.GetMessagesAsync(start, end);
        return Ok(messages);
    }
}