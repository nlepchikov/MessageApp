namespace MessageApp.Models;

public class Messages
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public int SequenceNumber { get; set; }
    }

    public class MessageRequest
    {
        public string Content { get; set; }
        public int SequenceNumber { get; set; }
    }
}