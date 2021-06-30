using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using CompletKitInstall.Data;
using CompletKitInstall.Data.Acces;
using CompletKitInstall.Models;
using CompletKitInstall.Repositories;
using CompletKitInstall.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CompletKitInstall.Pages
{
    [AllowAnonymous]
    public class ProductModel : PageModel
    {
        private readonly IProductRepository _productRepo;
        private readonly IProductImageRepository _productImageRepo;
        private readonly ICategoryRepository _categoryRepo;
        //private readonly IFormatter _formatter;
        [BindProperty]
        public IEnumerable<ProductViewModel> Products { get; private set; }
        [BindProperty]
        public List<ProductImageViewModel> ProductImages { get; private set; }
        [BindProperty]
        public ProductViewModel Product { get; private set; }
        [BindProperty]
        public bool IsById { get; set; }
        [BindProperty(SupportsGet = true)]
        public string searchString { get; set; }
        [BindProperty]
        public List<CategoryViewModel> Categories { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Category { get; set; }
        //[BindProperty]
        //public List<string> ProductDescription { get; set; }

        public ProductModel(IProductRepository productRepo, IProductImageRepository productImageRepo, ICategoryRepository categoryRepo/*, IFormatter formatter*/)
        {
            _productRepo = productRepo;
            _productImageRepo = productImageRepo;
            _categoryRepo = categoryRepo;
            //_formatter = formatter;
        }

        public async Task<IActionResult> OnGetWithIdAsync(int id)
        {
            IsById = true;
            Product = await _productRepo.GetById(id);
            //ProductDescription = await _formatter.ArrangeDescription(Product.Description);
            ProductImages = await _productImageRepo.GetByProductId(id);
            return Page();
        }
        public async Task<IActionResult> OnGetAsync(int? qid = null)
        {
            Categories = (List<CategoryViewModel>)await _categoryRepo.Get();
            if (qid != null)
                await OnGetWithIdAsync(qid.Value);

            if (!string.IsNullOrEmpty(searchString) || !string.IsNullOrEmpty(Category))
            {
                Products = await _productRepo.GetBySearchInput(searchString, Category);
            }
            else
                Products = await _productRepo.Get();
            return Page();
        }
    }
}
