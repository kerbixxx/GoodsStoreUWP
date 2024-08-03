using GoodsStoreUWP.Context;
using GoodsStoreUWP.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsStoreUWP.Services
{
    public static class DbInitializerService
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                if (!File.Exists("GoodsStore.db"))
                {
                    try
                    {
                        db.Database.EnsureCreated();
                        SeedAllEntities(db);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error initializing database: {ex.Message}");
                    }
                }
                var list = db.Products.ToList();
            }
        }

        private static void SeedAllEntities(ApplicationDbContext db)
        {
            var products = SeedProducts(db);
        }

        private static List<Product> SeedProducts(ApplicationDbContext db)
        {
            if (db.Products.Any())
            {
                return db.Products.ToList();
            }
            var products = new List<Product>();

            int productsCount = 20;
            Random rnd = new Random();
            for(var i = 0; i < productsCount; i++)
            {
                products.Add(new Product() { Name = "product" + i.ToString(), Price = Convert.ToDecimal(rnd.Next(10, 1000) + 0.99) });
            }
            db.AddRange(products);
            db.SaveChanges();
            return products;
        }
    }
}
