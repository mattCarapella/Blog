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

namespace Blog.Areas.Articles.Pages;

public class DeleteModel : PageModel
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteModel> _logger;

    public DeleteModel(IUnitOfWork unitOfWork, ILogger<DeleteModel> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    [BindProperty]
    public Article Article { get; set; } = default!;
    public string ErrorMessage { get; set; } = default!;


    public async Task<IActionResult> OnGetAsync(Guid? id, bool? saveChangesError = false)
    {
        if (id == null) return NotFound();
        var articleId = id.Value;
        Article = await _unitOfWork.ArticleRepository.ArticleFirstOrDefaultAsync(articleId);
        if (Article is null) return NotFound();
       
        if (saveChangesError.GetValueOrDefault())
        {
            ErrorMessage = String.Format("Delete failed. Try again.");
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        if (id == null) return NotFound();
        var articleId = id.Value;
        var article = await _unitOfWork.ArticleRepository.FindAsync(articleId);
        if (article is null) return NotFound();

        try
        {
            await _unitOfWork.ArticleRepository.Delete(article.Id);
            await _unitOfWork.SaveAsync();
            return RedirectToPage("./Index");
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, ErrorMessage);
            return RedirectToAction("./Delete", new { id, saveChangesError = true });
        }
    }

}
