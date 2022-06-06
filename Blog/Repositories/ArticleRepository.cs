using Blog.Core.Repositories;
using Blog.Data;
using Blog.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Repositories;

public class ArticleRepository : BlogRepository<Article>, IArticleRepository
{
    private readonly BlogContext _context;

    public ArticleRepository(BlogContext context):base(context)
    {
        _context = context;
    }

    //public async Task<Article> ArticleFindAsync(Guid id)
    //{
    //    var article = await _context.Articles.FindAsync(id);
    //    return article!;
    //}

    public async Task<Article> ArticleFirstOrDefaultAsync(Guid id)
    {
        var article = await _context.Articles
                            .AsNoTracking()
                            .FirstOrDefaultAsync(a => a.Id == id);
        if (article is null) return null!;
        return article;
    }


    public async Task<Article> ArticleDetails(Guid id)
    {
        var article = await _context.Articles
                            .Include(a => a.Author)
                            .Include(a => a.Categories)
                            .Include(a => a.Tags)
                            .AsNoTracking()
                            .FirstOrDefaultAsync(a => a.Id == id);
        if (article is null) return null!;
        return article;
    }


    public async Task<ICollection<Article>> GetAllArticles()
    {
        return await _context.Articles
                     .Include(a => a.Author)
                     .AsNoTracking()
                     .ToListAsync();
    }


    //public async Task AddArticle(Article article)
    //{
    //    await _context.Articles.AddAsync(article);
    //}


    //public async Task DeleteArticle(Guid id)
    //{
    //    var article = await _context.Articles.FindAsync(id);
    //    _context.Articles.Remove(article!);
    //}



    public bool ArticleExists(Guid id)
    {
        return (_context.Articles?.Any(e => e.Id == id)).GetValueOrDefault();
    }

}
