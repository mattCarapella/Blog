using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Blog.Models;
using Blog.Core.Repositories;

namespace Blog.Areas.Articles.Pages;

public class DetailsModel : PageModel
{
    private readonly IUnitOfWork _unitOfWork;

    public DetailsModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Article Article { get; set; } = default!; 

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null) return NotFound();
        var articleId = id.Value;
        Article = await _unitOfWork.ArticleRepository.ArticleDetails(articleId);
        if (Article is null) return NotFound();
        return Page();
    }
}

