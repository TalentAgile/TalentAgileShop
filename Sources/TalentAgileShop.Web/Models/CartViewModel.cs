using System;
using System.Collections.Generic;
using TalentAgileShop.Model;

namespace TalentAgileShop.Web.Models
{
    public class CartViewModel
    {
        public List<CartItem> Items { get; }
        public CartPrice Price { get; }

        public string DiscountCode { get; set; }


        public CartViewModel(List<CartItem> items, CartPrice price)
        {
            Items = items;
            Price = price;
        }
    }
}