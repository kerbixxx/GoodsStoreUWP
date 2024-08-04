using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsStoreUWP.Data.Repositories
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        void Add(T item);
        T GetById(int id);
        void Remove(T item);
    }
}
