using CompletKitInstall.Models;
using CompletKitInstall.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CompletKitInstall.Data.Acces
{

    public interface ICMSHandler
    {
        public Task AddCarouselContent();
        public Task GetCarouselContent();
        public Task EditCarouselContent();
        public Task RemoveCarouselContent();
        public Task AddCardContent();
        public Task<IEnumerable<CardContentViewModel>> GetCardContent();
        public Task<bool> EditCardContent(int id, CardContentViewModel newData,ClaimsPrincipal user);
        public Task RemoveCardContent();
    }
    public class CMSHandler : ICMSHandler
    {
        protected CompletKitDbContext _ctx;
        protected readonly IAuthorizationService _authorizationService;
        public CMSHandler(CompletKitDbContext ctx, IAuthorizationService authorizationService)
        {
            _ctx = ctx;
            _authorizationService = authorizationService;
        }
        public Task AddCardContent()

        {
            throw new NotImplementedException();
        }

        public Task AddCarouselContent()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> EditCardContent(int id,CardContentViewModel newData,ClaimsPrincipal user)
        {
            try
            {
                var cardContent = await _ctx.CardContents.FirstOrDefaultAsync(x => x.Id == id);
                if (cardContent == null)
                    return false;
                if ((newData.CardText != null) && (string.Equals(newData.CardText, cardContent.CardText)))
                    cardContent.CardText = newData.CardText;
                if ((newData.CardFooter != null) && (string.Equals(newData.CardFooter, cardContent.CardFooter)))
                    cardContent.CardFooter = newData.CardFooter;
                if ((newData.ImageUrl != newData.ImageUrl) && (string.Equals(newData.ImageUrl, cardContent.ImageUrl)))
                    cardContent.ImageUrl = newData.ImageUrl;
                await _ctx.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public Task EditCarouselContent()
        {
            throw new NotImplementedException();
        }
        public  async Task<IEnumerable<CardContentViewModel>> GetCardContent()
        {
            try
            {
                var rv = new List<CardContentViewModel>();
                var sourceCollection = await _ctx.CardContents.ToListAsync();
                foreach (var cardContent in sourceCollection)
                {
                    var vm = new CardContentViewModel()
                    {
                        Id = cardContent.Id,
                        CardText = cardContent.CardText,
                        CardFooter = cardContent.CardFooter,
                        ImageUrl = cardContent.ImageUrl
                    };
                    rv.Add(vm);
                }
                return rv;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public Task GetCarouselContent()
        {
            throw new NotImplementedException();
        }

        public Task RemoveCardContent()
        {
            throw new NotImplementedException();
        }

        public Task RemoveCarouselContent()
        {
            throw new NotImplementedException();
        }
    }
}
