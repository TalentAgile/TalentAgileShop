using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace TalentAgileShop.UITests
{
    [TestClass]
    public class TheSiteShould
    {
        private NavigationPrimitives GivenTheSite { get; set; }

        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInit()
        {
            GivenTheSite = new NavigationPrimitives(TestContext);
            GivenTheSite.InitializeBrowser();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            GivenTheSite.TakeScreenshotIfCurrentTestFailed();
            GivenTheSite.DisposeBrowser();
            GivenTheSite = null;
        }


        [TestMethod]
        public void show_me_the_welcome_text_on_the_homepage()
        {
            GivenTheSite
                .WhenINavigateToTheHomepage()
                .ThenIShouldSeeTheWelcomeText();
        }

        [TestMethod]
        public void show_me_the_catalog_when_I_click_on_the_catalog_link_on_the_homepage()
        {
            GivenTheSite
                .WhenINavigateToTheHomepage()
                .WhenIClickOnCatalog()
                .ThenIShouldSeeTheProductList();
        }

     

        [TestMethod]
        public void show_one_log_when_I_add_a_product_to_the_cart()
        {
            GivenTheSite
                .WhenINavigateToThisProductPage("tshirt-standup-meeting-m")
                .WhenIClickOnAddToCartButton()
                .ThenIShouldSeeThisLog(logCount: 1, log: "Added to cart!");
        }

        [TestMethod]
        public void show_two_logs_when_I_add_a_product_to_the_cart_twice()
        {
            GivenTheSite
                .WhenINavigateToThisProductPage("tshirt-standup-meeting-m")
                .WhenIClickOnAddToCartButton()
                .WhenIClickOnAddToCartButton()
                .ThenIShouldSeeThisLog(logCount: 2, log: "Added to cart!");
        }

        [TestMethod]
        [Ignore]
        public void show_me_the_category_filter_on_the_catalog_page()
        {
            GivenTheSite
                .WhenINavigateToTheCatalogPage()
                .ThenIShouldSeeTheCategoryFilters();
        }


        [TestMethod]
        [Ignore]
        public void let_me_switch_to_the_thumbnail_view_and_back_to_list_view()
        {
            GivenTheSite
                .WhenINavigateToTheCatalogPage()
                .WhenISwitchToThumbnailView()
                .ThenICanSwitchToListView();
        }


        [TestMethod]
        [Ignore]
        public void show_a_price_of_zero_for_an_empty_cart()
        {
            GivenTheSite
                .WhenINavigateToTheCartPage()
                .ThenTheProductCostIs(0)
                .ThenTheDeliveryCostIs(0);
        }



        [TestMethod]
        [Ignore]
        public void show_the_right_price_for_a_M_tshirt()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        [Ignore]
        public void show_the_right_price_for_a_M_tshirt_with_a_FREESMALL_discount()
        {
            throw new NotImplementedException();
        }
    }
}
