using System;
using System.Collections.Generic;
using NFluent;
using NUnit.Framework;
using TalentAgileShop.Model;

namespace TalentAgileShop.Cart.Tests
{
    [TestFixture]
    public class CartCalculatorTests
    {
        private CartPriceCalculator _calculator;


        [SetUp]
        public void Setup()
        {
            _calculator = new CartPriceCalculator();
        }


    


        // 1. remove the Ignore attribute and write the algorithm 
        [Test]
        [Ignore("")]
        public void Empty_Cart_Price_Is_Zero()
        {
            var cartItems = new List<CartItem>();


            var actualPrice = _calculator.ComputePrice(cartItems, null);

            Check.That(actualPrice.DeliveryCost).IsEqualTo(0);
            Check.That(actualPrice.ProductCost).IsEqualTo(0);
        }


        // 2. remove the Ignore attribute and write the algorithm 
        [Test]
        [Ignore("")]
        [TestCase(100,1)]
        [TestCase(200, 2)]
        [TestCase(300, 3)]
        [TestCase(400, 4)]
        public void Cart_With_N_Product_Price_Is_Sum_Of_Prices(decimal productPrice,int count)
        {
            var cartItems = new List<CartItem>();
           
            cartItems.AddProduct(productPrice, count);


            var price = _calculator.ComputePrice(cartItems, null);

            Check.That(price.ProductCost).IsEqualTo(count * productPrice);

        }

        [Test]
        [Ignore("")]
        [TestCase(ProductSize.Small,1,5)]
        [TestCase(ProductSize.Medium,1, 5)]
        [TestCase(ProductSize.Large,1,10)]
        [TestCase(ProductSize.ExtraLarge,1, 20)]
        [TestCase(ProductSize.Small, 2, 10)]
        [TestCase(ProductSize.Medium, 2, 10)]
        [TestCase(ProductSize.Large, 2, 20)]
        [TestCase(ProductSize.ExtraLarge, 2, 40)]
        public void Cart_Delivery_Price(ProductSize s,int count, decimal delivery)
        {
            var cartItems = new List<CartItem>();

            cartItems.AddProduct(0, count,s);

            var price = _calculator.ComputePrice(cartItems, null);


            Check.That(price.DeliveryCost).IsEqualTo(delivery);
        }


        // 3. remove the Ignore attribute and write the algorithm 
        [Test]
        [Ignore("")]
        public void Max_Delivery_Price_Is_50()
        {
            var cartItems = new List<CartItem>();

            cartItems.AddProduct(0, 5, ProductSize.ExtraLarge);

            var price = _calculator.ComputePrice(cartItems, null);


            Check.That(price.DeliveryCost).IsEqualTo(50);
        }

        // 4. Write a test for FREESMALL Discount first and then write the algorithm 
        [Test]
        [Ignore("")]
        public void FREESMALL_Discount()
        {
          // 
        }

      
        // 5. Write  test(s) for 5BIG Discount first and the write the algorithm
        

    }
}