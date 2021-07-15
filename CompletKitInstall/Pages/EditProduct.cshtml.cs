using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CompletKitInstall.Repositories;
using CompletKitInstall.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;

namespace CompletKitInstall.Pages
{
    [Authorize(Roles = "Administrators,Managers")]
    public class EditProductModel : PageModel
    {
        private readonly IProductRepository _productRepo;
        private readonly IProductImageRepository _productImageRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<AddProductModel> _logger;

        [BindProperty]
        public ProductViewModel Product { get; set; }
        [BindProperty]
        public List<ProductImageViewModel> ProductImages { get; set; }
        [BindProperty]
        public IFormFileCollection CatalogImages { get; set; }
        [BindProperty]
        public IFormFile Image { get; set; }
        public EditProductModel(IProductRepository productRepository,IWebHostEnvironment webHostEnvironment,ILogger<AddProductModel> logger,IProductImageRepository productImageRepository )
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
        public async Task<IActionResult> OnPostEditAsync(int id)
        {
            if (!ModelState.IsValid)
                return Page();
            await _productRepo.Update(id,Product, User);
            return RedirectToPage($"/Product/{id}");
        }
        public async Task<IActionResult>OnPostAddImagesAsync()
        {
            var pathCtl = Path.Combine(_webHostEnvironment.WebRootPath, "Images/CatalogImages");
            //if (!ModelState.IsValid)
            //    return Page();
            //CatalogImages = (IFormFileCollection)Request.Form["ctl_Images"].ToList();
           
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
                        ProductId=Product.Id,
                    };
                    await image.CopyToAsync(fileStream);
                    ProductImages.Add(productImage);
                }
                foreach (var item in ProductImages)
                {
                    await _productImageRepo.Add(item, User);
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return new PartialViewResult
            {
                ViewName = "~/Pages/Partials/_CatalogImageUpload.cshtml",
                ViewData = new ViewDataDictionary<IFormCollection>(ViewData),
            };
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var prodId = _productImageRepo.GetById(id).Result.ProductId;
            try
            {
                await _productImageRepo.RemoveById(id, User);
                return new PartialViewResult
                {
                    ViewName = "~/Pages/Partials/_ProductImagesCardGroup.cshtml",
                    ViewData = new ViewDataDictionary<List<ProductImageViewModel>>(ViewData, await _productImageRepo.GetByProductId(prodId)),
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Page();
                throw ex;
            }
            
        }
    }
}
