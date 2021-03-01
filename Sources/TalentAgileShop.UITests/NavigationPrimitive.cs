using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.PhantomJS;

namespace TalentAgileShop.UITests
{

    internal sealed class NavigationPrimitives
    {
        private readonly TestContext _context;

        public string SiteUrl { get; }

        public IWebDriver WebDriver { get; private set; }

        private string GetTestProperty(string propertyName)
        {
            var result = _context?.Properties[propertyName] as string;
            return result;
        }


        public void TakeScreenshotIfCurrentTestFailed()
        {
            if (_context.CurrentTestOutcome != UnitTestOutcome.Failed)
            {
                return;
            }
            try
            {
                string fileNameBase =
                    $"error_{_context.TestName}_{DateTime.Now:yyyyMMdd_HHmmss}";
                var resultsDirectory = _context.TestResultsDirectory;

                //if (!Directory.Exists(resultsDirectory))
                //    Directory.CreateDirectory(resultsDirectory);

                var pageSource = WebDriver.PageSource;
                var sourceFilePath = Path.Combine(resultsDirectory, fileNameBase + "_source.html");
                File.WriteAllText(sourceFilePath, pageSource, Encoding.UTF8);
                _context.AddResultFile(sourceFilePath);
            
                var takesScreenshot = WebDriver as ITakesScreenshot;

                if (takesScreenshot == null)
                {
                    return;
                }

                var screenshot = takesScreenshot.GetScreenshot();

                var screenshotFilePath = Path.Combine(resultsDirectory, fileNameBase + "_screenshot.png");

                screenshot.SaveAsFile(screenshotFilePath, ScreenshotImageFormat.Png);
                _context.AddResultFile(screenshotFilePath);
                }
            catch (Exception ex)
            {
                Console.WriteLine("Error while taking screenshot: {0}", ex);
            }
        }

        private IWebDriver CreateWebDriver()
        {
            var headless = GetTestProperty("headless");
            var environment = GetTestProperty("environment");

            var options = new ChromeOptions();
            options.AddArgument("-incognito");        
            if (string.Compare(headless, "true", StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                options.AddArgument("-headless");
            }

            IWebDriver webDriver;
            if (string.Compare(environment, "azure", StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                webDriver = new ChromeDriver(Environment.GetEnvironmentVariable("CHROMEWEBDRIVER"), options);
            }
            else
            {
                webDriver = new ChromeDriver(options);
            }
            
            webDriver.Manage().Window.Size = new Size(1200, 1000);
            return webDriver;      
        }


        public NavigationPrimitives(TestContext context)
        {
            _context = context;

            SiteUrl = GetTestProperty("siteUrl");
        }


        public void InitializeBrowser()
        {
            if (WebDriver == null)
            {
                DisposeBrowser();
            }

            WebDriver = CreateWebDriver();
        }

        public void DisposeBrowser()
        {
            if (WebDriver == null)
            {
                return;
            }

            WebDriver.Close();
            WebDriver.Dispose();
            WebDriver = null;
        }



        public NavigationPrimitives WhenINavigateToTheHomepage()
        {


            WebDriver.Navigate().GoToUrl(SiteUrl);
            return this;
        }

        public NavigationPrimitives WhenINavigateToTheCatalogPage()
        {


            WebDriver.Navigate().GoToUrl(SiteUrl + "/catalog");
            return this;
        }
        public NavigationPrimitives WhenINavigateToTheCartPage()
        {


            WebDriver.Navigate().GoToUrl(SiteUrl + "/cart");
            return this;
        }


        public NavigationPrimitives WhenINavigateToThisProductPage(string id)
        {


            var url = $"{SiteUrl}/products/{id}";

            WebDriver.Navigate().GoToUrl(url);
            return this;
        }



        public NavigationPrimitives WhenIClickOnCatalog()
        {
            var element = WebDriver.FindElement(By.Id("catalogMenuLink"));

            Check.That(element.TagName).IsEqualTo("a");

            element.Click();

            return this;
        }



        public NavigationPrimitives WhenIClickOnAddToCartButton()
        {
            var element = WebDriver.FindElement(By.Id("addToCartButton"));

            Check.That(element.TagName).IsEqualTo("a");

            element.Click();
            return this;
        }

        public NavigationPrimitives WhenISwitchToThumbnailView()
        {
            var element = WebDriver.FindElement(By.Id("goToThumbnailView"));

            Check.That(element.TagName).IsEqualTo("a");

            element.Click();
            return this;
        }


        public NavigationPrimitives WhenISwitchToListView()
        {
            var element = WebDriver.FindElement(By.Id("goToListView"));

            Check.That(element.TagName).IsEqualTo("a");

            element.Click();
            return this;
        }

        public NavigationPrimitives WhenISelectThisDiscountCode(string discountCode)
        {
            var discountCodeTextBox = WebDriver.FindElement(By.Id("discountCode"));

            var discountCodeButton = WebDriver.FindElement(By.Id("changeCodeBtn"));
            discountCodeTextBox.Clear();
            discountCodeTextBox.SendKeys(discountCode);
            discountCodeButton.Click();
            return this;
        }



        public NavigationPrimitives ThenIShouldSeeTheWelcomeText()
        {
            var element = WebDriver.FindElement(By.Id("startShopping"));

            Check.That(element).IsNotNull();
            Check.That(element.TagName).IsEqualTo("a");
            return this;
        }


        public NavigationPrimitives ThenIShouldSeeTheProductList()
        {
            Check.ThatCode(() =>
            {
                WebDriver.FindElement(By.Id("catalogList"));
            }).DoesNotThrow();
            return this;

        }


        public NavigationPrimitives ThenIShouldSeeTheCategoryFilters()
        {
            Check.ThatCode(() =>
            {
                WebDriver.FindElement(By.Id("categories"));
            }).DoesNotThrow();
            return this;

        }


        public NavigationPrimitives ThenIShouldSeeThisLog(int logCount, string log)
        {
            var allLogs = WebDriver
                .FindElements(By.ClassName("alert-success"))
                .ToList();

            var validLogs = allLogs.Where(element => element.Text.Contains(log));

            Check.That(validLogs.Count()).IsEqualTo(logCount);
            return this;
        }


        public NavigationPrimitives ThenICanSwitchToThumbnailView()
        {
            Check.ThatCode(() =>
            {
                WebDriver.FindElement(By.Id("goToThumbnailView"));
            }).DoesNotThrow();
            return this;
        }



        public NavigationPrimitives ThenICanSwitchToListView()
        {
            Check.ThatCode(() =>
            {
                WebDriver.FindElement(By.Id("goToListView"));
            }).DoesNotThrow();
            return this;
        }

        private string FormatCost(string cost)
        {
            return cost;
        }

        /// <summary>
        /// Parse a number from the website format to a decimal
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private decimal ParseCost(string number)
        {
            return decimal.Parse(number, NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US").NumberFormat);
        }


        public NavigationPrimitives ThenTheProductCostIs(decimal expectedProductCost)
        {
          throw new NotImplementedException();
        }

        


        public NavigationPrimitives ThenTheDeliveryCostIs(decimal expectedDeliveryCost)
        {
            throw new NotImplementedException();
        }
    }
}