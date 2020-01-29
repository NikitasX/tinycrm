using System;
using System.Collections.Generic;
using System.Linq;
using TinyCrm.Model;
using TinyCrm.Model.Options;

namespace TinyCrm.Services
{
    public class ProductService : IProductService
    {
        private List<Product> ProductsList = new List<Product>();

        public bool AddProduct(AddProductOptions options)
        {
            if (options == null) {
                return false;
            }

            if (string.IsNullOrWhiteSpace(options.Id)) {
                return false;
            }

            var product = GetProductById(options.Id);
            
            if (product != null) {
                return false;
            }

            if (string.IsNullOrWhiteSpace(options.Name)) {
                return false;
            }

            if (options.Price <= 0) {
                return false;
            }

            if (options.ProductCategory == ProductCategory.Invalid) {
                return false;
            }

            product = new Product()
            {
                Id = options.Id,
                Name = options.Name,
                Price = options.Price,
                Category = options.ProductCategory
            };

            ProductsList.Add(product);

            return true;
        }
        public bool UpdateProduct(string productId, 
            UpdateProductOptions options)
        {

            if (options == null) {
                return false;
            }

            if(string.IsNullOrWhiteSpace(productId)) {
                return false;
            }

            var product = GetProductById(productId);

            if (product == null) {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(options.Description)) {
                product.Description = options.Description;
            }

            if(options.Price != null) {
                if(options.Price <= 0) {
                    return false;
                } else {
                    product.Price = options.Price.Value;
                }
            }     
            
            if (options.Discount != null){
                if(options.Discount <= 0) {
                    return false;
                } else {
                    product.Discount = options.Discount.Value;
                }
            }

            return true;
        }
        public Product GetProductById(string productId)
        {
            if(string.IsNullOrWhiteSpace(productId)) {
                return default;
            }
            
            return ProductsList
                .Where(s => s.Id.Equals(productId))
                .SingleOrDefault();
        }
    }
}
