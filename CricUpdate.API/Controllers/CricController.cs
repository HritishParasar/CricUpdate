using CricUpdate.API.Models.DTOs;
using CricUpdate.API.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CricUpdate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CricController : ControllerBase
    {
        private readonly IMatchRepository matchRepository;
        private readonly ICricketerRepository cricketerRepository;

        public CricController(IMatchRepository matchRepository, ICricketerRepository cricketerRepository)
        {
            this.matchRepository = matchRepository;
            this.cricketerRepository = cricketerRepository;
        }
        [HttpGet("Upcomingmatches")]
        public async Task<IActionResult> GetUpcomingMatches()
        {
            var matches = await matchRepository.GetMatch();
            var upcomingMatches = matches.Where(m =>
                    m.IsCompleted == false && DateTime.Now <= m.StartTime)
                    .OrderBy(t => t.StartTime);
            var upcomingMatchesDto = upcomingMatches.Select(m => new MatchDTO
            {
                Id = m.Id,
                Teams = $"{m.TeamA} vs {m.TeamB}",
                StartTime = m.StartTime,
                Venue = m.Venue,
                IsCompleted = m.IsCompleted
            }).ToList();

            return Ok(upcomingMatchesDto);
        }
        [HttpGet("Pastmatches")]
        public async Task<IActionResult> GetPastResult()
        {
            var matches = await matchRepository.GetMatch();

            var pastMatches = matches.Where(m =>
                    m.IsCompleted == true && DateTime.Now > m.StartTime)
                    .OrderByDescending(t => t.StartTime);

            var pastMatchesDto = pastMatches.Select(m => new MatchDTO
            {
                Id = m.Id,
                Teams = $"{m.TeamA} vs {m.TeamB}",
                StartTime = m.StartTime,
                Venue = m.Venue,
                ResultSummary = m.ResultSummary,
                IsCompleted = m.IsCompleted
            });
            return Ok(pastMatchesDto);
        }
        [HttpGet("cricketer/{name}")]
        public async Task<IActionResult> GetCricketerById(string name)
        {
            var cricketer = await cricketerRepository.GetCricketer(name);
            if (cricketer == null)
            {
                return NotFound("No Cricketer Found.");
            }
            var cricketerDto = new CricketerDTO
            {
                FullName = cricketer.FullName,
                Country = cricketer.Country,
                DateOfBirth = cricketer.DateOfBirth,
                Role = cricketer.Role,
                BattingStyle = cricketer.BattingStyle,
                BowlingStyle = cricketer.BowlingStyle,
                MatchesPlayed = cricketer.MatchesPlayed,
                RunsScored = cricketer.RunsScored,
                WicketsTaken = cricketer.WicketsTaken
            };
            return Ok(cricketerDto);
        }
    }
}
