using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompletKitInstall.Models
{
    public class CarouselContent : IDbObject
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public bool Display { get; set; }

        public IDbObject MakeNew()
        {
            return new CarouselContent
            {
                ImageUrl = ImageUrl,
                Title = Title,
                SubTitle = SubTitle,
                Display=Display
            };
        }

        public void UpdateFrom(IDbObject obj)
        {
            var q = obj as CarouselContent;
            SubTitle = q.SubTitle;
            Title = q.Title;
            ImageUrl = q.ImageUrl;
        }
    }
}
