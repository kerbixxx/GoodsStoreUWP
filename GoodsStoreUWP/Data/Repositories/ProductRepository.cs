using GoodsStoreUWP.Context;
using GoodsStoreUWP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;

namespace GoodsStoreUWP.Data.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public List<Product> GetAll()
        {
            return _db.Products.ToList();
        }

        public void Add(Product product)
        {
            _db.Products.Add(product);
            _db.SaveChanges();
        }

        public Product GetById(int id)
        {
            return _db.Products.Find(id);
        }

        public void Update(Product product)
        {
            _db.Products.Update(product);
            _db.SaveChanges();
        }

        public void Remove(Product product)
        {
            _db.Products.Remove(product);
            _db.SaveChanges();
        }
    }
}
