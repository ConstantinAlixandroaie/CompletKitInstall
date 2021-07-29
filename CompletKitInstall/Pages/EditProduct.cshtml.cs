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
        public EditProductModel(IProductRepository productRepository, IWebHostEnvironment webHostEnvironment, ILogger<AddProductModel> logger, IProductImageRepository productImageRepository)
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
            var pathImg = Path.Combine(_webHostEnvironment.WebRootPath, "Images/Products");
            if (!Directory.Exists(pathImg))
            {
                Directory.CreateDirectory(pathImg);
            }

            if (Image.Length > 0)
                try
                {
                    var uniqueFileName = string.Concat(Guid.NewGuid().ToString(), Image.FileName);
                    using var fileStream = new FileStream(Path.Combine(pathImg, uniqueFileName), FileMode.Create);
                    //delete old product image
                    var imgPath =  _productRepo.GetById(id).Result.ImageUrl;
                    var prodImg = new FileInfo(Path.Combine(_webHostEnvironment.WebRootPath, imgPath));
                    prodImg.Refresh();
                    if (prodImg.Exists)
                    {
                        prodImg.Delete();
                        _logger.LogInformation($"File Deleted {prodImg.Name}");
                    }
                    Product.ImageUrl = Path.Combine("Images/Products", uniqueFileName);
                    await Image.CopyToAsync(fileStream);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw ex;
                }

            await _productRepo.Update(id, Product, User);
            var result = await OnGetAsync(id);
            return result;
        }
        public async Task<IActionResult> OnPostAddImagesAsync()
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
                        ProductId = Product.Id,
                    };
                    await image.CopyToAsync(fileStream);
                    ProductImages.Add(productImage);
                }
                foreach (var item in ProductImages)
                {
                    await _productImageRepo.Add(item, User);
                }

                var result = await OnPostRefreshImagesAsync(Product.Id);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Page();
                throw ex;
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var prodId = _productImageRepo.GetById(id).Result.ProductId;
            var ctlImagePath = _productImageRepo.GetById(id).Result.ImageUrl;

            try
            {
                await _productImageRepo.RemoveById(id, User);
                var ctlImg = new FileInfo(Path.Combine(_webHostEnvironment.WebRootPath, ctlImagePath));
                ctlImg.Refresh();
                if (ctlImg.Exists)
                {
                    ctlImg.Delete();
                    _logger.LogInformation($"File Deleted {ctlImg.Name}");
                }

                var result = await OnPostRefreshImagesAsync(prodId);
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Page();
                throw ex;
            }
        }
        public async Task<IActionResult> OnPostRefreshImagesAsync(int id)
        {

            var productImages = await _productImageRepo.GetByProductId(id);
            var result = new PartialViewResult
            {
                ViewName = "~/Pages/Partials/_ProductImagesCardGroup.cshtml",
                ViewData = new ViewDataDictionary<List<ProductImageViewModel>>(ViewData, productImages),
            };
            return result;
        }
    }
}
