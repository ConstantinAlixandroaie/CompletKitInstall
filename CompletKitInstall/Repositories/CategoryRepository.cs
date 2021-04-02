using CompletKitInstall.Data;
using CompletKitInstall.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompletKitInstall.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {

    }
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(CompletKitDbContext ctx) : base(ctx)
        {

        }
        public override async Task<Category> Add(Category item)
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
        public override async Task<IEnumerable<Category>> Get(bool asNoTracking = false)
        {
            try
            {
                var sourceCollection = _ctx.Categories.AsQueryable();
                if (asNoTracking)
                    sourceCollection = sourceCollection.AsNoTracking();
                return await sourceCollection.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override async Task<Category> GetById(int id, bool asNoTracking = false)
        {
            try
            {
                var sourceCollection = _ctx.Categories.AsQueryable();
                if (asNoTracking)
                    sourceCollection = sourceCollection.AsNoTracking();
                var category = await sourceCollection.FirstOrDefaultAsync(x => x.Id == id);
                if (category == null)
                    return null;
                return category;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override async Task<Category> RemoveById(int id)
        {
            try
            {
                var category = await _ctx.Categories.FirstOrDefaultAsync(x => x.Id == id);
                if (category == null)
                    return null;
                _ctx.Categories.Remove(category);
                await _ctx.SaveChangesAsync();
                return category;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public override async Task<bool> Update(int id,Category newData)
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
