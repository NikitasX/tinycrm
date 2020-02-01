using System.Collections.Generic;
using TinyCrm.Model;
using TinyCrm.Model.Options;

namespace TinyCrm.Services
{
    interface ICustomerService
    {
        bool CreateCustomer(CreateCustomerOptions options);

        bool UpdateCustomer(int customerId, UpdateCustomerOptions options);

        List<Customer> SearchCustomer(SearchCustomerOptions options);

        Customer GetCustomerById(int customerId);
    }
}
