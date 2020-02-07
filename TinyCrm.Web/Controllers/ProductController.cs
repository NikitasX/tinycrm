using System.Linq;
using Autofac;
using Microsoft.AspNetCore.Mvc;
using TinyCrm.Core;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;

namespace TinyCrm.Web.Controllers
{
    public class ProductController : Controller
    {

        public TinyCrmDbContext Context_;
        public IContainer Container_;

        public ProductController()
        {
            Container_ = ServiceRegistrator.GetContainer();
            Context_ = Container_.Resolve<TinyCrmDbContext>();
        }

        public IActionResult Index()
        {
            var productList = Context_.Set<Product>().Take(100).ToList();

            return View(productList);
        }
    }
}