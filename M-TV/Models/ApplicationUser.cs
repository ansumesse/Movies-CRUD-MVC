using Microsoft.AspNetCore.Identity;

namespace M_TV.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
