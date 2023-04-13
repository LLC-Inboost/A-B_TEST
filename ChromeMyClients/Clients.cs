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

namespace InBoost.ChromeMyClients
{
    internal class Clients
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
            driver.Url = "https://qa3prod.inboost.ai/";
            //driver.Url = "https://qa3dev.inboost.ai/";
            WaitForElement(By.XPath("//input[@placeholder='Робочий Email']"));
        }

        private IWebElement WaitForElement(By locator, int maxTimeout = 10)
        {
            return new WebDriverWait(driver, TimeSpan.FromSeconds(maxTimeout))
                .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator));
        }
        public void LogIn()
        {
            driver.FindElement(By.XPath("//input[@placeholder='Робочий Email']"))
                .SendKeys("amina_qa3_prod@inboost.ai");
            //driver.FindElement(By.XPath("//input[@placeholder='Робочий Email']"))
               //.SendKeys("amina_qa3_dev@inboost.ai");
            driver.FindElement(By.XPath("//input[@placeholder='Пароль із 8-ми і більше символів']"))
                .SendKeys("Amina21amina");
            driver.FindElement(By.XPath("//button[contains(text(),'Увійти')]")).Click();
        }
        [Test, Order(1)]
        public void OpenPage()
        {
            LogIn();
            WaitForElement(By.XPath("//*[@id=\"root\"]/div/div[1]/div[2]/div[3]"));
            IWebElement mainMenu = driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[1]/div[2]/div[3]"));
            IWebElement subMenu = driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[1]/div[2]/div[3]/div[2]/div[1]"));
            actions
                .MoveToElement(mainMenu)
                .MoveToElement(subMenu)
                .Click().Build().Perform();
            // Створення клієнта
            Thread.Sleep(500);
            driver.FindElement(By.XPath("//button[contains(text(),'Клієнт')]")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div[2]/div/main/div[1]/div/div/input"))
                .SendKeys("Amina");
            Thread.Sleep(700);
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div[2]/div/main/div[2]/div/div/input"))
                .SendKeys("Oliinyk");
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div[2]/div/main/div[3]/input")).Click();
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div[2]/div/main/div[3]/input"))
               .SendKeys("976754565");
            Thread.Sleep(500);
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div[2]/div/main/div[4]/div/div/header")).Click();
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div[2]/div/main/div[4]/div/div/ul/li[4]")).Click();
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div[2]/div/main/div[5]/button")).Click();
            WaitForElement(By.XPath("//*[@class='new-client-tooltip']"));
            string tooltipMessage = driver.FindElement(By.XPath("//*[@class='new-client-tooltip']")).Text;
            //Assert.IsTrue(tooltipMessage.StartsWith("Додано клієнта:"), "Tooltip message is NOT correct. Actual result: "
            //    + tooltipMessage + "  Expected result: Додано клієнта:");

     
            // Видалення клієнта
            /*
            Thread.Sleep(500);
            driver.FindElement(By.XPath("//*[@id=\"clients\"]/div/div[2]/ul[2]/li[2]/div[1]/div/button")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//div[@class='client-list__grid']//div[3]//div[1]")).Click();
            WaitForElement(By.XPath("//*[@id=\"root\"]/div[2]/div"));
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div[2]/div/main/div/button[2]")).Click();
            */
            driver.FindElement(By.XPath("//*[@id=\"clients\"]/div/div[1]/div[1]/div/input"))
                .SendKeys("0987826512");
        }

        [TearDown]
        public void CloseBrowser()
        {
            Thread.Sleep(3000);
            driver.Quit();
        }
    }
}
