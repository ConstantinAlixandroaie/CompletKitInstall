using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompletKitInstall.ViewModels
{
    public class ProductImageViewModel: IUIObject
    {
        public int Id { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public int ProductId { get; set; }
    }
}
