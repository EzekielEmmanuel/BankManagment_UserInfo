using System.Security.Claims;
using Application.Common.Services;
using Application.Interfaces;
using Application.UserManagment.Interfaces;
using Infrastructure.EF.Contexts;
using Infrastructure.EF.Models;
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
        });
        
        services.AddDbContext<UserDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
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

        services.AddScoped<ICurrentUserService<ClaimsPrincipal>, CurrentUserService>();
        services.AddScoped<IAuthService, AuthService>();
        // services.AddScoped(typeof(ICrudRepository<,>),typeof(CrudRepository<,>));
        // services.AddScoped(typeof(ICrudRepository<TestModel, int>), typeof(TestRepository));

        return services;
    }
}