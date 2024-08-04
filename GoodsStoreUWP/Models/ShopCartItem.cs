using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsStoreUWP.Models
{
    public class ShopCartItem
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        [Range(1,int.MaxValue)]
        public int Quantity { get; set; }
        public virtual Product Product { get; set; }
    }
}
