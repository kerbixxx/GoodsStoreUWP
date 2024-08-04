using GoodsStoreUWP.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsStoreUWP.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ShopCartItem> ShopCart { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Price).IsRequired();
            });
            // Указываем имя таблицы для ShopCartItem
            modelBuilder.Entity<ShopCartItem>().ToTable("ShopCart");

            // Проверка существования таблицы перед ее созданием
            if (!modelBuilder.Model.GetEntityTypes().Any(t => t.ClrType == typeof(ShopCartItem)))
            {
                modelBuilder.Entity<ShopCartItem>().ToTable("ShopCart");
            }

            // Создание таблицы Products, если она еще не существует
            if (!modelBuilder.Model.GetEntityTypes().Any(t => t.ClrType == typeof(Product)))
            {
                modelBuilder.Entity<Product>().ToTable("Products");
            }
        }
    }
}
