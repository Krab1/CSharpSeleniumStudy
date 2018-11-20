using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;

namespace SeleniumTests
{
    [TestFixture]
    class Task12 : Base
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
            driver.FindElement(By.XPath("//*[@id='app-']//span[contains(., 'Catalog')]")).Click();
            driver.FindElement(By.XPath("//*[@id='app-']//li[@id='doc-catalog']")).Click();
            driver.FindElement(By.XPath("//*[@id='content']//a[contains(., 'New Product')]")).Click();

            string productName = "Lalka"+GetRandomString(4);
            driver.FindElement(By.Name("name[en]")).SendKeys(productName);
            driver.FindElement(By.Name("code")).SendKeys(GetRandomString(5));

            driver.FindElement(By.XPath("//*/input[@value='1-3']")).Click();
            var quantity = driver.FindElement(By.Name("quantity"));
            quantity.Click();
            quantity.Clear();
            quantity.SendKeys("14,88");
            driver.FindElement(By.Name("new_images[]")).SendKeys(AppDomain.CurrentDomain.BaseDirectory + "img\\Lalka.jpg");
            driver.FindElement(By.Name("date_valid_from")).SendKeys(Keys.Home + DateTime.Now.Date.ToShortDateString());
            driver.FindElement(By.Name("date_valid_to")).SendKeys(Keys.Home + DateTime.Now.AddDays(1).ToShortDateString());
            driver.FindElement(By.XPath("//*/a[@href='#tab-information']")).Click();
            driver.FindElement(By.Name("keywords")).SendKeys("lalka");
            driver.FindElement(By.Name("short_description[en]")).SendKeys("Testovaya Lalka");
            driver.FindElement(By.ClassName("trumbowyg-editor")).SendKeys("On the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment...");
            driver.FindElement(By.Name("head_title[en]")).SendKeys("Lalka Title");
            driver.FindElement(By.Name("meta_description[en]")).SendKeys("lolka");
            driver.FindElement(By.XPath("//*/a[@href='#tab-prices']")).Click();
            driver.FindElement(By.XPath("//*/input[@name='purchase_price']")).Clear();
            driver.FindElement(By.XPath("//*/input[@name='purchase_price']")).SendKeys("12,3");
            driver.FindElement(By.Name("purchase_price_currency_code")).Click();
            driver.FindElement(By.CssSelector("[value=\"USD\"]")).Click();
            driver.FindElement(By.Name("prices[USD]")).SendKeys("3");
            driver.FindElement(By.Name("prices[EUR]")).SendKeys("2");
            driver.FindElement(By.XPath("//*/button[@name='save']")).Click();
            var listProducts = driver.FindElements(By.XPath("//*/td[img and a]"));
            var listProductsName = new List<String>();
            foreach (var product in listProducts)
            {
                listProductsName.Add(product.Text);
            }
            Assert.Contains(productName, listProductsName, "В списке продуктов найден добавленный продукт");
        }

        [TearDown]
        public void Stop()
        {
            StopDriver();
        }
    }
}
