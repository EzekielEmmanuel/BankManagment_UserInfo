using Application.Common.Services;
using Application.UserManagment.Interfaces;
using Infrastructure.EF.Contexts;
using Infrastructure.EF.Repositories;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            // options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

        services.AddDbContext<UserDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            // options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<UserDbContext>()
            .AddDefaultTokenProviders();

        //Set up identity
        // services
        //     .AddDefaultIdentity<ApplicationUser>()
        //     .AddRoles<IdentityRole>()
        //     .AddEntityFrameworkStores<UserDbContext>();

        // services.AddIdentityServer()
        //     .AddApiAuthorization<ApplicationUser, UserDbContext>();

        //Set up config
        services.Configure<JwtConfig>(configuration.GetSection("JwtConfig"));

        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IAuthService, AuthService>();

        services.AddRepositories(configuration);
        // services.AddScoped()
        // services.AddScoped(typeof(TestRepository), typeof(TestRepository));
        // services.AddScoped(typeof(ICrudRepository<,>),typeof(CrudRepository<,>));
        // services.AddScoped(typeof(ICrudRepository<TestModel, int>), typeof(TestRepository));

        return services;
    }
}