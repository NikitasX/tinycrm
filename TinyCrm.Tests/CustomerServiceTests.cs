using Xunit;
using System;
using Autofac;
using System.Linq;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model.Options;
using TinyCrm.Core.Services;
using TinyCrm.Core.Model;
using System.Threading.Tasks;

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
            csvc_ = fixture.Container.Resolve<ICustomerService>();
        }

        [Fact]
        public async Task CreateCustomer_Fail_Empty_Vat()
        {
            var country = context_
                .Set<Country>()
                .Where(c => c.CountryId == "GR")
                .SingleOrDefault();

            var customer = await csvc_.CreateCustomer(
                new CreateCustomerOptions() { 
                    VatNumber = "",
                    Email = $"testcustomer{DateTime.UtcNow.Millisecond:D6}@iamtesting.com",
                    Firstname = "Jon",
                    Lastname = "Doe",
                    Phone = "+30699999999",
                    Country = country
                });
            Assert.Null(customer.Data);
            Assert.Equal(Core.StatusCode.BadRequest, customer.ErrorCode);
        }          
        
        [Fact]
        public async Task CreateCustomer_Fail_Vat_Exists()
        {
            var country = context_
                .Set<Country>()
                .Where(c => c.CountryId == "GR")
                .SingleOrDefault();

            var customer = await csvc_.CreateCustomer(
                new CreateCustomerOptions() { 
                    VatNumber = "999000321",
                    Email = $"testcustomer{DateTime.UtcNow.Millisecond:D6}@iamtesting.com",
                    Firstname = "Jon",
                    Lastname = "Doe",
                    Phone = "+30699999999",
                    Country = country
                });
            Assert.Null(customer.Data);
            Assert.Equal(Core.StatusCode.Conflict, customer.ErrorCode);
        }        
        
        [Fact]
        public async Task CreateCustomer_Success()
        {
            var country = context_
                .Set<Country>()
                .Where(c => c.CountryId == "GR")
                .SingleOrDefault();

            var customer = await csvc_.CreateCustomer(
                new CreateCustomerOptions() { 
                    VatNumber = $"999{DateTime.UtcNow.Millisecond:D6}",
                    Email = $"testcustomer{DateTime.UtcNow.Millisecond:D6}@iamtesting.com",
                    Firstname = "Jon",
                    Lastname = "Doe",
                    Phone = "+30699999999",
                    Country = country
                });
            Assert.NotNull(customer.Data);
            Assert.Equal(Core.StatusCode.Ok, customer.ErrorCode);

            var databaseCustomer = csvc_.SearchCustomer(new SearchCustomerOptions()
                {
                    VatNumber = customer.Data.VatNumber
                }).SingleOrDefault();

            Assert.NotNull(databaseCustomer);

            Assert.Equal(customer.Data.Email, databaseCustomer.Email);
            Assert.Equal(customer.Data.Firstname, databaseCustomer.Firstname);
            Assert.Equal(customer.Data.Lastname, databaseCustomer.Lastname);
            Assert.Equal(customer.Data.Phone, databaseCustomer.Phone);
            Assert.True(databaseCustomer.Status);
        }
    }
}
