using System;

namespace Sensore.Models
{
    public class Comment
    {
        public int Id { get; set; }

        // ISO-8601 timestamp of the frame 
        public DateTime FrameTimestamp { get; set; }

        
        public int? FrameIndex { get; set; }

        
        public string Author { get; set; } = "Patient";

        public string Text { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Fields for clinician reply
        public string? ClinicianReply { get; set; }
        public DateTime? ReplyAt { get; set; }
    }
}