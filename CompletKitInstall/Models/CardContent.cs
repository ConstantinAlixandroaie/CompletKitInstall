using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompletKitInstall.Models
{
    public class CardContent : IDbObject
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string CardText { get; set; }
        public string CardFooter { get; set; }

        public IDbObject MakeNew()
        {
            return new CardContent
            {
                ImageUrl = ImageUrl,
                CardText = CardText,
                CardFooter = CardFooter,
            };
        }

        public void UpdateFrom(IDbObject obj)
        {
            var q = obj as CardContent;
            CardText = q.CardText;
            CardFooter = q.CardFooter;
            ImageUrl = q.ImageUrl;
        }
    }
}
