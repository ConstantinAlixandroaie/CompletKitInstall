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
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CompletKitInstall.Pages
{
    public class ProductModel : PageModel
    {
        private readonly IProductRepository _productRepo;
        [BindProperty]
        public IEnumerable<ProductViewModel> Products { get; private set; }
        [BindProperty]
        public ProductViewModel Product { get; private set; }
        [BindProperty]
        public bool IsById { get; set; }
        public ProductModel(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }
        public async Task<IActionResult> OnGetWithIdAsync(int id)
        {
            IsById = true;
            Product =await _productRepo.GetById(id);
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
