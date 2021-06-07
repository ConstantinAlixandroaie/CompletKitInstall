using CompletKitInstall.Models;
using CompletKitInstall.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CompletKitInstall.Repositories
{
    public interface IRepository<T,U> 
        where T:IDbObject 
        where U:IUIObject
    {
        Task<IEnumerable<U>> Get(bool asNoTracking = false);
        Task<U> GetById(int id, bool asNoTracking = false);
        Task<T> Add(U item, ClaimsPrincipal user);
        Task<bool> Remove(U item, ClaimsPrincipal user);
        Task RemoveById(int id, ClaimsPrincipal user);
        Task<bool> Update(int id, U newData, ClaimsPrincipal user);
    }
}
