using CricUpdate.API.Models;

namespace CricUpdate.API.Repository
{
    public interface IMatchRepository
    {
        Task<List<Match>> GetMatch();
        Task AddMatch(Match match);
        Task UpdateMatch(Match match);
        Task<Match> GetMatchByID(int id);
    }
}
