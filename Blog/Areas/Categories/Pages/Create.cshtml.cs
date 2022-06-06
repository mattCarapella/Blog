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

namespace Blog.Areas.Categories.Pages;

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
    public Category Category { get; set; } = default!;
    

    public async Task<IActionResult> OnPostAsync()
    {
        var newCategory = new Category { Id = Guid.NewGuid() };

        if (await TryUpdateModelAsync<Category>(
            newCategory,
            "category",
            c => c.Name))//, c => c.CategoryImage))
        {
            await _unitOfWork.CategoryRepository.AddCategory(newCategory);
            await _unitOfWork.SaveAsync();
            return RedirectToPage("./Index");
        }
        return Page();
    }

}
