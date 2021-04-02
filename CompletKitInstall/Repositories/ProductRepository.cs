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
                    ImageUrl = item.ImageUrl
                };
                _ctx.Products.Add(product);
                await _ctx.SaveChangesAsync();
                return product;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public override async Task<IEnumerable<Product>> Get(bool asNoTracking = false)
        {
            try
            {
                var sourceCollection = _ctx.Products.AsQueryable();
                if (asNoTracking)
                    sourceCollection = sourceCollection.AsNoTracking();
                return await sourceCollection.ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public override async Task<Product> GetById(int id, bool asNoTracking = false)
        {
            try
            {
                var sourceCollection = _ctx.Products.AsQueryable();
                if (asNoTracking)
                    sourceCollection = sourceCollection.AsNoTracking();
                var product = await sourceCollection.FirstOrDefaultAsync(x => x.Id == id);
                if (product == null)
                    return null;
                return product;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public override async Task<Product> RemoveById(int id)
        {
            try
            {
                var product = await _ctx.Products.FirstOrDefaultAsync(x => x.Id == id);
                if (product == null)
                    return null;
                _ctx.Products.Remove(product);
                await _ctx.SaveChangesAsync();
                return product;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public override async Task<bool> Update(int id, Product newData)
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
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
