using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompletKitInstall.Data.Acces.CMSRepositories;
using CompletKitInstall.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CompletKitInstall.Pages.CMS
{
    public class CarouselModel : PageModel
    {
        private readonly ICardContentRepository _cardContentRepo;
        private readonly ICarouselContentRepository _carouselContentRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        [BindProperty]
        public IFormFile CarouselImage { get; set; }
        [BindProperty]
        public IFormFile CardImage { get; set; }
        [BindProperty]
        public CarouselContentViewModel Carousel { get; set; }
        [BindProperty]
        public CardContentViewModel Card { get; set; }
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

