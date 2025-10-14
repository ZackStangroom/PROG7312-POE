namespace PROG7312_POE.Models
{
    public class LocalEvent
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string? ImagePath { get; set; }
        public EventStatus Status { get; set; } = EventStatus.Upcoming;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public enum EventStatus
    {
        Upcoming,
        InProgress,
        Completed,
        Cancelled
    }
}