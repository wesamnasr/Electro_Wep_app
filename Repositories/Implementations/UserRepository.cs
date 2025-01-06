using MY_API_PROJECT.Models;
using MY_API_PROJECT.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNet.Identity;
using MY_API_PROJECT.DTO.AuthDTOS;

namespace MY_API_PROJECT.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;
        private readonly AppDBContext _context;

        public UserRepository(AppDBContext context, Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this._userManager = userManager;
        }

        public async Task<ApplicationUser?> GetUserByIdAsync(int id)
        {
            return await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id.ToString());
        }

        
        public async Task<ApplicationUser?> GetUserByUsernameAsync(string username)
        {
            return await _userManager.Users.FirstOrDefaultAsync(u=>u.UserName == username);
        }

        
        public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
        {
            return await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

     
        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
           var users= await _userManager.Users.ToListAsync();
            return users;

        }

       
        public async Task<ApplicationUser> CreateUserAsync(RegisterDTO registerDTO)
        {
            var user = new ApplicationUser
            {
                UserName = registerDTO.UserName,
                Email = registerDTO.Email,
                FirstName=registerDTO.FirstName,
                LastName=registerDTO.LastName,

            };
            var result = await _userManager.CreateAsync(user,registerDTO.Password);

            if (result.Succeeded)
            {
                return user;
            }
            else
            {
             
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error.Description);
                }
                return null; 
            }
        }

    
        public async Task<ApplicationUser> UpdateUserAsync(int userId,UpdateUserDTO updateUser)
        {

            var user =await _userManager.FindByIdAsync(userId.ToString());

            if (user == null) { return null; }

            user.UserName = updateUser.UserName;
            user.Email = updateUser.Email;
            user.FirstName = updateUser.FirstName;
            user.LastName = updateUser.LastName;

            
            if (!string.IsNullOrEmpty(updateUser.Password))
            {
                var passwordHasher = new PasswordHasher<ApplicationUser>();
                user.PasswordHash = passwordHasher.HashPassword(user, updateUser.Password);
            }

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return user;
            }

            return null;



        }

      
        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await GetUserByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
                return true;
            }
            return false;
            
        }

     
    }
}
