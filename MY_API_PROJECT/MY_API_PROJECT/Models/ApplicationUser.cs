using Microsoft.AspNetCore.Identity;

namespace MY_API_PROJECT.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
