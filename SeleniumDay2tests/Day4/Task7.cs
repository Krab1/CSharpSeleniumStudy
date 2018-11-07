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
            OpenAdmin();
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
            else
                Assert.IsTrue(driver.FindElements(By.XPath($"//h1[contains(.,'{TrimName(name)}')]")).Count > 0, $"Не найден заголовок с именем {name}");
        }
        public void ClickOnSubMenu(string nameofsub)
        {
            var name = TrimName(nameofsub);
            var elem = driver.FindElement(By.XPath($"//*[@id='app-']/ul/*[contains(., '{name}')]"));
            elem.Click();
            Assert.IsTrue(driver.FindElements(By.XPath($"//h1[contains(.,'{name}')]")).Count > 0, $"Не найден заголовок с именем {name}");
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
