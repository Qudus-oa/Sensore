using System;

namespace Sensore.Models
{
    public class Comment
    {
        public int Id { get; set; }

        // ISO-8601 timestamp of the frame (use the frame's Timestamp)
        public DateTime FrameTimestamp { get; set; }

        // Optional: which frame index was on-screen when commenting
        public int? FrameIndex { get; set; }

        // Who wrote it (you can extend with Identity later)
        public string Author { get; set; } = "Patient";

        public string Text { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}