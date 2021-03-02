using CompletKitInstall.Models;
using CompletKitInstall.Repositories;
using Microsoft.AspNetCore.Mvc;
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
       
        private readonly IRepository<Product> _productRepo;
        public ProductController(IRepository<Product> productRepo)
        {
            _productRepo = productRepo;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var rv= await _productRepo.Get(asNoTracking:true);
            return Ok(rv);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var rv = await _productRepo.GetById(id);
            return Ok(rv);
        }
        [HttpPost]
        public async Task<IActionResult> Add(Product product)
        {
            var rv = await _productRepo.Add(product);
            return Ok(rv);
        }
        [HttpPost("{id}")]
        public async Task<IActionResult> Update(int id,Product product)
        {
            var rv = await _productRepo.Update(id,product);
            return Ok(rv);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var rv = await _productRepo.RemoveById(id);
            return Ok(rv);
        }

    }
}
