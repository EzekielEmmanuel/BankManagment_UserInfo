﻿using Application.Common.Identity;
using Infrastructure.EF.Contexts;
using Infrastructure.EF.Identity;
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
        services.AddDbContext<UserDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        // services.AddIdentity<ApplicationUser, IdentityRole>()
        //     .AddEntityFrameworkStores<UserDbContext>()
        //     .AddDefaultTokenProviders();
        
        //Set up identity
        services
            .AddDefaultIdentity<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<UserDbContext>();
        
        services.AddIdentityServer()
            .AddApiAuthorization<ApplicationUser, UserDbContext>();
        
        //Set up config
        services.Configure<JwtConfig>(configuration.GetSection("JwtConfig"));

        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IAuthService, AuthService>();
        
        return services;
    }
}