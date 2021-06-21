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

        public async override Task<CarouselContent> Add(CarouselContentViewModel item, ClaimsPrincipal user)
        {
            try
            {
                if (item == null)
                    return null;
                if (item.ImageUrl == null)
                    return null;
                if (item.Title == null)
                    return null;
                if (item.SubTitle == null)
                    return null;
                var carouselContent = new CarouselContent
                {
                    Title=item.Title,
                    SubTitle=item.SubTitle,
                    ImageUrl=item.ImageUrl,
                };
                _ctx.CarouselContents.Add(carouselContent);
                await _ctx.SaveChangesAsync();
                return carouselContent;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
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
