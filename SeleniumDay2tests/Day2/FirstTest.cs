using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumExtras.WaitHelpers;

namespace SeleniumTests
{
    [TestFixture]
    class FirstTest
    {
        private IWebDriver driver;
        private OpenQA.Selenium.Support.UI.WebDriverWait wait;

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }
        [Test]
        public void Test()
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
            }
            catch
            {
                throw new Exception();
            }
            
        }
        [TearDown]
        public void Stop()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
