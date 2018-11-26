using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    class BasketPage : Page
    {
        public BasketPage(IWebDriver driver) : base(driver) => PageFactory.InitElements(driver, this);
        [FindsBy(How = How.XPath, Using = "//*/a[contains(@href, 'checkout') and @class='link']")]
        internal IWebElement CheckoutLink;
        [FindsBy(How = How.CssSelector, Using = "[style=\"text-align: center;\"]")]
        internal IList<IWebElement> Listofproducts;

        internal void OpenCheckout()
        {
            CheckoutLink.Click();
            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector("[name=\"remove_cart_item\"]")));
        }
        internal void CleanBasket()
        {
            while(Listofproducts.Count > 0)
            {
                var elements = driver.FindElements(By.XPath("//*[@id='box-checkout-cart']//li[@class='shortcut']"));
                if (elements.Count > 0)
                    elements[0].Click();
                driver.FindElement(By.XPath("//*/button[@name='remove_cart_item']")).Click();
                wait.Until(ExpectedConditions.StalenessOf(Listofproducts[0]));
            }
        }
    }
}
