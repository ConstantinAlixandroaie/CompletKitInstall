using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompletKitInstall.Models
{
    public class ProductImage:IDbObject
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public IDbObject MakeNew()
        {
            return new ProductImage { ImageUrl = ImageUrl };
        }

        public void UpdateFrom(IDbObject obj)
        {
            var q = obj as ProductImage;
            ImageUrl = q.ImageUrl;
        }
    }
}
