using CricUpdate.API.Models;

namespace CricUpdate.API.Repository
{
    public interface ICricketerRepository
    {
        Task<Cricketer> GetCricketer(string name);
        Task AddCricketer(Cricketer cricketer);
        Task UpdateCricketer(Cricketer cricketer);
        Task DeleteCricketer(int id);
        Task<Cricketer> GetCricketerByID(int id);
    }
}
