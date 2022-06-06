using Blog.Core.Repositories;
using Blog.Data;

namespace Blog.Repositories;

public class BlogImageRepository : IBlogImageRepository
{
    private readonly BlogContext _context;

    public BlogImageRepository(BlogContext context)
    {
        _context = context;
    }


}
