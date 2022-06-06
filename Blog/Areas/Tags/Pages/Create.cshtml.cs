using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Blog.Data;
using Blog.Models;
using Blog.Core.Repositories;

namespace Blog.Areas.Tags.Pages;

public class CreateModel : PageModel
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public Tag Tag { get; set; } = default!;


    public async Task<IActionResult> OnPostAsync()
    {
        var newTag = new Tag { Id = Guid.NewGuid() };

        if (await TryUpdateModelAsync<Tag>(newTag, "tag", c => c.Name))
        {
            await _unitOfWork.TagRepository.Add(newTag);
            await _unitOfWork.SaveAsync();
            return RedirectToPage("./Index");
        }
        return Page();
    }
}
