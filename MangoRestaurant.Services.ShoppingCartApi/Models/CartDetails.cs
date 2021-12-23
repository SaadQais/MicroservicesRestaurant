namespace MangoRestaurant.Services.ShoppingCartApi.Models
{
    public class CartDetails
    {
        public int Id { get; set; }
        public int CartHeaderId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }

        public CartHeader CartHeader { get; set; }
        public Product Product { get; set; }
    }
}
