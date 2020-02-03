using System;
using System.Linq;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;

namespace TinyCrm
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new TinyCrmDbContext();

            Console.WriteLine(context.Database.CanConnect());

            context.Database.EnsureCreated();

            var products = context.Set<Product>()
                .Where(p => p.Price > 100)
                .ToList();

            Console.ReadKey();
        }
    }
}
