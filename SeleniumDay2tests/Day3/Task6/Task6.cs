using System;
using NUnit.Framework;
using OpenQA.Selenium.Firefox;

namespace SeleniumTests
{
    [TestFixture]
    class Task6 : Base
    {
        [SetUp]
        public void Start() => StartFireFox(new FirefoxOptions
            {
                BrowserExecutableLocation = "c:\\Program Files (x86)\\Nightly\\firefox.exe"
            });
        [Test]
        public void Test()
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
        [TearDown]
        public void Stop() => StopDriver();
    }
}
