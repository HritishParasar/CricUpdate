namespace CricUpdate.API.Models.DTOs
{
    public class UpdateMatchDTO
    {
        public string? ResultSummary { get; set; } // null for upcoming
        public bool IsCompleted { get; set; } = false;
    }
}
