using CompletKitInstall.Models;
using CompletKitInstall.Repositories;
using CompletKitInstall.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CompletKitInstall.Data.Acces.CMSRepositories
{
    public interface ICardContentRepository:IRepository<CardContent,CardContentViewModel>
    {

    }
    public class CardContentRepository : Repository<CardContent, CardContentViewModel>, ICardContentRepository
    {
        public CardContentRepository(CompletKitDbContext ctx, IAuthorizationService authorizationService) : base(ctx, authorizationService)
        {

        }
        public override Task<CardContent> Add(CardContentViewModel item, ClaimsPrincipal user)
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<CardContentViewModel>> Get(bool asNoTracking = false)
        {
            throw new NotImplementedException();
        }

        public override Task<CardContentViewModel> GetById(int id, bool asNoTracking = false)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> Remove(CardContentViewModel item, ClaimsPrincipal user)
        {
            throw new NotImplementedException();
        }

        public override Task RemoveById(int id, ClaimsPrincipal user)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> Update(int id, CardContentViewModel newData, ClaimsPrincipal user)
        {
            throw new NotImplementedException();
        }
    }
}
