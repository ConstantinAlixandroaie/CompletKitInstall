using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using CompletKitInstall.Models;
using CompletKitInstall.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CompletKitInstall.Pages
{
    public class ProductModel : PageModel
    {
        private readonly IProductRepository _productRepo;
        [BindProperty]
        public List<Product> Products { get; private set; }
        [BindProperty]
        public Product Product { get; private set; }
        public bool GetProductsError { get; private set; }
        public bool GetProductError { get; private set; }

        [BindProperty]
        public bool IsById { get; set; }
        public ProductModel(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }
        public async Task OnGetWithIdAsync(int id)
        {
            IsById = true;
            Product =await _productRepo.GetById(id);
        }
        public async Task OnGetAsync(int? qid = null)
        {
            if (qid != null)
            {
                await OnGetWithIdAsync(qid.Value);
            }
            Products = (List<Product>)await _productRepo.Get();
        }
    }
}
