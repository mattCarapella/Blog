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

namespace Blog.Areas.Categories.Pages;

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
    public Category Category { get; set; } = default!;
    public string ErrorMessage { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(Guid? id, bool? saveChangesError = false)
    {
        if (id == null) return NotFound();
        var categoryId = id.Value;
        Category = await _unitOfWork.CategoryRepository.CategoryFirstOrDefaultAsync(categoryId);
        if (Category is null) return NotFound();

        if (saveChangesError.GetValueOrDefault())
        {
            ErrorMessage = String.Format("Delete failed. Try again.");
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        if (id == null) return NotFound();
        var categoryId = id.Value;
        var category = await _unitOfWork.CategoryRepository.CategoryFindAsync(categoryId);
        if (category is null) return NotFound();

        try
        {
            await _unitOfWork.CategoryRepository.DeleteCategory(category.Id);
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
