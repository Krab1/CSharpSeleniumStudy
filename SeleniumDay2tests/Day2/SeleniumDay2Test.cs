using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    [TestFixture]
    public class SeleniumDay2Test
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void Start()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(20);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }
        [Test]
        public void Test()
        {
            driver.Url = "http://google.com";
            driver.FindElement(By.Name("q")).SendKeys("webdriver");
            driver.FindElement(By.Name("btnK")).Click();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TitleIs("webdriver - Поиск в Google"));
        }
        [TearDown]
        public void Stop()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
