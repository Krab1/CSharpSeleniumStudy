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
                var MenuElements = driver.FindElements(By.XPath("//*[@id='app-']")).Select(x => x.Text).ToList();
                MenuElements.ForEach(x => ClickonMenu(x));
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void ClickonMenu(string originalname)
        {
            var name = TrimName(originalname);
            var elem = driver.FindElement(By.XPath($"//*[@class='name' and contains(.,'{name}')]"));
            elem.Click();
            var submenu = driver.FindElements(By.XPath($"//*[@id='app-'and contains(., '{name}')]/ul/li/a/*")).Select(a => a.Text).ToList();
            if(submenu.Count > 0)
                submenu.ForEach(x => ClickOnSubMenu(x));
        }
        public void ClickOnSubMenu(string nameofsub)
        {
            var elem = driver.FindElement(By.XPath($"//*[@id='app-']/ul/*[contains(., '{TrimName(nameofsub)}')]"));
            elem.Click();
        }
        private string TrimName(string originalname)
        {
            return originalname.Contains('\r') ? originalname.Substring(0, originalname.IndexOf('\r')) : originalname;
        }
        [TearDown]
        public void Stop()
        {
            StopDriver();
        }
    }
}
