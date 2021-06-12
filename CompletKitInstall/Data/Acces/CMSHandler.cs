using System;
using System.Collections.Generic;
using System.Linq;
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
        public Task GetCardContent();
        public Task EditCardContent();
        public Task RemoveCardContent();
    }
    public class CMSHandler : ICMSHandler
    {
        public Task AddCardContent()

        {
            throw new NotImplementedException();
        }

        public Task AddCarouselContent()
        {
            throw new NotImplementedException();
        }

        public Task EditCardContent()
        {
            throw new NotImplementedException();
        }

        public Task EditCarouselContent()
        {
            throw new NotImplementedException();
        }

        public Task GetCardContent()
        {
            throw new NotImplementedException();
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
