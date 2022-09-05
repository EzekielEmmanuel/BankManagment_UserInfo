using Duende.IdentityServer.EntityFramework.Options;
using Infrastructure.EF.Models;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infrastructure.EF.Contexts;

public class UserDbContext : ApiAuthorizationDbContext<ApplicationUser>
{
    public UserDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions) : base(
        options, operationalStoreOptions)
    {
    }
}