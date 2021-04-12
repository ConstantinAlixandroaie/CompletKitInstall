using CompletKitInstall.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompletKitInstall.ViewModels
{
    public class ProductViewModel: IUIObject
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public Category Category { get; set; }
        public int CategoryId { get; set; }

        public DateTimeOffset DateCreated { get; set; }
    }
}
