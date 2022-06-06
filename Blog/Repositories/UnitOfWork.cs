using Blog.Core.Repositories;
using Blog.Data;
using Blog.Models;
using Microsoft.AspNetCore.Identity;

namespace Blog.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly BlogContext _context;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IHttpContextAccessor _accessor;

    public UnitOfWork(BlogContext context, SignInManager<ApplicationUser> signInManager, 
                        UserManager<ApplicationUser> userManager, IHttpContextAccessor accessor)
    {
        _context = context;
        _signInManager = signInManager;
        _userManager = userManager;
        _accessor = accessor;
    }

    public IArticleRepository ArticleRepository => new ArticleRepository(_context);
    public IBlogImageRepository BlogImageRepository => new BlogImageRepository(_context);
    public ICategoryRepository CategoryRepository => new CategoryRepository(_context);
    public ITagRepository TagRepository => new TagRepository(_context);
    public IUserRepository UserRepository => new UserRepository(_context, _signInManager, _userManager, _accessor);

    public async Task<bool> SaveAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
