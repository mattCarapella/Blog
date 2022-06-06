using Blog.Core.Repositories;
using Blog.Data;
using Blog.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Repositories;

public class TagRepository : BlogRepository<Tag>, ITagRepository
{
    private readonly BlogContext _context;

    public TagRepository(BlogContext context):base(context)
    {
        _context = context;
    }

    public async Task<Tag> TagFindAsync(Guid id)
    {
        var tag = await _context.Tags.FindAsync(id);
        if (tag == null) return null!;
        return tag;
    }

    public async Task<Tag> TagFirstOrDefaultAsync(Guid id)
    {
        var tag = await _context.Tags
                            .AsNoTracking()
                            .FirstOrDefaultAsync(a => a.Id == id);
        if (tag == null) return null!;
        return tag;
    }

    public async Task<Tag> TagDetails(Guid id)
    {
        var tag = await _context.Tags
                            .Include(c => c.Articles)
                                .ThenInclude(a => a.Author)
                            .AsNoTracking()
                            .FirstOrDefaultAsync(a => a.Id == id);
        if (tag == null) return null!;
        return tag;
    }

    public async Task<ICollection<Tag>> GetAllTags()
    {
        return await _context.Tags.AsNoTracking().ToListAsync();
    }

    //public async Task AddTag(Tag tag)
    //{
    //    await _context.Tags.AddAsync(tag);
    //}

    //public async Task DeleteTag(Guid id)
    //{
    //    var tag = await _context.Tags.FindAsync(id);
    //    _context.Tags.Remove(tag!);
    //}


    public bool TagExists(Guid id)
    {
        return (_context.Tags?.Any(e => e.Id == id)).GetValueOrDefault();
    }

}
