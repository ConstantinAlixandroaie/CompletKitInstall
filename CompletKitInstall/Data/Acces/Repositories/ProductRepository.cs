using CompletKitInstall.Data;
using CompletKitInstall.Models;
using CompletKitInstall.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CompletKitInstall.Repositories
{
    public interface IProductRepository : IRepository<Product, ProductViewModel>
    {

    }
    public class ProductRepository : Repository<Product, ProductViewModel>, IProductRepository
    {
        public ProductRepository(CompletKitDbContext ctx,IAuthorizationService authorizationService) : base(ctx,authorizationService)
        {

        }

        public override async Task<Product> Add(ProductViewModel item, ClaimsPrincipal user)
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
                        ImageUrl = product.ImageUrl,
                        DateCreated = product.DateCreated
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

        public override Task<bool> Remove(ProductViewModel item, ClaimsPrincipal user)
        {
            throw new NotImplementedException();
        }

        public override async Task RemoveById(int id, ClaimsPrincipal user)
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
        public override async Task<bool> Update(int id, ProductViewModel newData, ClaimsPrincipal user)
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
            catch (Exception)
            {

                throw;
            }

        }
    }
}
