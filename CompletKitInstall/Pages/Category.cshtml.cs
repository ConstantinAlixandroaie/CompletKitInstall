using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CompletKitInstall.Models;
using CompletKitInstall.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CompletKitInstall.Pages
{
    public class CategoryModel : PageModel
    {
        private readonly ICategoryRepository _categoryRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        [BindProperty]
        public IEnumerable<Category> Categories { get; private set; }
        [BindProperty]
        public Category Category { get; private set; }

        public CategoryModel(ICategoryRepository categoryRepo,IWebHostEnvironment webHostEnvironment)
        {
            _categoryRepo = categoryRepo;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> OnGet()
        {
            Categories=  await _categoryRepo.Get();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(Category category)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _categoryRepo.Add(category);
            return RedirectToPage("/Category");
        }
    }
}
