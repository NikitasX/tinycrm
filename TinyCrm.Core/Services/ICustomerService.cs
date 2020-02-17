using System.Linq;
using System.Threading.Tasks;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;

namespace TinyCrm.Core.Services
{
    public interface ICustomerService
    {
        Task<ApiResult<Customer>> CreateCustomer(CreateCustomerOptions options);

        Task<bool> UpdateCustomer(int customerId, UpdateCustomerOptions options);

        IQueryable<Customer> SearchCustomer(SearchCustomerOptions options);

        Customer GetCustomerById(int? customerId);
    }
}
