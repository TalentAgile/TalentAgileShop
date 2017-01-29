using System;
using System.Globalization;
using System.Linq;
using NFluent;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace TalentAgileShop.UITests.Steps
{
    [Binding]
    public  partial class Steps: StepsBase
    {

        [AfterScenario()]
        protected override void AfterScenario()
        {
            base.AfterScenario();            
        }


        [Given(@"I navigate to the homepage")]
        public void GivenINavigateToTheHomepage()
        {
            var url = GetTestProperty("siteUrl");

            WebDriver.Navigate().GoToUrl(url);
        }

        [Given(@"I navigate to the cart page")]
        public void GivenINavigateToTheCartPage()
        {
            var url = GetTestProperty("siteUrl");

            WebDriver.Navigate().GoToUrl(url+"/cart");
        }

        [Given(@"I navigate to the product page with the id '(.*)'")]
        public void GivenAProductPage(string id)
        {
            var siteUrl = GetTestProperty("siteUrl");

            var url = $"{siteUrl}/products/{id}";

            WebDriver.Navigate().GoToUrl(url);
        }

        [Given(@"I click on the catalog button on the menu")]
        // ReSharper disable once InconsistentNaming
        public void GivenIAClickOnCatalog()
        {
            var element = WebDriver.FindElement(By.Id("catalogMenuLink"));
           
            Check.That(element.TagName).IsEqualTo("a");

            element.Click();


        }

        [Given(@"I click on the add to cart button")]
        // ReSharper disable once InconsistentNaming
        public void GivenIAClickOnAddToCartButton()
        {
            var element = WebDriver.FindElement(By.Id("addToCartButton"));

            Check.That(element.TagName).IsEqualTo("a");

            element.Click();
        }



        [Given(@"I switch on the thumbnail view")]
        public void GivenIWitchToThumbnailView()
        {
            var element = WebDriver.FindElement(By.Id("goToThumbnailView"));
           
            Check.That(element.TagName).IsEqualTo("a");

            element.Click();
        }

        [Given(@"I switch on the list view")]
        public void GivenIWitchToListView()
        {
            var element = WebDriver.FindElement(By.Id("goToListView"));

            Check.That(element.TagName).IsEqualTo("a");

            element.Click();
        }


        [Given(@"I enter the discount code '(.*)'")]
        public void GivenISelectThisDiscountCode(string discountCode)
        {
            var discountCodeTextBox = WebDriver.FindElement(By.Id("discountCode"));

            var discountCodeButton = WebDriver.FindElement(By.Id("changeCodeBtn"));
            discountCodeTextBox.Clear();
            discountCodeTextBox.SendKeys(discountCode);
            discountCodeButton.Click();
        }

        [Then(@"I should see the welcome text")]
        public void ThenIShouldSeeTheWelcomeText()
        {
            var element = WebDriver.FindElement(By.Id("startShopping"));

            Check.That(element).IsNotNull();
            Check.That(element.TagName).IsEqualTo("a");
        }

        [Then(@"I should see the product list")]
        public void ThenIShouldSeeTheProductList()
        {
            Check.ThatCode(() =>
            {
                WebDriver.FindElement(By.Id("catalogList"));
            }).DoesNotThrow();

        }

        [Then(@"I should see the category filters")]
        public void ThenIShouldSeeTheCategoryFilters()
        {
            Check.ThatCode(() =>
            {
                WebDriver.FindElement(By.Id("categories"));
            }).DoesNotThrow();

        }

        [Then(@"There is (\d+) log '(.+)'")]
        public void ThenIShouldSeeTheCategoryFilters(int logCount, string log)
        {
            var allLogs = WebDriver
                .FindElements(By.ClassName("alert-success"))
                .ToList();

            var validLogs = allLogs.Where(element => element.Text.Contains(log));

            Check.That(validLogs.Count()).IsEqualTo(logCount);
        }

        [Then(@"I can switch to thumbnail view")]
        public void ThenICanSwitchToThumbnailView()
        {
            Check.ThatCode(() =>
            {
                WebDriver.FindElement(By.Id("goToThumbnailView"));
            }).DoesNotThrow();
        }


        [Then(@"I can switch to list view")]
        public void ThenICanSwitchToListView()
        {
            Check.ThatCode(() =>
            {
                WebDriver.FindElement(By.Id("goToListView"));
            }).DoesNotThrow();
        }

        private string FormatCost(string cost)
        {
            return cost.Replace(",", ".");
        }

        [Then(@"The product cost is (.+) €")]
        public void ThenTheProductCost(decimal expectedProductCost)
        {
            var productCostElement = WebDriver.FindElement(By.Id("productCost"));
            
            var cost = decimal.Parse(FormatCost(productCostElement.Text),NumberStyles.AllowDecimalPoint);

            Check.That(cost).IsEqualTo(expectedProductCost);
        }

        [Then(@"The delivery cost is (.+) €")]
        public void ThenTheDeliveryCost(decimal expectedDeliveryCost)
        {
            var productCostElement = WebDriver.FindElement(By.Id("deliveryCost"));

            var cost = decimal.Parse(FormatCost(productCostElement.Text), NumberStyles.AllowDecimalPoint);

            Check.That(cost).IsEqualTo(expectedDeliveryCost);
        }
    }
}
