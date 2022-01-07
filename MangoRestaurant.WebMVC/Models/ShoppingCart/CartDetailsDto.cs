using MangoRestaurant.WebMVC.Models.Product;

namespace MangoRestaurant.WebMVC.Models.ShoppingCart
{
    public class CartDetailsDto
    {
        public int Id { get; set; }
        public int CartHeaderId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }

        public CartHeaderDto CartHeader { get; set; }
        public ProductDto Product { get; set; }
    }
}
