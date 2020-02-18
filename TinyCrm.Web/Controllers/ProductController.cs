using System;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TinyCrm.Core;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;
using TinyCrm.Core.Services;
using TinyCrm.Web.Extensions;

namespace TinyCrm.Web.Controllers
{
    public class ProductController : Controller
    {

        private TinyCrmDbContext Context_ { get; set; }
        private IProductService Products_ { get; set; }

        public ProductController(TinyCrmDbContext context,
            IProductService product)
        {
            Context_ = context;
            Products_ = product;
        }

        public async Task<IActionResult> Index()
        {
            var productList = await Context_
                .Set<Product>()
                .Take(100)
                .ToListAsync();

            return View(productList);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateProduct(
            [FromBody] AddProductOptions options)
        {
            var result = await Products_.AddProduct(options);

            return result.AsStatusResult();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut("product/{id}")]
        public async Task<IActionResult> Put(
            string id, [FromBody] UpdateProductOptions options)
        {
            var result = await Products_.UpdateProduct(id, options);

            return result.AsStatusResult();
        }        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("product/{id}")]
        public async Task<IActionResult> Get(
            string id)
        {
            var result = await Products_.GetProductById(id);

            return result.AsStatusResult();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }        
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Search(Models.SearchProductViewModel model)
        {

            model.SearchOptions.MinPrice = decimal.Parse(model.SearchOptions.MinPrice.ToString());
            model.SearchOptions.MaxPrice = decimal.Parse(model.SearchOptions.MaxPrice.ToString());

            model.SearchOptions.MinDiscount = decimal.Parse(model.SearchOptions.MinDiscount.ToString());
            model.SearchOptions.MaxDiscount = decimal.Parse(model.SearchOptions.MaxDiscount.ToString());

            model.SearchOptions.Category = (ProductCategory)Enum.Parse(
                typeof(ProductCategory), Request.Form["SearchOptions_Category"].ToString());

            model.ProductList = Products_.SearchProduct(model.SearchOptions).ToList();

            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(
            Models.CreateProductViewModel model)
        {

            model.AddOptions.Price = decimal.Parse(model.AddOptions.Price.ToString());
            model.AddOptions.Discount = decimal.Parse(model.AddOptions.Discount.ToString());

            model.AddOptions.Category = (ProductCategory)Enum.Parse(
                typeof(ProductCategory), Request.Form["AddOptions_Category"].ToString());

            var result = await Products_.AddProduct(
                model?.AddOptions);

            if (result.Success == false) {
                model.ErrorText = "Oops. Something went wrong";

                return View(model);
            }

            model.SuccessText = "Product created succesfully!";
            return View(model);
        }
    }
}