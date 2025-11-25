using Microsoft.AspNetCore.Identity;


namespace Music_Domain.Entities
{
    public class Users : IdentityUser
    {

        public string? FullName { get; set; }
    }
}