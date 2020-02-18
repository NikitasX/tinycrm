using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;
using TinyCrm.Core.Services;
using TinyCrm.Web.Extensions;

namespace TinyCrm.Web.Controllers
{
    public class CustomerController : Controller
    {
        private TinyCrmDbContext context_;
        private ICustomerService customers_;

        public CustomerController(
            TinyCrmDbContext context, 
            ICustomerService customers)
        {
            context_ = context;
            customers_ = customers;
        }

        public async Task<IActionResult> Index()
        {
            var customerList = await context_
                .Set<Customer>()
                .Take(100)
                .ToListAsync();

            return View(customerList);
        }
        
        public async Task<IActionResult> List()
        {
            var customerList = await context_
                .Set<Customer>()
                .Take(100)
                .ToListAsync();

            return View("Index", customerList);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(
           [FromBody] CreateCustomerOptions options)
        {
            var result = await customers_.CreateCustomer(options);

            return result.AsStatusResult();
        }

        [HttpGet]
        public async Task<IActionResult> Search()
        {
            var model = new Models.SearchCustomerViewModel()
            {
                CountryList = await context_
                     .Set<Country>()
                     .ToListAsync()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Search(
            Models.SearchCustomerViewModel model)
        {
            model.SearchOptions.Country = new Country() {
                CountryId = Request.Form["SearchOptions_Country"].ToString()
            };

            model.CustomerList = customers_.SearchCustomer(
                model.SearchOptions)
                .ToList();

            model.CountryList = await context_
                 .Set<Country>()
                 .ToListAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new Models.CreateCustomerViewModel()
            {
                CountryList = await context_
                .Set<Country>()
                .ToListAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            Models.CreateCustomerViewModel model)
        {
            model.CountryList = await context_
                .Set<Country>()
                .ToListAsync();

            // svisimo ola ta Request.Form
            model.CreateOptions.Country = new Country()
            {
                CountryId = Request.Form["CreateOptions_Country"]
            };

            var result = await customers_.CreateCustomer(
                model?.CreateOptions);

            if (result == null) {
                model.ErrorText = "Oops. Something went wrong";

                return View(model);
            }

            model.SuccessText = "Customer created succesfully!";
            return View(model);
        }
    }
}