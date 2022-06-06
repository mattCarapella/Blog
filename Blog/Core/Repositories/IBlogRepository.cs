namespace Blog.Core.Repositories;

public interface IBlogRepository<T> where T : class
{
    Task<T> FindAsync(Guid id);
    Task Add(T entity);
    Task Delete(Guid id);
    IQueryable<T> GetIQueryableList();
}
