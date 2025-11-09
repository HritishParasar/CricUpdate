namespace CricUpdate.API.Models
{
    public class Match
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }  // use UTC
        public string Venue { get; set; } = null!;
        public string TeamA { get; set; } = null!;
        public string TeamB { get; set; } = null!;
        public string? ResultSummary { get; set; } // null for upcoming
        public bool IsCompleted { get; set; } = false;
    }
}
