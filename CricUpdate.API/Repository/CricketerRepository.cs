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

        public async Task AddCricketer(Cricketer cricketer)
        {
            await dbContext.Cricketers.AddAsync(cricketer);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteCricketer(int id)
        {
            var find = await dbContext.Cricketers.FindAsync(id);
            dbContext .Cricketers.Remove(find);
            await dbContext.SaveChangesAsync();
        }

        public async Task<Cricketer> GetCricketer(string name)
        {
            return await dbContext.Cricketers
                .FirstOrDefaultAsync(c => c.FullName.ToLower() == name.ToLower());
        }

        public async Task<Cricketer> GetCricketerByID(int id)
        {
            var find = await dbContext.Cricketers.FindAsync(id);
            if(find == null)
                return null;
            return find;
        }

        public async Task UpdateCricketer(Cricketer cricketer)
        {
            dbContext.Cricketers.Update(cricketer);
            await dbContext.SaveChangesAsync();
        }
    }
}
