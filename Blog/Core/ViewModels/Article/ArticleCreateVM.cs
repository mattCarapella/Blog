using Blog.Models;
using static Blog.Core.Enums;

namespace Blog.Core.ViewModels.Article;

public class ArticleCreateVM
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Entry { get; set; } = string.Empty;
    public Level Level { get; set; }
    public Status Status { get; set; } = Enums.Status.InProgress;
    public string? AuthorId { get; set; }
    public ApplicationUser? Author { get; set; }
    public ICollection<Models.Category> Categories { get; set; } = new List<Models.Category>();
    public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    public List<BlogImage> Images { get; set; } = new List<BlogImage>();
}
