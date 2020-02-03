using System;
using TinyCrm.Model;
using TinyCrm.Model.Options;
using TinyCrm.Services;

namespace TinyCrm
{
    class Program
    {
        static void Main(string[] args)
        {
            var customerService = new CustomerService();

            var createOptions = new CreateCustomerOptions() {
                Id = 1,
                Email = "test@test.com",
                VatNumber = "test",
                DateCreated = DateTime.Now,
                Status = 15
            };

            var createCustomer = customerService.CreateCustomer(createOptions);

            var updateCustomer = new UpdateCustomerOptions()
            {
                Email = "lala@lala.com"
            };

            var okUpdate = customerService.UpdateCustomer(1, updateCustomer);

            var customerById = customerService.GetCustomerById(1);

            Console.WriteLine(customerById.Email);
            Console.WriteLine(customerById.VatNumber);
            Console.WriteLine(customerById.Status);

            var searchOptions = new SearchCustomerOptions()
            {
                Email = "lala"
            };

            var search = customerService.SearchCustomer(searchOptions);

            Console.WriteLine(search.Count);

            foreach(var c in search) {
                Console.WriteLine(c.Email);
            }

            Console.ReadLine();
        }
    }
}
