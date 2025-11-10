using CricUpdate.API.Models;

namespace CricUpdate.API.Repository
{
    public interface IMatchRepository
    {
        Task<List<Match>> GetMatch();
    }
}
