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
        private readonly IProductImageRepository _productImageRepo;
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
        public AddProductModel(IProductRepository productRepo, IProductImageRepository productImageRepo, IWebHostEnvironment webHostEnvironment, ICategoryRepository categoryRepository, IComplexOperationsHandler complexOperationsHandler, ILogger<AddProductModel> logger)
        {
            _productRepo = productRepo;
            _productImageRepo = productImageRepo;
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

            var pathImg = Path.Combine(_webHostEnvironment.WebRootPath, "Images/Products");
            var pathCtl = Path.Combine(_webHostEnvironment.WebRootPath, "Images/CatalogImages");
            if (!Directory.Exists(pathImg))
            {
                Directory.CreateDirectory(pathImg);
            }

            if (!Directory.Exists(pathCtl))
            {
                Directory.CreateDirectory(pathCtl);
            }

            if (Image.Length > 0)
                try
                {
                    var uniqueFileName = string.Concat(Guid.NewGuid().ToString(), Image.FileName);
                    using var fileStream = new FileStream(Path.Combine(pathImg, uniqueFileName), FileMode.Create);
                    Product.ImageUrl = Path.Combine("Images/Products", uniqueFileName);
                    await Image.CopyToAsync(fileStream);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw ex;
                }
            if (CatalogImages.Count > 0)
            {
                try
                {
                    ProductImages = new List<ProductImageViewModel>();
                    foreach (var image in CatalogImages)
                    {
                        var uniqueFileName = string.Concat(Guid.NewGuid().ToString(), image.FileName);
                        using var fileStream = new FileStream(Path.Combine(pathCtl, uniqueFileName), FileMode.Create);
                        var productImage = new ProductImageViewModel
                        {
                            ImageUrl = Path.Combine("Images/CatalogImages", uniqueFileName),
                        };
                        await image.CopyToAsync(fileStream);
                        ProductImages.Add(productImage);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw ex;
                }
                await _complexOperationsHandler.AddProductAndImages(Product, ProductImages, User);
            }
            else
                await _productRepo.Add(Product, User);

            return RedirectToPage("./AddProduct");
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            try
            {
                var imgPath = _productRepo.GetById(id).Result.ImageUrl;
                var images = await _productImageRepo.GetByProductId(id);
                var ctlImagePaths = new List<string>();
                foreach (var img in images)
                {
                    ctlImagePaths.Add(img.ImageUrl);
                }

                await _complexOperationsHandler.RemoveProductWithImages(id, User);


                var prodImg = new FileInfo(imgPath);
                if (prodImg.Exists)
                {
                    prodImg.Delete();
                    _logger.LogInformation($"File Deleted {prodImg.Name}");
                }
                foreach (var path in ctlImagePaths)
                {
                    var ctlImg = new FileInfo(path);
                    if (ctlImg.Exists)
                    {
                        ctlImg.Delete();
                        _logger.LogInformation($"File Deleted {ctlImg.Name}");
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }

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
