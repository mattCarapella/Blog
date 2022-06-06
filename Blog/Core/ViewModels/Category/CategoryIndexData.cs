using Blog.Models;

namespace Blog.Core.ViewModels.Category;

public class CategoryIndexData
{
    public IEnumerable<Models.Category> Categories { get; set; }
    public IEnumerable<Models.Article> Articles { get; set; }
}
