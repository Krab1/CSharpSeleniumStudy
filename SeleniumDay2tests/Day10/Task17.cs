using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Linq;

namespace SeleniumTests
{
    [TestFixture]
    class Task17 : Base
    {
        [SetUp]
        public void Start()
        {
            StartChrome();
            OpenAdmin();
        }
        [Test]
        public void Test()
        {
            string currwindow = driver.CurrentWindowHandle;
            driver.FindElement(By.XPath("//*/span[contains(., 'Catalog')]")).Click();
            driver.FindElement(By.XPath("//*/tr[@class='row']//a[string-length(text()) > 0 and not(contains(.,'[Root]'))]")).Click();
            var products = driver.FindElements(By.XPath("//*/tr[@class='row']//a[string-length(text()) > 0  and contains(@href, 'product')]"));
            var list = products.Select(x => x.Text).ToArray();
            foreach (var product in list)
            {
                driver.FindElement(By.XPath($"//*/a[contains(., '{product}')]")).Click();
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector("a[href='#tab-general']")));
                var logs = driver.Manage().Logs.GetLog("browser");
                if (logs.Count > 0)
                {
                    foreach(var log in logs)
                    {
                        System.Console.WriteLine($"LogLevel: {log.Level} and Message: {log.Message}, Timestamp: {log.Timestamp}");
                    }
                    throw new Exception($"При открытии страницы с товаром ({product}) найдены ошибки браузера в логе");
                }
                driver.Navigate().Back();
            }
        }
    }
}
