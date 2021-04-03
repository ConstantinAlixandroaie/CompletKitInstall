using CompletKitInstall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompletKitInstall.Repositories
{
    public interface IRepository<T> where T:IDbObject
    {
        Task<IEnumerable<T>> Get(bool asNoTracking = false);
        Task<T> GetById(int id, bool asNoTracking = false);
        Task<T> Add(T item);
        Task<bool> Remove(T item);
        Task<T> RemoveById(int id);
        Task<bool> Update(int id, T newData);
    }
}
