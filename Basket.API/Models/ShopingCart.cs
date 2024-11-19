namespace Basket.API.Models
{
    public class ShoppingCart
    {
        public ShoppingCart(string userName)
        {
            UserName = userName;
        }

        public ShoppingCart()
        {
            
        }
        public string UserName { get; set; }

        public decimal TotalPrice { get 
            {
                decimal _totalPrice = 0;
                foreach (var item in Items) 
                {
                    _totalPrice += item.Price;
                }
                return _totalPrice;
            } 
        }

        public List<ShopingCartItem> Items { get; set; } = new List<ShopingCartItem>();
    }
}
