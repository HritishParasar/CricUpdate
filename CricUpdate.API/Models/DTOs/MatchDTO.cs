namespace CricUpdate.API.Models.DTOs
{
    public class MatchDTO
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }  // use UTC
        public string Venue { get; set; } = null!;
        public string Teams { get; set; } = null!;
        public string? ResultSummary { get; set; } // null for upcoming
        public bool IsCompleted { get; set; } = false;
    }
}
