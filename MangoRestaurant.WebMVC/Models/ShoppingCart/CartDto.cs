﻿namespace MangoRestaurant.WebMVC.Models.ShoppingCart
{
    public class CartDto
    {
        public CartHeaderDto CartHeader { get; set; }
        public IEnumerable<CartDetailsDto>? CartDetails { get; set; }
    }
}
