using System.Collections.Generic;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;

namespace TinyCrm.Core.Services
{
    public interface IOrderService
    {
        Order CreateOrder(int customerId, ICollection<string> productIds);

        string UpdateOrder(int orderId, UpdateOrderOptions options);

        Order GetOrderById(int orderId);
    }
}
