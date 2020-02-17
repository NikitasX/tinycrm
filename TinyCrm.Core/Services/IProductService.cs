using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TinyCrm.Core.Services
{
    public interface IProductService
    {
        Task<bool> AddProduct(AddProductOptions options);

        Task<bool> UpdateProduct(string productId, UpdateProductOptions options);

        List<Product> SearchProduct(SearchProductOptions options);

        Task<ApiResult<Product>> GetProductById(string productId);

        Dictionary<string, string> ParseCSV(string csvURL);
    }
}
