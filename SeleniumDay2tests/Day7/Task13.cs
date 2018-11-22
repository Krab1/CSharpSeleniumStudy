using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace SeleniumTests
{
    [TestFixture]
    class Task13 : Base
    {
        [SetUp]
        public void Start()
        {
            StartChrome();
            OpenMainSite();
        }
        [Test]
        public void Test()
        {
            for (int i = 1; i < 4; i++)
            {
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Id("box-account-login")));

                var products = driver.FindElements(By.CssSelector("li[class=\"product column shadow hover-light\"]"));
                products[0].Click();
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("add_cart_product")));

                if (driver.FindElements(By.Name("options[Size]")).Count > 0)
                {
                    driver.FindElement(By.Name("options[Size]")).Click();
                    wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector("option[value=\"Small\"]")));
                    driver.FindElement(By.CssSelector("option[value=\"Small\"]")).Click();
                }
                driver.FindElement(By.Name("add_cart_product")).Click();
                String countProducts = driver.FindElement(By.CssSelector("span[class=\"quantity\"]")).Text;
                IWebElement counter = driver.FindElement(By.CssSelector("span[class=\"quantity\"]"));
                String text = i + "";
                wait.Until(ExpectedConditions.TextToBePresentInElement(counter, text));
                driver.Navigate().Back();
            }

            driver.FindElement(By.XPath("//*/a[contains(@href, 'checkout') and @class='link']")).Click();
            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector("[name=\"remove_cart_item\"]")));
            var ListOfProducts = driver.FindElements(By.CssSelector("[style=\"text-align: center;\"]"));
            while (ListOfProducts.Count > 0)
            {
                var elements = driver.FindElements(By.XPath("//*[@id='box-checkout-cart']//li[@class='shortcut']"));
                if (elements.Count > 0)
                    elements[0].Click();
                driver.FindElement(By.XPath("//*/button[@name='remove_cart_item']")).Click();
                wait.Until(ExpectedConditions.StalenessOf(ListOfProducts[0]));
                ListOfProducts = driver.FindElements(By.CssSelector("[style=\"text-align: center;\"]"));
            }
        }
        [TearDown]
        public void Stop()
        {
            StopDriver();
        }
    }
}
