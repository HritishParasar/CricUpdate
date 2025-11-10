using CricUpdate.API.Data;
using CricUpdate.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CricUpdate.API.Repository
{
    public class MatchRepository : IMatchRepository
    {
        private readonly ApplicationDbContext dbContext;

        public MatchRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Match>> GetMatch()
        {
            return await dbContext.Matches.ToListAsync();
        }
    }
}
