using System;
using System.Collections.Generic;
using System.Linq;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;

namespace TinyCrm.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private static List<Customer> CustomerList = new List<Customer>();

        public bool CreateCustomer(CreateCustomerOptions options)
        {

            if(options == null) {
                return false;
            }

            if(string.IsNullOrWhiteSpace(options.Email) ||
                string.IsNullOrWhiteSpace(options.VatNumber) || 
                options.Id < 0) {
                return false;
            }

            var customer = GetCustomerById(options.Id);

            if(customer != null) {
                return false;
            }

            customer = new Customer()
            {
                Id = options.Id,
                VatNumber = options.VatNumber,
                Email = options.Email,
                Created = DateTime.UtcNow,
                Status = options.Status
            };

            CustomerList.Add(customer);

            return true;
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

            if(options.Firstname != null) {
                customer.Firstname = options.Firstname;
            }            
            
            if(string.IsNullOrWhiteSpace(options.Lastname)) {
                customer.Lastname = options.Lastname;
            }

            if(options.Phone != null) {
                customer.Phone = options.Phone;
            }

            return true;
        }

        public Customer GetCustomerById(int customerId)
        {

            if (customerId < 0) {
                return default;
            }

            return CustomerList
                .Where(s => s.Id == customerId)
                .SingleOrDefault();
        }

        public List<Customer> SearchCustomer(SearchCustomerOptions options)
        {
            if(options == null) {
                return default;
            }

            var customer = CustomerList
                .Where(s => s.Status == 1);

            if(!string.IsNullOrWhiteSpace(options.Email)
                && customer.ToList().Count != 0) {
                customer = customer.Where(s => s.Email.Contains(options.Email));
            }

            if(!string.IsNullOrWhiteSpace(options.VatNumber)
                && customer.ToList().Count != 0) {
                customer = customer.Where(s => s.VatNumber.Contains(options.VatNumber));
            }

            if (options.Created != null 
                && customer.ToList().Count != 0) {
                customer = customer.Where(s => s.Created < options.Created);
            }

            var filteredList = customer.ToList();

            return filteredList;
        }
    }
}
