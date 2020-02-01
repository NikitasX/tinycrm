using System;
using System.Collections.Generic;
using System.Linq;
using TinyCrm.Model;
using TinyCrm.Model.Options;

namespace TinyCrm.Services
{
    public class OrderService : IOrderService
    {

        private List<Order> OrderList = new List<Order>();

        private int AIID = 0;

        public Order CreateOrder(int customerId, List<Product> productList)
        {
            if(customerId < 0) {
                throw new ArgumentOutOfRangeException(
                    $"The value of {nameof(customerId)} cannot be smaller than 0");
            }

            var customerCheck = new CustomerService();
            var customer = customerCheck.GetCustomerById(customerId);

            if(customer == default(Customer) ||
                customer.Status == 0) {
                throw new ApplicationException(
                    $"Customer not found or Inactive");
            }

            if(productList == null) {
                throw new ArgumentNullException(
                    $"{nameof(productList)} cannot be empty");
            }

            var pService = new ProductService();
            var i = 0;

            decimal amount = 0;

            foreach(var p in productList) {
                if(pService.GetProductById(p.Id) == default) {
                    productList.RemoveAt(i);
                } else {
                    amount += p.Price;
                }
                i++;
            }

            if(productList == null) {
                throw new ArgumentNullException(
                    $"Products submitted in {nameof(productList)} " +
                    $"not found in Products database");
            }

            var order = new Order()
            {
                Id = AIID,
                CustomerId = customerId,
                Amount = amount,
                Status = OrderStatus.Pending,
                Products = productList
            };

            AIID++;

            OrderList.Add(order);

            return order;
        }

        public string UpdateOrder(int orderId, UpdateOrderOptions options)
        {
            if(orderId < 0) {
                return $"Invalid {nameof(orderId)}, smaller than 0";
            }

            if(options == null) {
                return $"Invalid {nameof(options)}, empty";
            }

            var order = GetOrderById(orderId);

            if(order == null) {
                return "Order not found by id";
            }

            if(options.Status.Equals(OrderStatus.Canceled)) {
                order.Status = OrderStatus.Canceled;
                return "Order cancelled succesfully";
            }

            if(!string.IsNullOrWhiteSpace(options.DeliveryAdress)) {
                order.DeliveryAdress = options.DeliveryAdress;
            }

            if (options.Products != null) {
                decimal amount = 0;
                
                foreach(var p in options.Products) {
                    amount += p.Price;
                }

                order.Products = options.Products;
                order.Amount = amount;
            }

            if(!options.Status.Equals(OrderStatus.Invalid)) {
                order.Status = options.Status;
            }

            return "Order Updated Successfully";
        }

        public Order GetOrderById(int orderId)
        {
            if (orderId < 0) {
                return default;
            }

            return OrderList
                .Where(s => s.Id.Equals(orderId))
                .SingleOrDefault();
        }
    }
}
