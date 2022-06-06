using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Blog.Data;
using Blog.Models;
using Blog.Core.Repositories;

namespace Blog.Areas.Articles.Pages;

public class EditModel : ArticleCategoriesPageModel
{
    private readonly BlogContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public EditModel(BlogContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    [BindProperty]
    public Article Article { get; set; } = default!;


    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id is null) return NotFound();
        var articleId = id.Value;

        Article = await _context.Articles
            .Include(a => a.Categories)
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == articleId);

        if (Article is null) return NotFound();

        PopulateAssignedCategoryData(_context, Article);
        return Page();

    }

    public async Task<IActionResult> OnPostAsync(Guid? id, string[] selectedCategories)
    {
        if (id is null) return NotFound();
        var articleId = id.Value;

        var articleToUpdate = await _context.Articles
            .Include(a => a.Categories)
            .FirstOrDefaultAsync(a => a.Id == articleId);

        if (articleToUpdate is null) return NotFound();

        if (await TryUpdateModelAsync<Article>(
            articleToUpdate,
            "Article",
            a => a.Title, a => a.Entry, a => a.Level, a => a.Status))
        {
            articleToUpdate.UpdatedAt = DateTime.Now;
            UpdateArticleCategories(selectedCategories, articleToUpdate);
            await _unitOfWork.SaveAsync();
            return RedirectToPage("./Index");
        }
        PopulateAssignedCategoryData(_context, articleToUpdate);
        return Page();
    }

    public void UpdateArticleCategories(string[] selectedCategories, Article articleToUpdate)
    {
        if (selectedCategories is null)
        {
            articleToUpdate.Categories = new List<Category>();
            return;
        }

        var selectedCategoriesHashSet = new HashSet<string>(selectedCategories);
        var articleCategories = new HashSet<Guid>
            (articleToUpdate.Categories.Select(c => c.Id));

        foreach (var category in _context.Categories)
        {
            if (selectedCategoriesHashSet.Contains(category.Id.ToString()))
            {
                if (!articleCategories.Contains(category.Id))
                {
                    articleToUpdate.Categories.Add(category);
                }
            }
            else
            {
                if (articleCategories.Contains(category.Id))
                {
                    var categoryToRemove = articleToUpdate.Categories.Single(
                        c =>c.Id == category.Id);
                        articleToUpdate.Categories.Remove(categoryToRemove);
                }
            }
        }
    }
}






//public class EditModel : PageModel
//{
//    private readonly IUnitOfWork _unitOfWork;

//    public EditModel(IUnitOfWork unitOfWork)
//    {
//        _unitOfWork = unitOfWork;
//    }

//    [BindProperty]
//    public Article Article { get; set; } = default!;

//    public async Task<IActionResult> OnGetAsync(Guid? id)
//    {
//        if (id is null) return NotFound();
//        var articleId = id.Value;
        
//        Article = await _unitOfWork.ArticleRepository.FindAsync(articleId); 
        
//        if (Article is null) return NotFound();
//        return Page();
//    }

//    public async Task<IActionResult> OnPostAsync(Guid id)
//    {
//        var articleToEdit = await _unitOfWork.ArticleRepository.FindAsync(id);
//        if (articleToEdit is null) return NotFound();

//        if (await TryUpdateModelAsync<Article>(
//            articleToEdit,
//            "Article",
//            a=>a.Title, a=>a.Entry, a=>a.Level, a=>a.Status))
//        {
//            articleToEdit.UpdatedAt = DateTime.Now;
//            await _unitOfWork.SaveAsync();
//            return RedirectToPage("./Index");
//        }

//        return Page();
//    }

//}
