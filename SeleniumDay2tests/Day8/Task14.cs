using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Linq;
using SeleniumExtras.WaitHelpers;

namespace SeleniumTests
{
    [TestFixture]
    class Task14 : Base
    {
        [SetUp]
        public void Start()
        {
            StartChrome();
            OpenAdmin();
        }
        [Test]
        public void Test()
        {
            string currwindow = driver.CurrentWindowHandle;
            driver.FindElement(By.XPath("//*/span[contains(., 'Countries')]")).Click();
            driver.FindElement(By.XPath("//*/a[contains(., 'Add New Country')]")).Click();
            var Links = driver.FindElements(By.XPath("//*/i[@class='fa fa-external-link']"));
            foreach(var link in Links)
            {
                link.Click();
                var newwindow = wait.Until(Func => ThereIsWindowOtherThan(currwindow));
                driver.SwitchTo().Window(newwindow);
                driver.Close();
                driver.SwitchTo().Window(currwindow);
            }
        }
        private string ThereIsWindowOtherThan(string oldwindow)
        {
            return driver.WindowHandles.First(x => !x.Equals(oldwindow));
        }
    }
}
