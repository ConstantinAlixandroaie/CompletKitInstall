using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompletKitInstall.Models
{
    public class Category:IDbObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset DateCreated { get; set; }

        public IDbObject MakeNew()
        {
            return new Category
            { 
                Name=Name,
                Description=Description
            };
        }

        public void UpdateFrom(IDbObject obj)
        {
            var q = obj as Product;
            Name = q.Name;
            Description = q.Description;
        }
    }
}
