using CompletKitInstall.Data;
using CompletKitInstall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompletKitInstall.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class, IDbObject
    {
        protected CompletKitDbContext _ctx;
        public Repository(CompletKitDbContext ctx)
        {
            _ctx = ctx;
        }
        public virtual async Task<T> Add(T item)
        {
            if (item == null)
                return null;
            var newCli = item.MakeNew();
            _ctx.Add(newCli);
            await _ctx.SaveChangesAsync();
            return newCli as T;
        }

        public abstract Task<IEnumerable<T>> Get(bool asNoTracking = false);

        public abstract Task<T> GetById(int id, bool asNoTracking = false);
        public virtual async Task<bool> Remove(T item)
        {
            if (item == null)
                return false;
            return (await RemoveById(item.Id)) != null;
        }

        public abstract Task<T> RemoveById(int id);

        public virtual async Task<bool> Update(int id, T newData)
        {
            var item = await GetById(id);
            if (item == null)
                return false;

            item.UpdateFrom(newData);
            await _ctx.SaveChangesAsync();
            return true;
        }
        
    }
}
