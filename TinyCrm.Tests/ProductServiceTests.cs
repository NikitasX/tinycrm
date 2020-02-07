using Xunit;
using TinyCrm.Core.Data;

namespace TinyCrm.Tests
{
    public partial class ProductServiceTests : IClassFixture<TinyCrmFixture>
    {
        private readonly Core.Services.IProductService psvc_;
        private readonly TinyCrm.Core.Data.TinyCrmDbContext context_;

        public ProductServiceTests (TinyCrmFixture fixture)
        {
            context_ = fixture.DbContext;
            psvc_ = new Core.Services.ProductService(context_);
        }

        [Fact]
        public void GetProductById_Success()
        {
            var product = psvc_.GetProductById("SKU8");

            Assert.NotNull(product);
            Assert.Equal(399.88M, product.Price);
        }

        [Fact]
        public void GetProductById_Failure_Null_ProductId()
        {
            var product = psvc_.GetProductById("     ");
            Assert.Null(product);

            product = psvc_.GetProductById(null);
            Assert.Null(product);
        }
    }
}
