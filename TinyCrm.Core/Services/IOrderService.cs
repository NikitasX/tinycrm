using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TinyCrm.Core.Services
{
    public interface IOrderService
    {
        Task<ApiResult<Order>> CreateOrder(int customerId, ICollection<string> productIds);

        Task<string> UpdateOrder(int orderId, UpdateOrderOptions options);

        Task<Order> GetOrderById(int orderId);
    }
}
