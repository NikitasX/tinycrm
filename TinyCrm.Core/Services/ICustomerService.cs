﻿using System.Collections.Generic;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;

namespace TinyCrm.Core.Services
{
    public interface ICustomerService
    {
        Customer CreateCustomer(CreateCustomerOptions options);

        bool UpdateCustomer(int customerId, UpdateCustomerOptions options);

        List<Customer> SearchCustomer(SearchCustomerOptions options);

        Customer GetCustomerById(int customerId);
    }
}
