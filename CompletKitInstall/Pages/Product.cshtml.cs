using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using CompletKitInstall.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CompletKitInstall.Pages
{
    public class ProductModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;
        //public HttpClient Client { get; }
        public List<ProductViewModel> Products { get; private set; }
        public bool GetProductsError { get; private set; }
        public ProductModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task OnGetAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:5001/api/Product");
            var Client = _clientFactory.CreateClient();
            var response = await Client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                using var responseJson = await response.Content.ReadAsStreamAsync();
                //Products = await Task.Run(() => .....);
                Products = await JsonSerializer.DeserializeAsync<List<ProductViewModel>>(responseJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                //in loc de using
                //responseJson.Dispose();
            }
            else
            {
                GetProductsError = true;
                Products = null;
            }

        }
    }
}
