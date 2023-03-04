using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public partial class Product :IEntity
    {
        public Product()
        {
            ProductLists = new HashSet<ProductList>();
        }

        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public short? UnitInStock { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public int? CategoryId { get; set; }

        public virtual Category? Category { get; set; }

        public virtual ICollection<ProductList> ProductLists { get; set; }
    }
}




