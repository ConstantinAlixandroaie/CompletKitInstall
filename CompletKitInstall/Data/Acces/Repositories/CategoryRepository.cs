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
    public interface ICategoryRepository : IRepository<Category, CategoryViewModel>
    {

    }
    public class CategoryRepository : Repository<Category, CategoryViewModel>, ICategoryRepository
    {
        public CategoryRepository(CompletKitDbContext ctx) : base(ctx)
        {

        }
        public override async Task<Category> Add(CategoryViewModel item)
        {
            try
            {
                if (item == null)
                    return null;
                if (item.Name == null)
                    return null;
                if (item.Description == null)
                    return null;
                var category = new Category
                {
                    Name = item.Name,
                    Description = item.Description,
                    DateCreated = DateTime.Now
                };
                _ctx.Categories.Add(category);
                await _ctx.SaveChangesAsync();
                return category;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public override async Task<IEnumerable<CategoryViewModel>> Get(bool asNoTracking = false)
        {
            try
            {
                var rv = new List<CategoryViewModel>();
                var sourceCollection = await _ctx.Categories.ToListAsync();
                foreach (var category in sourceCollection)
                {
                    var vm = new CategoryViewModel()
                    {
                        Id = category.Id,
                        Name = category.Name,
                        Description = category.Description,
                        DateCreated=category.DateCreated
                    };
                    rv.Add(vm);

                }
                return rv;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override async Task<CategoryViewModel> GetById(int id, bool asNoTracking = false)
        {
            try
            {
                var sourceCollection = _ctx.Categories.AsQueryable();
                if (asNoTracking)
                    sourceCollection = sourceCollection.AsNoTracking();
                var category = await sourceCollection.FirstOrDefaultAsync(x => x.Id == id);
                if (category == null)
                    return null;
                var rv = new CategoryViewModel
                {
                    Id=category.Id,
                    Name = category.Name,
                    Description = category.Description,
                    DateCreated = category.DateCreated
                };
                return rv;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override Task<bool> Remove(CategoryViewModel item)
        {
            throw new NotImplementedException();
        }

        public override async Task RemoveById(int id)
        {
            try
            {
                var category = await _ctx.Categories.FirstOrDefaultAsync(x => x.Id == id);
                if (category == null)
                    throw new ArgumentException($"An Article with the given ID = '{id}' was not found ");
                _ctx.Categories.Remove(category);
                await _ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override async Task<bool> Update(int id, ViewModels.CategoryViewModel newData)
        {
            try
            {
                var category = await _ctx.Categories.FirstOrDefaultAsync(x => x.Id == id);
                if (category == null)
                    return false;
                if (newData.Name != null)
                {
                    category.Name = newData.Name;
                }
                if (newData.Description != null)
                {
                    category.Description = category.Description;
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
