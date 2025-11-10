using CricUpdate.API.Data;
using CricUpdate.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CricUpdate.API.Repository
{
    public class CricketerRepository : ICricketerRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CricketerRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Cricketer> GetCricketer(string name)
        {
            return await dbContext.Cricketers
                .FirstOrDefaultAsync(c => c.FullName.ToLower() == name.ToLower());
        }
    }
}
