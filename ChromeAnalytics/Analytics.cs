using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;

namespace InBoost.ChromeAnalytics
{
    internal class Analytics
    {
        private IWebDriver driver;
        private Actions actions;

        [SetUp]
        public void Initialize()
        {
            driver = new ChromeDriver();
            //driver = new FirefoxDriver();
            //driver = new EdgeDriver();
            actions = new Actions(driver);
            driver.Manage().Window.Maximize();
            //driver.Url = "https://qa3prod.inboost.ai/";
            driver.Url = "https://qa3dev.inboost.ai/";
            WaitForElement(By.XPath("//input[@placeholder='Робочий Email']"));
        }

        private IWebElement WaitForElement(By locator, int maxTimeout = 10)
        {
            return new WebDriverWait(driver, TimeSpan.FromSeconds(maxTimeout))
                .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator));
        }
        public void LogIn()
        {
            //driver.FindElement(By.XPath("//input[@placeholder='Робочий Email']"))
            //.SendKeys("amina_qa3_prod@inboost.ai");
            driver.FindElement(By.XPath("//input[@placeholder='Робочий Email']"))
              .SendKeys("amina_qa3_dev@inboost.ai");
            driver.FindElement(By.XPath("//input[@placeholder='Пароль із 8-ми і більше символів']"))
                .SendKeys("Amina21amina");
            driver.FindElement(By.XPath("//button[contains(text(),'Увійти')]")).Click();
        }
        [Test, Order(1)]
        public void OpenPage()
        {
            LogIn();
            WaitForElement(By.XPath("//*[@id=\"root\"]/div/div[1]/div[2]/div[1]/div[1]"));
            IWebElement mainMenu = driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[1]/div[2]/div[1]/div[1]"));
            IWebElement subMenu = driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[1]/div[2]/div[1]/div[2]/div[1]"));
            actions
                .MoveToElement(mainMenu)
                .MoveToElement(subMenu)
                .Click().Build().Perform();
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div[2]/div")).Click();
            driver.FindElement(By.XPath("//button[normalize-space()='2023']")).Click();
            Thread.Sleep(800);
            IWebElement listyear = driver.FindElement(By.XPath("//ul[1]//li[2]"));
            actions
                .MoveToElement(listyear)
                .Click().Build().Perform();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div[2]/div")).Click();
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div[2]/button")).Click();
            Thread.Sleep(800);
            IWebElement list = driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div[2]/div[2]/div/ul/li[3]"));
            actions
                .MoveToElement(list)
                .Click().Build().Perform();
        }
        [TearDown]
        public void CloseBrowser()
        {
            Thread.Sleep(3000);
            driver.Quit();
        }
    }
      
}

