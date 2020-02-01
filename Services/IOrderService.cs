using System.Collections.Generic;
using TinyCrm.Model;
using TinyCrm.Model.Options;

namespace TinyCrm.Services
{
    interface IOrderService
    {
        Order CreateOrder(int customerId, List<Product> productList);

        string UpdateOrder(int orderId, UpdateOrderOptions options);

        Order GetOrderById(int orderId);
    }
}
