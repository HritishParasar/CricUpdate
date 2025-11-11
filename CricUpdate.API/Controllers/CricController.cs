using CricUpdate.API.Models;
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
        [HttpPost("addPlayer")]
        public async Task<IActionResult> AddCricketer([FromBody] CricketerDTO cricketerDTO)
        {
            var cricketer = new Cricketer
            {
                FullName = cricketerDTO.FullName,
                Country = cricketerDTO.Country,
                DateOfBirth = cricketerDTO.DateOfBirth,
                Role = cricketerDTO.Role,
                BattingStyle = cricketerDTO.BattingStyle,
                BowlingStyle = cricketerDTO.BowlingStyle,
                MatchesPlayed = cricketerDTO.MatchesPlayed,
                RunsScored = cricketerDTO.RunsScored,
                WicketsTaken = cricketerDTO.WicketsTaken
            };
            await cricketerRepository.AddCricketer(cricketer);
            return Ok("Successfully added.");
        }
        [HttpPut("updatePlayer/{id}")]
        public async Task<IActionResult> UpdatePlayer(int id, CricketerDTO cricketerDTO)
        {
            var cricketer = await cricketerRepository.GetCricketerByID(id);
            if (cricketer == null)
                return BadRequest("Cricketer with given Id could not found.");
            cricketer.FullName = cricketerDTO.FullName;
            cricketer.DateOfBirth = cricketerDTO.DateOfBirth;
            cricketer.Country = cricketerDTO.Country;
            cricketer.Role = cricketerDTO.Role;
            cricketer.RunsScored = cricketerDTO.RunsScored;
            cricketer.MatchesPlayed = cricketerDTO.MatchesPlayed;
            cricketer.BattingStyle = cricketerDTO.BattingStyle;
            cricketer.BattingStyle = cricketerDTO.BowlingStyle;
            cricketer.WicketsTaken = cricketerDTO.WicketsTaken;

            await cricketerRepository.UpdateCricketer(cricketer);
            return Ok("Successfully updated.");
        }
        [HttpDelete("deleteCricketer/{id}")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            await cricketerRepository.DeleteCricketer(id);
            return Ok("Successfully deleted.");
        }
        [HttpPost("addMatch")]
        public async Task<IActionResult> AddMatch([FromBody]Match match)
        {
           await matchRepository.AddMatch(match);
            return Ok("Successfully Added");
        }
        [HttpPut("updateMatch/{id}")]
        public async Task<IActionResult> UpdateMatch(int id, UpdateMatchDTO updateMatch)
        {
            var find = await matchRepository.GetMatchByID(id);
            if (find is null)
                return BadRequest("Match does not exist with given Id");
            find.ResultSummary = updateMatch.ResultSummary;
            find.IsCompleted = updateMatch.IsCompleted;
            await matchRepository.UpdateMatch(find);
            return Ok("Successfully Updated");
        }
    }
}
