using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumTests
{
    [TestFixture]
    public class SeleniumDay2Test
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }
        [Test]
        public void FirstTest()
        {
            driver.Url = "http://google.com";
            driver.FindElement(By.Name("q")).SendKeys("webdriver");
            driver.FindElement(By.Name("btnG")).Click();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TitleIs("webdriver - Поиск в Google"));
        }
        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
