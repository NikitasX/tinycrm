using System.Linq;
using Autofac;
using Microsoft.AspNetCore.Mvc;
using TinyCrm.Core;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;

namespace TinyCrm.Web.Controllers
{
    public class CustomerController : Controller
    {
        public TinyCrmDbContext Context_;
        public IContainer Container_;

        public CustomerController ()
        {
            Container_ = ServiceRegistrator.GetContainer();
            Context_ = Container_.Resolve<TinyCrmDbContext>();
        }

        public IActionResult Index()
        {

            var customerList = Context_.Set<Customer>().Take(100).ToList();

            return View(customerList);
        }
    }
}