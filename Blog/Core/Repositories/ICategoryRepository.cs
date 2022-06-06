using Blog.Models;

namespace Blog.Core.Repositories;

public interface ICategoryRepository : IBlogRepository<Category>
{
    Task<Category> CategoryFindAsync(Guid id);
    Task<Category> CategoryFirstOrDefaultAsync(Guid id);
    Task<Category> CategoryDetails(Guid id);
    Task<ICollection<Category>> GetAllCategories();
    Task AddCategory(Category category);
    Task DeleteCategory(Guid id);
    bool CategoryExists(Guid id);
}
