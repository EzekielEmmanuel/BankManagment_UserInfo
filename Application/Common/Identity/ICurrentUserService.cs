namespace Application.Common.Identity;

public interface ICurrentUserService<out TUser>
{
    string? UserId { get; }
    TUser? User { get; }
}