using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumTests
{
    class Base
    {
        public IWebDriver driver;
        private WebDriverWait wait;
        public List<IWebDriver> AllDrivers = new List<IWebDriver>() { };
        public void config()
        {
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }
        public void StartAll(bool firefoxUseLegacyImplementation = false)
        {
            StartChrome();
            AllDrivers.Add(driver);
            StartIE();
            AllDrivers.Add(driver);
            //StartFireFox(firefoxUseLegacyImplementation);
            //AllDrivers.Add(driver);
        }
        public void StartChrome()
        {
            driver = new ChromeDriver();
            config();
        }
        public void StartIE()
        {
            driver = new InternetExplorerDriver();
            config();
        }
        public void StartFireFox(bool UseLegacyImplementation = false)
        {
            FirefoxOptions options = new FirefoxOptions
            {
                UseLegacyImplementation = UseLegacyImplementation
            };
            driver = new FirefoxDriver(options);
            config();
        }
        public void StartFireFox(FirefoxOptions FFOptions)
        {
            driver = new FirefoxDriver(FFOptions);
            config();
        }
        public void OpenMainSite()
        {
            try
            {
                driver.Url = "http://localhost:88/litecart/";
                Assert.IsTrue(driver.FindElements(By.XPath("//*[@id='header']")).Count > 0, "Не найден лого, сайт не загрузился");
            }
            catch
            {
                throw new Exception();
            }
        }
        public void OpenAdmin()
        {
            try
            {
                driver.Url = "http://localhost:88/litecart/admin/login.php";
                var username = driver.FindElement(By.Name("username"));
                if (!string.IsNullOrWhiteSpace(username.Text))
                    username.Clear();
                username.SendKeys("admin");
                var password = driver.FindElement(By.Name("password"));
                if (!string.IsNullOrWhiteSpace(password.Text))
                    password.Clear();
                password.SendKeys("admin");
                var loginbtn = driver.FindElement(By.Name("login"));
                loginbtn.Click();
                Assert.True(driver.FindElements(By.XPath("//*[@id=\"sidebar\"]/div[2]/a[5]/i")).Count > 0);
            }
            catch
            {
                throw new Exception();
            }
        }
        public void WaitUntilPageLoad()
        {
            wait.Until(driver1 => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
        }
        public void StopDriver()
        {
            driver.Quit();
            driver.Dispose();
            AllDrivers.ForEach(x => 
            {
                x.Quit();
                x.Dispose();
            });
        }
    }
}
