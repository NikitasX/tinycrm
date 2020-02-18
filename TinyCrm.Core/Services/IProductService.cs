using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace TinyCrm.Core.Services
{
    public interface IProductService
    {
        Task<ApiResult<Product>> AddProduct(AddProductOptions options);

        Task<ApiResult<Product>> UpdateProduct(string productId, UpdateProductOptions options);

        IQueryable<Product> SearchProduct(SearchProductOptions options);

        Task<ApiResult<Product>> GetProductById(string productId);

        Dictionary<string, string> ParseCSV(string csvURL);
    }
}
