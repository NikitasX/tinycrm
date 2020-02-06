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
        /// <summary>
        /// 
        /// </summary>
        private readonly TinyCrmDbContext context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctx"></param>
        public CustomerService(TinyCrmDbContext ctx)
        {
            context = ctx
                ?? throw new ArgumentNullException(nameof(ctx));
        }
        
        /// <summary>
        /// 
        /// </summary>
        public CustomerService()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public Customer CreateCustomer(CreateCustomerOptions options)
        {

            if(options == null) {
                return default;
            }

            if(string.IsNullOrWhiteSpace(options.Email) ||
                string.IsNullOrWhiteSpace(options.VatNumber) ||
                options.VatNumber.Length != 9) {
                return default;
            }

            var exists = SearchCustomer(
                new SearchCustomerOptions()
                {
                    VatNumber = options.VatNumber
                }).Any();

            if(exists) {
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public Customer GetCustomerById(int? customerId)
        {

            if (customerId != null) {
                return default;
            }

            return SearchCustomer(
                new SearchCustomerOptions()
                {
                    Id = customerId
                }).SingleOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public IQueryable<Customer> SearchCustomer(SearchCustomerOptions options)
        {
            
            if(options == null) {
                return default;
            }

            var query = context
                .Set<Customer>()
                .AsQueryable();

            if (options.Id != null) {
                return query
                    .Where(s => s.Id == options.Id);
            }

            if (!string.IsNullOrWhiteSpace(options.VatNumber)) {
                query = query
                    .Where(s => s.VatNumber == options.VatNumber);
            }

            if (!string.IsNullOrWhiteSpace(options.Email)) {
                query = query
                    .Where(s => s.Email == options.Email);
            }

            
            if (options.CreatedFrom != null) {
                query = query
                    .Where(s => s.Created > options.CreatedFrom);
            }            
            
            if (options.CreatedTo != null) {
                query = query
                    .Where(s => s.Created < options.CreatedTo);
            }
            
            return query
                .Take(500);
        }
    }
}
