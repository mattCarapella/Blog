using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Blog.Data;
using Blog.Models;
using Blog.Core.Repositories;
using Blog.Core.ViewModels.Category;

namespace Blog.Areas.Categories.Pages;

public class IndexModel : PageModel
{
    private readonly BlogContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public IndexModel(BlogContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }


    //public CategoryIndexData CategoryData { get; set; }
    //public Guid CategoryId { get; set; }
    //public Guid ArticleId { get; set; }


    //public async Task OnGetAsync(Guid? id)
    //{
    //    CategoryData = new CategoryIndexData();
    //    CategoryData.Categories = await _context.Categories
    //        .Include(c => c.Articles)
    //        .OrderBy(a => a.Name)
    //        .ToListAsync();

    //    if (id != null)
    //    {
    //        CategoryId = id.Value;
    //        Category category = CategoryData.Categories
    //            .Where(c => c.Id == id.Value).Single();
    //        CategoryData.Articles = category.Articles;
    //    }

    //}

    public string NameSort { get; set; } = string.Empty;
    public string CurrentFilter { get; set; } = string.Empty;
    public string CurrentSort { get; set; } = string.Empty;

    public IList<Category> Categories { get; set; } = default!;

    public async Task OnGetAsync(string sortOrder, string searchString)
    {
        NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        CurrentFilter = searchString;

        IQueryable<Category> categories = from c in _context.Categories select c;

        if (!String.IsNullOrEmpty(searchString))
        {
            categories = categories.Where(c => EF.Functions.Collate(c.Name,
                            "SQL_Latin1_General_CP1_CI_AS").Contains(searchString));
        }

        switch (sortOrder)
        {
            case "name_desc":
                categories = categories.OrderByDescending(t => t.Name);
                break;
            default:
                categories = categories.OrderBy(t => t.Name);
                break;
        }

        Categories = await categories.AsNoTracking().ToListAsync();
    }
}
