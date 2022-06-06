using Blog.Core.ViewModels;
using Blog.Data;
using Blog.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Areas.Articles.Pages;

public class ArticleCategoriesPageModel : PageModel
{
    public List<AssignedCategoryData> AssignedCategoryDataList;

    public void PopulateAssignedCategoryData(BlogContext context, Article article)
    {
        var allCategories = context.Categories;
        var articleCategories = new HashSet<Guid>(
            article.Categories.Select(c => c.Id));
        AssignedCategoryDataList = new List<AssignedCategoryData>();

        foreach (var category in allCategories)
        {
            AssignedCategoryDataList.Add(new AssignedCategoryData
            {
                Id = category.Id,
                Name = category.Name,
                Assigned = articleCategories.Contains(category.Id)
            });
        }
    }
}
