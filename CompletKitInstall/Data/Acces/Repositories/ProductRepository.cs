﻿using CompletKitInstall.Data;
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
        public abstract Task<List<ProductViewModel>> GetBySearchInput(string searchString, string category);
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

        public async Task<List<ProductViewModel>> GetBySearchInput(string searchString, string category)
        {
            var rv = new List<ProductViewModel>();
            var products =await( from arts in _ctx.Products
                           select arts).ToListAsync();
            var searchProducts = from arts in _ctx.Products
                                   select arts;
            if(!string.IsNullOrEmpty(searchString)|| !string.IsNullOrEmpty(category))
            {
                if (!string.IsNullOrEmpty(category))
                {
                    searchProducts = from prod in _ctx.Products
                               join categs in _ctx.Categories on prod.CategoryId equals categs.Id
                               where categs.Id == int.Parse(category)
                               select prod;
                }
                if(!string.IsNullOrEmpty(searchString))
                {
                    searchProducts = searchProducts.Where(x => x.Name.Contains(searchString) || x.Description.Contains(searchString));
                }
                foreach (var product in searchProducts)
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
            }
            else
            {
                foreach (var product in products)
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
            }
            return rv;
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
