using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompletKitInstall.Repositories;
using CompletKitInstall.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CompletKitInstall.Pages
{
    public class EditProductModel : PageModel
    {
        private readonly IProductRepository _productRepo;
        private readonly IProductImageRepository _productImageRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger _logger;

        [BindProperty]
        public ProductViewModel Product { get; set; }
        [BindProperty]
        public List<ProductImageViewModel> ProductImages { get; set; }
        [BindProperty]
        public IFormFileCollection CatalogImages { get; set; }
        [BindProperty]
        public IFormFile Image { get; set; }
        public EditProductModel(IProductRepository productRepository,IWebHostEnvironment webHostEnvironment,ILogger logger,IProductImageRepository productImageRepository )
        {
            _productRepo = productRepository;
            _productImageRepo = productImageRepository;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Product = await _productRepo.GetById(id);
            ProductImages = await _productImageRepo.GetByProductId(id);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
                return Page();
            await _productRepo.Update(id,Product, User);
            return RedirectToPage($"/Product/{id}");
        }
        public async Task<IActionResult>OnAddImage(int id)
        {
            if (!ModelState.IsValid)
                return Page();
            foreach (var image in ProductImages)
            {
                await _productImageRepo.Add(image,User);
            }
            return RedirectToPage($"/Product/{id}");
        }
        public async Task<IActionResult> OnDeleteAsync(int id)
        {
            if (!ModelState.IsValid)
                return Page();
            await _productImageRepo.RemoveById(id, User);
            return Page();
        }
    }
}
