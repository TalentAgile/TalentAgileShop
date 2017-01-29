using System;
using System.Collections.Generic;

namespace TalentAgileShop.Model
{
    public interface ICartPriceCalculator
    {
        CartPrice ComputePrice(List<CartItem> items,string discountCode);
    }

    public class CartItem
    {
        public Product Product { get; set; }

        public int Count { get; set; }

        public static CartItem Create(Product product, int count)
        {
            return new CartItem()
            {
                Product = product,
                Count = count
            };

        }
    }
}