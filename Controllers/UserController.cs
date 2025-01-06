using Azure.Messaging;
using Microsoft.AspNetCore.Mvc;
using MY_API_PROJECT.DTO.AuthDTOS;
using MY_API_PROJECT.Models;
using MY_API_PROJECT.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MY_API_PROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDTO model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userRepository.CreateUserAsync(model);

                if (user != null)
                {
                    return Ok(new { Message = "User created successfully", User = user });
                }

                return BadRequest("User creation failed");


            }

            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id, [FromBody] UpdateUserDTO user)
        {
          if(ModelState.IsValid)
            {
                var update = await _userRepository.UpdateUserAsync(id,user);
                if(update==null)
                {
                    return NotFound(new { Message = "\"User not found or update failed\"" });
                }
                return Ok(new { Message = "User updated successfully", User = update });
            }
            return NotFound(new { Message = "\"User not found or update failed\"" });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
           var isDel = await _userRepository.DeleteUserAsync(id);
            if (isDel)
            {
                return Ok(new { Message = "User deleted successfully" });
            }

            return NotFound(new { Message = "User not found or could not be deleted" });
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return Ok(users);
        }
    }
}
