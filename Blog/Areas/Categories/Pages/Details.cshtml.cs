using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Blog.Models;
using Blog.Core.Repositories;

namespace Blog.Areas.Categories.Pages;

public class DetailsModel : PageModel
{
    private readonly IUnitOfWork _unitOfWork;

    public DetailsModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Category Category { get; set; } = default!; 

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null) return NotFound();
        var catId = id.Value;
        Category = await _unitOfWork.CategoryRepository.CategoryDetails(catId);
        if (Category == null) return NotFound();
        return Page();
    }
}
