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
            var test = new CustomerService();

            var temp = new CreateCustomerOptions() {
                Id = 1,
                Email = "test@test.com",
                VatNumber = "test",
                DateCreated = DateTime.Now
            };

            var lalala = test.CreateCustomer(temp);

            var kakaka = new UpdateCustomerOptions()
            {
                Email = "lala@lala.com"
            };

            var okUpdate = test.UpdateCustomer(1, kakaka);

            var ok = test.GetCustomerById(1);

            Console.WriteLine(ok.Email);
            Console.WriteLine(ok.VatNumber);

            Console.ReadLine();
        }
    }
}
