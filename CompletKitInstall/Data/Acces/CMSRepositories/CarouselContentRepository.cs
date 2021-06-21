﻿using CompletKitInstall.Models;
using CompletKitInstall.Repositories;
using CompletKitInstall.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CompletKitInstall.Data.Acces.CMSRepositories
{
    public interface ICarouselContentRepository : IRepository<CarouselContent, CarouselContentViewModel>
    {

    }
    public class CarouselContentRepository : Repository<CarouselContent, CarouselContentViewModel>, ICarouselContentRepository
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
                    Title = item.Title,
                    SubTitle = item.SubTitle,
                    ImageUrl = item.ImageUrl,
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

        public async override Task<IEnumerable<CarouselContentViewModel>> Get(bool asNoTracking = false)
        {
            try
            {
                var rv = new List<CarouselContentViewModel>();
                var sourceCollection = await _ctx.CarouselContents.ToListAsync();
                foreach (var item in sourceCollection)
                {
                    var vm = new CarouselContentViewModel
                    {
                        Id = item.Id,
                        ImageUrl = item.ImageUrl,
                        Title = item.Title,
                        SubTitle = item.SubTitle,
                    };
                    rv.Add(vm);
                }
                return rv;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }
        }

        public async override Task<CarouselContentViewModel> GetById(int id, bool asNoTracking = false)
        {
            try
            {
                var sourceCollection = _ctx.CarouselContents.AsQueryable();
                if (asNoTracking)
                    sourceCollection = sourceCollection.AsNoTracking();
                var carouselContent = await sourceCollection.FirstOrDefaultAsync(x => x.Id == id);
                if (carouselContent == null)
                    throw new ArgumentNullException("The carousel Content you requested does not exist");
                var rv = new CarouselContentViewModel
                {
                    Id = carouselContent.Id,
                    Title = carouselContent.Title,
                    SubTitle = carouselContent.SubTitle,
                    ImageUrl = carouselContent.ImageUrl,
                };
                return rv;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }
        }

        public override Task<bool> Remove(CarouselContentViewModel item, ClaimsPrincipal user)
        {
            throw new NotImplementedException();
        }

        public async override Task RemoveById(int id, ClaimsPrincipal user)
        {
            var carouselContent =await _ctx.CarouselContents.FirstOrDefaultAsync(x => x.Id == id);
            if (carouselContent == null)
                throw new ArgumentNullException("The Carousel item want to delete does not exist!");
            _ctx.Remove(carouselContent);
            await _ctx.SaveChangesAsync();
        }

        public async override Task<bool> Update(int id, CarouselContentViewModel newData, ClaimsPrincipal user)
        {
            try
            {
                var carouselContent = await _ctx.CarouselContents.FirstOrDefaultAsync(x => x.Id == id);
                if (carouselContent == null)
                    return false;
                if (newData.ImageUrl != null)
                    carouselContent.ImageUrl = newData.ImageUrl;
                if (newData.Title != null)
                    carouselContent.Title = newData.Title;
                if (carouselContent.SubTitle != null)
                    carouselContent.SubTitle = newData.SubTitle;
                await _ctx.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }
            
        }
    }
}
