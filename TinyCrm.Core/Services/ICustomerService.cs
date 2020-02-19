using System.Linq;
using System.Threading.Tasks;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;

namespace TinyCrm.Core.Services
{
    public interface ICustomerService
    {
        Task<ApiResult<Customer>> CreateCustomer(CreateCustomerOptions options);

        Task<ApiResult<Customer>> UpdateCustomer(int customerId, UpdateCustomerOptions options);

        IQueryable<Customer> SearchCustomer(SearchCustomerOptions options);

        Task<ApiResult<Customer>> GetCustomerById(int? customerId);
    }
}
