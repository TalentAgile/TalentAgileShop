using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace TalentAgileShop.Model
{
    public static class RepositoryHelpers
    {


        public static List<CartItem> GetCartProducts(this IDataContext context, Cart cart)
        {

            var products = new List<CartItem>();

            foreach (var productInfo in cart.Products)
            {
                var product = context.Products.Include(p => p.Category)
                    .Include(p => p.Origin)
                    .FirstOrDefault(p => p.Id == productInfo.Id);

                if (product == null)
                {
                    continue;
                }

                products.Add(CartItem.Create(product, productInfo.Count));

            }

            return products;
            
        }




    }
}