using System;
using System.Collections.Generic;
using TalentAgileShop.Model;

namespace TalentAgileShop.Cart.Tests
{
    public static class CartHelper
    {
        public static void AddProduct(this List<CartItem> products, decimal price, int count = 1, ProductSize size = ProductSize.Small, string category = "No category", string country = "Nowhere")
        {
            var p = new Product()
            {
                Category = new Category() { Name = category },
                Description = null,
                Id = Guid.NewGuid().ToString(),
                Price = price,
                Image = null,
                Name = "No name",
                Origin = new Country() { Name = country },
                Size = size
            };
            products.Add(CartItem.Create(p, count));
        }
    }
}