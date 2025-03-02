using MessageApp.Models;
using Npgsql;

namespace MessageApp.Repositories;

public class MessageRepository
{
    private readonly string _connectionString;

    public MessageRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task AddMessageAsync(string content, int sequenceNumber)
    {
        await using var conn = new NpgsqlConnection(_connectionString);
        await conn.OpenAsync();
        await using var cmd = new NpgsqlCommand(
            "INSERT INTO Messages (Content, Timestamp, SequenceNumber) VALUES (@content, @timestamp, @sequenceNumber)", conn);
        cmd.Parameters.AddWithValue("content", content);
        cmd.Parameters.AddWithValue("timestamp", DateTime.UtcNow);
        cmd.Parameters.AddWithValue("sequenceNumber", sequenceNumber);
        try
        {
        await cmd.ExecuteNonQueryAsync();

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<IEnumerable<Messages.Message>> GetMessagesAsync(DateTime start, DateTime end)
    {
        await using var conn = new NpgsqlConnection(_connectionString);
        await conn.OpenAsync();
        await using var cmd = new NpgsqlCommand(
            "SELECT * FROM Messages WHERE Timestamp BETWEEN @start AND @end", conn);
        cmd.Parameters.AddWithValue("start", start);
        cmd.Parameters.AddWithValue("end", end);
        await using var reader = await cmd.ExecuteReaderAsync();
        var messages = new List<Messages.Message>();
        while (await reader.ReadAsync())
        {
            messages.Add(new Messages.Message
            {
                Id = reader.GetInt32(0),
                Content = reader.GetString(1),
                Timestamp = reader.GetDateTime(2),
                SequenceNumber = reader.GetInt32(3)
            });
        }
        return messages;
    
    }
}