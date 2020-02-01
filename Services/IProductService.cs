using TinyCrm.Model;
using TinyCrm.Model.Options;

namespace TinyCrm.Services
{
    interface IProductService
    {
        bool AddProduct(AddProductOptions options);

        bool UpdateProduct(string productId, UpdateProductOptions options);

        Product GetProductById(string productId);
    }
}
