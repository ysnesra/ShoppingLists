using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ProductListsUsersProductsDto : IDto
    {
        public int UserId { get; set; }
        public int ProductListId { get; set; }
        public int ProductId { get; set; }

        public int CategoryId { get; set; }

        public string? ProductListName { get; set; }

        public string? ProductName { get; set; }

        public string? ImageUrl { get; set; } = "no_image.jpg";

        public string? Description { get; set; }
      
        public string CategoryName { get; set; }

    }
   
}
