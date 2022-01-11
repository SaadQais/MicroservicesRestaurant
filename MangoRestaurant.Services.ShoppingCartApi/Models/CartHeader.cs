namespace MangoRestaurant.Services.ShoppingCartApi.Models
{
    public class CartHeader
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string? CouponCode { get; set; }
    }
}
