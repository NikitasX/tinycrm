using Xunit;
using System;
using TinyCrm.Core.Model;

namespace TinyCrm.Tests
{
    public partial class ProductServiceTests : IDisposable
    {
        [Fact]
        public void AddProduct_Success()
        {
            var productId = $"SKU{DateTime.Now.Millisecond}";

            var validate = psvc_.AddProduct(new Core.Model.Options.AddProductOptions()
            {
                Id = productId,
                Name = "Sony Tv",
                Price = 399.88M,
                Description = "Some Sony Tv",
                Discount = 0,
                Category = Core.Model.ProductCategory.Televisions
            });

            Assert.True(validate);

            var product = psvc_.GetProductById(productId);
            Assert.NotNull(product);

            Assert.Equal("Sony Tv", product.Name);
            Assert.Equal(399.88M, product.Price);
            Assert.Equal(Core.Model.ProductCategory.Televisions, product.Category);
        }

        [Fact]
        public void AddProduct_Failure_Invalid_Category()
        {
            var productId = $"SKU{DateTime.Now.Millisecond}";

            var validate = psvc_.AddProduct(new Core.Model.Options.AddProductOptions()
            {
                Id = productId,
                Name = "Sony Tv",
                Price = 313.99M,
                Description = "Some Sony Tv",
                Discount = 0
                //Category = Core.Model.ProductCategory.Invalid
            });

            Assert.False(validate);
        }

        [Fact]
        public void AddProduct_From_Csv()
        {
            var productList = psvc_.ParseCSV("C:/Users/KCA4/devel/TinyCrm/products.csv");

            foreach(var p in productList) {

                var attributeArray = p.Value.Split('|');

                context_.Add(new Product()
                {
                    Id = $"{p.Key.ToString()}",
                    Name = attributeArray[0],
                    Price = decimal.Parse(attributeArray[1]),
                    Category = Core.Model.ProductCategory.Laptops
                });

                context_.SaveChanges();
            }
        }

        public void Dispose()
        {
            context_.Dispose();
        }
    }
}
