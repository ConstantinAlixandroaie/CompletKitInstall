using CompletKitInstall.Data;
using CompletKitInstall.Models;
using CompletKitInstall.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompletKitInstall.Repositories
{
    public interface IProductRepository : IRepository<Product, ProductViewModel>
    {
        public abstract Task AddProductAndImages(ProductViewModel product, List<ProductImageViewModel> productImages);
    }
    public class ProductRepository : Repository<Product, ProductViewModel>, IProductRepository
    {
        public ProductRepository(CompletKitDbContext ctx) : base(ctx)
        {

        }

        public override async Task<Product> Add(ProductViewModel item)
        {
            try
            {
                if (item == null)
                    return null;
                if (item.ImageUrl == null)
                    return null;
                if (item.Description == null)
                    return null;
                var product = new Product
                {
                    Name = item.Name,
                    Description = item.Description,
                    ImageUrl = item.ImageUrl,
                    CategoryId = item.CategoryId,
                    Category = await _ctx.Categories.FirstOrDefaultAsync(x => x.Id == item.CategoryId),
                    DateCreated = DateTime.Now
                };
                _ctx.Products.Add(product);
                await _ctx.SaveChangesAsync();
                return product;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task AddProductAndImages(ProductViewModel item, List<ProductImageViewModel> productImages)
        {
            using (var transaction = _ctx.Database.BeginTransaction())
            {
                try
                {
                    if (item == null)
                        throw new ArgumentNullException();
                    if (item.ImageUrl == null)
                        throw new ArgumentNullException();
                    if (item.Description == null)
                        throw new ArgumentNullException();
                    var product = new Product
                    {
                        Name = item.Name,
                        Description = item.Description,
                        ImageUrl = item.ImageUrl,
                        CategoryId = item.CategoryId,
                        Category = await _ctx.Categories.FirstOrDefaultAsync(x => x.Id == item.CategoryId),
                        DateCreated = DateTime.Now
                    };
                    _ctx.Products.Add(product);
                    await _ctx.SaveChangesAsync();
                }
                catch (Exception)
                {

                    throw;
                }
                if (productImages == null)
                    throw new ArgumentNullException("product Images list is empty");
                    foreach (var image in productImages)
                    {
                        try
                        {
                            if (image == null)
                                throw new ArgumentNullException("The image you want to upload is null.");
                            if (image.ImageUrl == null)
                                throw new ArgumentNullException("The image adress you want to upload is null.");

                            var productImage = new ProductImage
                            {
                                ImageUrl = image.ImageUrl,
                                ProductId = item.Id,
                                Product = await _ctx.Products.FirstOrDefaultAsync(x => x.Id == image.ProductId),
                            };
                            _ctx.ProductImages.Add(productImage);

                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }
                
                await _ctx.SaveChangesAsync();

            }
        }

        public override async Task<IEnumerable<ProductViewModel>> Get(bool asNoTracking = false)
        {
            try
            {
                var rv = new List<ProductViewModel>();
                var sourceCollection = await _ctx.Products.ToListAsync();
                foreach (var product in sourceCollection)
                {
                    var vm = new ProductViewModel()
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description,
                        ImageUrl=product.ImageUrl,
                        DateCreated = product.DateCreated
                    };
                    rv.Add(vm);

                }
                return rv;
            }
            catch (Exception )
            {

                throw;
            }
        }
        public override async Task<ProductViewModel> GetById(int id, bool asNoTracking = false)
        {
            try
            {
                var sourceCollection = _ctx.Products.AsQueryable();
                if (asNoTracking)
                    sourceCollection = sourceCollection.AsNoTracking();
                var product = await sourceCollection.FirstOrDefaultAsync(x => x.Id == id);
                if (product == null)
                    return null;
                var rv = new ProductViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    ImageUrl = product.ImageUrl,
                    CategoryId = product.CategoryId,
                    Category = product.Category,
                    DateCreated = product.DateCreated
                };
                return rv;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public override Task<bool> Remove(ProductViewModel item)
        {
            throw new NotImplementedException();
        }

        public override async Task RemoveById(int id)
        {
            try
            {
                var product = await _ctx.Products.FirstOrDefaultAsync(x => x.Id == id);
                if (product == null)
                    throw new ArgumentNullException($"The Product with Id= '{id}' does not exist.");
                _ctx.Products.Remove(product);
                await _ctx.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }

        }
        public override async Task<bool> Update(int id, ProductViewModel newData)
        {
            try
            {
                var product = await _ctx.Products.FirstOrDefaultAsync(x => x.Id == id);
                if (product == null)
                    return false;
                if (newData.ImageUrl != null)
                {
                    product.ImageUrl = newData.ImageUrl;
                }
                if (newData.Description != null)
                {
                    product.Description = newData.Description;
                }
                if (newData.Name != null)
                {
                    product.Name = newData.Name;
                }
                await _ctx.SaveChangesAsync();
                return true;
            }
            catch (Exception )
            {

                throw;
            }

        }
    }
}
