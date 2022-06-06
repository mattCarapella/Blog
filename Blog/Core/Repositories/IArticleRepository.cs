using Blog.Models;

namespace Blog.Core.Repositories;

public interface IArticleRepository : IBlogRepository<Article>
{
    //Task<Article> ArticleFindAsync(Guid id);
    Task<Article> ArticleFirstOrDefaultAsync(Guid articleId);
    Task<Article> ArticleDetails(Guid id);
    Task<ICollection<Article>> GetAllArticles();
    //Task AddArticle(Article article);
    //Task DeleteArticle(Guid id);
    bool ArticleExists(Guid id);
}
