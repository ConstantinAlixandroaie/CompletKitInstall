using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
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
        [BindProperty]
        public IEnumerable<ProductViewModel> Products { get; private set; }
        [BindProperty]
        public List<ProductImageViewModel> ProductImages { get; private set; }
        [BindProperty]
        public ProductViewModel Product { get; private set; }
        [BindProperty]
        public bool IsById { get; set; }
        public ProductModel(IProductRepository productRepo,IProductImageRepository productImageRepo)
        {
            _productRepo = productRepo;
            _productImageRepo = productImageRepo;
        }
        public async Task<IActionResult> OnGetWithIdAsync(int id)
        {
            IsById = true;
            Product =await _productRepo.GetById(id);
            ProductImages = await _productImageRepo.GetByProductId(id);
            return Page();
        }
        public async Task<IActionResult> OnGetAsync(int? qid = null)
        {
            if (qid != null)
            {
                await OnGetWithIdAsync(qid.Value);
            }
            Products = await _productRepo.Get();
            return Page();
        }
    }
}
