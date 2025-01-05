using MY_API_PROJECT.Data;
using MY_API_PROJECT.Interfaces;
using MY_API_PROJECT.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MY_API_PROJECT.Repositories
{
    public class AuthRepository : IAuthService
    {
        private readonly AppDBContext _context;

        public AuthRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<User?> RegisterUserAsync(User user)
        {
            user.PasswordHash = HashPassword(user.PasswordHash);
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> LoginAsync(string email, string password)
        {
            var user = await GetUserByEmailAsync(email);
            if (user == null || !VerifyPassword(user.PasswordHash, password))
            {
                return null; 
            }
            return user;
        }

        public async Task<bool> ResetPasswordAsync(string email, string newPassword)
        {
            var user = await GetUserByEmailAsync(email);
            if (user == null)
            {
                return false;
            }

            user.PasswordHash = HashPassword(newPassword);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<string?> ForgotPasswordAsync(string email)
        {
            var user = await GetUserByEmailAsync(email);
            if (user == null)
            {
                return null;
            }

          
            var token = GenerateResetToken();
            user.PasswordResetToken = token;
            user.TokenExpiration = DateTime.UtcNow.AddHours(1);

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return token;
        }

        public Task Logout()
        {
          
            return Task.CompletedTask;
        }

       
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        private bool VerifyPassword(string hashedPassword, string inputPassword)
        {
            var inputHashed = HashPassword(inputPassword);
            return hashedPassword == inputHashed;
        }

        private string GenerateResetToken()
        {
            using var rng = RandomNumberGenerator.Create();
            var bytes = new byte[32];
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
}
