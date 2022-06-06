using Blog.Core.Repositories;
using Blog.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Blog.Repositories;

public class BlogRepository<T> : IBlogRepository<T> where T : class
{
    private readonly BlogContext _context;

    public BlogRepository(BlogContext context)
    {
        _context = context;
    }

    public async Task<T> FindAsync(Guid id)
    {
        var item = await _context.Set<T>().FindAsync(id);
        if (item == null) return null!;
        return item;
    }


    public async Task<T> FirstOrDefaultAsync(Guid id, Expression<Func<T, bool>> expression)
    {
        var item = await _context.Set<T>()
                        .AsNoTracking()
                        .FirstOrDefaultAsync(expression);
        if (item == null) return null!;
        return item;
    }

    public IQueryable<T> GetIQueryableList()
    {
        var iq = from a in _context.Set<T>() select a;
        return iq;
    }

    public async Task Add(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }

    public async Task Delete(Guid id)
    {
        var item = await _context.Set<T>().FindAsync(id);
        if (item is null) return;
        _context.Set<T>().Remove(item);
    }
    

}
