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
                    CardText=item.CardText,
                    CardFooter=item.CardFooter,
                    ImageUrl=item.ImageUrl,
                };
                _ctx.CardContents.Add(cardContent);
                await _ctx.SaveChangesAsync();
                return cardContent;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
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
