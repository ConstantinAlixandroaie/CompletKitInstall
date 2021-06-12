using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompletKitInstall.Models
{
    public class Product : IDbObject
    {
        public int Id { get ; set ; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        //Images for catalog
        public List<ProductImage> ProductImages { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public DateTimeOffset DateCreated { get; set; }

        public IDbObject MakeNew()
        {
            return new Product { Name = Name,Description=Description,ImageUrl=ImageUrl, Category=Category,CategoryId=CategoryId };
        }

        public void UpdateFrom(IDbObject obj)
        {
            var q = obj as Product;
            Name = q.Name;
            Description = q.Description;
            ImageUrl = q.ImageUrl;
            Category = q.Category;
            CategoryId = q.CategoryId;
        }
    }
}

