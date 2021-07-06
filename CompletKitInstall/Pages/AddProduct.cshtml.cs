using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CompletKitInstall.Data;
using CompletKitInstall.Repositories;
using CompletKitInstall.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CompletKitInstall.Pages
{
    [Authorize(Roles = "Administrators,Managers")]
    public class AddProductModel : PageModel
    {
        private readonly IProductRepository _productRepo;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IComplexOperationsHandler _complexOperationsHandler;
        private readonly ILogger<AddProductModel> _logger;
        [BindProperty]
        public IEnumerable<ProductViewModel> Products { get; private set; }
        [BindProperty]
        public List<ProductImageViewModel> ProductImages { get; private set; }
        [BindProperty]
        public IEnumerable<CategoryViewModel> Categories { get; private set; }
        [BindProperty]
        public ProductViewModel Product { get; set; }
        [Required]
        [BindProperty]
        public IFormFile Image { get; set; }
        [BindProperty]
        public IFormFileCollection CatalogImages { get; set; }
        public AddProductModel(IProductRepository productRepo, IWebHostEnvironment webHostEnvironment, ICategoryRepository categoryRepository, IComplexOperationsHandler complexOperationsHandler, ILogger<AddProductModel> logger)
        {
            _productRepo = productRepo;
            _webHostEnvironment = webHostEnvironment;
            _categoryRepository = categoryRepository;
            _complexOperationsHandler = complexOperationsHandler;
            _logger = logger;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            return await GetPage();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return await GetPage();
            }

            var path = Path.Combine(_webHostEnvironment.WebRootPath, "Images/Products");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (Image.Length > 0)
                try
                {
                    using var fileStream = new FileStream(Path.Combine(path, Image.FileName), FileMode.Create);
                    Product.ImageUrl = Path.Combine("Images/Products", Image.FileName);
                    await Image.CopyToAsync(fileStream);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            if (CatalogImages.Count > 0)
            {
                try
                {
                    ProductImages = new List<ProductImageViewModel>();
                    foreach (var image in CatalogImages)
                    {
                        using var fileStream = new FileStream(Path.Combine(path, image.FileName), FileMode.Create);
                        var productImage = new ProductImageViewModel
                        {
                            ImageUrl = Path.Combine("Images/Products", image.FileName),
                        };
                        await image.CopyToAsync(fileStream);
                        ProductImages.Add(productImage);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
                await _complexOperationsHandler.AddProductAndImages(Product, ProductImages, User);
            }
            else
                await _productRepo.Add(Product, User);

            return RedirectToPage("./AddProduct");
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _complexOperationsHandler.RemoveProductWithImages(id, User);
            return RedirectToPage("./AddProduct");
        }

        private async Task<IActionResult> GetPage()
        {
            Categories = await _categoryRepository.Get();
            Products = await _productRepo.Get();
            Products = Products.OrderByDescending(x => x.DateCreated);
            return Page();
        }
    }
}
