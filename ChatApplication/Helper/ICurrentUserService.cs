using ChatApplication.Models;

namespace ChatApplication.Helper;

public interface ICurrentUserService
{
    public string UserId { get; }
    Task<AppUser> GetCurrentUserAsync();
}
