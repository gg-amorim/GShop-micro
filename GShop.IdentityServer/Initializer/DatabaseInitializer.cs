using GShop.IdentityServer.Configuration;
using GShop.IdentityServer.Data;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace GShop.IdentityServer.Initializer;

public class DatabaseInitializer : IDatabaseInitializer
{
    private readonly AppDbContext _context;
    private readonly UserManager<ApplicationUser> _user;
    private readonly RoleManager<IdentityRole> _role;

    public DatabaseInitializer(AppDbContext context, UserManager<ApplicationUser> user, RoleManager<IdentityRole> role)
    {
        _context = context;
        _user = user;
        _role = role;
    }

    public void Initialize()
    {
        if (_role.FindByNameAsync(IdentityConfiguration.Admin).Result is not null)
            return;
        _role.CreateAsync(new IdentityRole(IdentityConfiguration.Admin)).GetAwaiter().GetResult();
        _role.CreateAsync(new IdentityRole(IdentityConfiguration.Client)).GetAwaiter().GetResult();

        ApplicationUser admin = new()
        {
            UserName = "gui-admin",
            Email = "gui-admin@dev.com.br",
            EmailConfirmed = true,
            PhoneNumber = "+55 (43) 12345-6789",
            FirstName = "Guilherme",
            LastName = "Admin"
        };

        _user.CreateAsync(admin, "Devmaior@123").GetAwaiter().GetResult();
        _user.AddToRoleAsync(admin, IdentityConfiguration.Admin).GetAwaiter().GetResult();

        var adminClaims = _user.AddClaimsAsync(admin, new Claim[]
        {
            new Claim(JwtClaimTypes.Name, $"{admin.FirstName} {admin.LastName}"),
            new Claim(JwtClaimTypes.GivenName, admin.FirstName),
            new Claim(JwtClaimTypes.FamilyName, admin.LastName),
            new Claim(JwtClaimTypes.Role,  IdentityConfiguration.Admin )
        }).Result;

        ApplicationUser client = new()
        {
            UserName = "gui-client",
            Email = "gui-client@dev.com.br",
            EmailConfirmed = true,
            PhoneNumber = "+55 (43) 12345-6789",
            FirstName = "Guilherme",
            LastName = "Client"
        };

        _user.CreateAsync(client, "Devmaior@123").GetAwaiter().GetResult();
        _user.AddToRoleAsync(client, IdentityConfiguration.Client).GetAwaiter().GetResult();

        var clienteClaims = _user.AddClaimsAsync(client, new Claim[]
        {
            new Claim(JwtClaimTypes.Name, $"{client.FirstName} {client.LastName}"),
            new Claim(JwtClaimTypes.GivenName, client.FirstName),
            new Claim(JwtClaimTypes.FamilyName, client.LastName),
            new Claim(JwtClaimTypes.Role,  IdentityConfiguration.Client )
        }).Result;
    }
}
