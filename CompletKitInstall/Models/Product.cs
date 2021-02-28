using System;
using System.Collections.Generic;
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

        public IDbObject MakeNew()
        {
            return new Product { };
        }

        public void UpdateFrom(IDbObject obj)
        {
            var q = obj as Product;
            Name = q.Name;
            Description = q.Description;
            ImageUrl = q.ImageUrl;
        }
    }
}
