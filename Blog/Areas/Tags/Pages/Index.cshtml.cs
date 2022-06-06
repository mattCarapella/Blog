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

namespace Blog.Areas.Tags.Pages;

public class IndexModel : PageModel
{
    private readonly BlogContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public IndexModel(BlogContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public string NameSort { get; set; } = string.Empty;
    public string CurrentFilter { get; set; } = string.Empty;
    public string CurrentSort { get; set; } = string.Empty;


    public IList<Tag> Tags { get;set; } = default!;

    public async Task OnGetAsync(string sortOrder, string searchString)
    {
        NameSort = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        CurrentFilter = searchString;

        IQueryable<Tag> tags = from t in _context.Tags select t;

        if (!String.IsNullOrEmpty(searchString))
        {
            tags = tags.Where(c => EF.Functions.Collate(c.Name, "SQL_Latin1_General_CP1_CI_AS")
                       .Contains(searchString));
        }

        switch (sortOrder)
        {
            case "name_desc":
                tags = tags.OrderByDescending(t => t.Name);
                break;
            default:
                tags = tags.OrderBy(t => t.Name);
                break;
        }

        Tags = await tags.AsNoTracking().ToListAsync();
    }
}
