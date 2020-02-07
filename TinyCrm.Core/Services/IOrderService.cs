using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;
using System.Collections.Generic;

namespace TinyCrm.Core.Services
{
    public interface IOrderService
    {
        Order CreateOrder(int customerId, ICollection<string> productIds);

        string UpdateOrder(int orderId, UpdateOrderOptions options);

        Order GetOrderById(int orderId);
    }
}
