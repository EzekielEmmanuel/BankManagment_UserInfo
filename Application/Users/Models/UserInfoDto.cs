namespace Application.Users.Models;

public record UserInfoDto(string Id, string FirstName, string LastName, string Ssn, DateTimeOffset Dob, string Address,
    string Phone, string Email);