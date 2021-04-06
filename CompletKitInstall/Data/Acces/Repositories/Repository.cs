using CompletKitInstall.Data;
using CompletKitInstall.Models;
using CompletKitInstall.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompletKitInstall.Repositories
{
    public abstract class Repository<T, U> : IRepository<T, U>
        where T : class, IDbObject
        where U : class, IUIObject
    {
        protected CompletKitDbContext _ctx;
        public Repository(CompletKitDbContext ctx)
        {
            _ctx = ctx;
        }
        public abstract Task<T> Add(U item);

        public abstract Task<IEnumerable<U>> Get(bool asNoTracking = false);

        public abstract Task<U> GetById(int id, bool asNoTracking = false);
        public abstract Task<bool> Remove(U item);
        public abstract Task RemoveById(int id);

        public abstract Task<bool> Update(int id, U newData);

    }
}
