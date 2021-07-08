using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompletKitInstall.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CompletKitInstall.Pages.Partials
{
    public class _ProductImagesCardGroupModel : PageModel
    {
        //private readonly IProductImageRepository _productImageRepo;
        //private readonly ILogger<_ProductImagesCardGroupModel> _logger;

        //[BindProperty]
        //public List<ProductImageViewModel> ProductImages { get; set; }

        //public _ProductImagesCardGroupModel(IProductImageRepository productImageRepository, ILogger<_ProductImagesCardGroupModel> logger)
        //{
        //    _productImageRepo = productImageRepository;
        //    _logger = logger;
        //}
        
        //public async Task<IActionResult> OnGet(int id)
        //{
        //    ProductImages = await _productImageRepo.GetByProductId(id, false);
        //    return Page();
        //}

        //public async Task<IActionResult> OnDelete(int id)
        //{
        //    await _productImageRepo.RemoveById(id,User);
        //    return Partial()
        //}
    }
}
