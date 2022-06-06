using Blog.Models;

namespace Blog.Core.ViewModels;

public class CategoryArticleGroup
{
    public Models.Category Category { get; set; }
    public int ArticleCount { get; set; }
}
