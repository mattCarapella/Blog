using Blog.Core.Repositories;
using Blog.Data;
using Blog.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Repositories;

public class CategoryRepository : BlogRepository<Category>, ICategoryRepository
{
    private readonly BlogContext _context;

    public CategoryRepository(BlogContext context):base(context)
    {
        _context = context;
    }


    public async Task<Category> CategoryFindAsync(Guid id)
    {
        var category = await _context.Categories.FindAsync(id);
        return category!;
    }

    public async Task<Category> CategoryFirstOrDefaultAsync(Guid id)
    {
        var category = await _context.Categories
                            .AsNoTracking()
                            .FirstOrDefaultAsync(a => a.Id == id);
        if (category == null) return null!;
        return category;
    }

    public async Task<Category> CategoryDetails(Guid id)
    {
        var category = await _context.Categories
                            .Include(c => c.Articles)
                                .ThenInclude(a => a.Author)
                            .AsNoTracking()
                            .FirstOrDefaultAsync(a => a.Id == id);
        if (category == null) return null!;
        return category;
    }

    public async Task<ICollection<Category>> GetAllCategories()
    {
        return await _context.Categories
                     .AsNoTracking()
                     .ToListAsync();
    }

    public async Task AddCategory(Category category)
    {
        await _context.Categories.AddAsync(category);
    }


    public async Task DeleteCategory(Guid id)
    {
        var category = await _context.Categories.FindAsync(id);
        _context.Categories.Remove(category!);
    }


    public bool CategoryExists(Guid id)
    {
        return (_context.Categories?.Any(e => e.Id == id)).GetValueOrDefault();
    }

}
