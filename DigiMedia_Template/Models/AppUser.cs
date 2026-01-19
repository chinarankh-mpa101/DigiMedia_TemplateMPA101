using Microsoft.AspNetCore.Identity;

namespace DigiMedia_Template.Models
{
    public class AppUser:IdentityUser
    {
        public string Fullname { get; set; }
    }
}
