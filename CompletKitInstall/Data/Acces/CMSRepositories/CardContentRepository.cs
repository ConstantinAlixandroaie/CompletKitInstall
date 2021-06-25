using CompletKitInstall.Authorization;
using CompletKitInstall.Models;
using CompletKitInstall.Repositories;
using CompletKitInstall.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CompletKitInstall.Data.Acces.CMSRepositories
{
    public interface ICardContentRepository : IRepository<CardContent, CardContentViewModel>
    {

    }
    public class CardContentRepository : Repository<CardContent, CardContentViewModel>, ICardContentRepository
    {
        public CardContentRepository(CompletKitDbContext ctx, IAuthorizationService authorizationService, ILogger<CardContentRepository> logger) : base(ctx, authorizationService, logger)
        {

        }
        public override async Task<CardContent> Add(CardContentViewModel item, ClaimsPrincipal user)
        {
            try
            {
                if (item == null)
                    return null;
                if (item.CardText == null)
                    return null;
                if (item.ImageUrl == null)
                    return null;
                var cardContent = new CardContent
                {
                    CardText = item.CardText,
                    CardFooter = item.CardFooter,
                    ImageUrl = item.ImageUrl,
                };
                var isAuthorized = await _authorizationService.AuthorizeAsync(user, cardContent, Operations.Create);
                if (!isAuthorized.Succeeded)
                    throw new AuthenticationException("The user trying to add content to the database is not authorized.");
                else
                {
                    _ctx.CardContents.Add(cardContent);
                    await _ctx.SaveChangesAsync();
                    return cardContent;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "An error occurred adding the item to the database.");
                throw ex;
            }
        }

        public async override Task<IEnumerable<CardContentViewModel>> Get(bool asNoTracking = false)
        {
            try
            {
                var rv = new List<CardContentViewModel>();
                var sourceCollection = await _ctx.CardContents.ToListAsync();
                foreach (var item in sourceCollection)
                {
                    var vm = new CardContentViewModel
                    {
                        Id = item.Id,
                        CardText = item.CardText,
                        CardFooter = item.CardFooter,
                        ImageUrl = item.ImageUrl,
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

        public async override Task<CardContentViewModel> GetById(int id, bool asNoTracking = false)
        {
            try
            {
                var sourceCollection = _ctx.CardContents.AsQueryable();
                if (asNoTracking)
                    sourceCollection = sourceCollection.AsNoTracking();
                var cardContent = await sourceCollection.FirstOrDefaultAsync(x => x.Id == id);
                if (cardContent == null)
                    throw new NullReferenceException("The card Content you requested does not exist!");
                var rv = new CardContentViewModel
                {
                    Id = cardContent.Id,
                    CardText = cardContent.CardText,
                    CardFooter = cardContent.CardFooter,
                    ImageUrl = cardContent.ImageUrl,
                };
                return rv;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }
        }

        public override Task<bool> Remove(CardContentViewModel item, ClaimsPrincipal user)
        {
            throw new NotImplementedException();
        }

        public async override Task RemoveById(int id, ClaimsPrincipal user)
        {
            try
            {
                var cardContent = await _ctx.CardContents.FirstOrDefaultAsync(x => x.Id == id);
                var isAuthorized = await _authorizationService.AuthorizeAsync(user, cardContent, Operations.Delete);
                if (!isAuthorized.Succeeded)
                    throw new AuthenticationException("The user trying to delete the content from the database is not authorized.");
                else
                {
                    if (cardContent == null)
                        throw new ArgumentNullException("The Card Content you want to remove does not exist");
                    _ctx.CardContents.Remove(cardContent);
                    await _ctx.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "An error occurred removing the item from the database.");
                throw ex;
            }
        }

        public async override Task<bool> Update(int id, CardContentViewModel newData, ClaimsPrincipal user)
        {
            try
            {
                var cardContent = await _ctx.CardContents.FirstOrDefaultAsync(x => x.Id == id);

                if (cardContent == null)
                    return false;
                var isAuthorized = await _authorizationService.AuthorizeAsync(user, cardContent, Operations.Update);
                if (!isAuthorized.Succeeded)
                    throw new AuthenticationException("The user trying to delete the content from the database is not authorized.");
                else
                {
                    if (newData.CardText != null)
                        cardContent.CardText = newData.CardText;
                    if (newData.CardFooter != null)
                        cardContent.CardFooter = newData.CardFooter;
                    if (newData.ImageUrl != null)
                        cardContent.ImageUrl = newData.ImageUrl;
                    await _ctx.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "An error occurred removing the item from the database.");
                throw ex;
            }
        }
    }
}
