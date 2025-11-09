using CricUpdate.API.Models;

namespace CricUpdate.API.Repository
{
    public interface IAuthRepository
    {
        Task<User?> GetUserAsync(string username);
        Task<User> RegisterAsync(User user);
        Task<bool> SaveAllAsync();
    }
}
