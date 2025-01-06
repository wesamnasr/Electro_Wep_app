using MY_API_PROJECT.Models;
using System.Threading.Tasks;

namespace MY_API_PROJECT.Interfaces
{
    public interface IAuthService
    {
        Task<User?> RegisterUserAsync(User user);
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> LoginAsync(string email, string password);
        Task<bool> ResetPasswordAsync(string email, string newPassword);
        Task<string?> ForgotPasswordAsync(string email);

        Task Logout();
    }
}
