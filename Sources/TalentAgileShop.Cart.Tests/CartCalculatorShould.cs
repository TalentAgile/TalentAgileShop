using System;
using System.Collections.Generic;
using NFluent;
using NUnit.Framework;
using TalentAgileShop.Model;

namespace TalentAgileShop.Cart.Tests
{
    [TestFixture]
    public class CartCalculatorShould
    {
        private CartPriceCalculator _calculator;


        [SetUp]
        public void Setup()
        {
            _calculator = new CartPriceCalculator();
        }





        // 1. remove the Ignore attribute and write the algorithm 
        [Test]

        public void return_zero_when_the_cart_is_empty()
        {
            var cartItems = new List<CartItem>();


            var actualPrice = _calculator.ComputePrice(cartItems, null);

            Check.That(actualPrice.DeliveryCost).IsEqualTo(0);
            Check.That(actualPrice.ProductCost).IsEqualTo(0);
        }


        // 2. remove the Ignore attribute and write the algorithm 
        [Test]
        [Ignore("")]
        [TestCase(100, 1, 100)]
        //[TestCase(100, 2, 200)]
        //[TestCase(100, 3, 300)]
        //[TestCase(100, 4, 400)]
        public void return_total_price_given_the_product_price_and_quantity_of_articles(decimal productPrice, int quantityOfArticles, decimal totalPrice)
        {
            var cartItems = new List<CartItem>();

            cartItems.AddProduct(productPrice, quantityOfArticles);


            var price = _calculator.ComputePrice(cartItems, null);

            Check.That(price.ProductCost).IsEqualTo(totalPrice);

        }

        [Test]
        [Ignore("")]
        [TestCase(ProductSize.Small, 1, 5)]
        //[TestCase(ProductSize.Medium, 1, 5)]
        //[TestCase(ProductSize.Large, 1, 10)]
        //[TestCase(ProductSize.ExtraLarge, 1, 20)]
        //[TestCase(ProductSize.Small, 2, 10)]
        //[TestCase(ProductSize.Medium, 2, 10)]
        //[TestCase(ProductSize.Large, 2, 20)]
        //[TestCase(ProductSize.ExtraLarge, 2, 40)]
        public void return_delivery_price_given_the_product_size_and_the_quantity_of_articles(ProductSize productSize, int quantityOfArticles, decimal deliveryPrice)
        {
            var cartItems = new List<CartItem>();
            var productPrice = 0;
            cartItems.AddProduct(productPrice, quantityOfArticles, productSize);

            var price = _calculator.ComputePrice(cartItems, null);


            Check.That(price.DeliveryCost).IsEqualTo(deliveryPrice);
        }


        // 3. remove the Ignore attribute and write the algorithm 
        [Test]
        [Ignore("")]
        public void return_50_as_maximun_delivery_price()
        {
            var cartItems = new List<CartItem>();
            var productPrice = 0;
            var quantityOfArticles = 5;
            cartItems.AddProduct(productPrice, quantityOfArticles, ProductSize.ExtraLarge);
            string discount = null;
            var price = _calculator.ComputePrice(cartItems, discount);


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