using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public partial class ProductList : IEntity
    {
        public ProductList()
        {
            Products = new HashSet<Product>();
        }

        public int ProductListId { get; set; }
        public string? ProductListName { get; set; }
        public DateTime? CreateDate { get; set; }
        public bool? IsItBuy { get; set; }
        public bool? ShopGo { get; set; }
        public bool? ShopFinish { get; set; }
        public int? UserId { get; set; }

        public virtual User? User { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
