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

namespace Blog.Areas.Tags.Pages
{
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public EditModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public Tag Tag { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id is null) return NotFound();
            var tagId = id.Value;
            Tag = await _unitOfWork.TagRepository.FindAsync(tagId);
            if (Tag is null) return NotFound();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            var tagToEdit = await _unitOfWork.TagRepository.FindAsync(id);
            if (tagToEdit is null) return NotFound();

            if (await TryUpdateModelAsync<Tag>(tagToEdit, "Tag", a => a.Name))
            {
                await _unitOfWork.SaveAsync();
                return RedirectToPage("./Index");
            }
            return Page();
        }

        
    }
}
