using System;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;

namespace SeleniumTests
{
    [TestFixture]
    class Task8 : Base
    {
        [SetUp]
        public void Start()
        {
            StartChrome();
            OpenMainSite();
        }
        [Test]
        public void Test()
        {
            try
            {
                var Ducks = driver.FindElements(By.XPath("//*[@class='product column shadow hover-light']"));
                foreach(var duck in Ducks)
                {
                    var stickers = duck.FindElements(By.XPath(".//div[contains(@class, 'sticker')]"));
                    var name = duck.FindElement(By.XPath(".//div[@class='name']"));
                    Assert.IsTrue(stickers.Count <= 1, $"Количество стикеров у элемента {name.Text} больше 1");
                }
            }
            catch (Exception e)
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
