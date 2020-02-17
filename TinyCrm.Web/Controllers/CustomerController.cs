using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Services;

namespace TinyCrm.Web.Controllers
{
    public class CustomerController : Controller
    {
        private TinyCrmDbContext Context_;
        private ICustomerService customers_;

        public CustomerController(
            TinyCrmDbContext context, 
            ICustomerService customers)
        {
            Context_ = context;
            customers_ = customers;
        }

        public async Task<IActionResult> Index()
        {

            var customerList = await Context_
                .Set<Customer>()
                .Take(100)
                .ToListAsync();

            return View(customerList);
        }
        
        public async Task<IActionResult> List()
        {
            var customerList = await Context_
                .Set<Customer>()
                .Take(100)
                .ToListAsync();

            return View("Index", customerList);
        }


        [HttpGet]
        public async Task<IActionResult> Search()
        {
            var model = new Models.SearchCustomerViewModel()
            {
                CountryList = await Context_
                     .Set<Country>()
                     .ToListAsync()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Search(Models.SearchCustomerViewModel model)
        {

            model.SearchOptions.Country = new Country() {
                CountryId = Request.Form["SearchOptions_Country"].ToString()
            };

            model.CustomerList = customers_.SearchCustomer(model.SearchOptions).ToList();


            model.CountryList = await Context_
                 .Set<Country>()
                 .ToListAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            var model = new Models.CreateCustomerViewModel()
            {
                CountryList = await Context_
                .Set<Country>()
                .ToListAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            Models.CreateCustomerViewModel model)
        {

            model.CountryList = await Context_
                .Set<Country>()
                .ToListAsync();


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