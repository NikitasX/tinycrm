﻿using Xunit;
using Autofac;
using System.Linq;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Services;
using System.Collections.Generic;

namespace TinyCrm.Tests
{
    public class OrderServiceTests : IClassFixture<TinyCrmFixture>
    {
        private readonly IOrderService orders_;
        private readonly IProductService products_;
        private readonly ICustomerService customers_;
        private readonly TinyCrmDbContext context_;


        public OrderServiceTests(TinyCrmFixture fixture)
        {
            context_ = fixture.DbContext;
            products_ = fixture.Container.Resolve<IProductService>();
            customers_ = fixture.Container.Resolve<ICustomerService>();
            orders_ = fixture.Container.Resolve<IOrderService>();
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
    }
}
