using CompletKitInstall.Models;
using CompletKitInstall.Repositories;
using CompletKitInstall.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompletKitInstall.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController:ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IRepository<Product,ProductViewModel> _productRepo;
        public ProductController(IRepository<Product, ProductViewModel> productRepo,ILogger<ProductController> logger)
        {
            _productRepo = productRepo;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
           var rv= await _productRepo.Get(asNoTracking:true);
           return Ok(rv);
           //return Ok("You acted on product . get");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var rv = await _productRepo.GetById(id);
            return Ok(rv);
        }
        [HttpPost]
        public async Task<IActionResult> Add(ProductViewModel product)
        {
            var rv = await _productRepo.Add(product);
            return Ok(rv);
        }
        [HttpPost("{id}")]
        public async Task<IActionResult> Update(int id, ProductViewModel product)
        {
            var rv = await _productRepo.Update(id,product);
            return Ok(rv);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productRepo.RemoveById(id);
            return Ok();
        }

    }
}
