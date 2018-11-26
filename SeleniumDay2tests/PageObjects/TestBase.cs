using NUnit.Framework;

namespace SeleniumTests
{
    public class TestBase
    {
        public Application app;

        [SetUp]
        public void Start()
        {
            app = new Application();
        }
        [TearDown]
        public void Stop()
        {
            app.Quit();
            app = null;
        }
        public void AddToCart()
        {
            app.AddProductToCart();
        }
        public void CleanBasket()
        {
            app.CleanBasket();
        }
    }
}
