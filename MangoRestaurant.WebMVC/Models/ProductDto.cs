﻿using System.ComponentModel.DataAnnotations;

namespace MangoRestaurant.WebMVC.Models
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string ImageUrl { get; set; }

        [Range(1, 100)]
        public int Count { get; set; } = 1;
    }
}
