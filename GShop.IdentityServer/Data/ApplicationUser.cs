using Microsoft.AspNetCore.Identity;

namespace GShop.IdentityServer.Data;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
}
