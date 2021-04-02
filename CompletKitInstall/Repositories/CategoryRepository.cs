using CompletKitInstall.Data;
using CompletKitInstall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompletKitInstall.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        
    }
    public class CategoryRepository :Repository<Category>,ICategoryRepository
    {
        public CategoryRepository(CompletKitDbContext ctx):base(ctx)
        {

        }
        public override async Task<Category>Add(Category item)
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
            return category ;
        }
        public override Task<IEnumerable<Category>> Get(bool asNoTracking = false)
        {
            throw new NotImplementedException();
        }

        public override Task<Category> GetById(int id, bool asNoTracking = false)
        {
            throw new NotImplementedException();
        }

        public override Task<Category> RemoveById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
