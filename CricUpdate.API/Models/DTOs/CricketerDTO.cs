namespace CricUpdate.API.Models.DTOs
{
    public class CricketerDTO
    {
        public string FullName { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string Role { get; set; } = null!; // e.g., Batsman, Bowler, Allrounder
        public DateTime? DateOfBirth { get; set; }
        public string BattingStyle { get; set; } = null!;
        public string BowlingStyle { get; set; } = null!;
        public int MatchesPlayed { get; set; }
        public int RunsScored { get; set; }
        public int WicketsTaken { get; set; }
    }
}
