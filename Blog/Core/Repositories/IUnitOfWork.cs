namespace Blog.Core.Repositories;

public interface IUnitOfWork
{
    IArticleRepository ArticleRepository { get; }
    IBlogImageRepository BlogImageRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    ITagRepository TagRepository { get; }
    IUserRepository UserRepository { get; }
    Task<bool> SaveAsync();
}
