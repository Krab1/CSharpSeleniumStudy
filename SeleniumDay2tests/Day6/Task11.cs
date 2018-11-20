using System;
using NUnit.Framework;
using OpenQA.Selenium;

namespace SeleniumTests
{
    [TestFixture]
    class Task11 : Base
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
            driver.FindElement(By.XPath("//*[@id='box-account-login']//a")).Click();
            Assert.IsTrue(driver.Title.Contains("Create Account"));
            driver.FindElement(By.Name("tax_id")).SendKeys("810");
            driver.FindElement(By.Name("company")).SendKeys("TestCompany");
            driver.FindElement(By.Name("firstname")).SendKeys("Test");
            driver.FindElement(By.Name("lastname")).SendKeys("Testoviy");
            driver.FindElement(By.Name("address1")).SendKeys("15 Lambard str #22");
            driver.FindElement(By.Name("postcode")).SendKeys("90001");
            driver.FindElement(By.Name("city")).SendKeys("Los Angeles");
            driver.FindElement(By.ClassName("select2-selection__arrow")).Click();
            driver.FindElement(By.ClassName("select2-search__field")).SendKeys("United States");
            driver.FindElement(By.CssSelector("[id$=\"US\"]")).Click();
            var email = GetRandomEmail(new Random().Next(4, 8));
            driver.FindElement(By.Name("email")).SendKeys(email);
            driver.FindElement(By.Name("phone")).SendKeys("+11122334455");
            var pass = GetRandomString(8);
            driver.FindElement(By.Name("password")).SendKeys(pass);
            driver.FindElement(By.Name("confirmed_password")).SendKeys(pass);
            driver.FindElement(By.CssSelector("button[type=\"submit\"]")).Click();
            driver.FindElement(By.XPath("//div[@class='content']//a[contains(@href, 'logout')]")).Click();
            driver.FindElement(By.Name("email")).SendKeys(email);
            driver.FindElement(By.Name("password")).SendKeys(pass);
            driver.FindElement(By.Name("login")).Click();
            driver.FindElement(By.XPath("//div[@class='content']//a[contains(@href, 'logout')]")).Click();
            var register = driver.FindElements(By.XPath("//*[@id='box-account-login']//a"));
            Assert.IsTrue(register.Count > 0);
        }

        [TearDown]
        public void Stop()
        {
            StopDriver();
        }
    }
}
