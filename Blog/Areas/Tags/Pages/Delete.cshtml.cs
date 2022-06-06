using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Blog.Models;
using Blog.Core.Repositories;

namespace Blog.Areas.Tags.Pages;

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
    public Tag Tag { get; set; } = default!;
    public string ErrorMessage { get; set; } = default!;


    public async Task<IActionResult> OnGetAsync(Guid? id, bool? saveChangesError = false)
    {
        if (id == null) return NotFound();
        var tagId = id.Value;
        Tag = await _unitOfWork.TagRepository.TagFirstOrDefaultAsync(tagId);
        if (Tag is null) return NotFound();

        if (saveChangesError.GetValueOrDefault())
        {
            ErrorMessage = String.Format("Delete failed. Try again.");
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        if (id == null) return NotFound();
        var tagId = id.Value;
        var tag = await _unitOfWork.TagRepository.FindAsync(tagId);
        if (tag is null) return NotFound();

        try
        {
            await _unitOfWork.TagRepository.Delete(tag.Id);
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
