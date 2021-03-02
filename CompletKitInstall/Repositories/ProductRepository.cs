using CompletKitInstall.Data;
using CompletKitInstall.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompletKitInstall.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {

    }
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(CompletKitDbContext ctx) : base(ctx)
        {

        }

        public override async Task<Product> Add(Product item)
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
                ImageUrl = item.ImageUrl
            };
            _ctx.Products.Add(product);
            await _ctx.SaveChangesAsync();
            return product;
        }
        public override async Task<IEnumerable<Product>> Get(bool asNoTracking = false)
        {
            var sourceCollection = _ctx.Products.AsQueryable();
            if (asNoTracking)
                sourceCollection = sourceCollection.AsNoTracking();
            return await _ctx.Products.ToListAsync();
        }
        public override async Task<Product> GetById(int id, bool asNoTracking = false)
        {
            var sourceCollection = _ctx.Products.AsQueryable();
            if (asNoTracking)
                sourceCollection = sourceCollection.AsNoTracking();
            var product = await _ctx.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
                return null;
            return product;
        }
        public override async Task<Product> RemoveById(int id)
        {
            var product = await _ctx.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
                return null;
            _ctx.Products.Remove(product);
            await _ctx.SaveChangesAsync();
            return product;
                  
        }
        public override async Task<bool> Update(int id, Product newData)
        {
            var item = await _ctx.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null)
                return false;
            if (newData.ImageUrl != null)
            {
                item.ImageUrl = newData.ImageUrl;
            }
            if (newData.Description != null)
            {
                item.Description = newData.Description;
            }
            if (newData.Name != null)
            {
                item.Name = newData.Name;
            }
            await _ctx.SaveChangesAsync();
            return true;

        }
    }
}
