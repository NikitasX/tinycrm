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
    public partial class CustomerServiceTests
    {
        private readonly Core.Services.ICustomerService csvc_;

        public CustomerServiceTests()
        {
            csvc_ = new Core.Services.CustomerService(
                new TinyCrmDbContext());
        }

        [Fact]
        public int Add_Customer_Contacts ()
        {
            using (var context = new TinyCrmDbContext()) {

                var customer = new Customer()
                {
                    VatNumber = "117003949",
                    Email = "dpnevmatikos@codehub.gr",
                    Status = true
                };

                customer.ContactPeople.Add(new ContactPerson {
                    Lastname = "Kopsidis",
                    Email = "kopsidis@kopsidis.gr",
                    Firstname = "Giwrgos",
                    Phone = "+306988423433",
                    Position = ContactPersonPositions.Developer
                });

                context.Add(customer);
                context.SaveChanges();

                return customer.Id;
            }
        }

        [Fact]
        public void Retrieve_Contacts()
        {
            var customerId = Add_Customer_Contacts();
            using (var context = new TinyCrmDbContext()) {
                var contactPeople = context
                    .Set<ContactPerson>()
                    .Where(c => c.Customer.Id == customerId)
                    .ToList();
            }
        }

        [Fact]
        public void Customer_Order_Success()
        {
            using (var context = new TinyCrmDbContext()) {
                var customer = new Customer() { 
                    VatNumber = "117003949",
                    Email = "dpnevmatikos@codehub.gr",
                    Status = true
                };

                customer.Orders.Add(
                    new Order()
                    {
                        DeliveryAdress = "Kleeman Kilkis",
                        Amount = 999,
                        Status = OrderStatus.Pending
                    });
                context.Add(customer);
                context.SaveChanges();
            }
        }

        [Fact]
        public void Orders_Retrieve()
        {
            using (var context = new TinyCrmDbContext()) {
                var orders = context.Set<Order>()
                    .Include(o => o.Customer)
                    .ToList();
            };
        }

        [Fact]
        public void Customer_Order_Retrieve()
        {
            using (var context = new TinyCrmDbContext()) {
                var customer = context
                    .Set<Customer>()
                    .Include(c => c.Orders)
                    .Where(c => c.VatNumber == "117003949")
                    .FirstOrDefault();

                Assert.NotNull(customer);
            }
        }

        [Fact]
        public void CreateCustomer_Success()
        {
            var customerOptions = new CreateCustomerOptions()
            {
                Email = "papaki@papaki.gr",
                Firstname = "Lakis",
                Lastname = "Papakis",
                Phone = "+3928282828",
                VatNumber = "832832272"
            };

            Assert.NotNull(csvc_.CreateCustomer(customerOptions));

            ///Continue Create customer success
        }
    }
}
