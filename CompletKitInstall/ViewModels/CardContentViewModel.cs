using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompletKitInstall.ViewModels
{
    public class CardContentViewModel:IUIObject
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string CardText { get; set; }
        public string CardFooter { get; set; }
    }
}
