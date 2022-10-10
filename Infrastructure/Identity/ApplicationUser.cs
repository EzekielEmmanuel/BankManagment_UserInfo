using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Ssn { get; set; }
    public DateTimeOffset Dob { get; set; }
    public string Address { get; set; }
}