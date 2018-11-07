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
                var DucksCount = driver.FindElements(By.XPath("//*[@class='image-wrapper']")).Count;
                var StickersCount = driver.FindElements(By.XPath("//*[@class='image-wrapper']/div[contains(@class, 'sticker')]")).Count;
                Assert.IsTrue(DucksCount == StickersCount, "Количество товаров не соответсвует количеству стикеров на них");
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
