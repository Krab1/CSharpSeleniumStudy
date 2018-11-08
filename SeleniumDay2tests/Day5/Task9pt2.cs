using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;

namespace SeleniumTests
{
    [TestFixture]
    class Task9pt2 : Base
    {
        [SetUp]
        public void Start()
        {
            StartChrome();
            OpenAdmin();
            driver.Url = "http://localhost:88/litecart/admin/?app=geo_zones&doc=geo_zones";
        }
        [Test]
        public void Test()
        {
            try
            {
                var full = driver.FindElements(By.XPath("//*[@id='content']/form/table/tbody/tr[@class='row']"));
                var parcedlist = ParceText(full);
                var listdifcountries = parcedlist.Where(x => x.Value > 0).Select(a => a.Key);
                foreach(var country in listdifcountries)
                {
                    Assert.IsTrue(CheckInCountry(country), "Список регионов внутри страны отсортирован не по алфавиту");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private bool CheckInCountry(string name)
        {
            driver.FindElement(By.LinkText(name)).Click();
            var list = driver.FindElements(By.XPath("//*[@id='table-zones']/tbody/tr/td[3]/select/option[@selected]"));
            List<string> Regions = new List<string>();
            foreach(var row in list)
            {
                if (string.IsNullOrWhiteSpace(row.Text))
                    continue;
                Regions.Add(row.Text);
            }
            var SortedRegions = Regions.OrderBy(x => x);
            driver.Navigate().Back();
            return SortedRegions.SequenceEqual(Regions);
        }
        private List<KeyValuePair<string,int>> ParceText(IEnumerable<IWebElement> data)
        {
            List<KeyValuePair<string, int>> list = new List<KeyValuePair<string, int>>();
            foreach (var element in data)
            {
                var key = element.FindElement(By.XPath("./td[3]")).Text;
                var value = element.FindElement(By.XPath("./td[4]")).Text;
                list.Add(new KeyValuePair<string, int>(key, Convert.ToInt32(value)));
            }
            return list;
        }
        [TearDown]
        public void Stop()
        {
            StopDriver();
        }
    }
}
