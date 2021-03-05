using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompletKitInstall.Models;
using CompletKitInstall.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CompletKitInstallFE.Views
{
    public class ProductModel : PageModel
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IProductRepository _productRepository;
        public IEnumerable<Product> Products { get; set; }
        public Product Product { get; set; }
        public bool IsById { get; set; }
        public ProductModel(IWebHostEnvironment webHostEnvironment,IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        //Open products page - if there is a parameter for ID then open a single product
        public async Task<IActionResult> OnGetAsync(int? qid=null)
        {
            if (qid != null)
                return await OnGetWithIdAsync(qid.Value);
            else
                Products = await _productRepository.Get();

            return Page();
        }
        public async Task<IActionResult> OnGetWithIdAsync(int id)
        {
            Product = await _productRepository.GetById(id);
            IsById = true;
            return Page();
        }

    }
}
