using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using SeleniumExtras.WaitHelpers;

namespace SeleniumDay2tests
{
    [TestFixture]
    class LitecartDay3HW6
    {
        private OpenQA.Selenium.Support.UI.WebDriverWait wait;
        IWebDriver driver;

        [SetUp]
        public void start()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.BrowserExecutableLocation = "c:\\Program Files (x86)\\Nightly\\firefox.exe";
            driver = new FirefoxDriver(options);
        }

        [Test]
        private void Test()
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
        public void Stop()
        {
            driver.Close();
            driver.Dispose();
        }
    }
}
