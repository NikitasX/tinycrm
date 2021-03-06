﻿using System;
using System.IO;
using System.Linq;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using System.Collections.Generic;
using TinyCrm.Core.Model.Options;

namespace TinyCrm.Core.Services
{
    public class ProductService : IProductService
    {
        // Assign the random class to a `Utility` property.
        public static Random GenerateRandomNumber = new Random();

        private readonly TinyCrmDbContext context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctx"></param>
        public ProductService(TinyCrmDbContext ctx) 
        {
            context = ctx 
                ?? throw new ArgumentNullException(nameof(ctx));
        }        
        
        /// <summary>
        /// 
        /// </summary>
        public ProductService() 
        {
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="csvURL"></param>
        /// <returns></returns>
        public Dictionary<string, string> ParseCSV(string csvURL)
        {

            if (string.IsNullOrWhiteSpace(csvURL)) {
                throw new ArgumentNullException($"{nameof(csvURL)} can't be null");
            }

            // Open products.csv and parse its lines to the `Lines` variable
            var Lines = File.ReadAllLines($@"{csvURL}");

            // Create new dictionary for both product properties
            var productPropertyList = new Dictionary<string, string>();

            // Parse each line using the variable `line` to the `filteredProductList` Dictionary
            // This ensures that Dictionary keys are Unique. Keys are also Trimmed to ensure
            // there are no bugs due to spaces.
            foreach (var line in Lines) {

                var productProperties = line.Split(';');

                // Create a random price from 1-1000 with decimals rounded up 
                // to the second decimal number. Run the `randomPrice` variable
                // through the `TwoDecimals` recursive function to ensure 
                // there are 2 decimals
                var randomPrice = (double)GenerateRandomNumber.Next(1, 1000) +
                Math.Round(GenerateRandomNumber.NextDouble(), 2);

                randomPrice = TwoDecimals(randomPrice);

                // Add the values to the `filteredProductList` Dictionary.
                // This ensures that all keys are Unique.
                // Prices are stores after the `||` characters to be retrieved later
                productPropertyList[productProperties[0].ToString().Trim()] =
                productProperties[1].ToString() + "|" + randomPrice.ToString();
            }
            


            return productPropertyList;
        }

        /// <summary>
        /// Recursive function to check if there are two decimals in a random number used
        /// as the product's price. If two decimal numbers aren't present
        /// a new number is generated
        /// </summary>
        /// <param name="randomPrice"></param>
        /// <returns></returns>
        public static double TwoDecimals(double randomPrice)
        {
            var verifyTwoDecimals = randomPrice.ToString().Split('.');

            if (verifyTwoDecimals.ElementAtOrDefault(1) == null) {
                randomPrice = (double)GenerateRandomNumber.Next(1, 1000) +
                Math.Round(GenerateRandomNumber.NextDouble(), 2);

                TwoDecimals(randomPrice);
            }
            return randomPrice;
        }
    }
}
