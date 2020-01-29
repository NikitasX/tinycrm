using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using TinyCrm.Model;
using TinyCrm.Model.Options;
using TinyCrm.Services;

namespace TinyCrm
{
    class Program
    {
        static void Main(string[] args)
        {
            //Log.Logger = new LoggerConfiguration()
            //    .WriteTo.Console()
            //    .WriteTo.File($@"{System.IO.Directory.GetCurrentDirectory()}\logs\{DateTime.Now:yyyy-MM-dd}\log-.txt",
            //        rollingInterval: RollingInterval.Day)
            //    .CreateLogger();
            //Log.Error("this is an error");
            //Console.ReadKey();

            var productService = new ProductService();

            productService.AddProduct(new AddProductOptions()
            {
                Id = "123",
                Price = 12.33M,
                ProductCategory = ProductCategory.Cameras,
                Name = "Camera 1"
            });            
            
            productService.AddProduct(new AddProductOptions()
            {
                Id = "456",
                Price = 12.33M,
                ProductCategory = ProductCategory.Cameras,
                Name = "Camera 2"
            });
            productService.UpdateProduct("123", new UpdateProductOptions()
            {
                Price = 22.22M
            });
        }
    }
}
