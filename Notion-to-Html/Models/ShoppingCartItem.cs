using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notion_to_Html.Models
{
    internal class ShoppingCartItem
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime Created { get; set; } = DateTime.Now;
        public string ItemName { get; set; }
        public bool Collected { get; set; }

    }

    internal class CreateShoppingCartItem
    {
        public string  ItemName { get; set; }
    }

    internal class UpdateShoppingCart
    {
        public string ItemName { get; set;}
        public bool Collected { get; set; }
    }
}
