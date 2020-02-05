using System;
using System.Collections.Generic;
using System.Linq;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;

namespace TinyCrm.Core.Services
{
    public class CustomerService : ICustomerService
    {

        private readonly TinyCrmDbContext context;

        public CustomerService(TinyCrmDbContext ctx)
        {
            context = ctx
                ?? throw new ArgumentNullException(nameof(ctx));
        }

        public CustomerService()
        {
        }

        public Customer CreateCustomer(CreateCustomerOptions options)
        {

            if(options == null) {
                return default;
            }

            if(string.IsNullOrWhiteSpace(options.Email) ||
                string.IsNullOrWhiteSpace(options.VatNumber) ||
                string.IsNullOrWhiteSpace(options.Lastname)) {
                return default;
            }

            var customer = new Customer()
            {
                VatNumber = options.VatNumber,
                Phone = options.Phone,
                Email = options.Email,
                Firstname = options.Firstname,
                Lastname = options.Lastname,
                Created = DateTime.UtcNow,
                Status = true
            };

            context.Add(customer);

            var success = false;

            try {
                success = context.SaveChanges() > 0;
            } catch (Exception e) {
                // Log
            }

            if(success == true) {
                return customer;
            } else {
                return default;
            }
        }

        public bool UpdateCustomer(int customerId, UpdateCustomerOptions options)
        {
            if (options == null ||
                customerId < 0) {
                return false;
            }

            var customer = GetCustomerById(customerId);

            if(customer == null) {
                return false;
            }

            if(!string.IsNullOrWhiteSpace(options.Email)) {
                customer.Email = options.Email;
            }            

            if(!string.IsNullOrWhiteSpace(options.VatNumber)) {
                customer.VatNumber = options.VatNumber;
            }

            if(options.Status != null) {
                customer.Status = options.Status;
            }

            if(!string.IsNullOrWhiteSpace(options.Firstname)) {
                customer.Firstname = options.Firstname;
            }            
            
            if(!string.IsNullOrWhiteSpace(options.Lastname)) {
                customer.Lastname = options.Lastname;
            }

            if(!string.IsNullOrWhiteSpace(options.Phone)) {
                customer.Phone = options.Phone;
            }

            context.Update(customer);

            var success = false;

            try {
                success = context.SaveChanges() > 0;
            } catch (Exception e) {
                //
            }

            return success;
        }

        public Customer GetCustomerById(int customerId)
        {

            if (customerId < 0) {
                return default;
            }

            return SearchCustomer(
                new SearchCustomerOptions() { 
                    Id = customerId
                }
            ).SingleOrDefault();
        }

        public List<Customer> SearchCustomer(SearchCustomerOptions options)
        {
            
            if(options == null) {
                return default;
            }

            var customerList = context.Set<Customer>()
                .Where(s => s.Status == true)
                .ToList();

            if(options.Id != 0) {
                return customerList
                    .Where(s => s.Id == options.Id)
                    .ToList();
            }

            if(!string.IsNullOrWhiteSpace(options.Email)) {
                customerList = customerList
                    .Where(s => s.Email.Contains(options.Email))
                    .ToList();
            }

            if(!string.IsNullOrWhiteSpace(options.VatNumber)) {
                customerList = customerList
                    .Where(s => s.VatNumber.Contains(options.VatNumber))
                    .ToList();
            }

            if (options.CreatedFrom != null) {
                customerList = customerList
                    .Where(s => s.Created > options.CreatedFrom)
                    .ToList();
            }            
            
            if (options.CreatedTo != null) {
                customerList = customerList
                    .Where(s => s.Created < options.CreatedTo)
                    .ToList();
            }

            return customerList;
        }
    }
}
