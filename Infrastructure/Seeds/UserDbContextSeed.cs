using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Seeds;

public class UserDbContextSeed
{
    public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        var roles = new List<IdentityRole>
        {
            new("Administrator"),
            new("Teller"),
            new("Customer")
        };

        if (!roleManager.Roles.Any())
            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);

                var user = new ApplicationUser
                {
                    UserName = role.Name,
                    Email = $"{role.Name}@mail.com"
                };

                await userManager.CreateAsync(user, role.Name + "@123");
                await userManager.AddToRoleAsync(user, role.Name);
            }
    }
}