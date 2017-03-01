using System;
using System.Collections.Generic;
using TalentAgileShop.Model;

namespace TalentAgileShop.Cart
{
    public class CartPriceCalculator: ICartPriceCalculator
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

            var result = new CartPrice();



            result.ProductCost = 0;

            result.DeliveryCost = 0;

            foreach (var product in items)
            {

                if (discountCode == "5BIG" &&
                    (product.Product.Size == ProductSize.Large || product.Product.Size == ProductSize.ExtraLarge))
                {
                    result.ProductCost += product.Product.Price * 0.95m * product.Count;
                }
                else
                {
                    result.ProductCost += product.Product.Price * product.Count;
                }

                switch (product.Product.Size)
                {
                    case ProductSize.Small:

                        if (discountCode == "FREESMALL")
                        {
                            break;
                        }
                        result.DeliveryCost += 5 * product.Count;
                        break;
                    case ProductSize.Medium:
                        if (discountCode == "FREESMALL")
                        {
                            break;
                        }
                        result.DeliveryCost += 5 * product.Count;
                        break;
                    case ProductSize.Large:
                        result.DeliveryCost += 10 * product.Count;
                        break;
                    case ProductSize.ExtraLarge:
                        result.DeliveryCost += 20 * product.Count;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            result.DeliveryCost = Math.Min(50, result.DeliveryCost);
            if (discountCode != null)
            {
                result.InvalidDiscountCode = discountCode != "FREESMALL" && discountCode != "5BIG";
            }

            return result;

        }

    }


}