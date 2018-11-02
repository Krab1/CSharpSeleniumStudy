using System;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;

namespace SeleniumTests
{
    [TestFixture]
    class Task7 : Base
    {
        [SetUp]
        public void Start()
        {
            StartChrome();
            Login();
        }
        [Test]
        public void Test()
        {
            try
            {
                var MenuElements = driver.FindElements(By.CssSelector("li#app-")).Select(x => x.Text).ToList();
                MenuElements.ForEach(x => 
                {
                    var title = driver.Title;
                    var name = x.Contains('\r') ? x.Substring(0, x.IndexOf('\r')) : x;
                    var elem = driver.FindElement(By.XPath($"//*[@class='name' and contains(text(),'{name}')]"));
                    elem.Click();
                    WaitUntilPageLoad();
                });
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }
        [TearDown]
        public void Stop()
        {
            StopDriver();
        }
    }
}
