using MessageApp.Models;
using MessageApp.Repositories;

namespace MessageApp.Services;

public class MessageService
{ 
    private readonly MessageRepository _repository;

    public MessageService(MessageRepository repository)
    {
        _repository = repository;
    }

    public async Task AddMessageAsync(string content, int sequenceNumber)
    {
        await _repository.AddMessageAsync(content, sequenceNumber);
    }

    public async Task<IEnumerable<Messages.Message>> GetMessagesAsync(DateTime start, DateTime end)
    {
        return await _repository.GetMessagesAsync(start, end);
    }
}