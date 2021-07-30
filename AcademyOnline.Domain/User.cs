using Microsoft.AspNetCore.Identity;

namespace AcademyOnline.Domain
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
    }
}
