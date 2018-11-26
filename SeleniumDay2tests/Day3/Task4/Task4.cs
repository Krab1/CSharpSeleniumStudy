using System;
using NUnit.Framework;
using OpenQA.Selenium;

namespace SeleniumTests
{
    [TestFixture]
    class Task4 : Base
    {
        [SetUp]
        public void Start()
        {
            StartAll();
        }

        [Test]
        public void Test() => AllDrivers.ForEach(x => Testing(x));

        private void Testing(IWebDriver driver)
        {
            try
            {
                OpenAdmin();
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}
