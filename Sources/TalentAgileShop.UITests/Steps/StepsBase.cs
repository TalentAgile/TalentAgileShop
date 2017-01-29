using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Remote;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Tracing;

namespace TalentAgileShop.UITests.Steps
{

    public class StepsBase
    {
        private IWebDriver _driver;

        protected IWebDriver WebDriver => _driver ?? (_driver = CreateWebDriver());


        public string GetTestProperty(string propertyName)
        {
            var context = ScenarioContext.Current["TestContext"] as TestContext;

            var result = context?.Properties[propertyName] as string;
            return result;
        }


        protected IWebDriver CreateWebDriver()
        {
            var driverName = GetTestProperty("webDriver");

            
            if (string.Compare(driverName, "chrome", StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                var options = new ChromeOptions();
                options.AddArgument("-incognito ");
                options.AddArgument("--start-maximized");
                var location = GetTestProperty("chromeDriverLocation");
                var webDriver = new ChromeDriver(location, options);
                return webDriver;
            }
            else if (string.Compare(driverName, "phantomJs", StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                var driver = new PhantomJSDriver();

                driver.Manage().Window.Size = new Size(1200, 1000);
               return driver;
            }
            else
            {
                if (string.IsNullOrEmpty(driverName))
                {
                    throw new InvalidOperationException("Driver name missing: do you have selected a test setting file?");
                }
                throw new InvalidOperationException($"Invalid driver name: {driverName}");
            }
           
        }
    
        protected virtual void AfterScenario()
        {
            if (_driver == null)
            {
                return;
            }

            _driver.Close();
            _driver.Dispose();
            _driver = null;
        }
    }


}