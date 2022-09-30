using System.Security.Claims;
using Application;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace RazorUI;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.Cookie.Name = CookieAuthenticationDefaults.AuthenticationScheme;
                options.LoginPath = "/";
                options.ExpireTimeSpan = TimeSpan.FromDays(1);
                options.SlidingExpiration = false;
            });

        services.AddAuthorization(config =>
        {
            config.AddPolicy("Administrator",
                policyBuilder => policyBuilder.RequireClaim(ClaimTypes.Role, "Administrator"));
            config.AddPolicy("Teller", policyBuilder => policyBuilder.RequireClaim(ClaimTypes.Role, "Teller"));
            config.AddPolicy("Customer", policyBuilder => policyBuilder.RequireClaim(ClaimTypes.Role, "Customer"));
            // config.AddPolicy("Teller", policyBuilder => policyBuilder.RequireClaim(ClaimTypes.Role, "Teller","Administrator"));
            // config.AddPolicy("Customer", policyBuilder => policyBuilder.RequireClaim(ClaimTypes.Role, "Customer","Teller","Administrator"));
        });

        services.AddControllersWithViews();

        services.AddRazorPages(options =>
        {
            options.Conventions.AuthorizeFolder("/Administrator", "Administrator");
            options.Conventions.AuthorizeFolder("/Teller", "Teller");
            options.Conventions.AuthorizeFolder("/Customer", "Customer");
            options.Conventions.AllowAnonymousToFolder("/Account");
        });

        services.AddApplication(Configuration);
        services.AddInfrastructure(Configuration);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                "default",
                "{controller=Home}/{action=Index}/{id?}");
            endpoints.MapControllers();
            endpoints.MapRazorPages();
        });
    }
}