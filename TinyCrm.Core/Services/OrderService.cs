using System;
using System.Linq;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TinyCrm.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly ICustomerService customers_;
        private readonly IProductService products_;
        private readonly TinyCrmDbContext context_;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="customers"></param>
        /// <param name="context"></param>
        public OrderService(
            ICustomerService customers,
            IProductService products,
            TinyCrmDbContext context)
        {
            context_ = context;
            customers_ = customers;
            products_ = products;
        }

        public async Task<ApiResult<Order>> CreateOrder(int customerId, ICollection<string> productIds)
        {
            if (customerId <= 0) {
                return new ApiResult<Order>(StatusCode.BadRequest, "Invalid customer Id");
            }

            if (productIds == null ||
                productIds.Count == 0) {
                return new ApiResult<Order>(StatusCode.BadRequest, "Invalid or empty product list");
            }

            productIds = productIds
                .Distinct()
                .ToList();

            var customer = customers_.SearchCustomer(
                new SearchCustomerOptions()
                {
                    Id = customerId
                })
                .Where(c => c.Status == true)
                .SingleOrDefault();

            if (customer == null) {
                return new ApiResult<Order>(StatusCode.NotFound, "Customer not found in database");
            }

            var products = await context_
                .Set<Product>()
                .Where(p => productIds.Contains(p.Id))
                .Distinct()
                .ToListAsync();

            if (products.Count != productIds.Count) {
                return new ApiResult<Order>(StatusCode.BadRequest, "Not all product Ids found in database");
            }

            var order = new Order()
            {
                Customer = customer
            };

            foreach (var p in products) {
                order.Products.Add(
                    new OrderProduct()
                    {
                        ProductId = p.Id
                    });
            }
            context_.Add(order);
            try {
                context_.SaveChanges();
            } catch (Exception e) {
                return new ApiResult<Order>(StatusCode.InternalServerError, $"Something went wrong {e}");
            }

            return ApiResult<Order>.CreateSuccess(order);
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            throw new NotImplementedException();
        }

        public async Task<string> UpdateOrder(int orderId, UpdateOrderOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
