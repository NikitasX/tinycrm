using Xunit;
using System;
using System.Linq;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model.Options;

namespace TinyCrm.Tests
{
    public partial class CustomerServiceTests : IClassFixture<TinyCrmFixture>
    {
        private readonly Core.Services.ICustomerService csvc_;
        private readonly TinyCrmDbContext context_;

        /// <summary>
        /// Constructor
        /// </summary>
        public CustomerServiceTests(TinyCrmFixture fixture)
        {
            context_ = fixture.DbContext;
            csvc_ = new Core.Services.CustomerService(context_);
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
    }
}
