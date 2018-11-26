using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace SeleniumTests
{
    internal class ProductPage : Page
    {
        public ProductPage(IWebDriver driver) : base(driver) => PageFactory.InitElements(driver, this);

        [FindsBy(How = How.Name, Using = "options[Size]")]
        internal IList<IWebElement> Options;
        [FindsBy(How= How.CssSelector, Using = "option[value=\"Small\"]")]
        internal IWebElement smallSize;
        [FindsBy(How = How.Name, Using = "add_cart_product")]
        internal IWebElement AddToCart;
        [FindsBy(How = How.CssSelector, Using = "span[class=\"quantity\"]")]
        internal IWebElement AddedtoBasketCounter;

        internal void Add()
        {
            if(Options.Count > 0)
            {
                smallSize.Click();
            }
            AddToCart.Click();
        }
        internal int GetBasketCount()
        {
            return Convert.ToInt32(AddedtoBasketCounter.Text);
        }
    }
}
