using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    public class Application
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private MainPage mainPage;
        private BasketPage basketPage;
        private ProductPage productPage;
        private void config()
        {
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }
        public Application()
        {
            driver = new ChromeDriver();
            config();
            mainPage = new MainPage(driver);
            basketPage = new BasketPage(driver);
            productPage = new ProductPage(driver);
        }

        public void Quit()
        {
            driver.Quit();
        }

        internal void AddProductToCart()
        {
            mainPage.Open();
            mainPage.ClickFirst();
            productPage.Add();
        }
        internal void CleanBasket()
        {
            basketPage.OpenCheckout();
            basketPage.CleanBasket();
        }
    }
}
