using CompletKitInstall.Data;
using CompletKitInstall.Models;
using CompletKitInstall.Repositories;
using CompletKitInstall.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompletKitInstall.Data.Acces.Repositories
{ }
public interface IProductImageRepository : IRepository<ProductImage, ProductImageViewModel>
{

}
public class ProductImageRepository : Repository<ProductImage, ProductImageViewModel>, IProductImageRepository
{
    public ProductImageRepository(CompletKitDbContext ctx) : base(ctx)
    {

    }

    public override async Task<ProductImage> Add(ProductImageViewModel item)
    {
        try
        {
            if (item == null)
                return null;
            if (item.ImageUrl == null)
                return null;

            var productImage = new ProductImage
            {
                ImageUrl = item.ImageUrl,
                ProductId = item.ProductId,
                Product = await _ctx.Products.FirstOrDefaultAsync(x => x.Id == item.ProductId),
            };
            _ctx.ProductImages.Add(productImage);
            await _ctx.SaveChangesAsync();
            return productImage;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public override async Task<IEnumerable<ProductImageViewModel>> Get(bool asNoTracking = false)
    {
        try
        {
            var rv = new List<ProductImageViewModel>();
            var sourceCollection = await _ctx.ProductImages.ToListAsync();
            foreach (var item in sourceCollection)
            {
                var vm = new ProductImageViewModel()
                {
                    Id = item.Id,
                    ProductId = item.ProductId,
                    ImageUrl = item.ImageUrl
                };
                rv.Add(vm);
            }
            return rv;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public override async Task<ProductImageViewModel> GetById(int id, bool asNoTracking = false)
    {
        try
        {
            var sourceCollection = _ctx.ProductImages.AsQueryable();
            if (asNoTracking)
                sourceCollection = sourceCollection.AsNoTracking();
            var productImage = await sourceCollection.FirstOrDefaultAsync(x => x.Id == id);
            if (productImage == null)
                return null;
            var rv = new ProductImageViewModel
            {
                Id = productImage.Id,
                ProductId = productImage.ProductId,
                ImageUrl = productImage.ImageUrl,
            };
            return rv;
                 
        }
        catch (Exception)
        {

            throw;
        }
    }

    public override  Task<bool> Remove(ProductImageViewModel item)
    {
        throw new NotImplementedException();
    }

    public override async Task RemoveById(int id)
    {
        try
        {
            var productImage = await _ctx.ProductImages.FirstOrDefaultAsync(x => x.Id == id);
            if(productImage==null)
                throw new ArgumentNullException($"The Image with Id= '{id}' does not exist.");
            _ctx.ProductImages.Remove(productImage);
            await _ctx.SaveChangesAsync();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public override async Task<bool> Update(int id, ProductImageViewModel newData)
    {
        try
        {
            var productImage = await _ctx.ProductImages.FirstOrDefaultAsync(x => x.Id == id);
            if (productImage == null)
                return false;
            if (newData.ImageUrl != null)
            {
                productImage.ImageUrl = newData.ImageUrl;
            }
            
            await _ctx.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {

            throw;
        }
    }
}
