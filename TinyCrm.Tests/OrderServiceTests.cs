using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Services;
using Xunit;

namespace TinyCrm.Tests
{
    public class OrderServiceTests : IDisposable
    {
        private readonly IOrderService orders_;
        private readonly IProductService products_;
        private readonly ICustomerService customers_;
        private readonly TinyCrmDbContext context_;


        public OrderServiceTests()
        {
            context_ = new TinyCrmDbContext();
            products_ = new ProductService(context_);
            customers_ = new CustomerService(context_);
            orders_ = new OrderService(customers_, context_);
        }

        [Fact]
        public void CreateOrder_Success()
        {
            var customerId = 6;
            var productIds = new List<string>()
            {
                "1480000010",
                "1480000010",
                "1480000023",
                "1480051175",
                "1480051275",
                "1480051324"
            };

            var addOrder = orders_.CreateOrder(customerId, productIds);

            Assert.NotNull(addOrder);

            Assert.True(context_
                .Set<OrderProduct>()
                .All(op => productIds.Contains(op.ProductId)));
        }

        public void Dispose()
        {
            context_.Dispose();
        }
    }
}
