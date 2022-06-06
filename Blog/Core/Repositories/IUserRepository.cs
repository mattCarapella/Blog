using Blog.Models;

namespace Blog.Core.Repositories;

public interface IUserRepository
{
    Task<ApplicationUser> GetCurrentUser();
}
