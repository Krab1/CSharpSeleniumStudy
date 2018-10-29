using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using SeleniumExtras.WaitHelpers;

namespace SeleniumDay2tests
{
    [TestFixture]
    class LitecartDay3HW4
    {
        private OpenQA.Selenium.Support.UI.WebDriverWait wait;
        private List<IWebDriver> drivers;
        FirefoxOptions options = new FirefoxOptions();

        [SetUp]
        public void start()
        {
            options.UseLegacyImplementation = false;
            drivers = new List<IWebDriver>() { new ChromeDriver(), new FirefoxDriver(options), new InternetExplorerDriver() };
        }

        [Test]
        public void Test() => drivers.ForEach(x =>
        {
            wait = new OpenQA.Selenium.Support.UI.WebDriverWait(x, TimeSpan.FromSeconds(10));
            Testing(x);
        });

        private void Testing(IWebDriver driver)
        {
            try
            {
                driver.Url = "http://localhost:88/litecart/admin/login.php";
                var username = driver.FindElement(By.Name("username"));
                if (!string.IsNullOrWhiteSpace(username.Text))
                    username.Clear();
                username.SendKeys("admin");
                var password = driver.FindElement(By.Name("password"));
                if (!string.IsNullOrWhiteSpace(password.Text))
                    password.Clear();
                password.SendKeys("admin");
                var loginbtn = driver.FindElement(By.Name("login"));
                loginbtn.Click();
                wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id=\"sidebar\"]/div[2]/a[5]/i")));
                driver.Quit();
                driver.Dispose();
            }
            catch
            {
                throw new Exception();
            }
        }
        [TearDown]
        public void Stop() => drivers.RemoveAll(x => x != null);
    }
}
