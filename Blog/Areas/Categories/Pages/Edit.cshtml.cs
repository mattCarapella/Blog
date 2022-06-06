using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Blog.Data;
using Blog.Models;
using Blog.Core.Repositories;

namespace Blog.Areas.Categories.Pages
{
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public EditModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public Category Category { get; set; } = default!;


        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id is null) return NotFound();
            var catId = id.Value;
            Category = await _unitOfWork.CategoryRepository.CategoryFindAsync(catId);
            if (Category is null) return NotFound();
            return Page();
        }

      
        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            var categoryToEdit = await _unitOfWork.CategoryRepository.CategoryFindAsync(id);
            if (categoryToEdit is null) return NotFound();

            if (await TryUpdateModelAsync<Category>(categoryToEdit, "Category", a => a.Name))
            {
                await _unitOfWork.SaveAsync();
                return RedirectToPage("./Index");
            }
            return Page();
        }

        
    }
}
