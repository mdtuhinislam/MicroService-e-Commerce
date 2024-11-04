namespace Basket.API.Models
{
    public class ShopingCartItem
    {
        public decimal Quantity { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
    }
}
