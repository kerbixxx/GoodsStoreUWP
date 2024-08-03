using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsStoreUWP.Data.Base
{
    public interface IRepository<TEntity, TId>
    {
        List<TEntity> GetAll();
    }
}
