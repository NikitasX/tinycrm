using System;
using System.Collections.Generic;
using System.Linq;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;

namespace TinyCrm.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly TinyCrmDbContext context;

        public ProductService(TinyCrmDbContext ctx) 
        {
            context = ctx 
                ?? throw new ArgumentNullException(nameof(ctx));
        }        
        
        public ProductService() 
        {
        }

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

            if (options.Category == ProductCategory.Invalid) {
                return false;
            }

            product = new Product()
            {
                Id = options.Id,
                Name = options.Name,
                Price = options.Price,
                Discount = options.Discount,
                Description = options.Description,
                Category = options.Category
            };

            context.Add(product);

            var success = false;

            try {
                success = context.SaveChanges() > 0;
            } catch (Exception e) {
                //
            }

            return success;
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

            if (!string.IsNullOrWhiteSpace(options.Name)) {
                product.Name = options.Name;
            }

            if (options.Price > 0) {
                product.Price = options.Price;
            }
            
            if(options.Discount > 0) {
                product.Discount = options.Discount;
            }

            if (!string.IsNullOrWhiteSpace(options.Description)) {
                product.Description = options.Description;
            }

            if(options.Category != ProductCategory.Invalid) {
                product.Category = options.Category;
            }

            context.Update(product);

            var success = false;

            try {
                success = context.SaveChanges() > 0;
            } catch (Exception e) {
                //
            }

            return success;
        }

        public List<Product> SearchProduct(SearchProductOptions options)
        {
            if (options == null) {
                return default;
            }

            var productList = context.Set<Product>()
                .ToList();

            if (!string.IsNullOrWhiteSpace(options.Id)) {
                productList = productList
                    .Where(p => p.Id == options.Id)
                    .ToList();

                return productList;
            }

            if (!string.IsNullOrWhiteSpace(options.Name)) {
                productList = productList
                    .Where(p => p.Name.Contains(options.Name))
                    .ToList();
            }

            if (!string.IsNullOrWhiteSpace(options.Description)) {
                productList = productList
                    .Where(p => p.Description.Contains(options.Description))
                    .ToList();
            }

            if (options.Category != ProductCategory.Invalid) {
                productList = productList
                    .Where(p => p.Category == options.Category)
                    .ToList();
            }

            if (options.MinPrice != 0) {
                productList = productList
                    .Where(p => p.Price > options.MinPrice)
                    .ToList();
            }

            if (options.MaxPrice != 0) {
                productList = productList
                    .Where(p => p.Price < options.MaxPrice)
                    .ToList();
            }

            if (options.MinDiscount != 0) {
                productList = productList
                    .Where(p => p.Discount > options.MinDiscount)
                    .ToList();
            }

            if (options.MaxDiscount != 0) {
                productList = productList
                    .Where(p => p.Discount < options.MaxDiscount)
                    .ToList();
            }

            return productList;
        }

        public Product GetProductById(string productId)
        {
            if(string.IsNullOrWhiteSpace(productId)) {
                return default;
            }
            
            return SearchProduct(
                new SearchProductOptions() { 
                    Id = productId
                }
            ).SingleOrDefault();
        }
    }
}
