using System.Security.Cryptography;
using System.Text;

namespace CricUpdate.API.Services
{
    public class PasswordService
    {
        public static void CreatePasswordHash(string password, out byte[] hashpass , out byte[] saltpass)
        {
            using var hmac = new HMACSHA512();  
            saltpass = hmac.Key;
            hashpass = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
        public static bool VerifyPasswordHash(string password, byte[] hashpass , byte[] saltpass)
        {
            using var hmac = new HMACSHA512(saltpass);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            if(computedHash.Length != hashpass.Length) return false;
            for (int i = 0; i < computedHash.Length;i ++)
            {
                if (computedHash[i] != hashpass[i]) return false;
            }
            return true;
        }
    }
}
