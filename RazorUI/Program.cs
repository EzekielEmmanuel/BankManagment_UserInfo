using Infrastructure.EF.Contexts;
using Infrastructure.Identity;
using Infrastructure.Seeds;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace RazorUI;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build(); //.Run();
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            var userContext = services.GetRequiredService<UserDbContext>();
            userContext.Database.Migrate();

            // When the context is set up
            var dataContext = services.GetRequiredService<DataDbContext>();
            userContext.Database.Migrate();

            //Initialize Users and Roles
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            await UserDbContextSeed.SeedDefaultUserAsync(userManager, roleManager);
        }

        await host.RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}