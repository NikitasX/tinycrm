using Xunit;
using TinyCrm.Core.Model.Options;
using System.Threading.Tasks;

namespace TinyCrm.Tests
{
    public partial class ProductServiceTests
    {
        [Fact]
        public async Task UpdateProduct_SuccessAsync()
        {
            var productId = "1450001164";
            var updateProductOptions = new UpdateProductOptions()
            {
                Name = "Pocophone F1",
                Price = 399.88M,
                Discount = 5,
                Description = "My New Phone",
                Category = Core.Model.ProductCategory.Smartphones
            };

            var temp = await psvc_.UpdateProduct(productId, updateProductOptions);

            Assert.True(temp.Success);

            var dProduct = await psvc_.GetProductById(productId);

            Assert.Equal(updateProductOptions.Name, dProduct.Data.Name);
            Assert.Equal(updateProductOptions.Price, dProduct.Data.Price);
            Assert.Equal(updateProductOptions.Discount, dProduct.Data.Discount);
            Assert.Equal(updateProductOptions.Description, dProduct.Data.Description);
            Assert.Equal(updateProductOptions.Category, dProduct.Data.Category);
        }
    }
}
