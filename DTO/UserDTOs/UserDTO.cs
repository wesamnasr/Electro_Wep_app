using System.ComponentModel.DataAnnotations;

namespace MY_API_PROJECT.DTO.UserDTOs
{
    public class UserDTO
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
