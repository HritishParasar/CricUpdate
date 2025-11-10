using CricUpdate.API.Models;

namespace CricUpdate.API.Repository
{
    public interface ICricketerRepository
    {
        Task<Cricketer> GetCricketer(string name);
    }
}
