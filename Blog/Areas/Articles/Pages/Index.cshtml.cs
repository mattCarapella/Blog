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
using Blog.Core;

namespace Blog.Areas.Articles.Pages;

public class IndexModel : PageModel
{
    private readonly IUnitOfWork _unitOfWork;
    //private readonly IConfiguration _config;
    //private readonly BlogContext _context;

    public IndexModel(IUnitOfWork unitOfWork)//, BlogContext context, IConfiguration config)
    {
        _unitOfWork = unitOfWork;
        //_context = context;
        //_config = config;
    }
    
    public string TitleSort { get; set; } = string.Empty;
    public string CreatedAtSort { get; set; } = string.Empty;
    public string PublishedAtSort { get; set; } = string.Empty;
    public string CurrentFilter { get; set; } = string.Empty;
    public string CurrentSort { get; set; } = string.Empty;
    public PaginatedList<Article> Articles { get;set; } = default!;


    public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
    {
        CurrentSort = sortOrder;
        TitleSort = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
        CreatedAtSort = sortOrder == "createdAt" ? "createdAt_desc" : "createdAt";
        PublishedAtSort = sortOrder == "publishedAt" ? "publishedAt_desc" : "publishedAt";

        if (searchString is not null)
        {
            pageIndex = 1;
        }
        else
        {
            searchString = currentFilter;
        }

        CurrentFilter = searchString;

        //IQueryable <Article> articles = from a in _context.Articles select a;
        var articles = _unitOfWork.ArticleRepository.GetIQueryableList();

        if (!String.IsNullOrEmpty(searchString))
        {
            // Collate is used to query database with case sensitivity, without hurting performance as much.
            articles = articles.Where(c => EF.Functions.Collate(c.Title, "SQL_Latin1_General_CP1_CI_AS").Contains(searchString) 
                                        || EF.Functions.Collate(c.Entry, "SQL_Latin1_General_CP1_CI_AS").Contains(searchString));
        }

        switch (sortOrder)
        {
            case "title_desc":
                articles = articles.OrderByDescending(a => a.Title);
                break;
            case "createdAt":
                articles = articles.OrderBy(a => a.CreatedAt);
                break;
            case "createdAt_desc":
                articles = articles.OrderByDescending(a => a.CreatedAt);
                break;
            case "publishedAt":
                articles = articles.OrderBy(a => a.CreatedAt);
                break;
            case "publishedAt_desc":
                articles = articles.OrderByDescending(a => a.CreatedAt);
                break;
            default:
                articles = articles.OrderBy(a => a.Title);
                break;
        }

        var pageSize = 8;
        Articles = await PaginatedList<Article>.CreateAsync(
            articles.AsNoTracking(), pageIndex ?? 1, pageSize);
    }
}











//var pageSize = _config.GetValue("PageSize", 10);


//if (string.IsNullOrEmpty(sortOrder)) sortOrder = "Title";
//bool descending = false;

//if (sortOrder.EndsWith("_desc"))
//{
//    sortOrder = sortOrder.Substring(0, sortOrder.Length - 5);
//    descending = true;
//}

//if (descending)
//{
//    articles = articles.OrderByDescending(e => EF.Property<object>(e, sortOrder));
//}
//else
//{
//    articles = articles.OrderBy(e => EF.Property<object>(e, sortOrder));
//}