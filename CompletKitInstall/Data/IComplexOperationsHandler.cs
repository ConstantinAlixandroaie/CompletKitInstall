using CompletKitInstall.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompletKitInstall.Data
{
   public interface IComplexOperationsHandler
    {
        public Task AddProductAndImages(ProductViewModel product, List<ProductImageViewModel> productImages);
        public Task RemoveProductWithImages(int id);
    }
}
