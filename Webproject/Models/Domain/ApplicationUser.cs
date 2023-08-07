using Microsoft.AspNetCore.Identity;

namespace AnimalStoreMvc.Models.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}