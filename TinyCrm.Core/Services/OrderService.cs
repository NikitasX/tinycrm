using System;
using System.Linq;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;
using System.Collections.Generic;

namespace TinyCrm.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly ICustomerService customers_;
        private readonly TinyCrmDbContext context_;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="customers"></param>
        /// <param name="context"></param>
        public OrderService(
            ICustomerService customers,
            TinyCrmDbContext context)
        {
            context_ = context;
            customers_ = customers;
        }

        public Order CreateOrder(int customerId, ICollection<string> productIds)
        {
            if(customerId <= 0) {
                return null;
            }            
            
            if(productIds == null ||
                productIds.Count == 0) {
                return null;
            }

            productIds = productIds
                .Distinct()
                .ToList();

            var customer = customers_.SearchCustomer(
                new SearchCustomerOptions() { 
                    Id = customerId
                })
                .Where(c => c.Status == true)
                .SingleOrDefault();

            if(customer == null) {
                return null;
            }

            var products = context_
                .Set<Product>()
                .Where(p => productIds.Contains(p.Id))
                .Distinct()
                .ToList();

            if (products.Count != productIds.Count) {
                return null;
            }

            var order = new Order()
            {
                Customer = customer
            };

            foreach(var p in products) {
                order.Products.Add(
                    new OrderProduct()
                    {
                        ProductId = p.Id
                    });
            }
            context_.Add(order);
            try {
                context_.SaveChanges();
            } catch (Exception) {
                return null;
            }

            return order;
        }

        public Order GetOrderById(int orderId)
        {
            throw new NotImplementedException();
        }

        public string UpdateOrder(int orderId, UpdateOrderOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
