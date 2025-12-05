using System;

namespace Sensore.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Title { get; set; } = "Plan to Move Soon";
        public string Message { get; set; } = string.Empty;
        public bool IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
