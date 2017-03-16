using System;
using System.Collections.Generic;
using TalentAgileShop.Model;

namespace TalentAgileShop.Cart
{
    public class CartPriceCalculator : ICartPriceCalculator
    {

        /// <summary>
        /// Compute the cart price.
        /// </summary>
        /// <param name="items">The products in the cart. 
        /// The first element of the tuple is the product itself, the second is the number of times this product is in the cart
        /// </param>
        /// <param name="discountCode">the discount code. NULL if it is not supplied</param>
        /// <returns>
        /// The delivery cost and the product cost. The InvalidDiscountCode property is used when the discountCode is not recognized
        /// </returns>
        public CartPrice ComputePrice(List<CartItem> items, string discountCode)
        {

            var result = new CartPrice
            {
                ProductCost = 0,
                DeliveryCost = 0
            };

            return result;

        }

    }


}