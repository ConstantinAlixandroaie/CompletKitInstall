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
    public interface ICarouselContentRepository:IRepository<CarouselContent,CarouselContentViewModel>
    {

    }
    public class CarouselContentRepository:Repository<CarouselContent,CarouselContentViewModel>,ICarouselContentRepository
    {
        public CarouselContentRepository(CompletKitDbContext ctx, IAuthorizationService authorizationService) : base(ctx, authorizationService)
        {

        }

        public override Task<CarouselContent> Add(CarouselContentViewModel item, ClaimsPrincipal user)
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<CarouselContentViewModel>> Get(bool asNoTracking = false)
        {
            throw new NotImplementedException();
        }

        public override Task<CarouselContentViewModel> GetById(int id, bool asNoTracking = false)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> Remove(CarouselContentViewModel item, ClaimsPrincipal user)
        {
            throw new NotImplementedException();
        }

        public override Task RemoveById(int id, ClaimsPrincipal user)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> Update(int id, CarouselContentViewModel newData, ClaimsPrincipal user)
        {
            throw new NotImplementedException();
        }
    }
}
