using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompletKitInstall.Data.Acces.CMSRepositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CompletKitInstall.Pages.CMS
{
    public class CarouselModel : PageModel
    {
        private readonly ICardContentRepository _cardContentRepo;
        private readonly ICarouselContentRepository _carouselContentRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CarouselModel(ICardContentRepository cardContentRepo,ICarouselContentRepository carouselContentRepo,IWebHostEnvironment webHostEnvironment)
        {
            _cardContentRepo = cardContentRepo;
            _carouselContentRepo = carouselContentRepo;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> OnGetASync()
        {
            return Page();
        }
    }
}
