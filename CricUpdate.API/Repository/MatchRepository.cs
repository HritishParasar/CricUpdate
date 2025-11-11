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

        public async Task AddMatch(Match match)
        {
            await dbContext.Matches.AddAsync(match);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<Match>> GetMatch()
        {
            return await dbContext.Matches.ToListAsync();
        }

        public async Task<Match> GetMatchByID(int id)
        {
            var fin = await dbContext.Matches.FindAsync(id);
            if (fin == null)
                return null;
            return fin;
        }

        public async Task UpdateMatch(Match match)
        {
            dbContext.Matches.Update(match);
            await dbContext.SaveChangesAsync();
        }
    }
}
