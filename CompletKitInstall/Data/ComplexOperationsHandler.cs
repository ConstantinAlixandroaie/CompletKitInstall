using CompletKitInstall.Repositories;
using CompletKitInstall.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompletKitInstall.Data
{
    public class ComplexOperationsHandler : IComplexOperationsHandler
    {
        protected CompletKitDbContext _ctx;
        protected readonly IProductImageRepository _productImageRepo;
        protected readonly IProductRepository _productRepo;
        public ComplexOperationsHandler(CompletKitDbContext ctx, IProductImageRepository productImageRepo, IProductRepository productRepo)
        {
            _ctx = ctx;
            _productImageRepo = productImageRepo;
            _productRepo = productRepo;
        }
        //Add product that has images for the product paghe slider
        public async Task AddProductAndImages(ProductViewModel product, List<ProductImageViewModel> productImages)
        {
            using (var transaction = _ctx.Database.BeginTransaction())
            {
                try
                {
                    var currentProduct = await _productRepo.Add(product);

                    foreach (var productImage in productImages)
                    {
                        productImage.ProductId = currentProduct.Id;
                        await _productImageRepo.Add(productImage);
                    }

                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex);
                }
                transaction.Commit();
            }
        }
        public async Task RemoveProductWithImages(int id)
        {
            using (var transaction = _ctx.Database.BeginTransaction())
            {
                var productImages = await _productImageRepo.GetByProductId(id);
               
                if (productImages.Count==0)
                {
                    await _productRepo.RemoveById(id);
                }
                else
                {
                    await _productImageRepo.RemoveByProductId(id);
                    await _productRepo.RemoveById(id);
                }
                transaction.Commit();
            }

        }
    }
}

