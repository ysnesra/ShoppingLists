using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ProductListDto : IDto
    {
        public int ProductListId { get; set; }

        public string? ProductListName { get; set; }

        public int? UserId { get; set; }

        public string CreateDate { get; set; }

        public bool? IsItBuy { get; set; } = false;

        public bool? ShopGo { get; set; } = false;

        public bool? ShopFinish { get; set; } = false;

        public int? Amount { get; set; }

        public int? TotalAmount { get; set; }


    }
}
