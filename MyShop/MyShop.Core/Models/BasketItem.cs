using MyShop.Core.Models;

namespace MyShop.Core
{
    public  class BasketItem : BaseEntity
    {
        public string BasketId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }

    }
}