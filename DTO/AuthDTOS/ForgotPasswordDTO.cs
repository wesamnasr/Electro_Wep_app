using System.ComponentModel.DataAnnotations;

namespace MY_API_PROJECT.DTO.AuthDTOS
{
    public class ForgotPasswordDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
