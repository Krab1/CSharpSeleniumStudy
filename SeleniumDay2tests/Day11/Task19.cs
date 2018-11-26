using NUnit.Framework;

namespace SeleniumTests
{
    [TestFixture]
    class Task19 : TestBase
    {
        [Test]
        public void Test()
        {
            for (int i = 1; i < 4; i++)
            {
                AddToCart();
            }
            CleanBasket();
        }
    }
}
