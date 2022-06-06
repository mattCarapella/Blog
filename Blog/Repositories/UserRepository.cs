using Blog.Core.Repositories;
using Blog.Data;
using Blog.Models;
using Microsoft.AspNetCore.Identity;

namespace Blog.Repositories;

public class UserRepository : IUserRepository
{
    private readonly BlogContext _context;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IHttpContextAccessor _accessor;

    public UserRepository(BlogContext context, SignInManager<ApplicationUser> signInManager,
                          UserManager<ApplicationUser> userManager, IHttpContextAccessor accessor)
    {
        _context = context;
        _signInManager = signInManager;
        _userManager = userManager;
        _accessor = accessor;
    }

    public async Task<ApplicationUser> GetCurrentUser()
    {
        var currentUser = await _userManager.GetUserAsync(_accessor?.HttpContext!.User);
        return currentUser;
    }
}
