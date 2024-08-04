using GoodsStoreUWP.Context;
using GoodsStoreUWP.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GoodsStoreUWP.Data.Repositories
{
    public class ShopCartRepository : IRepository<ShopCartItem>
    {
        private readonly ApplicationDbContext _db;
        public ShopCartRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<ShopCartItem> GetAll()
        {
            return _db.ShopCart.Include(c=>c.Product).ToList();
        }

        public void Add(ShopCartItem obj)
        {
            var item = _db.ShopCart.FirstOrDefault(sc=>sc.ProductId == obj.ProductId);
            if (item == null)
            {
                _db.ShopCart.Add(obj);
            }
            else
            {
                item.Quantity++;
                _db.ShopCart.Update(item);
            }
            _db.SaveChanges();
        }

        public ShopCartItem GetById(int id)
        {
            return _db.ShopCart.Find(id);
        }

        public void Update(ShopCartItem obj)
        {
            _db.ShopCart.Update(obj);
            _db.SaveChanges();
        }

        public void Remove(ShopCartItem item)
        {
            _db.ShopCart.Remove(item);
            _db.SaveChanges();
        }
    }
}
