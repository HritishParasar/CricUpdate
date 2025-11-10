using CricUpdate.API.Data;
using CricUpdate.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CricUpdate.API.SeedData
{
    public class Seed
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            if (await context.Cricketers.AnyAsync() || await context.Matches.AnyAsync())
                return; // already seeded

            var cricketers = new List<Cricketer>
            {
                new Cricketer { FullName = "Virat Kohli", Country = "India", Role = "Batsman", DateOfBirth = new DateTime(1988,11,5), BattingStyle = "Right-Hand Bat",BowlingStyle="Right-arm medium pace",RunsScored=3325,WicketsTaken=21,MatchesPlayed=108 },
                new Cricketer { FullName = "Rohit Sharma", Country = "India", Role = "Batsman", DateOfBirth = new DateTime(1987,4,30),BattingStyle = "Right-Hand Bat",BowlingStyle="Right-arm off spin",RunsScored=4425,WicketsTaken=13,MatchesPlayed=128  },
                new Cricketer { FullName = "Joe Root", Country = "England", Role = "Batsman", DateOfBirth = new DateTime(1990,12,30) ,BattingStyle = "Right-Hand Bat",BowlingStyle="Right-arm off spin",RunsScored=1325,WicketsTaken=37,MatchesPlayed=79 },
                new Cricketer { FullName = "Kane Williamson", Country = "New Zealand", Role = "Batsman", DateOfBirth = new DateTime(1990,8,8),BattingStyle = "Right-Hand Bat",BowlingStyle="Right-arm spin",RunsScored=3335,WicketsTaken=0,MatchesPlayed=111  },
            };

            await context.Cricketers.AddRangeAsync(cricketers);

            var now = DateTime.UtcNow;

            var matches = new List<Match>
            {
                // past completed
                new Match { StartTime = now.AddDays(-30), Venue = "Wankhede", TeamA = "India", TeamB = "England", ResultSummary = "India won by 8 wickets", IsCompleted = true },
                new Match { StartTime = now.AddDays(-20), Venue = "Lord's", TeamA = "England", TeamB = "New Zealand", ResultSummary = "England won by 21 runs", IsCompleted = true },
                // upcoming
                new Match { StartTime = now.AddDays(5), Venue = "Eden Gardens", TeamA = "India", TeamB = "New Zealand", IsCompleted = false },
                new Match { StartTime = now.AddDays(12), Venue = "MCG", TeamA = "Australia", TeamB = "India", IsCompleted = false },
            };

            await context.Matches.AddRangeAsync(matches);

            await context.SaveChangesAsync();
        }
    }
}
