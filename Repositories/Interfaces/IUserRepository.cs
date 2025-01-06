using MY_API_PROJECT.DTO.AuthDTOS;
using MY_API_PROJECT.Models;

namespace MY_API_PROJECT.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser?> GetUserByIdAsync(int id);
        Task<ApplicationUser?> GetUserByUsernameAsync(string username);
        Task<ApplicationUser?> GetUserByEmailAsync(string email);
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task <ApplicationUser> CreateUserAsync(RegisterDTO user);
        Task <ApplicationUser>UpdateUserAsync(int userId, UpdateUserDTO user);
        Task<bool> DeleteUserAsync(int id);
    }
}
