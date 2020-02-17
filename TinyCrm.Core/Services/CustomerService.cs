using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
        public async Task<ApiResult<Customer>> CreateCustomer(CreateCustomerOptions options)
        {

            if (options == null) {
                return new ApiResult<Customer>(
                    StatusCode.BadRequest, 
                    $"Null {options}");
            }

            if(string.IsNullOrWhiteSpace(options.Email)) {
                return new ApiResult<Customer>(
                    StatusCode.BadRequest, 
                    "Null or Whitespace Email");
            }

            if (string.IsNullOrWhiteSpace(options.VatNumber)) {
                return new ApiResult<Customer>(
                    StatusCode.BadRequest, 
                    "Null or Whitespace VatNumber");
            }

            if (options.VatNumber.Length != 9) {
                return new ApiResult<Customer>(
                    StatusCode.BadRequest, 
                    "VatNumber is not 9 numbers");
            }

            var exists = SearchCustomer(
                new SearchCustomerOptions()
                {
                    VatNumber = options.VatNumber
                }).Any();

            if(exists) {
                return new ApiResult<Customer>(
                    StatusCode.Conflict, 
                    "Vat already exists in database");
            }

            var country = await context
                .Set<Country>()
                .Where(c => c.CountryId == options.Country.CountryId)
                .SingleOrDefaultAsync();

            var customer = new Customer()
            {
                VatNumber = options.VatNumber,
                Phone = options.Phone,
                Email = options.Email,
                Firstname = options.Firstname,
                Lastname = options.Lastname,
                Created = DateTime.UtcNow,
                Status = true,
                Country = country
            };

            context.Add(customer);

            var success = false;

            try {
                success = await context.SaveChangesAsync() > 0;
            } catch (Exception e) {
                return new ApiResult<Customer>(
                    StatusCode.InternalServerError, 
                    "Could not save customer");
            }

            if(success == true) {
                return new ApiResult<Customer>() {
                    ErrorCode = StatusCode.Ok,
                    ErrorText = "Customer added succesfully",
                    Data = customer
                };
            } else {
                return new ApiResult<Customer>(
                    StatusCode.InternalServerError, 
                    "Something went wrong");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<bool> UpdateCustomer(int customerId, UpdateCustomerOptions options)
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
            
            if(options.Country != null) {
                customer.Country = options.Country;
            }

            context.Update(customer);

            var success = false;

            try {
                success = await context.SaveChangesAsync() > 0;
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

            if(options.Country != null) {
                query = query
                    .Where(s => s.Country == options.Country);
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
