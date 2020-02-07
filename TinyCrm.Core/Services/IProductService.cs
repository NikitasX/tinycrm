using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;
using System.Collections.Generic;

namespace TinyCrm.Core.Services
{
    public interface IProductService
    {
        bool AddProduct(AddProductOptions options);

        bool UpdateProduct(string productId, UpdateProductOptions options);

        Product GetProductById(string productId);

        Dictionary<string, string> ParseCSV(string csvURL);
    }
}
