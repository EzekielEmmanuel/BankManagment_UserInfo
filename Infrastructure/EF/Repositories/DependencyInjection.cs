using Application;
using Application.BankAccounts.Repositories;
using Infrastructure.BankAccounts.Repositories;
using Infrastructure.Identity.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.EF.Repositories;

public static class DependencyInjection
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(typeof(IBankAccountRepository), typeof(BankAccountRepository));
        services.AddScoped(typeof(IUserInfoRepository), typeof(UserInfoRepository));

        return services;
    }
}