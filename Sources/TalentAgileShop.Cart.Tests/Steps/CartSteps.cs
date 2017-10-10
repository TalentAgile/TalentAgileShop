using System;
using System.Collections.Generic;
using NFluent;
using TalentAgileShop.Model;
using TechTalk.SpecFlow;

namespace TalentAgileShop.Cart.Tests.Steps
{
    [Binding]
    public class CartSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public CartSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given("a cart")]
        public void GivenACart()
        {
            _scenarioContext["Cart"] = new List<CartItem>();
            _scenarioContext["Code"] = null;
        }


        [Given(@"(.*) (.*) product with a price of (.*)")]
        public void GivenSmallProductWithAPriceOf(int count, ProductSize size,  decimal price)
        {
            var products = _scenarioContext["Cart"] as List<CartItem>;

            products.AddProduct(price,count,size);
        }

        [Given(@"the discountCode (.*)")]
        public void GivenSmallProductWithAPriceOf(string discountCode)
        {
            _scenarioContext["Code"] = discountCode;
        }

        [When(@"I calculate the total price")]
        public void WhenICalculateTheTotalPrice()
        {
            var products = _scenarioContext["Cart"] as List<CartItem>;
            var discountCode = _scenarioContext["Code"] as string;

            var calculator = new CartPriceCalculator();

            var price = calculator.ComputePrice(products, discountCode);

            _scenarioContext["Price"] = price;

        }

        [Then(@"the product price should be (.*)")]
        public void ThenTheProductPriceShouldBe(decimal productsPrice)
        {
            var price = _scenarioContext["Price"] as CartPrice;

            Check.That(price.ProductCost).IsEqualTo(productsPrice);
        }

        [Then(@"the delivery price should be (.*)")]
        public void ThenTheDeliveryPriceShouldBe(decimal deliveryPrice)
        {
            var price = _scenarioContext["Price"] as CartPrice;

            Check.That(price.DeliveryCost).IsEqualTo(deliveryPrice);
        }
 
    }
}
