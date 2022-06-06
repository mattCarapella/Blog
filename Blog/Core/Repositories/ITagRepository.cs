using Blog.Models;

namespace Blog.Core.Repositories;

public interface ITagRepository : IBlogRepository<Tag>
{
    //Task<Tag> TagFindAsync(Guid id);
    Task<Tag> TagFirstOrDefaultAsync(Guid id);
    Task<Tag> TagDetails(Guid id);
    Task<ICollection<Tag>> GetAllTags();
    //Task AddTag(Tag tag);
    //Task DeleteTag(Guid id);
    bool TagExists(Guid id);
}
