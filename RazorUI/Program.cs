namespace RazorUI;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build(); //.Run();
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            // var userContext = services.GetRequiredService<UserDbContext>();
            // userContext.Database.Migrate();

            // When the context is set up
            // var dataContext = services.GetRequiredService<DataDbContext>();
            // userContext.Database.Migrate();

            //Initialize Users and Roles
            // var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            // var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            //
            // await UserDbContextSeed.SeedDefaultUserAsync(userManager, roleManager);
            //
            // var authService = services.GetRequiredService<IAuthService>();
            // var result = await authService.Login(new UserLoginDto(){Email = "Customer@mail.com", Password = "Customer@123"});
            // Console.WriteLine(result.Succeeded);
        }

        await host.RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}


// var builder = WebApplication.CreateBuilder(args);
//
// // Add services to the container.
// builder.Services.AddRazorPages();
//
// var app = builder.Build();
//
// // Configure the HTTP request pipeline.
// if (!app.Environment.IsDevelopment())
// {
//     app.UseExceptionHandler("/Error");
//     // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//     app.UseHsts();
// }
//
// app.UseHttpsRedirection();
// app.UseStaticFiles();
//
// app.UseRouting();
//
// app.UseAuthorization();
//
// app.MapRazorPages();
//
// app.Run();