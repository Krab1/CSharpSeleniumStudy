using System;
using NUnit.Framework;

namespace SeleniumTests
{
    [TestFixture]
    class FirstTest : Base
    {
        [SetUp]
        public void Start()
        {
            StartChrome();
        }
        [Test]
        public void Test()
        {
            try
            {
                Login();
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
