using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public partial class User : IEntity
    {
        public User()
        {
            ProductLists = new HashSet<ProductList>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public string? Role { get; set; } = "user";

        public virtual ICollection<ProductList> ProductLists { get; set; }
     

    }
}
