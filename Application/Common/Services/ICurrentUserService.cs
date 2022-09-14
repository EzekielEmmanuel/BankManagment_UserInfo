namespace Application.Common.Services;

public interface ICurrentUserService<out TUser>
{
    string? UserId { get; }
    TUser? User { get; }
}