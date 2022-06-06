using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Blog.Models;
using Blog.Core.Repositories;
using Blog.Data;
using Microsoft.EntityFrameworkCore;

namespace Blog.Areas.Articles.Pages;

public class CreateModel : ArticleCategoriesPageModel
{
    private readonly BlogContext _context;
    private readonly ILogger<ArticleCategoriesPageModel> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public CreateModel(BlogContext context, ILogger<ArticleCategoriesPageModel> logger, IUnitOfWork unitOfWork)
    {
        _context = context;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public IActionResult OnGet()
    {
        var article = new Article();
        article.Categories = new List<Category>();
        PopulateAssignedCategoryData(_context, article);
        return Page();
    }

    [BindProperty]
    public Article Article { get; set; } = default!;

    public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
    {
        var currentUser = await _unitOfWork.UserRepository.GetCurrentUser();
        if (currentUser is null) return NotFound();
        var newArticle = new Article { Id = Guid.NewGuid(), AuthorId = currentUser.Id };

        if (selectedCategories.Length > 0)
        {
            newArticle.Categories = new List<Category>();
            _context.Categories.Load();
        }

        // Add selected categories to the new Article
        foreach (var category in selectedCategories)
        {
            var foundCategory = await _context.Categories.FindAsync(Guid.Parse(category));
            if (foundCategory != null)
            {
                newArticle.Categories.Add(foundCategory);
            }
            else
            {
                _logger.LogWarning("Category: {category} not found", category);
            }
        }

        try
        {
            if (await TryUpdateModelAsync<Article>(
                newArticle,
                "Article",
                a => a.Title, a => a.Entry, a => a.Level))
            {
                await _unitOfWork.ArticleRepository.Add(newArticle);
                await _unitOfWork.SaveAsync();
                return RedirectToPage("./Index");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }

        PopulateAssignedCategoryData(_context, newArticle);
        return Page();

    }
}







//public class CreateModel : PageModel
//{
//    private readonly IUnitOfWork _unitOfWork;

//    public CreateModel(IUnitOfWork unitOfWork)
//    {
//        _unitOfWork = unitOfWork;
//    }

//    public IActionResult OnGet()
//    {
//        return Page();
//    }

//    [BindProperty]
//    public Article Article { get; set; } = default!;

//    public async Task<IActionResult> OnPostAsync()
//    {

//        var currentUser = await _unitOfWork.UserRepository.GetCurrentUser();
//        if (currentUser is null) return NotFound();
//        var newArticle = new Article { Id = Guid.NewGuid(), AuthorId = currentUser.Id };

//        if (await TryUpdateModelAsync<Article>(
//            newArticle,
//            "article",
//            a => a.Title, a => a.Entry, a => a.Level))
//        {
//            //await _unitOfWork.ArticleRepository.AddArticle(newArticle);
//            await _unitOfWork.ArticleRepository.Add(newArticle);
//            await _unitOfWork.SaveAsync();
//            return RedirectToPage("./Index");
//        }
//        return Page();
//    }
//}


