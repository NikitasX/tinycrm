using System.Linq;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;

namespace TinyCrm.Core.Services
{
    public interface ICustomerService
    {
        Customer CreateCustomer(CreateCustomerOptions options);

        bool UpdateCustomer(int customerId, UpdateCustomerOptions options);

        IQueryable<Customer> SearchCustomer(SearchCustomerOptions options);

        Customer GetCustomerById(int? customerId);
    }
}
