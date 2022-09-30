namespace Application.Common.Services;

public interface ICurrentUserService
{
    string? UserId { get; }
}