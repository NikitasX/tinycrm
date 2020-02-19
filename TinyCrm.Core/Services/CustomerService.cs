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

            var country = default(Country);

            if(options.Country != null) {
                country = await context
                    .Set<Country>()
                    .Where(c => c.CountryId == options.Country.CountryId)
                    .SingleOrDefaultAsync();
            }

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
                    $"Something went wrong {e}");
            }

            if(success == true) {
                return ApiResult<Customer>.CreateSuccess(customer);
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
        public async Task<ApiResult<Customer>> UpdateCustomer(
            int customerId, UpdateCustomerOptions options)
        {
            if (options == null ||
                customerId < 0) {
                return new ApiResult<Customer>(
                    StatusCode.BadRequest,
                    $"Null {options} or wrong {customerId}");
            }

            var customer = await GetCustomerById(customerId);

            if(customer == null) {
                return new ApiResult<Customer>(
                    StatusCode.NotFound,
                    $"{customerId} not found in database");
            }

            if(!string.IsNullOrWhiteSpace(options.Email)) {
                customer.Data.Email = options.Email;
            }            

            if(!string.IsNullOrWhiteSpace(options.VatNumber)) {
                customer.Data.VatNumber = options.VatNumber;
            }

            if(options.Status != null) {
                customer.Data.Status = options.Status;
            }

            if(!string.IsNullOrWhiteSpace(options.Firstname)) {
                customer.Data.Firstname = options.Firstname;
            }            
            
            if(!string.IsNullOrWhiteSpace(options.Lastname)) {
                customer.Data.Lastname = options.Lastname;
            }

            if(!string.IsNullOrWhiteSpace(options.Phone)) {
                customer.Data.Phone = options.Phone;
            }            
            
            if(options.Country != null) {
                customer.Data.Country = options.Country;
            }

            context.Update(customer.Data);

            var success = false;

            try {
                success = await context.SaveChangesAsync() > 0;
            } catch (Exception e) {
                return new ApiResult<Customer>(
                    StatusCode.InternalServerError,
                    $"Something went wrong {e}");
            }

            return ApiResult<Customer>.CreateSuccess(customer.Data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public async Task<ApiResult<Customer>> GetCustomerById(int? customerId)
        {

            if (customerId == null) {
                return new ApiResult<Customer>(StatusCode.BadRequest, $"Null {customerId}");
            }

            var customer = await context
                .Set<Customer>()
                .SingleOrDefaultAsync(p => p.Id == customerId);

            if (customer == null) {
                return new ApiResult<Customer>(StatusCode.NotFound, $"Customer not found in database");
            }

            return ApiResult<Customer>.CreateSuccess(customer);
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
