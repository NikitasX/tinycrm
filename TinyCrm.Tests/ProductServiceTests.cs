using Xunit;
using Autofac;
using TinyCrm.Core.Services;
using System.Threading.Tasks;

namespace TinyCrm.Tests
{
    public partial class ProductServiceTests : IClassFixture<TinyCrmFixture>
    {
        private readonly Core.Services.IProductService psvc_;
        private readonly TinyCrm.Core.Data.TinyCrmDbContext context_;

        public ProductServiceTests (TinyCrmFixture fixture)
        {
            context_ = fixture.DbContext;
            psvc_ = fixture.Container.Resolve<IProductService>();
        }

        [Fact]
        public async Task GetProductById_Success()
        {
            var product = await psvc_.GetProductById("1450001164");

            Assert.NotNull(product.Data);
        }

        [Fact]
        public async Task GetProductById_Failure_Null_ProductId()
        {
            var product = await psvc_.GetProductById("     ");
            Assert.Null(product);

            product = await psvc_.GetProductById(null);
            Assert.Null(product);
        }
    }
}
