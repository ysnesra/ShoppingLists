using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public partial class ProductListDetail
    {
        public int ProductId { get; set; }
        public int ProductListId { get; set; }
        public bool? IsBuy { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual ProductList ProductList { get; set; } = null!;
    }
}
