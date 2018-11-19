using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;
using OpenQA.Selenium;

namespace SeleniumTests
{
    [TestFixture]
    class Task10 : Base
    {
        [SetUp]
        public void Start()
        {
            StartChrome();
            OpenMainSite();
        }
        /// <summary>
        /// на главной странице и на странице товара совпадает текст названия товара
        /// </summary>
        [Test]
        public void Test1()
        {
            var Duck = driver.FindElements(By.XPath("//*[@id='box-campaigns']//div[@class='content']//li[@class='product column shadow hover-light']")).First();
            String MainName = Duck.FindElement(By.XPath(".//div[@class='name']")).Text; ;
            Duck.Click();
            String DetailedName = driver.FindElement(By.XPath("//h1[@class='title']")).Text;
            Assert.IsTrue(MainName.Equals(DetailedName), $"Имена на главной и на странице товаров не одинаковые ({MainName} и {DetailedName})");
        }
        /// <summary>
        /// на главной странице и на странице товара совпадают цены (обычная и акционная)
        /// </summary>
        [Test]
        public void Test2()
        {
            var Duck = driver.FindElements(By.XPath("//*[@id='box-campaigns']//div[@class='content']//li[@class='product column shadow hover-light']")).First();
            var priceElement = Duck.FindElement(By.XPath(".//div[@class='price-wrapper']"));
            var regularpriceelement = priceElement.FindElement(By.XPath("./s"));
            var campaignpriceelement = priceElement.FindElement(By.XPath("./strong"));
            String MainRegularPrice = regularpriceelement.Text;
            String MainCampaignPrice = campaignpriceelement.Text;
            Duck.Click();
            var productpriceelement = driver.FindElement(By.XPath("//*[@id='box-product']//div[@class='price-wrapper']"));
            var Detailedregularpriceelement = productpriceelement.FindElement(By.XPath("./s"));
            var Detailedcampaignpriceelement = productpriceelement.FindElement(By.XPath("./strong"));
            String DetailedRegularPrice = Detailedregularpriceelement.Text;
            String DetailedCampaignPrice = Detailedcampaignpriceelement.Text;
            Assert.IsTrue(MainRegularPrice.Equals(DetailedRegularPrice));
            Assert.IsTrue(MainCampaignPrice.Equals(DetailedCampaignPrice));
        }
        /// <summary>
        /// обычная цена зачёркнутая и серая (можно считать, что "серый" цвет это такой, у которого в RGBa представлении одинаковые значения для каналов R, G и B)
        /// </summary>
        [Test]
        public void Test3()
        {
            var Duck = driver.FindElements(By.XPath("//*[@id='box-campaigns']//div[@class='content']//li[@class='product column shadow hover-light']")).First();
            var priceElement = Duck.FindElement(By.XPath(".//div[@class='price-wrapper']"));
            var regularpriceelement = priceElement.FindElement(By.XPath("./s"));
            String MainColorRegularPrice = regularpriceelement.GetCssValue("color");
            String MainDecorRegularPrice = regularpriceelement.GetCssValue("text-decoration-line");
            Duck.Click();
            var productpriceelement = driver.FindElement(By.XPath("//*[@id='box-product']//div[@class='price-wrapper']"));
            var Detailedregularpriceelement = productpriceelement.FindElement(By.XPath("./s"));
            String DetailedColorRegularPrice = Detailedregularpriceelement.GetCssValue("color");
            String DetailedDecorRegularPrice = Detailedregularpriceelement.GetCssValue("text-decoration-line");
            Assert.IsTrue(ColorIsGrey(MainColorRegularPrice), "Цвет на основной странице не серый");
            Assert.IsTrue(ColorIsGrey(DetailedColorRegularPrice), "Цвет на странице товара не серый");
            Assert.IsTrue(MainDecorRegularPrice.Equals("line-through"));
            Assert.IsTrue(DetailedDecorRegularPrice.Equals("line-through"));
        }
        /// <summary>
        /// акционная жирная и красная (можно считать, что "красный" цвет это такой, у которого в RGBa представлении каналы G и B имеют нулевые значения)
        /// </summary>
        [Test]
        public void Test4()
        {
            var Duck = driver.FindElements(By.XPath("//*[@id='box-campaigns']//div[@class='content']//li[@class='product column shadow hover-light']")).First();
            var priceElement = Duck.FindElement(By.XPath(".//div[@class='price-wrapper']"));
            var campaignpriceelement = priceElement.FindElement(By.XPath("./strong"));
            String MainColorCampaignPrice = campaignpriceelement.GetCssValue("color");
            String MainBoldCampaignPrice = campaignpriceelement.GetCssValue("font-weight");
            Duck.Click();
            var productpriceelement = driver.FindElement(By.XPath("//*[@id='box-product']//div[@class='price-wrapper']"));
            var Detailedcampaignpriceelement = productpriceelement.FindElement(By.XPath("./strong"));
            String DetailedColorCampaignPrice = Detailedcampaignpriceelement.GetCssValue("color");
            String DetailedBoldCampaignPrice = Detailedcampaignpriceelement.GetCssValue("font-weight");
            Assert.IsTrue(ColorIsRed(MainColorCampaignPrice), "Цвет на основной странице не красный");
            Assert.IsTrue(ColorIsRed(DetailedColorCampaignPrice), "Цвет на странице товара не красный");
            Assert.IsTrue(MainBoldCampaignPrice.ToLower().Equals("bold") || MainBoldCampaignPrice.ToLower().Equals("700"), "Шрифт акционной цены главной странице, не жирный");
            Assert.IsTrue(DetailedBoldCampaignPrice.ToLower().Equals("bold") || DetailedBoldCampaignPrice.ToLower().Equals("700"), "Шрифт акционной цены главной странице, не жирны");
        }
        /// <summary>
        /// акционная цена крупнее, чем обычная (это тоже надо проверить на каждой странице независимо)
        /// </summary>
        [Test]
        public void Test5()
        {
            var Duck = driver.FindElements(By.XPath("//*[@id='box-campaigns']//div[@class='content']//li[@class='product column shadow hover-light']")).First();
            var priceElement = Duck.FindElement(By.XPath(".//div[@class='price-wrapper']"));
            var campaignpriceelement = priceElement.FindElement(By.XPath("./strong")).GetCssValue("font-size");
            var regularpriceelement = priceElement.FindElement(By.XPath("./s")).GetCssValue("font-size");
            double MainSizeRegularPrice = double.Parse(regularpriceelement.Substring(0, regularpriceelement.IndexOf("p")), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
            double MainSizeCampaignPrice = double.Parse(campaignpriceelement.Substring(0, campaignpriceelement.IndexOf("p")), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
            Duck.Click();
            var productpriceelement = driver.FindElement(By.XPath("//*[@id='box-product']//div[@class='price-wrapper']"));
            var Detailedregularpriceelement = productpriceelement.FindElement(By.XPath("./s")).GetCssValue("font-size");
            var Detailedcampaignpriceelement = productpriceelement.FindElement(By.XPath("./strong")).GetCssValue("font-size");
            double DetailedSizeRegularPrice = double.Parse(Detailedregularpriceelement.Substring(0, Detailedregularpriceelement.IndexOf("p")), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
            double DetailedSizeCampaignPrice = double.Parse(Detailedcampaignpriceelement.Substring(0, Detailedcampaignpriceelement.IndexOf("p")), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
            Assert.IsTrue(MainSizeCampaignPrice > MainSizeRegularPrice, "Размер шрифта спец цены не больше чем обычной(главная страница)");
            Assert.IsTrue(DetailedSizeCampaignPrice > DetailedSizeRegularPrice, "Размер шрифта спец цены не больше чем обычной(страница товара)");
        }
        public bool ColorIsGrey(string color)
        {
            var rgb = color.Split('(', ')')[1].Split(',').ToList();
            if (rgb.Count() == 4)
                rgb.RemoveAt(3);
            if (rgb.Count(x => x.Trim().Equals(rgb.First()))==3)
                return true;
            return false;
        }
        public bool ColorIsRed(string color)
        {
            var rgb = color.Split('(', ')')[1].Split(',');
            if (rgb[1].Trim().Equals("0") && rgb[2].Trim().Equals("0"))
                return true;
            return false;
        }
        [TearDown]
        public void Stop()
        {
            StopDriver();
        }
    }
}
