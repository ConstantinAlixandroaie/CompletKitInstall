using CompletKitInstall.Data;
using CompletKitInstall.Models;
using CompletKitInstall.Repositories;
using CompletKitInstall.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CompletKitInstall.Data.Acces.Repositories
{ }
public interface IProductImageRepository : IRepository<ProductImage, ProductImageViewModel>
{
    public abstract Task<List<ProductImageViewModel>> GetByProductId(int id, bool asNoTracking = false);
    public abstract Task RemoveByProductId(int id, ClaimsPrincipal user);

}
public class ProductImageRepository : Repository<ProductImage, ProductImageViewModel>, IProductImageRepository
{
    public ProductImageRepository(CompletKitDbContext ctx,IAuthorizationService authorizationService) : base(ctx,authorizationService)
    {

    }

    public override async Task<ProductImage> Add(ProductImageViewModel item, ClaimsPrincipal user)
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

    public async Task<List<ProductImageViewModel>> GetByProductId(int id, bool asNoTracking = false)
    {
        try
        {
            var rv = new List<ProductImageViewModel>();
            var sourceCollection = await (from productImage in _ctx.ProductImages
                                          where productImage.ProductId == id
                                          select productImage).ToListAsync();

            foreach (var image in sourceCollection)
            {
                var vm = new ProductImageViewModel
                {
                    Id = image.Id,
                    ProductId = image.ProductId,
                    ImageUrl = image.ImageUrl,
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

    public override Task<bool> Remove(ProductImageViewModel item, ClaimsPrincipal user)
    {
        throw new NotImplementedException();
    }

    public override async Task RemoveById(int id, ClaimsPrincipal user)
    {
        try
        {
            var productImage = await _ctx.ProductImages.FirstOrDefaultAsync(x => x.Id == id);
            if (productImage == null)
                throw new ArgumentNullException($"The Image with Id= '{id}' does not exist.");


            _ctx.ProductImages.Remove(productImage);
            await _ctx.SaveChangesAsync();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task RemoveByProductId(int id, ClaimsPrincipal user)
    {
        try
        {
            var sourceCollection = await (from productImage in _ctx.ProductImages
                                          where productImage.ProductId == id
                                          select productImage).ToListAsync();

            foreach (var image in sourceCollection)
            {
                _ctx.ProductImages.Remove(image);
            }
            await _ctx.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public override async Task<bool> Update(int id, ProductImageViewModel newData, ClaimsPrincipal user)
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
