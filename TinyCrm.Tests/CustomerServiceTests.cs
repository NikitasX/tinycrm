using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;
using Xunit;

namespace TinyCrm.Tests
{
    public partial class CustomerServiceTests : IDisposable
    {
        private readonly Core.Services.ICustomerService csvc_;
        private readonly TinyCrmDbContext context;

        /// <summary>
        /// Constructor
        /// </summary>
        public CustomerServiceTests()
        {
            context = new TinyCrmDbContext();
            csvc_ = new Core.Services.CustomerService(context);
        }

        [Fact]
        public void CreateCustomer_Success()
        {
            var customer = csvc_.CreateCustomer(
                new CreateCustomerOptions() { 
                    VatNumber = $"999{DateTime.UtcNow.Millisecond:D6}",
                    Email = "testcustomer@iamtesting.com",
                    Firstname = "Jon",
                    Lastname = "Doe",
                    Phone = "+30699999999"
                });
            Assert.NotNull(customer);

            var databaseCustomer = csvc_.SearchCustomer(new SearchCustomerOptions()
                {
                    VatNumber = customer.VatNumber
                }).SingleOrDefault();

            Assert.NotNull(databaseCustomer);

            Assert.Equal(customer.Email, databaseCustomer.Email);
            Assert.Equal(customer.Firstname, databaseCustomer.Firstname);
            Assert.Equal(customer.Lastname, databaseCustomer.Lastname);
            Assert.Equal(customer.Phone, databaseCustomer.Phone);
            Assert.True(databaseCustomer.Status);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            context.Dispose();
        }

    }
}
