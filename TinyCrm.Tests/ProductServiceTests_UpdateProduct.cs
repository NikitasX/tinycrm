using System;
using System.Collections.Generic;
using System.Text;
using TinyCrm.Core.Model.Options;
using Xunit;

namespace TinyCrm.Tests
{
    public partial class ProductServiceTests
    {
        [Fact]
        public void UpdateProduct_Success()
        {
            var productId = "SKU8";
            var updateProductOptions = new UpdateProductOptions()
            {
                Name = "Pocophone F1",
                Price = 399.88M,
                Discount = 5,
                Description = "My New Phone",
                Category = Core.Model.ProductCategory.Smartphones
            };

            Assert.True(psvc_.UpdateProduct(productId, updateProductOptions));

            var dProduct = psvc_.GetProductById(productId);

            Assert.Equal(updateProductOptions.Name, dProduct.Name);
            Assert.Equal(updateProductOptions.Price, dProduct.Price);
            Assert.Equal(updateProductOptions.Discount, dProduct.Discount);
            Assert.Equal(updateProductOptions.Description, dProduct.Description);
            Assert.Equal(updateProductOptions.Category, dProduct.Category);
        }
    }
}
