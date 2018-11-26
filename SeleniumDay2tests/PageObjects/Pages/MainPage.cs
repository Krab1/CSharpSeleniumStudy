using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Collections.Generic;
using System.Linq;

namespace SeleniumTests
{
    internal class MainPage : Page
    {
        public MainPage(IWebDriver driver) : base(driver) => PageFactory.InitElements(driver, this);

        [FindsBy(How = How.CssSelector, Using = "li[class='product column shadow hover-light']")]
        internal IList<IWebElement> Products;
        internal void Open()
        {
            driver.Url = "http://localhost:88/litecart/";
        }
        internal void ClickFirst()
        {
            Click(Products.First().Text);
        }
        internal void Click(string name)
        {
            Products.Where(x => x.Text.Equals(name)).First().Click();
        }
    }
}
